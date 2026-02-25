Public Class Emi_Display_Request_General

    Dim JudulForm As String = "Request General"
    Public Lokasi_Req, Lokasi_Sup, asal As String

    Dim Lv_Lokasi, Lv_KdBarang, Lv_Nama, Lv_Keterangan, Lv_TglPermintaan, Lv_Jumlah, Lv_JumlahTransfer, Lv_Satuan, Lv_KdSo, Lv_SatuanKecil, Lv_UrutOto As String

    Dim item_lokasi As Integer = 0
    Dim item_kdbarang As Integer = 1
    Dim item_Nama As Integer = 2
    Dim item_Keterangan As Integer = 3
    Dim item_TglPermintaan As Integer = 4
    Dim item_Jumlah As Integer = 5
    Dim item_JumlahTransfer As Integer = 6
    Dim item_Satuan As Integer = 7
    Dim item_KdSo As Integer = 8
    Dim item_SatuanKecil As Integer = 9
    Dim item_UrutOto As Integer = 10



    Private Sub Emi_Display_Request_General_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        Kosong()
    End Sub

    Private Sub Kosong()

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Lokasi", 180, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Nama", 0, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Keterangan", 300, HorizontalAlignment.Left) '3
        Lv_Data.Columns.Add("Tanggal Permintaan", 130, HorizontalAlignment.Center) '4
        Lv_Data.Columns.Add("Jumlah", 150, HorizontalAlignment.Right) '5
        Lv_Data.Columns.Add("Jumlah Transfer", 150, HorizontalAlignment.Right) '6
        Lv_Data.Columns.Add("Satuan", 90, HorizontalAlignment.Center) '7
        'Hide
        Lv_Data.Columns.Add("KdSo", 0, HorizontalAlignment.Left) '8
        Lv_Data.Columns.Add("satuanKecil", 0, HorizontalAlignment.Left) '9
        Lv_Data.Columns.Add("urut_oto", 0, HorizontalAlignment.Left) '10
        Lv_Data.View = View.Details

        Try
            OpenConn()

            '====================
            '=     GET DATA     =
            '====================
            Lv_Data.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.Kode_Stock_Owner_Req, a.Kode_Stock_Owner_Sup, "
            SQL = SQL & "isnull(( select z.Keterangan from Stock_Owner_Gudang z where a.kode_perusahaan = z.kode_perusahaan and a.Kode_Stock_Owner_Req = z.Kode_Stock_Owner ), '-') as Lokasi, "
            SQL = SQL & "b.Kode_Barang, c.Nama, b.Keterangan, a.Tanggal, b.Jumlah, b.Jumlah_Barang ,  "
            SQL = SQL & "b.Satuan, b.Satuan_Barang, b.urut_oto "

            If asal = "TF_Stock" Then

                SQL = SQL & ", ISNULL((select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w   "
                SQL = SQL & "where x.kode_Perusahaan=y.kode_perusahaan and x.no_faktur=y.no_faktur and x.status is null  "
                SQL = SQL & "and y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.urut_oto=z.urut_tf  "
                SQL = SQL & "and (z.selesai is null or z.selesai='Y') and z.kode_Perusahaan=w.kode_perusahaan and z.no_faktur=w.no_faktur  "
                SQL = SQL & "and z.urut_oto=w.Urut_Det and b.Kode_Perusahaan = y.Kode_Perusahaan and b.Urut_Oto = y.Urut_Material_Requisition_Convert and y.Flag_Jenis_Request = 'GENERAL'), '0') as Total_TF "

            ElseIf asal = "Split_Stock" Then

                SQL = SQL & ", ISNULL((select sum(w.jumlah) from Tf_Stock_QC x, Tf_Stock_QC_Detail y, Tf_Stock_QC_det z, Tf_Stock_QC_det2 w  "
                SQL = SQL & "where x.kode_Perusahaan=y.kode_perusahaan and x.no_faktur=y.no_faktur and x.status is null  "
                SQL = SQL & "and y.kode_Perusahaan=z.kode_perusahaan and y.no_faktur=z.no_faktur and y.urut_oto=z.urut_tf  "
                SQL = SQL & "and (z.selesai is null or z.selesai='Y') and z.kode_Perusahaan=w.kode_perusahaan and z.no_faktur=w.no_faktur  "
                SQL = SQL & "and z.urut_oto=w.Urut_Det and b.Kode_Perusahaan = y.Kode_Perusahaan and b.Urut_Oto = y.Urut_Material_Requisition_Convert and y.Flag_Jenis_Request = 'GENERAL'), '0') as Total_TF "

            End If

            SQL = SQL & "from Emi_Material_Requisition_General a, Emi_Material_Requisition_General_Detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Barang = c.Kode_Barang and a.Kode_Stock_Owner_Sup = c.Kode_Stock_Owner "
            SQL = SQL & "and a.Status is null  "
            SQL = SQL & "and a.flag_selesai is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Stock_Owner_Req = '" & Lokasi_Req & "' "
            SQL = SQL & "and a.Kode_Stock_Owner_Sup = '" & Lokasi_Sup & "' "
            SQL = SQL & "order by a.Tanggal, b.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add("X")
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Total_TF"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    'Hide
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner_Req"))
                    Lv.SubItems.Add(Dr("Satuan_Barang"))
                    Lv.SubItems.Add(Dr("urut_oto"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)

        Lv_Lokasi = Lv_Data.Items(index).SubItems(item_lokasi).Text
        Lv_KdBarang = Lv_Data.Items(index).SubItems(item_kdbarang).Text
        Lv_Nama = Lv_Data.Items(index).SubItems(item_Nama).Text
        Lv_Keterangan = Lv_Data.Items(index).SubItems(item_Keterangan).Text
        Lv_TglPermintaan = Lv_Data.Items(index).SubItems(item_TglPermintaan).Text
        Lv_Jumlah = Lv_Data.Items(index).SubItems(item_Jumlah).Text
        Lv_JumlahTransfer = Lv_Data.Items(index).SubItems(item_JumlahTransfer).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_KdSo = Lv_Data.Items(index).SubItems(item_KdSo).Text
        Lv_SatuanKecil = Lv_Data.Items(index).SubItems(item_SatuanKecil).Text
        Lv_UrutOto = Lv_Data.Items(index).SubItems(item_UrutOto).Text

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

            Get_Data_Lv(Lv_Data.FocusedItem.Index)

            Try
                OpenConn()

                '===========================
                '=     GET DATA BARANG     =
                '===========================
                SQL = "select a.kode_barang, a.nama, "
                SQL = SQL & "ISNULL((dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang,  "
                SQL = SQL & "isnull((select z.Satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan =a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and z.Flag_Tampil_Display is null ), 0) "
                SQL = SQL & ", isnull((select top 1 z.Satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan =a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and z.Flag_Tampil_Display ='Y' ), 0) "
                SQL = SQL & ", a.Good_Stock )), 0) as good_stock, "
                SQL = SQL & "a.jumlah_bags, ISNULL((a.Metode_Pengeluaran_Stok), '-') as Metode_Pengeluaran_Stok, ISNULL((a.Jenis_Kemasan), '-') as Jenis_Kemasan "
                SQL = SQL & "from Barang a "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner = '" & Lokasi_Sup & "' and a.Kode_Barang = '" & Lv_KdBarang & "' and a.Good_Stock <> 0 "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                Transfer_Stock_3.TxtKd_Barang.Text = .Rows(i).Item("kode_barang")
                                Transfer_Stock_3.TxtNm_Barang.Text = "X"
                                Transfer_Stock_3.TxtStockDisplay.Text = Format(.Rows(i).Item("good_stock"), "N2") + " KG"
                                Transfer_Stock_3.TxtBags.Text = Format(.Rows(i).Item("jumlah_bags"), "N0")
                                Transfer_Stock_3.TxtMetPotStok.Text = .Rows(i).Item("Metode_Pengeluaran_Stok")
                                Transfer_Stock_3.TxtJenisBags.Text = .Rows(i).Item("Jenis_Kemasan")

                            Next
                        Else
                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan")
                            Exit Sub
                        End If
                    End With
                End Using


                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            Transfer_Stock_3.Txt_Jenis_Transfer.Text = "GENERAL"
            Transfer_Stock_3.TxtSatuan.Text = Lv_Satuan
            Transfer_Stock_3.TxtSatuanKecil.Text = Lv_SatuanKecil
            Transfer_Stock_3.Txt_OtoMaterial_req.Text = Lv_UrutOto
            Transfer_Stock_3.TxtjmlPermintaanDisplay.Text = Format(Val(HilangkanTanda(Lv_Jumlah)), "N2") + " " + Lv_Satuan
            Transfer_Stock_3.TxtjmlPermintaanBersih.Text = HilangkanTanda(Format(Val(HilangkanTanda(Lv_Jumlah)), "N2"))
            Transfer_Stock_3.Lv_DetBarang.Visible = False
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

            Get_Data_Lv(Lv_Data.FocusedItem.Index)

            Try
                OpenConn()

                '===========================
                '=     GET DATA BARANG     =
                '===========================
                SQL = "select a.kode_barang, a.nama, "
                SQL = SQL & "ISNULL((dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang,  "
                SQL = SQL & "isnull((select z.Satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan =a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and z.Flag_Tampil_Display is null ), 0) "
                SQL = SQL & ", isnull((select top 1 z.Satuan from Barang_Detail_Satuan z where z.Kode_Perusahaan =a.Kode_Perusahaan and z.Kode_barang = a.Kode_Barang and z.Flag_Tampil_Display ='Y' ), 0) "
                SQL = SQL & ", a.Good_Stock )), 0) as good_stock, "
                SQL = SQL & "a.jumlah_bags, ISNULL((a.Metode_Pengeluaran_Stok), '-') as Metode_Pengeluaran_Stok, ISNULL((a.Jenis_Kemasan), '-') as Jenis_Kemasan "
                SQL = SQL & "from Barang a "
                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner = '" & Lokasi_Sup & "' and a.Kode_Barang = '" & Lv_KdBarang & "' and a.Good_Stock <> 0 "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                Emi_Split_Stock_QC.TxtKd_Barang.Text = .Rows(i).Item("kode_barang")
                                Emi_Split_Stock_QC.TxtNm_Barang.Text = "X"
                                Emi_Split_Stock_QC.TxtStockDisplay.Text = Format(.Rows(i).Item("good_stock"), "N2") + " KG"
                                Emi_Split_Stock_QC.TxtBags.Text = Format(.Rows(i).Item("jumlah_bags"), "N0")
                                Emi_Split_Stock_QC.TxtMetPotStok.Text = .Rows(i).Item("Metode_Pengeluaran_Stok")
                                Emi_Split_Stock_QC.TxtJenisBags.Text = .Rows(i).Item("Jenis_Kemasan")


                            Next
                        Else
                            CloseConn()
                            MessageBox.Show("Data Tidak Ditemukan")
                            Exit Sub
                        End If
                    End With
                End Using


                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            Emi_Split_Stock_QC.Txt_Jenis_Transfer.Text = "GENERAL"
            Emi_Split_Stock_QC.TxtSatuan.Text = Lv_Satuan
            Emi_Split_Stock_QC.Txt_OtoMaterial_req.Text = Lv_UrutOto
            Emi_Split_Stock_QC.TxtSatuanKecil.Text = Lv_SatuanKecil
            Emi_Split_Stock_QC.TxtjmlPermintaanDisplay.Text = Format(Val(HilangkanTanda(Lv_Jumlah)), "N2") + " " + Lv_Satuan
            Emi_Split_Stock_QC.TxtjmlPermintaanBersih.Text = HilangkanTanda(Format(Val(HilangkanTanda(Lv_Jumlah)), "N2"))
            Emi_Split_Stock_QC.Lv_DetBarang.Visible = False
            Emi_Split_Stock_QC.Btn_Insert_Click(Lv_Data, e)
            'Transfer_Stock_3.DGV_Data_TF.Rows.Clear()



        End If




        Me.Close()

    End Sub















End Class