Public Class Emi_Display_Request_Material_BackUP
    Dim Jenis = "Emi_Display_Request_Material"
    Public lokasi_kirim, asal As String
    Dim lv_Keterangan, lv_NoSplit, lv_kodeSO, Lv_MetPotStok, lv_KdBrg, lv_NmBrg, lv_Jenis As String
    Dim lv_TglPermintaan, lv_JamPermintaan, lv_Jumlah, lv_Satuan, lv_UserInput, lv_Warna, lv_GoodStock As String
    Dim lv_SatuanBesar, lv_SatuanDisplay, lv_JmlBags, lv_SatuanBags, Lv_Oto, lv_JumlahTF, LvJenisKemasan As String

    Dim item_NoSplit As Integer = 0
    Dim item_KodeSo As Integer = 1
    Dim item_KdBarang As Integer = 2
    Dim item_NmBarang As Integer = 3
    Dim item_Keterangan As Integer = 4
    Dim item_TglPermintaan As Integer = 5
    Dim item_Jumlah As Integer = 6
    Dim item_JumlahTransfer As Integer = 7
    Dim item_SatuanDisplay As Integer = 8
    'HIDE
    Dim item_JamPermintaan As Integer = 9
    Dim item_Satuan As Integer = 10
    Dim item_UserInput As Integer = 11
    Dim item_Warna As Integer = 12
    Dim item_Stock As Integer = 13
    Dim item_SatuanBesar As Integer = 14
    Dim item_JmlBags As Integer = 15
    Dim item_SatuanBags As Integer = 16
    Dim item_Oto As Integer = 17
    Dim item_MetodePengeluaran As Integer = 18
    Dim item_JenisKemasan As Integer = 19

    Dim Flag_Opname As Boolean




    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)

        lv_NoSplit = Lv_Data.Items(NoIndex).SubItems(item_NoSplit).Text
        lv_kodeSO = Lv_Data.Items(NoIndex).SubItems(item_KodeSo).Text
        lv_KdBrg = Lv_Data.Items(NoIndex).SubItems(item_KdBarang).Text
        lv_NmBrg = Lv_Data.Items(NoIndex).SubItems(item_NmBarang).Text
        lv_TglPermintaan = Lv_Data.Items(NoIndex).SubItems(item_TglPermintaan).Text
        lv_Jumlah = Lv_Data.Items(NoIndex).SubItems(item_Jumlah).Text
        lv_JumlahTF = Lv_Data.Items(NoIndex).SubItems(item_JumlahTransfer).Text
        lv_SatuanDisplay = Lv_Data.Items(NoIndex).SubItems(item_SatuanDisplay).Text
        lv_Keterangan = Lv_Data.Items(NoIndex).SubItems(item_Keterangan).Text
        lv_JamPermintaan = Lv_Data.Items(NoIndex).SubItems(item_JamPermintaan).Text
        lv_Satuan = Lv_Data.Items(NoIndex).SubItems(item_Satuan).Text
        lv_UserInput = Lv_Data.Items(NoIndex).SubItems(item_UserInput).Text
        lv_Warna = Lv_Data.Items(NoIndex).SubItems(item_Warna).Text
        lv_GoodStock = Lv_Data.Items(NoIndex).SubItems(item_Stock).Text
        lv_SatuanBesar = Lv_Data.Items(NoIndex).SubItems(item_SatuanBesar).Text
        lv_JmlBags = Lv_Data.Items(NoIndex).SubItems(item_JmlBags).Text
        lv_SatuanBags = Lv_Data.Items(NoIndex).SubItems(item_SatuanBags).Text
        Lv_Oto = Lv_Data.Items(NoIndex).SubItems(item_Oto).Text
        Lv_MetPotStok = Lv_Data.Items(NoIndex).SubItems(item_MetodePengeluaran).Text
        LvJenisKemasan = Lv_Data.Items(NoIndex).SubItems(item_JenisKemasan).Text

    End Sub

    Private Sub Emi_Display_Request_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Split", 150, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("Lokasi", 170, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Nama Barang", 150, HorizontalAlignment.Left) '3
        Lv_Data.Columns.Add("Keterangan", 300, HorizontalAlignment.Left) '4
        Lv_Data.Columns.Add("Tanggal Permintaan", 130, HorizontalAlignment.Center) '5
        Lv_Data.Columns.Add("Jumlah", 110, HorizontalAlignment.Right) '6
        Lv_Data.Columns.Add("Jumlah Transfer", 110, HorizontalAlignment.Right) '7
        Lv_Data.Columns.Add("Satuan Display", 100, HorizontalAlignment.Center) '8
        'HIDE
        Lv_Data.Columns.Add("Jam Permintaan", 0, HorizontalAlignment.Center) '9
        Lv_Data.Columns.Add("Satuan", 0, HorizontalAlignment.Center) '10
        Lv_Data.Columns.Add("User Input", 0, HorizontalAlignment.Center) '11
        Lv_Data.Columns.Add("Warna", 0, HorizontalAlignment.Center) '12
        Lv_Data.Columns.Add("Stock", 0, HorizontalAlignment.Right) '13
        Lv_Data.Columns.Add("Satuan Besar", 0, HorizontalAlignment.Center) '14
        Lv_Data.Columns.Add("Jumlah Bags", 0, HorizontalAlignment.Right) '15
        Lv_Data.Columns.Add("Satuan Bags", 0, HorizontalAlignment.Center) '16
        Lv_Data.Columns.Add("Urut Oto", 0, HorizontalAlignment.Center) '17
        Lv_Data.Columns.Add("Metode Stock", 0, HorizontalAlignment.Center) '18
        Lv_Data.Columns.Add("Jenis Kemasan", 0, HorizontalAlignment.Center) '19
        Lv_Data.View = View.Details

        Kosong()
    End Sub

    Private Sub Kosong()
        Try
            OpenConn()

            SQL = "select Flag_Opname from init where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Opname")) = "Y" Then
                        Flag_Opname = True
                    Else
                        Flag_Opname = False
                    End If
                End If
            End Using
            Lv_Data.Items.Clear()
            'SQL = "select a.no_faktur_order, a.keterangan, d.Metode_Pengeluaran_Stok, d.jenis_kemasan, c.Kode_Stock_Owner, c.Kode_Barang, d.Nama, b.Kode_Group_Jenis, a.Tanggal, a.Jam, c.Jumlah,  a.UserId, c.warna, "
            'SQL = SQL & "dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, d.satuan, c.satuan, d.good_stock) as Good_Stock, d.Satuan, c.Satuan as Satuan_Display, "
            'SQL = SQL & "ISNULL(d.Jumlah_Bags, 0) as Jumlah_Bags, d.Satuan_Isi_Bags, c.Urut_Oto, "

            ''SQL = SQL & "ISNULL((select sum(z.total) from Tf_Stock z, tf_stock_parent y where "
            ''SQL = SQL & "y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.status is null and "
            ''SQL = SQL & "c.Kode_Perusahaan = z.Kode_Perusahaan and c.Urut_Oto = z.urut_material_requisition_convert), '0') as Total_TF "

            'SQL = SQL & "ISNULL((select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w  where "
            'SQL = SQL & "x.kode_Perusahaan=y.kode_perusahaan and x.no_faktur=y.no_faktur and x.status is null and "
            'SQL = SQL & "y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.urut_oto=z.urut_tf and (z.selesai is null or z.selesai='Y') and "
            'SQL = SQL & "z.kode_Perusahaan=w.kode_perusahaan and z.no_faktur=w.no_faktur and z.urut_oto=w.Urut_Det and "
            'SQL = SQL & "c.Kode_Perusahaan = y.Kode_Perusahaan and c.Urut_Oto = y.urut_material_requisition_convert  and y.Flag_Jenis_Request = 'PRODUKSI'), '0') as Total_TF "

            'SQL = SQL & "from Emi_Material_Requisition a, EMI_Group_Jenis b, Emi_Material_Requisition_Det_Convert c, barang d  "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            'SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            'SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Faktur = c.No_Faktur "
            'SQL = SQL & "and c.kode_barang = d.kode_barang and d.kode_stock_owner='" & lokasi_kirim & "' "
            'SQL = SQL & "and a.Flag_Process = 'Y' and a.status is null and c.jumlah<>0 "
            'SQL = SQL & "and c.Flag_Transfer is null "
            'SQL = SQL & "and d.Id_Kategori_Gudang = ( "
            'SQL = SQL & "select top 1 z.Id_Kategori_Gudang "
            'SQL = SQL & "from EMI_Kategori_Gudang_PerLokasi z "
            'SQL = SQL & "where a.Kode_Perusahaan = z.kode_perusahaan and z.Lokasi_Gudang = d.kode_stock_owner )"
            'SQL = SQL & "order by a.no_faktur_order"

            SQL = "select a.no_faktur_order, a.keterangan, d.Metode_Pengeluaran_Stok, d.jenis_kemasan, c.Kode_Stock_Owner, c.Kode_Barang, d.Nama, b.Kode_Group_Jenis, a.Tanggal, a.Jam, c.Jumlah,  a.UserId, c.warna, "
            SQL = SQL & "dbo.ubah_satuan(a.kode_Perusahaan, 'masa', a.kode_barang, d.satuan, c.satuan, d.good_stock) as Good_Stock, d.Satuan, c.Satuan as Satuan_Display, "
            SQL = SQL & "ISNULL(d.Jumlah_Bags, 0) as Jumlah_Bags, d.Satuan_Isi_Bags, c.Urut_Oto, "
            SQL = SQL & "ISNULL(( select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w  where "
            SQL = SQL & "x.kode_Perusahaan=y.kode_perusahaan and x.no_faktur=y.no_faktur and x.status is null and "
            SQL = SQL & "y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.urut_oto=z.urut_tf and (z.selesai is null or z.selesai='Y') and "
            SQL = SQL & "z.kode_Perusahaan=w.kode_perusahaan and z.no_faktur=w.no_faktur and z.urut_oto=w.Urut_Det and "
            SQL = SQL & "c.Kode_Perusahaan = y.Kode_Perusahaan and c.Urut_Oto = y.urut_material_requisition_convert  and y.Flag_Jenis_Request = 'PRODUKSI'), '0') as Total_TF, "
            SQL = SQL & "(c.jumlah - (c.Jumlah * (d.Toleransi_Tf_Min / 100))) as Toleransi_Min, "
            SQL = SQL & "(c.jumlah + (c.Jumlah * (d.Toleransi_Tf_Max / 100))) as Toleransi_Max "
            SQL = SQL & "from Emi_Material_Requisition a, EMI_Group_Jenis b, Emi_Material_Requisition_Det_Convert c, barang d  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Group_Jenis = b.Id_Group_Jenis "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' and a.No_Faktur = c.No_Faktur "
            SQL = SQL & "and c.kode_barang = d.kode_barang and d.kode_stock_owner='" & lokasi_kirim & "' "
            SQL = SQL & "and a.Flag_Process = 'Y' and a.status is null "
            SQL = SQL & "and c.Flag_Transfer is null "
            SQL = SQL & "and c.jumlah > 0 and c.jumlah_barang > 0"
            SQL = SQL & "and d.Id_Kategori_Gudang = ( "
            SQL = SQL & "select top 1 z.Id_Kategori_Gudang "
            SQL = SQL & "from EMI_Kategori_Gudang_PerLokasi z "
            SQL = SQL & "where a.Kode_Perusahaan = z.kode_perusahaan and z.Lokasi_Gudang = d.kode_stock_owner ) "
            SQL = SQL & "and case when a.flag_tambah = 'Y' then a.Flag_Validasi_Tambah else 'Y' end = 'Y' "
            SQL = SQL & "order by a.no_faktur_order "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As ListViewItem
                    lv = Lv_Data.Items.Add(Dr("no_faktur_order"))
                    lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(General_Class.CekNULL(Dr("keterangan")))
                    lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    lv.SubItems.Add(Format(Dr("Total_TF"), "N4"))
                    lv.SubItems.Add(Dr("Satuan_Display"))
                    'HIDE
                    lv.SubItems.Add(Dr("Jam"))

                    lv.SubItems.Add(Dr("Satuan"))
                    lv.SubItems.Add(Dr("UserId"))
                    lv.SubItems.Add(Dr("warna"))

                    If Flag_Opname = True Then
                        lv.SubItems.Add(Format(0, "N4"))
                        lv.SubItems.Add(Dr("Satuan"))
                        lv.SubItems.Add(Format(0, "N4"))
                    Else
                        lv.SubItems.Add(Format(Dr("Good_Stock"), "N4"))
                        lv.SubItems.Add(Dr("Satuan"))
                        lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N4"))
                    End If

                    If General_Class.CekNULL(Dr("Satuan_Isi_Bags")) = "" Then
                        lv.SubItems.Add("-")
                    Else
                        lv.SubItems.Add(Dr("Satuan_Isi_Bags"))
                    End If

                    lv.SubItems.Add(Dr("Urut_Oto"))
                    lv.SubItems.Add(Dr("Metode_Pengeluaran_Stok"))
                    lv.SubItems.Add(Dr("jenis_kemasan"))

                    If Dr("Total_TF") > Dr("Jumlah") Then
                        lv.BackColor = Color.LightGreen
                    ElseIf Dr("Total_TF") > 0 Then
                        lv.BackColor = Color.LightYellow
                    Else
                        lv.BackColor = Color.White
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


    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        If asal = "TF_Stock" Then

            'Transfer_Stock_3.kosong()
            Transfer_Stock_3.CmbJnsTransfer.Enabled = False
            Transfer_Stock_3.CmbSO_Asal.Enabled = False
            Transfer_Stock_3.CmbSo_Tujuan.Enabled = False

            Transfer_Stock_3.TxtKd_Barang.Text = String.Empty
            Transfer_Stock_3.Txt_SO.Text = String.Empty
            Transfer_Stock_3.TxtNm_Barang.Text = String.Empty

            Get_Isi_ListView(Lv_Data.FocusedItem.Index)
            Transfer_Stock_3.asal = Jenis
            Transfer_Stock_3.Lv_DetBarang.Visible = False
            Transfer_Stock_3.TxtKd_Barang.Enabled = False
            'Transfer_Stock_3.Btn_GetData.Enabled = False
            Transfer_Stock_3.TxtKd_Barang.Text = lv_KdBrg
            Transfer_Stock_3.TxtNm_Barang.Text = "X"
            Transfer_Stock_3.Txt_SO.Text = lv_kodeSO
            Transfer_Stock_3.TxtSatuanKecil.Text = lv_Satuan
            Transfer_Stock_3.Txt_Warna.Text = lv_Warna
            Transfer_Stock_3.TxtStock.Text = Format(lv_GoodStock, "N4")
            Transfer_Stock_3.TxtSatuan.Text = lv_SatuanDisplay
            Transfer_Stock_3.TxtBags.Text = Format(Val(HilangkanTanda(lv_JmlBags)), "N4")

            Transfer_Stock_3.TxtJenisBags.Text = LvJenisKemasan

            Transfer_Stock_3.Cmb_Warna.SelectedItem = lv_Warna

            Transfer_Stock_3.TxtStockDisplay.Text = Format(Val(HilangkanTanda(lv_GoodStock)), "N4") + " " + lv_SatuanDisplay

            Dim Jumlah_Permintaan As Double = lv_Jumlah - lv_JumlahTF

            Transfer_Stock_3.Txt_JumlahPermintaan.Text = Format(Jumlah_Permintaan, "N4")
            Transfer_Stock_3.Txt_SatuanPermintaan.Text = lv_SatuanDisplay
            Transfer_Stock_3.TxtjmlPermintaanDisplay.Text = Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4") + " " + lv_SatuanDisplay
            Transfer_Stock_3.TxtjmlPermintaanBersih.Text = HilangkanTanda(Format(Val(HilangkanTanda(Jumlah_Permintaan)), "N4"))
            Transfer_Stock_3.Txt_OtoMaterial_req.Text = Lv_Oto
            Transfer_Stock_3.Txt_Jenis_Transfer.Text = "PRODUKSI"
            Transfer_Stock_3.TxtMetPotStok.Text = Lv_MetPotStok
            Transfer_Stock_3.Txt_Urut_Request.Text = Lv_Oto
            Transfer_Stock_3.Btn_Insert_Click(Lv_Data, e)
            'Transfer_Stock_3.DGV_Data_TF.Rows.Clear()


        ElseIf asal = "Split_Stock" Then


            'Transfer_Stock_3.kosong()
            Emi_Split_Stock_QC.CmbJnsTransfer.Enabled = False
            Emi_Split_Stock_QC.CmbSO_Asal.Enabled = False
            Emi_Split_Stock_QC.CmbSo_Tujuan.Enabled = False

            Emi_Split_Stock_QC.TxtKd_Barang.Text = String.Empty
            Emi_Split_Stock_QC.Txt_SO.Text = String.Empty
            Emi_Split_Stock_QC.TxtNm_Barang.Text = String.Empty

            Get_Isi_ListView(Lv_Data.FocusedItem.Index)
            Emi_Split_Stock_QC.asal = Jenis
            Emi_Split_Stock_QC.Lv_DetBarang.Visible = False
            Emi_Split_Stock_QC.TxtKd_Barang.Enabled = False
            'Transfer_Stock_3.Btn_GetData.Enabled = False
            Emi_Split_Stock_QC.TxtKd_Barang.Text = lv_KdBrg
            Emi_Split_Stock_QC.TxtNm_Barang.Text = "X"
            Emi_Split_Stock_QC.Txt_SO.Text = lv_kodeSO
            Emi_Split_Stock_QC.TxtSatuanKecil.Text = lv_Satuan
            Emi_Split_Stock_QC.Txt_Warna.Text = lv_Warna
            Emi_Split_Stock_QC.TxtStock.Text = Format(lv_GoodStock, "N4")
            Emi_Split_Stock_QC.TxtSatuan.Text = lv_SatuanDisplay
            Emi_Split_Stock_QC.TxtBags.Text = Format(Val(HilangkanTanda(lv_JmlBags)), "N4")
            Emi_Split_Stock_QC.TxtJenisBags.Text = LvJenisKemasan
            Emi_Split_Stock_QC.TxtMetPotStok.Text = Lv_MetPotStok
            Emi_Split_Stock_QC.Cmb_Warna.SelectedItem = lv_Warna

            Emi_Split_Stock_QC.TxtStockDisplay.Text = Format(Val(HilangkanTanda(lv_GoodStock)), "N4") + " " + lv_SatuanDisplay
            Emi_Split_Stock_QC.Txt_JumlahPermintaan.Text = Format(lv_Jumlah, "N4")
            Emi_Split_Stock_QC.Txt_SatuanPermintaan.Text = lv_SatuanDisplay
            Emi_Split_Stock_QC.TxtjmlPermintaanDisplay.Text = Format(Val(HilangkanTanda(lv_Jumlah)), "N4") + " " + lv_SatuanDisplay
            Emi_Split_Stock_QC.TxtjmlPermintaanBersih.Text = HilangkanTanda(Format(Val(HilangkanTanda(lv_Jumlah)), "N4"))
            Emi_Split_Stock_QC.Txt_OtoMaterial_req.Text = Lv_Oto
            Emi_Split_Stock_QC.Txt_Jenis_Transfer.Text = "PRODUKSI"
            Emi_Split_Stock_QC.Txt_Urut_Request.Text = Lv_Oto
            Emi_Split_Stock_QC.Btn_Insert_Click(Lv_Data, e)
            'Transfer_Stock_3.DGV_Data_TF.Rows.Clear()




        End If



        Me.Close()


    End Sub

    Private Sub SelesaiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelesaiToolStripMenuItem.Click

        Try
            OpenConn()

            Dim Hapus1 As String = MessageBox.Show("Anda yakin ingin selesaikan data", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If Hapus1 = vbYes Then
                '==============================
                '=     UPDATE FLAG TAMPIL     =
                ''=============================
                Get_Isi_ListView(Lv_Data.FocusedItem.Index)

                SQL = "update Emi_Material_Requisition_Det_Convert set Flag_Transfer = 'Y' where Urut_Oto = '" & Lv_Oto & "' "
                ExecuteTrans(SQL)
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
    End Sub
End Class