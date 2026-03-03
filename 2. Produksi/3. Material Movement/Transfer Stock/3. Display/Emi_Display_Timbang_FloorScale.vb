Public Class Emi_Display_Timbang_FloorScale

    Dim Arr1, Arr2, Arr3, Arr4, arrAlreadyPrinted As New ArrayList

    Dim LvNoFaktur, LvNoPembLoading, LvIdNametagPallet, LvNoSJ, LvNoPlat, LvNmSupplier, LvTgl, LvJam, LvUserId, LvKodeSO, LvKdBrg, LvNmBrg, LvTglProd, LvTglExp, LvJumlah, LvJmlBags, LvSatuan, LvNilaiPengali, LvNilaiBrg, LvSatuanBrg, LvUrutOto, LvMetodeTimbang As String

    Dim tahunMulaiProduksi As String = ""

    Dim itemNoFaktur As Integer = 0
    Dim itemNoPembLoading As Integer = 1
    Dim itemIdNametagPallet As Integer = 2

    Private Sub Lv_BM_FloorScale_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_BM_FloorScale.SelectedIndexChanged

    End Sub

    Dim itemNoSJ As Integer = 3
    Dim itemNoPlat As Integer = 4
    Dim itemNmSupplier As Integer = 5
    Dim itemTgl As Integer = 6
    Dim itemJam As Integer = 7
    Dim itemUserId As Integer = 8
    Dim itemKodeSO As Integer = 9
    Dim itemKdBrg As Integer = 10
    Dim itemNmBrg As Integer = 11
    Dim itemTglProd As Integer = 12
    Dim itemTglExp As Integer = 13
    Dim itemJumlah As Integer = 14
    Dim itemJmlBags As Integer = 15
    Dim itemSatuan As Integer = 16
    Dim itemNilaiPengali As Integer = 17
    Dim itemNilaiBrg As Integer = 18
    Dim itemSatuanBrg As Integer = 19
    Dim itemUrutOto As Integer = 20
    Dim itemMetodeTimbang As Integer = 21



    Private Sub Emi_Display_Timbang_FloorScale_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub



    Public Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Display_Barang_Masuk")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")

            Lv_BM_FloorScale.Items.Clear()
            Lv_BM_FloorScale.Columns.Clear()
            Lv_BM_FloorScale.Columns.Add(Base_Language.Lang_Global_NoFaktur, 120, HorizontalAlignment.Left)
            Lv_BM_FloorScale.Columns.Add("No Pembelian Loading", 0, HorizontalAlignment.Left)
            Lv_BM_FloorScale.Columns.Add("Id Nametag Pallet", 0, HorizontalAlignment.Left)
            Lv_BM_FloorScale.Columns.Add("No SJ", 100, HorizontalAlignment.Left)
            Lv_BM_FloorScale.Columns.Add("No Plat", 100, HorizontalAlignment.Left)
            Lv_BM_FloorScale.Columns.Add("Nama Supplier", 200, HorizontalAlignment.Left)
            Lv_BM_FloorScale.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
            Lv_BM_FloorScale.Columns.Add("Jam", 80, HorizontalAlignment.Center)
            Lv_BM_FloorScale.Columns.Add("User ID", 0, HorizontalAlignment.Center)
            Lv_BM_FloorScale.Columns.Add("Lokasi", 0, HorizontalAlignment.Left)
            Lv_BM_FloorScale.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
            Lv_BM_FloorScale.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
            Lv_BM_FloorScale.Columns.Add("Tanggal Produksi", 150, HorizontalAlignment.Center)
            Lv_BM_FloorScale.Columns.Add("Tanggal Expired", 150, HorizontalAlignment.Center)
            Lv_BM_FloorScale.Columns.Add("Jumlah", 100, HorizontalAlignment.Center)
            Lv_BM_FloorScale.Columns.Add("Jumlah Bags", 100, HorizontalAlignment.Center)
            Lv_BM_FloorScale.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
            Lv_BM_FloorScale.Columns.Add("Nilai Pengali", 0, HorizontalAlignment.Right)
            Lv_BM_FloorScale.Columns.Add("Nilai Barang", 0, HorizontalAlignment.Right)
            Lv_BM_FloorScale.Columns.Add("Satuan Barang", 0, HorizontalAlignment.Center)
            Lv_BM_FloorScale.Columns.Add("UrutOto", 0, HorizontalAlignment.Left)
            Lv_BM_FloorScale.Columns.Add("Metode Timbang", 120, HorizontalAlignment.Left)
            Lv_BM_FloorScale.View = View.Details

            Load_Lv()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Load_Lv()
        Try
            OpenConn()

            arrAlreadyPrinted.Clear()

            SQL = "select a.No_Faktur, a.No_Pembelian_Loading, a.Id_Nametag_Pallet, a.No_SJ, a.No_Plat, c.Nama as nama_supplier, "
            SQL = SQL & "a.tanggal, a.jam, a.userid, a.kode_stock_owner, a.kode_barang, d.nama as nama_barang, a.tgl_produksi_real, a.tgl_expired_real, "
            SQL = SQL & "a.jumlah, a.jumlah_bags, a.satuan, a.nilai_barang, a.satuan_barang,  a.sdh_cetak, a.metode_Timbang "
            SQL = SQL & "from EMI_Barang_Masuk_Perpallet a, Suppliers c, Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = c.Kode_Supplier "
            SQL = SQL & "and a.Kode_Barang = d.Kode_Barang and a.Kode_Stock_Owner = d.Kode_Stock_Owner "
            SQL = SQL & "and a.flag_angkut is null and Flag_Timbang is null and a.status is null "
            'SQL = SQL & "and a.sdh_cetak is null "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.lokasi = '" & Lokasi & "' and a.Metode_Timbang = 'FLOOR SCALE'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_BM_FloorScale.Items.Add(Dr("No_Faktur"))
                    lvw.SubItems.Add(Dr("No_Pembelian_Loading"))
                    lvw.SubItems.Add("")
                    lvw.SubItems.Add(Dr("No_SJ"))
                    lvw.SubItems.Add(Dr("No_Plat"))
                    lvw.SubItems.Add(Dr("nama_supplier"))
                    lvw.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Dr("jam"))
                    lvw.SubItems.Add(Dr("userid"))
                    lvw.SubItems.Add(Dr("kode_stock_owner"))
                    lvw.SubItems.Add(Dr("kode_barang"))
                    lvw.SubItems.Add(Dr("nama_barang"))
                    lvw.SubItems.Add(Format(Dr("tgl_produksi_real"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Format(Dr("tgl_expired_real"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    If General_Class.CekNULL(Dr("jumlah_bags")) = "" Then
                        lvw.SubItems.Add("-")
                    Else
                        lvw.SubItems.Add(Dr("jumlah_bags"))
                    End If
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add("")
                    lvw.SubItems.Add(Format(Dr("nilai_barang"), "N2"))
                    lvw.SubItems.Add(Dr("satuan_barang"))
                    lvw.SubItems.Add("")
                    lvw.SubItems.Add(Dr("metode_timbang"))

                    If General_Class.CekNULL(Dr("sdh_cetak")) = "Y" Then
                        lvw.BackColor = Color.Yellow
                        arrAlreadyPrinted.Add(Dr("No_Faktur"))
                    End If
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvNoFaktur = Lv_BM_FloorScale.Items(No_Index).Text
        LvNoPembLoading = Lv_BM_FloorScale.Items(No_Index).SubItems(itemNoPembLoading).Text
        LvIdNametagPallet = Lv_BM_FloorScale.Items(No_Index).SubItems(itemIdNametagPallet).Text
        LvNoSJ = Lv_BM_FloorScale.Items(No_Index).SubItems(itemNoSJ).Text
        LvNoPlat = Lv_BM_FloorScale.Items(No_Index).SubItems(itemNoPlat).Text
        LvNmSupplier = Lv_BM_FloorScale.Items(No_Index).SubItems(itemNmSupplier).Text
        LvTgl = Lv_BM_FloorScale.Items(No_Index).SubItems(itemTgl).Text
        LvJam = Lv_BM_FloorScale.Items(No_Index).SubItems(itemJam).Text
        LvUserId = Lv_BM_FloorScale.Items(No_Index).SubItems(itemUserId).Text
        LvKodeSO = Lv_BM_FloorScale.Items(No_Index).SubItems(itemKodeSO).Text
        LvKdBrg = Lv_BM_FloorScale.Items(No_Index).SubItems(itemKdBrg).Text
        LvNmBrg = Lv_BM_FloorScale.Items(No_Index).SubItems(itemNmBrg).Text
        LvTglProd = Lv_BM_FloorScale.Items(No_Index).SubItems(itemTglProd).Text
        LvTglExp = Lv_BM_FloorScale.Items(No_Index).SubItems(itemTglExp).Text
        LvJumlah = Lv_BM_FloorScale.Items(No_Index).SubItems(itemJumlah).Text
        LvJmlBags = Lv_BM_FloorScale.Items(No_Index).SubItems(itemJmlBags).Text
        LvSatuan = Lv_BM_FloorScale.Items(No_Index).SubItems(itemSatuan).Text
        LvNilaiPengali = Lv_BM_FloorScale.Items(No_Index).SubItems(itemNilaiPengali).Text
        LvNilaiBrg = Lv_BM_FloorScale.Items(No_Index).SubItems(itemNilaiBrg).Text
        LvSatuanBrg = Lv_BM_FloorScale.Items(No_Index).SubItems(itemSatuanBrg).Text
        LvUrutOto = Lv_BM_FloorScale.Items(No_Index).SubItems(itemUrutOto).Text
        LvMetodeTimbang = Lv_BM_FloorScale.Items(No_Index).SubItems(itemMetodeTimbang).Text
    End Sub



    Private Sub Lv_BM_FloorScale_DoubleClick(sender As Object, e As EventArgs) Handles Lv_BM_FloorScale.DoubleClick

        If Lv_BM_FloorScale.Items.Count = 0 Then Exit Sub

        Get_Isi_Listview(Lv_BM_FloorScale.FocusedItem.Index)

        Try
            OpenConn()

            SQL = "select Flag_Timbang from EMI_Barang_Masuk_Perpallet where No_Faktur='" & LvNoFaktur.ToString & "' and status is null "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    If General_Class.CekNULL(dr("Flag_Timbang")) = "Y" Then
                        CloseConn()
                        'MessageBox.Show("Data Sudah DiTimbang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Loop
            End Using

            SQL = ""
            SQL = SQL & ""
            SQL = SQL & ""
            SQL = SQL & ""
            SQL = SQL & ""
            SQL = SQL & ""
            SQL = SQL & ""
            SQL = SQL & ""
            SQL = SQL & ""
            SQL = SQL & ""





            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        EMI_Timbang_Floor_Scale.kosong()
        EMI_Timbang_Floor_Scale.CmbJenisTimbang.Text = "BARANG MASUK"
        EMI_Timbang_Floor_Scale.txtKodeTransfer.Text = LvNoFaktur
        EMI_Timbang_Floor_Scale.UNIX.Text = LvNoFaktur
        EMI_Timbang_Floor_Scale.txt_lokasi.Text = LvKodeSO
        EMI_Timbang_Floor_Scale.txt_barang.Text = LvNmBrg
        EMI_Timbang_Floor_Scale.TxtKdBarang.Text = LvKdBrg
        EMI_Timbang_Floor_Scale.txt_Jml_Estimasi.Text = 0


        'EMI_Timbang_Floor_Scale.txtUrutOto.Text = GetDataUrutOto
        'EMI_Timbang_Floor_Scale.txt_Barang_SN.Text = GetDataBrgSN
        'EMI_Timbang_Floor_Scale.TxtJumlahBags.Text = GetDataJumlahBags
        'EMI_Timbang_Floor_Scale.TxtBeratBags.Text = GetDataBeratBags & " " & GetDataSatuanBeratBags
        'EMI_Timbang_Floor_Scale.Txt_Berat_Bags_Bersih.Text = GetDataBeratBags
        'EMI_Timbang_Floor_Scale.TxtSatuan_FloorScale.Text = GetDataSatuanBeratBesar

        'EMI_Timbang_Floor_Scale.Txt_SatuanKecil.Text = GetDataSatuanKecil
        'EMI_Timbang_Floor_Scale.TxtBarcode.Text = Txt_ScanBarcode.Text
        'EMI_Timbang_Floor_Scale.Txt_Sisa_Request.Text = SisaRequest


        EMI_Timbang_Floor_Scale.ShowDialog()

    End Sub


    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Emi_Display_Timbang_FloorScale_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If EMI_Timbang_Floor_Scale.SerialPort.IsOpen Then
            EMI_Timbang_Floor_Scale.SerialPort.Close()
            EMI_Timbang_Floor_Scale.SerialPort.Dispose()
        End If
    End Sub
End Class