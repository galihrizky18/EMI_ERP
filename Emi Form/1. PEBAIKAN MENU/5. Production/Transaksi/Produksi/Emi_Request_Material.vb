Public Class Emi_Request_Material

    Dim judulForm As String = "Request Material"

    Dim arr_So As New ArrayList

    Public No_faktur As String = ""

    Dim Dgv_NoFak, Dgv_KdSo, Dgv_KdBarang, Dgv_JmlhKebutuhan, Dgv_JmlhDiProduksi, Dgv_Sisa, Dgv_SatuanBesar, Dgv_JmlhInput, Dgv_SatuanKecil, Dgv_Tipe, Dgv_Warna, Dgv_JenisBahan, Dgv_StockProduksi, Dgv_TotalTF As String

    Dim cell_NoFak As Integer = 0
    Dim cell_Kd_SO As Integer = 1
    Dim cell_Kd_Barang As Integer = 2
    Dim cell_JumlahKebutuhan As Integer = 3
    Dim cell_JumlahDiproduksi As Integer = 4

    Dim cell_Sisa As Integer = 5
    Dim cell_SatuanBesar As Integer = 6
    Dim cell_JumlahInput As Integer = 7
    Dim cell_SatuanKecil As Integer = 8
    Dim cell_Tipe As Integer = 9
    Dim cell_warna As Integer = 1
    Dim cell_JenisBahan As Integer = 11
    Dim cell_StockProduksi As Integer = 12
    Dim cell_TotalTransfer As Integer = 13
    Dim cell_LokasiTujuan As Integer = 14

    Private Sub Btn_2Batch_Click(sender As Object, e As EventArgs) Handles Btn_2Batch.Click
        If Txt_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No Faktur Tidak ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim confirmNext As Boolean = False



        Try
            OpenConn()

            '==============================
            '=     GET NILAI PERBATCH     =
            '==============================
            SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "
            SQL = SQL & "ISNULL(( FLOOR( (c.Jumlah /  "
            SQL = SQL & "(select z.Hasil from Emi_Transaksi_Formulator z  "
            SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur)) "
            SQL = SQL & "* "
            SQL = SQL & "(ISNULL((select z.Qty_PerBatch * 2 from EMI_Master_Routing z "
            SQL = SQL & "where z.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "And z.Id_Routing = b.Id_Routing ), 0)))), 0) as Nilai_PerBatch, c.satuan "
            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, a.No_PO as No_PO, b.kode_barang, "
            SQL = SQL & "ISNULL((( (dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, 'KG', 'PCS', "
            SQL = SQL & "(ISNULL(( select z.Qty_PerBatch*2 from EMI_Master_Routing z where z.Kode_Perusahaan = b.Kode_Perusahaan And z.Id_Routing = c.Id_Routing ), 0)))) "
            SQL = SQL & "/ "
            SQL = SQL & "(select z.jumlah_barang from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) "
            SQL = SQL & "* "
            SQL = SQL & "((select z.jumlah_bahan from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) ), 0) as Nilai_PerBatch, b.Satuan "
            SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, EMI_Order_Produksi c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.No_PO = c.No_Faktur "
            SQL = SQL & "and a.No_Transaksi = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim lebih As Boolean = False
                            '===============================
                            '=     CEK APAKAH BAHAN ADA    =
                            '===============================
                            For j As Integer = 0 To Dgv_Data.Rows.Count - 1
                                If Dgv_Data.Rows(j).Cells(cell_Kd_Barang).Value = .Rows(i).Item("kode_barang") AndAlso Dgv_Data.Rows(j).Cells(cell_SatuanBesar).Value = .Rows(i).Item("Satuan") Then
                                    If Val(HilangkanTanda(Dgv_Data.Rows(j).Cells(cell_Sisa).Value)) < Val(HilangkanTanda(.Rows(i).Item("Nilai_PerBatch"))) Then

                                        Dgv_Data.Rows(j).Cells(cell_JumlahInput).Value = Format(Val(HilangkanTanda(Dgv_Data.Rows(j).Cells(cell_Sisa).Value)), "N4")

                                    Else

                                        Dgv_Data.Rows(j).Cells(cell_JumlahInput).Value = Format(.Rows(i).Item("Nilai_PerBatch"), "N4")

                                    End If

                                End If
                            Next

                        Next
                    End If
                End With
            End Using


            Get_Total_Request()


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_3Batch_Click(sender As Object, e As EventArgs) Handles Btn_3Batch.Click
        If Txt_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No Faktur Tidak ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim confirmNext As Boolean = False

        Try
            OpenConn()

            '==============================
            '=     GET NILAI PERBATCH     =
            '==============================
            SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "
            SQL = SQL & "ISNULL(( FLOOR( (c.Jumlah /  "
            SQL = SQL & "(select z.Hasil from Emi_Transaksi_Formulator z  "
            SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur)) "
            SQL = SQL & "* "
            SQL = SQL & "(ISNULL((select z.Qty_PerBatch*3 from EMI_Master_Routing z "
            SQL = SQL & "where z.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "And z.Id_Routing = b.Id_Routing ), 0)))), 0) as Nilai_PerBatch, c.satuan "
            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, a.No_PO as No_PO, b.kode_barang, "
            SQL = SQL & "ISNULL((( (dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, 'KG', 'PCS', "
            SQL = SQL & "(ISNULL(( select z.Qty_PerBatch*3 from EMI_Master_Routing z where z.Kode_Perusahaan = b.Kode_Perusahaan And z.Id_Routing = c.Id_Routing ), 0)))) "
            SQL = SQL & "/ "
            SQL = SQL & "(select z.jumlah_barang from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) "
            SQL = SQL & "* "
            SQL = SQL & "((select z.jumlah_bahan from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) ), 0) as Nilai_PerBatch, b.Satuan "
            SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, EMI_Order_Produksi c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.No_PO = c.No_Faktur "
            SQL = SQL & "and a.No_Transaksi = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim lebih As Boolean = False
                            '===============================
                            '=     CEK APAKAH BAHAN ADA    =
                            '===============================
                            For j As Integer = 0 To Dgv_Data.Rows.Count - 1
                                If Dgv_Data.Rows(j).Cells(cell_Kd_Barang).Value = .Rows(i).Item("kode_barang") AndAlso Dgv_Data.Rows(j).Cells(cell_SatuanBesar).Value = .Rows(i).Item("Satuan") Then
                                    If Val(HilangkanTanda(Dgv_Data.Rows(j).Cells(cell_Sisa).Value)) < Val(HilangkanTanda(.Rows(i).Item("Nilai_PerBatch"))) Then

                                        Dgv_Data.Rows(j).Cells(cell_JumlahInput).Value = Format(Val(HilangkanTanda(Dgv_Data.Rows(j).Cells(cell_Sisa).Value)), "N4")

                                    Else

                                        Dgv_Data.Rows(j).Cells(cell_JumlahInput).Value = Format(.Rows(i).Item("Nilai_PerBatch"), "N4")

                                    End If

                                End If
                            Next

                        Next
                    End If
                End With
            End Using


            Get_Total_Request()


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Emi_Request_Material_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Request_Material_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        kosong()
    End Sub

    Private Sub get_no_faktur()
        Txt_NoFaktur_ReqMaterial.Text = fRequestMaterial & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("Emi_Material_Requisition", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(fRequestMaterial) + 4 & ")", fRequestMaterial & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub kosong()

        Dgv_Data.Rows.Clear()
        TxtTotalRequest.Text = 0

        Dgv_Data.Columns(cell_StockProduksi).DisplayIndex = 3
        Dgv_Data.Columns(cell_TotalTransfer).DisplayIndex = 6
        Dgv_Data.Columns(cell_LokasiTujuan).DisplayIndex = 8

        get_jam()

        Try
            OpenConn()

            get_no_faktur()

            Dgv_Data.Rows.Clear()

            'TODO : LoadData

            SQL = "Select a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.Nilai_Barang, a.Satuan_Barang, 'Bahan' as tipe, c.lokasi_gudang, "
            SQL = SQL & "(ISNULL( "
            SQL = SQL & "(select sum(z.jumlah) "
            SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order ), 0)) as Jumlah_Diproduksi, " 'JUMLAH DI PRODUKSI

            SQL = SQL & "(a.Jumlah - ISNULL( "
            SQL = SQL & "(select sum(z.jumlah) "
            SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order ), 0)) as sisa, " 'SISA
            SQL = SQL & "'BAHAN' as Jenis_Bahan, " ' JENIS BAHAN

            SQL = SQL & "ISNULL(( select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Barang, a.Satuan, sum(z.Jumlah)) "
            SQL = SQL & "from Barang_SN z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang "
            SQL = SQL & "), 0) as Stock_Gudang_Produksi, " ' Stock_Gudang_Produksi

            SQL = SQL & "ISNULL((select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w, "
            SQL = SQL & "emi_material_requisition_det_convert m, emi_material_requisition n where "
            SQL = SQL & "x.kode_Perusahaan = y.kode_perusahaan And x.no_faktur = y.no_faktur And x.status Is null And "
            SQL = SQL & "y.kode_Perusahaan = z.kode_perusahaan And y.no_faktur = z.no_faktur And y.urut_oto = z.urut_tf And (z.selesai Is null Or z.selesai ='Y') and "
            SQL = SQL & "z.kode_Perusahaan = w.kode_perusahaan And z.no_faktur = w.no_faktur And z.urut_oto = w.Urut_Det And "
            SQL = SQL & "m.Kode_Perusahaan = y.Kode_Perusahaan And m.Urut_Oto = y.urut_material_requisition_convert and "
            SQL = SQL & "y.Flag_Jenis_Request = 'PRODUKSI' and "
            SQL = SQL & "m.kode_Perusahaan = n.kode_perusahaan And m.no_faktur = n.no_faktur and n.status is null and "
            SQL = SQL & "a.Kode_Perusahaan = m.Kode_Perusahaan and a.Kode_Stock_Owner = m.Kode_Stock_Owner and a.Kode_Barang = m.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = n.Kode_Perusahaan and a.No_Faktur = n.No_Faktur_Order "
            SQL = SQL & "), '0') as Total_TF "

            SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan a, Barang b, EMI_Kategori_Gudang_PerLokasi c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
            SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & Txt_NoFaktur.Text & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select a.No_Faktur, a.Kode_Stock_Owner, a.kode_barang, b.Nama, a.Jumlah, a.Satuan, a.Nilai_Barang, a.Satuan_Barang, 'Packaging' as tipe, c.lokasi_gudang, "
            SQL = SQL & "(ISNULL( "
            SQL = SQL & "(select sum(z.jumlah) "
            SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order ), 0)) as Jumlah_Diproduksi, " 'JUMLAH DI PRODUKSI

            SQL = SQL & "(a.Jumlah - ISNULL( "
            SQL = SQL & "(select sum(z.jumlah) "
            SQL = SQL & "from Emi_Material_Requisition_det z, Emi_Material_Requisition x "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order ), 0)) as sisa, " 'SISA

            SQL = SQL & "'PACKAGING' as Jenis_Bahan, " ' JENIS BAHAN

            SQL = SQL & "ISNULL(( select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Barang, a.Satuan, sum(z.Jumlah)) "
            SQL = SQL & "from Barang_SN z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang "
            SQL = SQL & "), 0) as Stock_Gudang_Produksi, " ' Stock_Gudang_Produksi

            SQL = SQL & "ISNULL((select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w, "
            SQL = SQL & "emi_material_requisition_det_convert m, emi_material_requisition n where "
            SQL = SQL & "x.kode_Perusahaan = y.kode_perusahaan And x.no_faktur = y.no_faktur And x.status Is null And "
            SQL = SQL & "y.kode_Perusahaan = z.kode_perusahaan And y.no_faktur = z.no_faktur And y.urut_oto = z.urut_tf And (z.selesai Is null Or z.selesai ='Y') and "
            SQL = SQL & "z.kode_Perusahaan = w.kode_perusahaan And z.no_faktur = w.no_faktur And z.urut_oto = w.Urut_Det And "
            SQL = SQL & "m.Kode_Perusahaan = y.Kode_Perusahaan And m.Urut_Oto = y.urut_material_requisition_convert and "
            SQL = SQL & "y.Flag_Jenis_Request = 'PRODUKSI' and "
            SQL = SQL & "m.kode_Perusahaan = n.kode_perusahaan And m.no_faktur = n.no_faktur and n.status is null and "
            SQL = SQL & "a.Kode_Perusahaan = m.Kode_Perusahaan and a.Kode_Stock_Owner = m.Kode_Stock_Owner and a.Kode_Barang = m.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = n.Kode_Perusahaan and a.No_Faktur = n.No_Faktur_Order "
            SQL = SQL & "), '0') as Total_TF "

            SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a, Barang b, EMI_Kategori_Gudang_PerLokasi c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
            SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & Txt_NoFaktur.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dgv_Data.Rows.Add(1)
                            Dgv_Data.Rows(i).Cells(cell_NoFak).Value = .Rows(i).Item("No_Faktur") '0
                            Dgv_Data.Rows(i).Cells(cell_Kd_SO).Value = .Rows(i).Item("Kode_Stock_Owner") '1
                            Dgv_Data.Rows(i).Cells(cell_Kd_Barang).Value = .Rows(i).Item("Kode_Barang") '2
                            Dgv_Data.Rows(i).Cells(cell_JumlahKebutuhan).Value = Format(Val(.Rows(i).Item("Jumlah")), "N4") '3
                            Dgv_Data.Rows(i).Cells(cell_JumlahDiproduksi).Value = Format(Val(.Rows(i).Item("Jumlah_Diproduksi")), "N4") '4
                            Dgv_Data.Rows(i).Cells(cell_Sisa).Value = Format(Val(.Rows(i).Item("Sisa")), "N4") '5
                            Dgv_Data.Rows(i).Cells(cell_SatuanBesar).Value = .Rows(i).Item("Satuan") '6
                            Dgv_Data.Rows(i).Cells(cell_SatuanKecil).Value = .Rows(i).Item("Satuan_Barang") '7
                            Dgv_Data.Rows(i).Cells(cell_Tipe).Value = .Rows(i).Item("tipe") '8
                            Dgv_Data.Rows(i).Cells(cell_warna).Value = "Hijau" '8
                            Dgv_Data.Rows(i).Cells(cell_JenisBahan).Value = .Rows(i).Item("Jenis_Bahan") '9

                            Dgv_Data.Rows(i).Cells(cell_StockProduksi).Value = Format(Val(.Rows(i).Item("Stock_Gudang_Produksi")), "N4") '10
                            Dgv_Data.Rows(i).Cells(cell_TotalTransfer).Value = Format(Val(.Rows(i).Item("Total_TF")), "N4") '11
                            Dgv_Data.Rows(i).Cells(cell_LokasiTujuan).Value = .Rows(i).Item("lokasi_gudang") '12

                            Dgv_Data.Rows(i).Cells(cell_JumlahInput).Style.BackColor = Color.LightGray
                        Next
                    End If
                End With
            End Using


            Get_Total_Request()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Get_DGV_Items(ByVal index As Integer)
        'TODO : GetDataLV
        Dgv_NoFak = Dgv_Data.Rows(index).Cells(cell_NoFak).Value
        Dgv_KdSo = Dgv_Data.Rows(index).Cells(cell_Kd_SO).Value
        Dgv_KdBarang = Dgv_Data.Rows(index).Cells(cell_Kd_Barang).Value
        Dgv_JmlhKebutuhan = Dgv_Data.Rows(index).Cells(cell_JumlahKebutuhan).Value
        Dgv_JmlhDiProduksi = Dgv_Data.Rows(index).Cells(cell_JumlahDiproduksi).Value
        Dgv_Sisa = Dgv_Data.Rows(index).Cells(cell_Sisa).Value
        Dgv_SatuanBesar = Dgv_Data.Rows(index).Cells(cell_SatuanBesar).Value
        Dgv_JmlhInput = Dgv_Data.Rows(index).Cells(cell_JumlahInput).Value
        Dgv_SatuanKecil = Dgv_Data.Rows(index).Cells(cell_SatuanKecil).Value
        Dgv_Tipe = Dgv_Data.Rows(index).Cells(cell_Tipe).Value
        Dgv_Warna = Dgv_Data.Rows(index).Cells(cell_warna).Value
        Dgv_JenisBahan = Dgv_Data.Rows(index).Cells(cell_JenisBahan).Value
    End Sub

    Private Sub Dgv_Data_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEndEdit

        If Dgv_Data.RowCount = 0 Then Exit Sub

        If Not IsNumeric(Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value) Then
            Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = ""
            Get_Total_Request()
            Exit Sub
        End If

        If Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value)) > Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(cell_Sisa).Value)) Then
            MessageBox.Show("Jumlah Tidak Boleh Lebih Dari Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = ""
            Get_Total_Request()
            Exit Sub
        End If

        '======================
        '=     SET FORMAT     =
        '======================

        If Dgv_Data.CurrentCell.ColumnIndex = cell_JumlahInput Then

            Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

            If cellKuantity.Contains(",") Then
                MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = Format(0, "N4")
                Get_Total_Request()
                Exit Sub
            End If

            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N4", Globalization.CultureInfo.GetCultureInfo("en-us"))

            Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = formattedValue
        End If

        Get_Total_Request()

    End Sub

    Private Sub Dgv_Data_CellEnter(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEnter
        '======================
        '=     SET FORMAT     =
        '======================

        If Dgv_Data.CurrentCell.ColumnIndex = cell_JumlahInput Then
            Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If

            Dim cleanedStr As String = HilangkanTanda(cellKuantity) ' Menghapus titik
            Dim nilai As Decimal = Decimal.Parse(cleanedStr)

            Dgv_Data.CurrentCell.Value = nilai
        End If
    End Sub

    Private Sub Dgv_Data_CellLeave(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellLeave
        '======================
        '=     SET FORMAT     =
        '======================

        If Dgv_Data.CurrentCell.ColumnIndex = cell_JumlahInput Then
            Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

            If cellKuantity = "" Then
                Exit Sub
            End If


            Dim nilai As Decimal = Decimal.Parse(cellKuantity)
            Dim formattedValue As String = nilai.ToString("N4", Globalization.CultureInfo.GetCultureInfo("en-us"))

            Dgv_Data.CurrentCell.Value = formattedValue

        End If
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        If Dgv_Data.RowCount = 0 Then Exit Sub
        If Txt_So.Text = "" Or Txt_KdBarang.Text = "" Then Exit Sub


        '============================
        '=     CEK DATAGRIDVIEW     =
        '============================
        Dim hasData As Boolean = False
        For i As Integer = 0 To Dgv_Data.RowCount - 1
            Get_DGV_Items(i)
            If String.IsNullOrWhiteSpace(Dgv_JmlhInput) Then
                Continue For
            Else
                hasData = True
                Exit For
            End If
        Next

        If Not hasData Then
            MessageBox.Show("Tidak Ada Jumlah yang Diinput", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim Id_Group_Jennis As String = ""

            get_no_faktur()

            '=============================
            '=     GET ID GROUP JENIS    =
            '=============================
            SQL = "select Id_Group_Jenis from barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & Txt_So.Text & "' and Kode_Barang='" & Txt_KdBarang.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Id_Group_Jennis = Dr("Id_Group_Jenis")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jenis Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End Using


            '==============================
            '=     INSERT TABEL INDUK     =
            '==============================

            SQL = "insert into Emi_Material_Requisition (Kode_Perusahaan, No_Faktur, No_Faktur_Order, Kode_Stock_Owner, Kode_Barang, Id_Group_Jenis, Tanggal, Jam, Flag_Process, UserId, Status, Keterangan) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial.Text & "', '" & Txt_NoFaktur.Text & "', "
            '''SQL = SQL & "'" & Txt_So.Text & "', '" & Txt_KdBarang.Text & "', '" & Txt_NamaBarang.Text & "', '" & Id_Group_Jennis & "', "
            SQL = SQL & "'" & Txt_So.Text & "', '" & Txt_KdBarang.Text & "', '" & Id_Group_Jennis & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', 'Y', '" & UserID & "', NULL, '" & TxtKeterangan.Text & "')"
            ExecuteTrans(SQL)

            For i As Integer = 0 To Dgv_Data.RowCount - 1

                Get_DGV_Items(i)

                '================================
                '=     CONVERT SATUAN KECIL     =
                '================================
                Dim nilai_kecil As Double = 0
                SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Dgv_KdBarang & "', '" & Dgv_SatuanBesar & "',"
                SQL = SQL & "'" & Dgv_SatuanKecil & "', '" & HilangkanTanda(Dgv_JmlhInput) & "' ) as hasil"
                Using Dr1 = OpenTrans(SQL)
                    If Dr1.Read Then
                        If General_Class.CekNULL(Dr1("hasil")) = "" Then
                            Dr1.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("data konversi satuan kirim tidak ada ")
                            Exit Sub
                        End If

                        nilai_kecil = Val(HilangkanTanda(Format(Dr1("hasil"), "N4")))
                    Else
                        Dr1.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("data konversi satuan kirim tidak ada ")
                        Exit Sub
                    End If
                End Using

                '==============================
                '=     INSERT TABEL DET     =
                '==============================

                SQL = "insert into Emi_Material_Requisition_det (Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Barang, Kebutuhan, Jumlah, Satuan, Jumlah_Barang, Satuan_Barang, Jenis_Material) values "
                SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial.Text & "', '" & Dgv_KdSo & "', '" & Dgv_KdBarang & "', '" & HilangkanTanda(Dgv_JmlhKebutuhan) & "',  "

                If Dgv_JmlhInput = "" Then
                    SQL = SQL & "'0', "
                Else
                    SQL = SQL & "'" & HilangkanTanda(Dgv_JmlhInput) & "', "
                End If

                SQL = SQL & "'" & Dgv_SatuanBesar & "', '" & nilai_kecil & "', '" & Dgv_SatuanKecil & "', '" & Dgv_Tipe & "')"
                ExecuteTrans(SQL)


                Dim x_ident_currentPackaging As Integer = 0
                SQL = "select IDENT_CURRENT('Emi_Material_Requisition_det') as urutan"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        x_ident_currentPackaging = Dr("urutan")
                    End If
                End Using


                SQL = "insert into Emi_Material_Requisition_det_convert(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Jumlah_Barang,Satuan_Barang,Warna,No_Urut_Det)"
                SQL = SQL & "values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial.Text & "', '" & Dgv_KdSo & "', '" & Dgv_KdBarang & "', "
                If Dgv_JmlhInput = "" Then
                    SQL = SQL & "'0', "
                Else
                    SQL = SQL & "'" & HilangkanTanda(Dgv_JmlhInput) & "', "
                End If
                SQL = SQL & "'" & Dgv_SatuanBesar & "', '" & nilai_kecil & "', '" & Dgv_SatuanKecil & "', '" & Dgv_Warna & "', '" & x_ident_currentPackaging & "')"
                ExecuteTrans(SQL)



                '======================================
                '=     CEK APAKAH BAHAN TERPENUHI     =
                '======================================
                If Dgv_JenisBahan = "BAHAN" Then

                    SQL = "select "
                    SQL = SQL & "(a.jumlah - ISNULL(( "
                    SQL = SQL & "select sum(x.Jumlah) "
                    SQL = SQL & "from Emi_Material_Requisition z, Emi_Material_Requisition_det x "
                    SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                    SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                    SQL = SQL & "and a.No_Faktur = z.No_Faktur_Order "
                    SQL = SQL & "and a.Kode_Stock_Owner = x.Kode_Stock_Owner and a.Kode_Barang = x.Kode_Barang "
                    SQL = SQL & "), 0)) as Sisa "
                    SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan a "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
                    SQL = SQL & "and a.Kode_Barang = '" & Dgv_KdBarang & "' "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then

                                Dim cekDataDouble As Integer = 0
                                For j As Integer = 0 To .Rows.Count - 1
                                    cekDataDouble = cekDataDouble + 1

                                    If cekDataDouble > 1 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    If Val(.Rows(j).Item("Sisa")) = 0 Then

                                        SQL = "update Emi_Split_Production_Order_Detail_Bahan set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' and Kode_Stock_Owner = '" & Txt_So.Text & "' and Kode_Barang = '" & Dgv_KdBarang & "'"
                                        ExecuteTrans(SQL)

                                    End If
                                Next
                            End If
                        End With
                    End Using

                ElseIf Dgv_JenisBahan = "PACKAGING" Then

                    SQL = "select "
                    SQL = SQL & "(a.jumlah - ISNULL(( "
                    SQL = SQL & "select sum(x.Jumlah) "
                    SQL = SQL & "from Emi_Material_Requisition z, Emi_Material_Requisition_det x "
                    SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
                    SQL = SQL & "and z.No_Faktur = x.No_Faktur "
                    SQL = SQL & "and a.No_Faktur = z.No_Faktur_Order "
                    SQL = SQL & "and a.Kode_Stock_Owner = x.Kode_Stock_Owner and a.Kode_Barang = x.Kode_Barang "
                    SQL = SQL & "), 0)) as Sisa "
                    SQL = SQL & "from Emi_Split_Production_Order_Detail_Packaging a "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Faktur = '" & Txt_NoFaktur.Text & "' "
                    SQL = SQL & "and a.Kode_Barang = '" & Dgv_KdBarang & "' "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then

                                Dim cekDataDouble As Integer = 0
                                For j As Integer = 0 To .Rows.Count - 1
                                    cekDataDouble = cekDataDouble + 1

                                    If cekDataDouble > 1 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    If Val(.Rows(j).Item("Sisa")) = 0 Then

                                        SQL = "update Emi_Split_Production_Order_Detail_Packaging set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' and Kode_Stock_Owner = '" & Txt_So.Text & "' and Kode_Barang = '" & Dgv_KdBarang & "'"
                                        ExecuteTrans(SQL)

                                    End If
                                Next
                            End If
                        End With
                    End Using

                End If


            Next


            Cmd.Transaction.Commit()
            MessageBox.Show("Berhasil Disimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            CloseTrans()
            CloseConn()
            kosong()

            Emi_Request_Material_Display.kosong()
            Me.Close()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Get_Total_Request()
        If Dgv_Data.RowCount = 0 Then Exit Sub

        Dim total As Double = 0
        For i As Integer = 0 To Dgv_Data.RowCount - 1

            Get_DGV_Items(i)

            If Dgv_JmlhInput = "" Then
                total = total + 0
            Else
                total = total + Dgv_JmlhInput
            End If

        Next

        TxtTotalRequest.Text = Format(total, "N4")
    End Sub

    Private Sub Btn_1Batch_Click(sender As Object, e As EventArgs) Handles Btn_1Batch.Click
        If Txt_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No Faktur Tidak ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim confirmNext As Boolean = False

        Try
            OpenConn()

            '==============================
            '=     GET NILAI PERBATCH     =
            '==============================
            SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "
            SQL = SQL & "ISNULL(( FLOOR( (c.Jumlah /  "
            SQL = SQL & "(select z.Hasil from Emi_Transaksi_Formulator z  "
            SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur)) "
            SQL = SQL & "* "
            SQL = SQL & "(ISNULL((select z.Qty_PerBatch from EMI_Master_Routing z "
            SQL = SQL & "where z.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "And z.Id_Routing = b.Id_Routing ), 0)))), 0) as Nilai_PerBatch, c.satuan "
            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, a.No_PO as No_PO, b.kode_barang, "
            SQL = SQL & "ISNULL((( (dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, 'KG', 'PCS', "
            SQL = SQL & "(ISNULL(( select z.Qty_PerBatch from EMI_Master_Routing z where z.Kode_Perusahaan = b.Kode_Perusahaan And z.Id_Routing = c.Id_Routing ), 0)))) "
            SQL = SQL & "/ "
            SQL = SQL & "(select z.jumlah_barang from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) "
            SQL = SQL & "* "
            SQL = SQL & "((select z.jumlah_bahan from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) ), 0) as Nilai_PerBatch, b.Satuan "
            SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, EMI_Order_Produksi c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.No_PO = c.No_Faktur "
            SQL = SQL & "and a.No_Transaksi = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim lebih As Boolean = False
                            '===============================
                            '=     CEK APAKAH BAHAN ADA    =
                            '===============================
                            For j As Integer = 0 To Dgv_Data.Rows.Count - 1
                                If Dgv_Data.Rows(j).Cells(cell_Kd_Barang).Value = .Rows(i).Item("kode_barang") AndAlso Dgv_Data.Rows(j).Cells(cell_SatuanBesar).Value = .Rows(i).Item("Satuan") Then
                                    If Val(HilangkanTanda(Dgv_Data.Rows(j).Cells(cell_Sisa).Value)) < Val(HilangkanTanda(.Rows(i).Item("Nilai_PerBatch"))) Then

                                        Dgv_Data.Rows(j).Cells(cell_JumlahInput).Value = Format(Val(HilangkanTanda(Dgv_Data.Rows(j).Cells(cell_Sisa).Value)), "N4")

                                    Else

                                        Dgv_Data.Rows(j).Cells(cell_JumlahInput).Value = Format(.Rows(i).Item("Nilai_PerBatch"), "N4")

                                    End If

                                End If
                            Next

                        Next
                    End If
                End With
            End Using

            Get_Total_Request()



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try





    End Sub



End Class