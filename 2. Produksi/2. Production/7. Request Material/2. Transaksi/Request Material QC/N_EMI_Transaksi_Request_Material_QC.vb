Public Class N_EMI_Transaksi_Request_Material_QC

    Dim judulForm As String = "Request Material"

    Dim arr_So As New ArrayList

    Public No_faktur As String = ""

    Dim Dgv_NoFak, Dgv_KdSo, Dgv_KdBarang, Dgv_JmlhKebutuhan, Dgv_JmlhDiProduksi, Dgv_Sisa, Dgv_SatuanBesar, Dgv_JmlhInput, Dgv_SatuanKecil, Dgv_Tipe, Dgv_Warna As String
    Dim Dgv_JenisBahan, Dgv_StockProduksi, Dgv_TotalTF, Dgv_Lokasi_Tujuan, Dgv_Jumlah_Tambah, Dgv_Total, Dgv_NilaiPerbatch, Dgv_Total_Perbatch, Dgv_Total_NamaBarang As String

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
    Dim cell_warna As Integer = 10
    Dim cell_JenisBahan As Integer = 11
    Dim cell_StockProduksi As Integer = 12
    Dim cell_TotalTransfer As Integer = 13
    Dim cell_LokasiTujuan As Integer = 14
    Dim cell_JumlahTambah As Integer = 15
    Dim cell_Total As Integer = 16
    Dim cell_NilaiPerbatch As Integer = 17
    Dim cell_TotalPerBatch As Integer = 18
    Dim cell_NamaBarang As Integer = 19

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
            'SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "
            'SQL = SQL & "ISNULL(( FLOOR( (c.Jumlah /  "
            'SQL = SQL & "(select z.Hasil from Emi_Transaksi_Formulator z  "
            'SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur)) "
            'SQL = SQL & "* "
            'SQL = SQL & "(ISNULL((select z.Qty_Batch * 2 from Emi_Split_Production_Order z "
            'SQL = SQL & "where z.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "And z.No_Transaksi = a.No_Transaksi ), 0)))), 0) as Nilai_PerBatch, c.satuan "
            'SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            'SQL = SQL & "and a.No_PO = b.No_Faktur "
            'SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            'SQL = SQL & "union all "

            'SQL = SQL & "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, a.No_PO as No_PO, b.kode_barang, "
            'SQL = SQL & "ISNULL((( (dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, 'KG', 'PCS', "
            'SQL = SQL & "(ISNULL(( select z.Qty_Batch*2 from Emi_Split_Production_Order z where z.Kode_Perusahaan = b.Kode_Perusahaan And z.No_Transaksi = a.No_Transaksi ), 0)))) "
            'SQL = SQL & "/ "
            'SQL = SQL & "(select z.jumlah_barang from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) "
            'SQL = SQL & "* "
            'SQL = SQL & "((select z.jumlah_bahan from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) ), 0) as Nilai_PerBatch, b.Satuan "
            'SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, EMI_Order_Produksi c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = c.kode_perusahaan "
            'SQL = SQL & "and a.No_PO = c.No_Faktur "
            'SQL = SQL & "and a.No_Transaksi = b.No_Faktur "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            '====================================================================================================================

            SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "

            SQL = SQL & "isnull(( ( (dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Batch, 'KG', a.Qty_Batch * 2)) / "
            SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',z.Kode_Barang, z.Satuan_Hasil, 'KG', z.Hasil)  "
            SQL = SQL & "from Emi_Transaksi_Formulator z where z.Kode_Perusahaan = b.Kode_Perusahaan and z.No_Faktur = b.Kode_Formula and z.Status is null) "
            SQL = SQL & ") * c.Jumlah ), 0) as Nilai_PerBatch, c.satuan "

            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, a.No_PO as No_PO, b.kode_barang, "
            SQL = SQL & "ISNULL((( (dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, 'KG', 'PCS', "
            SQL = SQL & "(ISNULL(( select z.Qty_Batch * 2 from Emi_Split_Production_Order z where z.Kode_Perusahaan = b.Kode_Perusahaan And z.No_Transaksi = a.No_Transaksi ), 0)))) "
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
            'SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "
            'SQL = SQL & "ISNULL(( FLOOR( (c.Jumlah /  "
            'SQL = SQL & "(select z.Hasil from Emi_Transaksi_Formulator z  "
            'SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur)) "
            'SQL = SQL & "* "
            'SQL = SQL & "(ISNULL((select z.Qty_Batch*3 from Emi_Split_Production_Order z "
            'SQL = SQL & "where z.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "And z.No_Transaksi = a.No_Transaksi ), 0)))), 0) as Nilai_PerBatch, c.satuan "
            'SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            'SQL = SQL & "and a.No_PO = b.No_Faktur "
            'SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            'SQL = SQL & "union all "

            'SQL = SQL & "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, a.No_PO as No_PO, b.kode_barang, "
            'SQL = SQL & "ISNULL((( (dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, 'KG', 'PCS', "
            'SQL = SQL & "(ISNULL(( select z.Qty_Batch*3 from Emi_Split_Production_Order z where z.Kode_Perusahaan = b.Kode_Perusahaan And z.No_Transaksi = a.No_Transaksi ), 0)))) "
            'SQL = SQL & "/ "
            'SQL = SQL & "(select z.jumlah_barang from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) "
            'SQL = SQL & "* "
            'SQL = SQL & "((select z.jumlah_bahan from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) ), 0) as Nilai_PerBatch, b.Satuan "
            'SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, EMI_Order_Produksi c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = c.kode_perusahaan "
            'SQL = SQL & "and a.No_PO = c.No_Faktur "
            'SQL = SQL & "and a.No_Transaksi = b.No_Faktur "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            '===================================================

            SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "

            SQL = SQL & "isnull(( ( (dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Batch, 'KG', a.Qty_Batch * 3)) / "
            SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',z.Kode_Barang, z.Satuan_Hasil, 'KG', z.Hasil)  "
            SQL = SQL & "from Emi_Transaksi_Formulator z where z.Kode_Perusahaan = b.Kode_Perusahaan and z.No_Faktur = b.Kode_Formula and z.Status is null) "
            SQL = SQL & ") * c.Jumlah ), 0) as Nilai_PerBatch, c.satuan "

            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            SQL = SQL & "union all "

            SQL = SQL & "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, a.No_PO as No_PO, b.kode_barang, "
            SQL = SQL & "ISNULL((( (dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, 'KG', 'PCS', "
            SQL = SQL & "(ISNULL(( select z.Qty_Batch * 3 from Emi_Split_Production_Order z where z.Kode_Perusahaan = b.Kode_Perusahaan And z.No_Transaksi = a.No_Transaksi ), 0)))) "
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

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Tambah_RM_QC") = "T" Then
                Btn_Tambah.Visible = False
            Else
                Btn_Tambah.Visible = True
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Dgv_Data.Columns(cell_NamaBarang).DisplayIndex = 3

        kosong()
    End Sub

    Private Sub get_no_faktur()
        Txt_NoFaktur_ReqMaterial.Text = fRequestMaterialQC & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("N_EMI_Transaksi_Material_Requisition_QC", "No_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(No_Faktur, 1, " & Len(fRequestMaterialQC) + 4 & ")", fRequestMaterialQC & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub kosong()

        Dgv_Data.Rows.Clear()
        TxtTotalRequest.Text = 0

        Dgv_Data.Columns(cell_StockProduksi).DisplayIndex = 3
        Dgv_Data.Columns(cell_TotalTransfer).DisplayIndex = 6
        Dgv_Data.Columns(cell_SatuanBesar).DisplayIndex = 8
        Dgv_Data.Columns(cell_LokasiTujuan).DisplayIndex = 9
        Dgv_Data.Columns(cell_NilaiPerbatch).DisplayIndex = 10
        Dgv_Data.Columns(cell_JumlahTambah).DisplayIndex = 11
        Dgv_Data.Columns(cell_TotalPerBatch).DisplayIndex = 12
        Btn_Tambah.Text = "&Tambahan"
        Btn_Tambah.Tag = "OPEN"

        Dgv_Data.Columns(cell_JumlahTambah).Visible = False
        Dgv_Data.Columns(cell_Total).Visible = False

        TxtKeterangan.Text = ""
        TxtTotalRequest.Text = "0"

        get_jam()

        Dgv_Data.Columns(cell_Kd_Barang).Width = 130
        Dgv_Data.Columns(cell_LokasiTujuan).Width = 200
        Dgv_Data.Columns(cell_SatuanBesar).Width = 100
        Dgv_Data.Columns(cell_NilaiPerbatch).Width = 200
        Dgv_Data.Columns(cell_JumlahInput).Width = 250

        Try
            OpenConn()

            get_no_faktur()

            Dgv_Data.Rows.Clear()

            SQL = "select Kode_Stock_Owner from Stock_Owner where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("Kode_Stock_Owner"))
                Loop
                Cmb_Lokasi.SelectedItem = "HEAD OFFICE"
            End Using


            'For i As Integer = 0 To Data_Dummy.Count - 1
            '    Dim Data As Dictionary(Of String, String) = Data_Dummy(i)

            '    Dgv_Data.Rows.Add(1)
            '    Dgv_Data.Rows(i).Cells(cell_NoFak).Value = Data("No_Faktur") '0
            '    Dgv_Data.Rows(i).Cells(cell_Kd_SO).Value = Data("Kode_Stock_Owner") '1
            '    Dgv_Data.Rows(i).Cells(cell_Kd_Barang).Value = Data("Kode_Barang") '2
            '    Dgv_Data.Rows(i).Cells(cell_JumlahKebutuhan).Value = Format(Val(Data("Jumlah")), "N4") '3
            '    Dgv_Data.Rows(i).Cells(cell_JumlahDiproduksi).Value = Format(Val(Data("Jumlah_Diproduksi")), "N4") '4
            '    Dgv_Data.Rows(i).Cells(cell_Sisa).Value = Format(Val(Data("Sisa")), "N4") '5
            '    Dgv_Data.Rows(i).Cells(cell_SatuanBesar).Value = Data("Satuan") '6
            '    Dgv_Data.Rows(i).Cells(cell_SatuanKecil).Value = Data("Satuan_Barang") '7
            '    Dgv_Data.Rows(i).Cells(cell_Tipe).Value = Data("tipe") '8
            '    Dgv_Data.Rows(i).Cells(cell_warna).Value = "Hijau" '8
            '    Dgv_Data.Rows(i).Cells(cell_JenisBahan).Value = Data("Jenis_Bahan") '9

            '    Dgv_Data.Rows(i).Cells(cell_StockProduksi).Value = Format(Val(Data("Stock_Gudang_Produksi")), "N4") '10
            '    Dgv_Data.Rows(i).Cells(cell_TotalTransfer).Value = Format(Val(Data("Total_TF")), "N4") '11
            '    Dgv_Data.Rows(i).Cells(cell_LokasiTujuan).Value = Data("lokasi_gudang") '12

            '    Dgv_Data.Rows(i).Cells(cell_JumlahInput).Style.BackColor = Color.LightGray

            'Next

            '======================================
            '=     GET JUMLAH BATCH PER SPLIT     =
            '======================================

            SQL = "select b.no_transaksi, d.Keterangan as Routing, a.keterangan, b.kode_barang, c.nama, b.Jumlah_Batch, b.Qty_Batch, b.Satuan_Batch "
            SQL = SQL & "from emi_order_produksi a, Emi_Split_Production_Order b, barang c, emi_master_routing d where "
            SQL = SQL & "a.Kode_Perusahaan=b.Kode_Perusahaan and a.no_faktur=b.no_po and "
            SQL = SQL & "b.Kode_Perusahaan=c.Kode_Perusahaan and b.Kode_Barang=c.Kode_Barang and b.Kode_Stock_Owner=c.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Perusahaan=d.Kode_Perusahaan and a.Id_Routing=d.Id_Routing "
            SQL = SQL & "and a.status Is null And b.status Is null And a.kode_perusahaan ='" & KodePerusahaan & "' "
            SQL = SQL & "and b.no_transaksi = '" & Txt_NoFaktur.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_JumlahBatch.Text = Dr("jumlah_batch")
                    Txt_NoSplit.Text = Dr("no_transaksi")
                    Txt_Routing.Text = Dr("Routing")
                    Txt_KdBarang.Text = Dr("kode_barang")
                    Txt_NmBarang.Text = Dr("nama")
                    Txt_NIlaiBatch.Text = Format(Dr("Qty_Batch"), "N0") + " " + Dr("Satuan_Batch")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==============================
            '=     CEK BATCH TERAKHIR     =
            '==============================
            SQL = "select top 1 b.batch  "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b  "
            SQL = SQL & "where a.Kode_Perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.no_Faktur = b.no_Faktur "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur_Order = '" & Txt_NoFaktur.Text & "' "
            SQL = SQL & "ORDER BY b.batch DESC; "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    'Txt_InputJmlhBatch.Text = Val(Dr("batch")) + 1
                    Txt_CurrentBatch.Text = Val(Dr("batch"))
                Else
                    'Txt_InputJmlhBatch.Text = 0
                    Txt_CurrentBatch.Text = 0
                End If
            End Using

            Txt_InputJmlhBatch.Text = 0

            'TODO : LoadData
            SQL = "Select a.No_Faktur, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.Nilai_Barang, a.Satuan_Barang, 'Bahan' as tipe, c.lokasi_gudang, "
            SQL = SQL & "(ISNULL( "
            SQL = SQL & "(select sum(z.jumlah_Per_Batch) "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC_Det z, N_EMI_Transaksi_Material_Requisition_QC x "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.Status is null ), 0)) as Jumlah_Diproduksi, "

            'SQL = SQL & "(a.Jumlah - ISNULL( "
            'SQL = SQL & "(select sum(z.jumlah) "
            'SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC_Det z, N_EMI_Transaksi_Material_Requisition_QC x "
            'SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan and a.Kode_Stock_Owner =z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            'SQL = SQL & "and z.Kode_Perusahaan = x.Kode_Perusahaan and z.No_Faktur = x.No_Faktur and a.No_Faktur = x.No_Faktur_Order and x.status is null ), 0)) as sisa, " 'SISA

            SQL = SQL & " (a.jumlah - isnull(( "
            SQL = SQL & "select sum(w.Jumlah) "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC z, N_EMI_Transaksi_Material_Requisition_QC_Detail x, N_EMI_Transaksi_Material_Requisition_QC_Det y, N_EMI_Transaksi_Material_Requisition_QC_Validasi w "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_Detail "
            SQL = SQL & "and y.No_Faktur = w.No_Faktur_RM and y.Urut_Oto = w.Urut_Det_RM "
            SQL = SQL & "and z.Status is null and y.Flag_Terpenuhi = 'Y' "
            SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur_Order = a.No_Faktur "
            SQL = SQL & "and w.Kode_Stock_Owner_Tujuan = a.Kode_Stock_Owner and w.Kode_Barang = a.Kode_Barang "
            SQL = SQL & "), 0)) as Sisa, "

            SQL = SQL & "'BAHAN' as Jenis_Bahan, "

            SQL = SQL & "ISNULL(( select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Barang, a.Satuan, sum(z.Jumlah)) "
            SQL = SQL & "from Barang_SN z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Stock_Owner = a.Kode_Stock_Owner and z.Kode_Barang = a.Kode_Barang "
            SQL = SQL & "), 0) as Stock_Gudang_Produksi, "

            'SQL = SQL & "ISNULL((select sum(w.jumlah) from tf_stock_parent x, Tf_Stock y, Tf_Stock_det z, Tf_Stock_det2 w, "
            'SQL = SQL & "N_EMI_Transaksi_Material_Requisition_QC_Convert m, N_EMI_Transaksi_Material_Requisition_QC n where "
            'SQL = SQL & "x.kode_Perusahaan = y.kode_perusahaan And x.no_faktur = y.no_faktur And x.status Is null And "
            'SQL = SQL & "y.kode_Perusahaan = z.kode_perusahaan And y.no_faktur = z.no_faktur And y.urut_oto = z.urut_tf And (z.selesai Is null Or z.selesai ='Y') and "
            'SQL = SQL & "z.kode_Perusahaan = w.kode_perusahaan And z.no_faktur = w.no_faktur And z.urut_oto = w.Urut_Det And "
            'SQL = SQL & "m.Kode_Perusahaan = y.Kode_Perusahaan And m.Urut_Oto = y.urut_material_requisition_convert and "
            'SQL = SQL & "y.Flag_Jenis_Request = 'PRODUKSI' and "
            'SQL = SQL & "m.kode_Perusahaan = n.kode_perusahaan And m.no_faktur = n.no_faktur and n.status is null and "
            'SQL = SQL & "a.Kode_Perusahaan = m.Kode_Perusahaan and a.Kode_Stock_Owner = m.Kode_Stock_Owner and a.Kode_Barang = m.Kode_Barang "
            'SQL = SQL & "and a.Kode_Perusahaan = n.Kode_Perusahaan and a.No_Faktur = n.No_Faktur_Order and n.Status is null "
            'SQL = SQL & "), '0') as Total_TF "

            SQL = SQL & "isnull((select sum(w.Jumlah) "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC z, N_EMI_Transaksi_Material_Requisition_QC_Detail x, N_EMI_Transaksi_Material_Requisition_QC_Det y, N_EMI_Transaksi_Material_Requisition_QC_Validasi w "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and x.Kode_Perusahaan = y.Kode_Perusahaan and y.Kode_Perusahaan = w.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "and x.No_Faktur = y.No_Faktur and x.Urut_Oto = y.Urut_Detail "
            SQL = SQL & "and y.No_Faktur = w.No_Faktur_RM and y.Urut_Oto = w.Urut_Det_RM "
            SQL = SQL & "and z.Status is null and y.Flag_Terpenuhi = 'Y' "
            SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur_Order = a.No_Faktur "
            SQL = SQL & "and w.Kode_Stock_Owner_Tujuan = a.Kode_Stock_Owner and w.Kode_Barang = a.Kode_Barang "
            SQL = SQL & "), 0) as Total_TF "

            SQL = SQL & "from Emi_Split_Production_Order_Detail_Bahan a, Barang b, EMI_Kategori_Gudang_PerLokasi c, Stock_Owner_Gudang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and b.Kode_Perusahaan = c.kode_perusahaan and b.ID_Kategori_Gudang = c.Id_Kategori_Gudang "
            SQL = SQL & "and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and c.Lokasi_Gudang = d.Kode_Stock_Owner "
            SQL = SQL & "and d.Flag_QC = 'Y' "
            SQL = SQL & "and a.kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & Txt_NoFaktur.Text & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim NilaiPerBatch As Double = 0
                            SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "
                            SQL = SQL & "isnull(( ( (dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Batch, 'KG', a.Qty_Batch)) / "
                            SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',z.Kode_Barang, z.Satuan_Hasil, 'KG', z.Hasil)  "
                            SQL = SQL & "from Emi_Transaksi_Formulator z where z.Kode_Perusahaan = b.Kode_Perusahaan and z.No_Faktur = b.Kode_Formula and z.Status is null) "
                            SQL = SQL & ") * c.Jumlah ), 0) as Nilai_PerBatch, "
                            SQL = SQL & "c.satuan "
                            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                            SQL = SQL & "and a.No_PO = b.No_Faktur "
                            SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "
                            SQL = SQL & "and c.kode_barang = '" & .Rows(i).Item("Kode_Barang") & "' "
                            SQL = SQL & "and c.satuan = '" & .Rows(i).Item("Satuan") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    NilaiPerBatch = Val(HilangkanTanda(Dr("Nilai_PerBatch")))
                                End If
                            End Using

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

                            Dgv_Data.Rows(i).Cells(cell_NilaiPerbatch).Value = Format(NilaiPerBatch, "N4") '17
                            Dgv_Data.Rows(i).Cells(cell_NamaBarang).Value = .Rows(i).Item("Nama") '18


                            'Dgv_Data.Rows(i).Cells(cell_JumlahInput).Style.BackColor = Color.LightGray
                            Dgv_Data.Rows(i).Cells(cell_JumlahTambah).Style.BackColor = Color.LightGray
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

        Txt_InputJmlhBatch.Focus()

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
        Dgv_StockProduksi = Dgv_Data.Rows(index).Cells(cell_StockProduksi).Value
        Dgv_TotalTF = Dgv_Data.Rows(index).Cells(cell_TotalTransfer).Value
        Dgv_Lokasi_Tujuan = Dgv_Data.Rows(index).Cells(cell_LokasiTujuan).Value
        Dgv_Jumlah_Tambah = Dgv_Data.Rows(index).Cells(cell_JumlahTambah).Value
        Dgv_Total = Dgv_Data.Rows(index).Cells(cell_Total).Value
        Dgv_NilaiPerbatch = Dgv_Data.Rows(index).Cells(cell_NilaiPerbatch).Value
        Dgv_Total_Perbatch = Dgv_Data.Rows(index).Cells(cell_TotalPerBatch).Value
        Dgv_Total_NamaBarang = Dgv_Data.Rows(index).Cells(cell_NamaBarang).Value

    End Sub

    Private Sub Btn_Tambah_Click(sender As Object, e As EventArgs) Handles Btn_Tambah.Click

        Try
            OpenConn()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Tambah_RM_QC") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Penambahan Jumlah", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If Btn_Tambah.Tag = "OPEN" Then

            Dgv_Data.Columns(cell_JumlahTambah).Visible = True
            Dgv_Data.Columns(cell_Total).Visible = True
            Dgv_Data.Columns(cell_TotalPerBatch).Visible = True

            Dgv_Data.Columns(cell_Kd_Barang).Width = 140
            Dgv_Data.Columns(cell_LokasiTujuan).Width = 150
            Dgv_Data.Columns(cell_SatuanBesar).Width = 80
            Dgv_Data.Columns(cell_NilaiPerbatch).Width = 150
            Dgv_Data.Columns(cell_JumlahInput).Width = 150
            Dgv_Data.Columns(cell_JumlahTambah).Width = 150
            Dgv_Data.Columns(cell_Total).Width = 150
            Dgv_Data.Columns(cell_TotalPerBatch).Width = 150

            For Each row As DataGridViewRow In Dgv_Data.Rows
                If Not row.IsNewRow Then
                    row.Cells(cell_JumlahTambah).Value = Nothing
                    row.Cells(cell_Total).Value = Nothing
                    row.Cells(cell_TotalPerBatch).Value = Nothing
                End If
            Next

            Btn_Tambah.Text = "&Tutup"
            Btn_Tambah.Tag = "CLOSE"
        Else

            Dgv_Data.Columns(cell_JumlahTambah).Visible = False
            Dgv_Data.Columns(cell_Total).Visible = False
            Dgv_Data.Columns(cell_TotalPerBatch).Visible = False

            Dgv_Data.Columns(cell_Kd_Barang).Width = 250
            Dgv_Data.Columns(cell_LokasiTujuan).Width = 250
            Dgv_Data.Columns(cell_SatuanBesar).Width = 120
            Dgv_Data.Columns(cell_NilaiPerbatch).Width = 250
            Dgv_Data.Columns(cell_JumlahInput).Width = 250

            For Each row As DataGridViewRow In Dgv_Data.Rows
                If Not row.IsNewRow Then
                    row.Cells(cell_JumlahTambah).Value = Nothing
                    row.Cells(cell_Total).Value = Nothing
                    row.Cells(cell_TotalPerBatch).Value = Nothing
                End If
            Next

            Btn_Tambah.Text = "&Tambah"
            Btn_Tambah.Tag = "OPEN"
        End If

        Get_Total_Request()

    End Sub

    Private Sub Dgv_Data_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles Dgv_Data.CellEndEdit

        If Dgv_Data.RowCount = 0 Then Exit Sub

        If Val(HilangkanTanda(Txt_InputJmlhBatch.Text)) <= 0 Or Txt_InputJmlhBatch.Text = "" Then
            MessageBox.Show("Harap Input Jumlah Batch Terlebih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dgv_Data.CurrentRow.Cells(cell_JumlahTambah).Value = ""
            Get_Total_Request()
            Txt_InputJmlhBatch.Focus()
            Exit Sub
        End If

        If Not IsNumeric(Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value) Then
            Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = ""
            Get_Total_Request()
            Exit Sub
        End If

        If Not IsNumeric(Dgv_Data.CurrentRow.Cells(cell_JumlahTambah).Value) Then
            Dgv_Data.CurrentRow.Cells(cell_JumlahTambah).Value = ""
            Dgv_Data.CurrentRow.Cells(cell_TotalPerBatch).Value = ""
            Dgv_Data.CurrentRow.Cells(cell_Total).Value = ""
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

        If Dgv_Data.CurrentCell.ColumnIndex = cell_JumlahTambah Then

            Dim cellKuantity As String = Dgv_Data.CurrentCell.Value

            If cellKuantity.Contains(",") Then
                MessageBox.Show("Kuantity Tidak Boleh Koma, Ganti dengan Titik", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = Format(0, "N4")
                Get_Total_Request()
                Exit Sub
            End If
        End If

        Dim jumlahInput As Double = If(Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = "" Or Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value = Nothing, 0, Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(cell_JumlahInput).Value)))
        Dim jumlahTambah As Double = If(Dgv_Data.CurrentRow.Cells(cell_JumlahTambah).Value = "" Or Dgv_Data.CurrentRow.Cells(cell_JumlahTambah).Value = Nothing, 0, Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(cell_JumlahTambah).Value)))
        Dim JumlahPerBatch As Double = If(Dgv_Data.CurrentRow.Cells(cell_NilaiPerbatch).Value = "" Or Dgv_Data.CurrentRow.Cells(cell_NilaiPerbatch).Value = Nothing, 0, Val(HilangkanTanda(Dgv_Data.CurrentRow.Cells(cell_NilaiPerbatch).Value)))
        Dim TotalTambahPerBatch As Double = JumlahPerBatch + jumlahTambah
        Dim TotalTambah As Double = jumlahTambah * Val(HilangkanTanda(Txt_InputJmlhBatch.Text))
        Dim Total As Double = jumlahInput + TotalTambah

        Dgv_Data.CurrentRow.Cells(cell_TotalPerBatch).Value = TotalTambahPerBatch
        Dgv_Data.CurrentRow.Cells(cell_Total).Value = Total

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
        If Txt_NoSplit.Text = "" Or Txt_KdBarang.Text = "" Then Exit Sub

        Dim NoFakturCetak As String = ""
        Dim NoFakturOrderCetak As String = ""

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

            NoFakturCetak = Txt_NoFaktur_ReqMaterial.Text
            NoFakturOrderCetak = Txt_NoFaktur.Text

            '=============================
            '=     GET ID GROUP JENIS    =
            '=============================

            '======================================
            '=     GET JUMLAH BATCH PER SPLIT     =
            '======================================
            Dim JumlahMaxBatchSplit As Integer = 0
            Dim lks_produksi As String = ""
            SQL = "select jumlah_batch, kode_stock_owner from Emi_Split_Production_Order where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_transaksi = '" & Txt_NoFaktur.Text & "' "
            SQL = SQL & "and Status is null and Flag_Selesai_Request_Material is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    JumlahMaxBatchSplit = Dr("jumlah_batch")
                    lks_produksi = Dr("kode_stock_owner")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select top(1) Id_Group_Jenis from barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & Txt_KdBarang.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Id_Group_Jennis = Dr("Id_Group_Jenis")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Jenis Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

            End Using

            '==============================
            '=     INSERT TABEL INDUK     =
            '==============================
            SQL = "insert into N_EMI_Transaksi_Material_Requisition_QC (Kode_Perusahaan, No_Faktur, No_Faktur_Order, Kode_Stock_Owner, Kode_Barang, nama, Id_Group_Jenis, Tanggal, Jam, UserId, Status, Keterangan, Lokasi) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial.Text & "', '" & Txt_NoFaktur.Text & "', "
            '''SQL = SQL & "'" & Txt_So.Text & "', '" & Txt_KdBarang.Text & "', '" & Txt_NamaBarang.Text & "', '" & Id_Group_Jennis & "', "
            SQL = SQL & "'" & lks_produksi & "', '" & Txt_KdBarang.Text & "', '" & Txt_NmBarang.Text & "', '" & Id_Group_Jennis & "', "
            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & UserID & "', NULL, '" & TxtKeterangan.Text & "', '" & Cmb_Lokasi.Text & "')"
            ExecuteTrans(SQL)



            '==============================
            '=     CEK BATCH TERAKHIR     =
            '==============================
            Dim BatchTerakhir As Integer = 0
            SQL = "select top 1 b.Batch  "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b  "
            SQL = SQL & "where a.Kode_Perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.no_Faktur = b.no_Faktur "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur_Order = '" & Txt_NoFaktur.Text & "' "
            SQL = SQL & "ORDER BY b.Batch DESC; "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    BatchTerakhir = Val(Dr("Batch")) + 1
                Else
                    BatchTerakhir = 1
                End If
            End Using

            For i As Integer = 0 To Val(Txt_InputJmlhBatch.Text) - 1

                '=====================================
                '=     GET NILAI PER 1 BATCH SUM     =
                '=====================================
                Dim Qty1BatchSum As Double = 0
                SQL = ";with cte as (select isnull((((dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Batch, 'KG', a.Qty_Batch)) / "
                SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',z.Kode_Barang, z.Satuan_Hasil, 'KG', z.Hasil)  "
                SQL = SQL & "from Emi_Transaksi_Formulator z where z.Kode_Perusahaan = b.Kode_Perusahaan and z.No_Faktur = b.Kode_Formula and z.Status is null) "
                SQL = SQL & ") * c.Jumlah ), 0) as Nilai_PerBatch "
                SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c, barang d, EMI_Kategori_Gudang_PerLokasi e , Stock_Owner_Gudang f "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.kode_perusahaan = d.kode_perusahaan "
                SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and e.Kode_Perusahaan = f.Kode_Perusahaan "
                SQL = SQL & "and a.No_PO = b.No_Faktur "
                SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
                SQL = SQL & "and c.kode_Stock_owner = d.kode_Stock_Owner and c.kode_barang = d.kode_barang "
                SQL = SQL & "and d.Id_Kategori_Gudang = e.ID_Kategori_Gudang "
                SQL = SQL & "and e.Lokasi_Gudang = f.Kode_Stock_Owner "
                SQL = SQL & "and f.Flag_QC = 'Y' "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "') select sum(Nilai_PerBatch) as Nilai_PerBatch from cte "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Qty1BatchSum = Val(HilangkanTanda(Dr("Nilai_PerBatch")))
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = "INSERT INTO N_EMI_Transaksi_Material_Requisition_QC_Detail "
                SQL = SQL & "(Kode_Perusahaan, No_Faktur, Batch, Qty_PerBatch) "
                SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial.Text & "', " & BatchTerakhir + i & ", '" & Val(HilangkanTanda(Qty1BatchSum)) & "'); "
                ExecuteTrans(SQL)

                Dim Urut_Detail As Integer = 0
                SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Material_Requisition_QC_Detail') as urutan"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Urut_Detail = Dr("urutan")
                    End If
                End Using

                Dim Tot_JmlhTambah As Double = 0
                For j As Integer = 0 To Dgv_Data.RowCount - 1

                    Get_DGV_Items(j)

                    Dim JumlahInput As Double = HilangkanTanda(If(Dgv_JmlhInput = "", 0, Dgv_JmlhInput))

                    Dim sisa As Double = JumlahInput

                    Dim JumlahInsert As Double = 0

                    '=====================================
                    '=     GET NILAI PER 1 BATCH SUM     =
                    '=====================================
                    SQL = "select isnull((((dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Batch, 'KG', a.Qty_Batch)) / "
                    SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',z.Kode_Barang, z.Satuan_Hasil, 'KG', z.Hasil)  "
                    SQL = SQL & "from Emi_Transaksi_Formulator z where z.Kode_Perusahaan = b.Kode_Perusahaan and z.No_Faktur = b.Kode_Formula and z.Status is null) "
                    SQL = SQL & ") * c.Jumlah ), 0) as Nilai_PerBatch "
                    SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c, barang d, EMI_Kategori_Gudang_PerLokasi e , Stock_Owner_Gudang f "
                    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.kode_perusahaan = d.kode_perusahaan "
                    SQL = SQL & "and d.Kode_Perusahaan = e.Kode_Perusahaan and e.Kode_Perusahaan = f.Kode_Perusahaan "
                    SQL = SQL & "and a.No_PO = b.No_Faktur "
                    SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
                    SQL = SQL & "and c.kode_Stock_owner = d.kode_Stock_Owner and c.kode_barang = d.kode_barang "
                    SQL = SQL & "and d.Id_Kategori_Gudang = e.ID_Kategori_Gudang "
                    SQL = SQL & "and e.Lokasi_Gudang = f.Kode_Stock_Owner "
                    SQL = SQL & "and f.Flag_QC = 'Y' "
                    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "
                    SQL = SQL & "and c.Kode_Barang = '" & Dgv_KdBarang & "' "
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            JumlahInsert = Val(HilangkanTanda(Dr("Nilai_PerBatch")))
                        End If
                    End Using

                    'If (JumlahInsert * i + 1) > JumlahInput Then
                    '    CloseTrans()
                    '    CloseConn()
                    '    MessageBox.Show("Terjadi Kesalahan Saat Input Batch", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                    'End If

                    '================================
                    '=     CONVERT SATUAN KECIL     =
                    '================================
                    Dim nilai_kecil As Double = 0
                    SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Dgv_KdBarang & "', '" & Dgv_SatuanBesar & "',"
                    SQL = SQL & "'" & Dgv_SatuanKecil & "', '" & HilangkanTanda(JumlahInsert) & "' ) as hasil"
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

                    SQL = "insert into N_EMI_Transaksi_Material_Requisition_QC_Det (Kode_Perusahaan, No_Faktur, Kode_Stock_Owner, Kode_Stock_Owner_Tujuan, Kode_Barang, Kebutuhan, Jumlah_Per_Batch, Satuan, Jumlah_Barang, Satuan_Barang, Jenis_Material, Urut_Detail, Jumlah_Tambah) values "
                    SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial.Text & "', '" & Dgv_KdSo & "', '" & Dgv_Lokasi_Tujuan & "', '" & Dgv_KdBarang & "', '" & HilangkanTanda(Dgv_JmlhKebutuhan) & "',  "
                    SQL = SQL & "'" & HilangkanTanda(JumlahInsert) & "', "
                    SQL = SQL & "'" & Dgv_SatuanBesar & "', '" & nilai_kecil & "', '" & Dgv_SatuanKecil & "', '" & Dgv_Tipe & "', " & Urut_Detail & ", "

                    If Dgv_Jumlah_Tambah <> "" Then
                        SQL = SQL & "'" & HilangkanTanda(Dgv_Jumlah_Tambah) & "' "
                    Else
                        SQL = SQL & "NULL "
                    End If

                    SQL = SQL & ") "

                    ExecuteTrans(SQL)

                    Dim x_ident_currentPackaging As Integer = 0
                    SQL = "select IDENT_CURRENT('N_EMI_Transaksi_Material_Requisition_QC_Det') as urutan"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            x_ident_currentPackaging = Dr("urutan")
                        End If
                    End Using

                    'SQL = "insert into N_EMI_Transaksi_Material_Requisition_QC_Convert(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Jumlah_Barang,Satuan_Barang,Warna,No_Urut_Det)"
                    'SQL = SQL & "values("
                    'SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur_ReqMaterial.Text & "', '" & Dgv_KdSo & "', '" & Dgv_KdBarang & "', "
                    'SQL = SQL & "'" & HilangkanTanda(JumlahInsert) & "', "
                    'SQL = SQL & "'" & Dgv_SatuanBesar & "', '" & nilai_kecil & "', '" & Dgv_SatuanKecil & "', '" & Dgv_Warna & "', '" & x_ident_currentPackaging & "')"
                    'ExecuteTrans(SQL)

                    '======================================
                    '=     CEK APAKAH BAHAN TERPENUHI     =
                    '======================================
                    If Dgv_JenisBahan = "BAHAN" Then

                        SQL = "select "
                        SQL = SQL & "(a.jumlah - ISNULL(( "
                        SQL = SQL & "select sum(x.Jumlah_Per_Batch) "
                        SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC z, N_EMI_Transaksi_Material_Requisition_QC_Det x "
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
                                    For k As Integer = 0 To .Rows.Count - 1
                                        cekDataDouble = cekDataDouble + 1

                                        If cekDataDouble > 1 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalahan Saat Cek Sisa", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        If Val(.Rows(k).Item("Sisa")) = 0 Then

                                            SQL = "update Emi_Split_Production_Order_Detail_Bahan set Flag_Terpenuhi =  'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
                                            SQL = SQL & "and No_Faktur = '" & Txt_NoFaktur.Text & "' and Kode_Stock_Owner = '" & lks_produksi & "' and Kode_Barang = '" & Dgv_KdBarang & "'"
                                            ExecuteTrans(SQL)

                                        End If
                                    Next
                                End If
                            End With
                        End Using

                    End If


                    Tot_JmlhTambah += If(Dgv_Jumlah_Tambah = "", 0, Val(HilangkanTanda(Dgv_Jumlah_Tambah)))
                Next


                SQL = "Update N_EMI_Transaksi_Material_Requisition_QC_Detail set Total_Tambah = '" & Tot_JmlhTambah & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Txt_NoFaktur_ReqMaterial.Text & "' "
                SQL = SQL & "and Batch = '" & BatchTerakhir + i & "' and urut_oto = '" & Urut_Detail & "' "
                ExecuteTrans(SQL)


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

        '========================
        '=     CETAK FAKTUR     =
        '========================
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim SF As String = ""
            Dim kertas As String = "Faktur"

            SQL = "select Kode_Perusahaan from N_EMI_View_Faktur_Material_Requisition_QC "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoFakturCetak & "' "
            SQL = SQL & "and Flag_Terpenuhi_Batch is null and Flag_Terpenuhi_Bahan is null "
            SF = "{N_EMI_View_Faktur_Material_Requisition_QC.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_View_Faktur_Material_Requisition_QC.No_Faktur} = '" & NoFakturCetak & "' "
            SF = SF & "and IsNull({N_EMI_View_Faktur_Material_Requisition_QC.Flag_Terpenuhi_Batch}) "
            SF = SF & "and IsNull({N_EMI_View_Faktur_Material_Requisition_QC.Flag_Terpenuhi_Bahan}) "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        CrDoc = New N_EMI_CR_Faktur_Request_Material_QC

                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    'CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = SF
                        '    CrDoc.SummaryInfo.ReportTitle = "Faktur Request Material Quality Control"
                        '    .Text = "Faktur Request Material Quality Control"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        '=====================================


                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = PrinterNameSPB
                        CrDoc.RecordSelectionFormula = SF
                        'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterNameSPB
                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertas Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

                        '=======================================
                        '=     CEK APAKAH KERTAS DITEMUKAN     =
                        '=======================================
                        If rawKind <> -1 Then
                            CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                        Else
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            Debug.Print("Ukuran kertas tidak ditemukan, menggunakan default.")
                        End If

                        CrDoc.PrintToPrinter(1, False, 1, 99)

                        MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Else
                        CloseConn()
                        MessageBox.Show("No Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

    End Sub

    Private Sub Get_Total_Request()
        If Dgv_Data.RowCount = 0 Then Exit Sub

        Dim total As Double = 0
        Dim jmlahInput As Double = 0
        Dim jmllhTambah As Double = 0
        For i As Integer = 0 To Dgv_Data.RowCount - 1

            Get_DGV_Items(i)

            If Dgv_JmlhInput = "" Then
                jmlahInput = jmlahInput + 0
            Else
                jmlahInput = jmlahInput + Dgv_JmlhInput
            End If

            If Dgv_Jumlah_Tambah = "" Or Dgv_Jumlah_Tambah = Nothing Then
                jmllhTambah = jmllhTambah + 0
            Else
                jmllhTambah = jmllhTambah + Dgv_Jumlah_Tambah
            End If

        Next

        total = jmlahInput + (jmllhTambah * Val(HilangkanTanda(Txt_InputJmlhBatch.Text)))

        TxtTotalRequest.Text = Format(total, "N4")
    End Sub

    Private Sub Btn_Set_Click(sender As Object, e As EventArgs) Handles Btn_Set.Click
        If Txt_NoFaktur.Text.Trim.Length = 0 Then
            MessageBox.Show("No Faktur Tidak ditemukan", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "

            SQL = SQL & "isnull(( ( (dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Batch, 'KG', a.Qty_Batch * " & Val(HilangkanTanda(Txt_InputJmlhBatch.Text)) & ")) / "
            SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',z.Kode_Barang, z.Satuan_Hasil, 'KG', z.Hasil)  "
            SQL = SQL & "from Emi_Transaksi_Formulator z where z.Kode_Perusahaan = b.Kode_Perusahaan and z.No_Faktur = b.Kode_Formula and z.Status is null) "
            SQL = SQL & ") * c.Jumlah ), 0) as Nilai_PerBatch, "

            SQL = SQL & "c.satuan "
            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
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


                        'Btn_Tambah.Tag = "CLOSE"
                        'Btn_Tambah_Click(sender, e)

                        For Each row As DataGridViewRow In Dgv_Data.Rows
                            If Not row.IsNewRow Then
                                row.Cells(cell_JumlahTambah).Value = Nothing
                                row.Cells(cell_Total).Value = Nothing
                                row.Cells(cell_TotalPerBatch).Value = Nothing
                            End If
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
            'SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "
            'SQL = SQL & "ISNULL(( FLOOR( (c.Jumlah /  "
            'SQL = SQL & "(select z.Hasil from Emi_Transaksi_Formulator z  "
            'SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur)) "
            'SQL = SQL & "* "
            'SQL = SQL & "(ISNULL((select z.Qty_Batch from Emi_Split_Production_Order z "
            'SQL = SQL & "where z.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "And z.No_Transaksi = a.No_Transaksi ), 0)))), 0) as Nilai_PerBatch, c.satuan "
            'SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            'SQL = SQL & "and a.No_PO = b.No_Faktur "
            'SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            'SQL = SQL & "union all "

            'SQL = SQL & "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, a.No_PO as No_PO, b.kode_barang, "
            'SQL = SQL & "ISNULL((( (dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, 'KG', 'PCS', "
            'SQL = SQL & "(ISNULL(( select z.Qty_Batch from Emi_Split_Production_Order z where z.Kode_Perusahaan = b.Kode_Perusahaan And z.No_Transaksi = a.No_Transaksi ), 0)))) "
            'SQL = SQL & "/ "
            'SQL = SQL & "(select z.jumlah_barang from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) "
            'SQL = SQL & "* "
            'SQL = SQL & "((select z.jumlah_bahan from Barang_Detail_Bahan_Penolong z where z.kode_barang= a.Kode_Barang and z.kode_Bahan = b.Kode_Barang)) ), 0) as Nilai_PerBatch, b.Satuan "
            'SQL = SQL & "from Emi_Split_Production_Order a, Emi_Split_Production_Order_Detail_Packaging b, EMI_Order_Produksi c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = c.kode_perusahaan "
            'SQL = SQL & "and a.No_PO = c.No_Faktur "
            'SQL = SQL & "and a.No_Transaksi = b.No_Faktur "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Transaksi = '" & Txt_NoFaktur.Text.Trim & "' "

            '========================================================================

            SQL = "select a.Kode_Perusahaan, a.No_Transaksi as No_Split, b.No_Faktur as No_PO, c.kode_barang, "

            SQL = SQL & "isnull(( ( (dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.Kode_Barang, a.Satuan_Batch, 'KG', a.Qty_Batch)) / "
            SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',z.Kode_Barang, z.Satuan_Hasil, 'KG', z.Hasil)  "
            SQL = SQL & "from Emi_Transaksi_Formulator z where z.Kode_Perusahaan = b.Kode_Perusahaan and z.No_Faktur = b.Kode_Formula and z.Status is null) "
            SQL = SQL & ") * c.Jumlah ), 0) as Nilai_PerBatch, "

            SQL = SQL & "c.satuan "
            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
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

    Private Sub TxtKeterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKeterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Txt_InputJmlhBatch_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_InputJmlhBatch.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If

        If e.KeyChar = Chr(13) Then
            Btn_Set.Focus()
        End If
    End Sub

    Private Sub Txt_InputJmlhBatch_Leave(sender As Object, e As EventArgs) Handles Txt_InputJmlhBatch.Leave
        If Txt_InputJmlhBatch.Text.Trim.Length = 0 Then Exit Sub

        If Val(Txt_InputJmlhBatch.Text) > (Val(HilangkanTanda(Txt_JumlahBatch.Text)) - Val(Txt_CurrentBatch.Text)) Then
            MessageBox.Show("Jumlah Batch Tidak Boleh Lebih Besar Dari Batch Split", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_InputJmlhBatch.Text = (Val(HilangkanTanda(Txt_JumlahBatch.Text)) - Val(Txt_CurrentBatch.Text))
            Exit Sub

        ElseIf Val(Txt_InputJmlhBatch.Text) < 0 Then
            MessageBox.Show("Jumlah Batch Tidak Boleh Lebih Kecil Batch Sekarang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_InputJmlhBatch.Text = Txt_CurrentBatch.Text
            Exit Sub
        End If

    End Sub

End Class
