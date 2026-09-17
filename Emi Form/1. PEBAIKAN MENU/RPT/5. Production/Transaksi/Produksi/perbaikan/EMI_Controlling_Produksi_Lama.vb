Public Class EMI_Controlling_Produksi_Lama

    Dim arrCari As New ArrayList
    Dim judulForm As String = "Controlling Produksi"

    Dim AsumsiIsiPerBatch As Integer = 10

    Public asal = ""
    Public NoSplit = ""


    Dim Lv_NoFaktur, Lv_TglProduksi, Lv_Jam, Lv_NmBarang, Lv_Jmlh, Lv_JnsProduksi, Lv_JmlhBatch, Lv_BatchDosing, Lv_KdBarang, Lv_Satuan, Lv_JmlhKg, Lv_SatuanKg, Lv_NoPO As String
    Dim LvGR_NoFaktur, LvGR_TglProduksi, LvGR_Jam, LvGR_NmBarang, LvGR_Jmlh, LvGR_JnsProduksi, LvGR_JmlhBatch, LvGR_BatchDosing, LvGR_KdBarang, LvGR_Satuan, LvGR_JmlhKg, LvGR_SatuanKg, LvGR_NoPO As String
    Dim LvGRDET_NoBatch, LvGRDET_JmlhPcs, LvGRDET_JmlhPakai, LvGRDET_Selisih, LvGRDET_NoFaktur As String

    Dim item_NoFaktur As Integer = 0
    Dim item_TglProduksi As Integer = 1
    Dim item_Jam As Integer = 2
    Dim item_NmBarang As Integer = 3
    Dim item_Jmlh As Integer = 4
    Dim item_JnsProduksi As Integer = 5
    Dim item_JmlhBatch As Integer = 6
    Dim item_BatchDosing As Integer = 7
    Dim item_KdBarang As Integer = 8
    Dim item_Satuan As Integer = 9
    Dim item_JmlhKG As Integer = 10
    Dim item_SatuanKg As Integer = 11
    Dim item_NoPO As Integer = 12

    Dim itemGR_NoFaktur As Integer = 0
    Dim itemGR_TglProduksi As Integer = 1
    Dim itemGR_Jam As Integer = 2

    Private Sub Lv_GI_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Dim itemGR_NmBarang As Integer = 3
    Dim itemGR_Jmlh As Integer = 4
    Dim itemGR_JnsProduksi As Integer = 5
    Dim itemGR_JmlhBatch As Integer = 6
    Dim itemGR_BatchDosing As Integer = 7
    Dim itemGR_KdBarang As Integer = 8
    Dim itemGR_Satuan As Integer = 9
    Dim itemGR_JmlhKG As Integer = 10
    Dim itemGR_SatuanKg As Integer = 11
    Dim itemGR_NoPO As Integer = 12

    Dim itemGRDET_NoBatch As Integer = 0
    Dim itemGRDET_JmlhPcs As Integer = 1
    Dim itemGRDET_JmlhTerpakai As Integer = 2
    Dim itemGRDET_Selisih As Integer = 3
    Dim itemGRDET_NoFaktur As Integer = 4


    Private Sub EMI_Controlling_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated, Lv_GI.SelectedIndexChanged
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Controlling_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()
    End Sub


    Public Sub Kosong()

        Cmb_Filter.Items.Clear() : Cmb_Filter.Text = ""
        Txt_FilterValue.Text = ""

        LvGR_Det.Items.Clear()

        Lv_GI.Columns.Clear()
        Lv_GI.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("Tanggal Produksi", 120, HorizontalAlignment.Center)
        Lv_GI.Columns.Add("Jam", 105, HorizontalAlignment.Center)
        Lv_GI.Columns.Add("Nama Barang", 240, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
        Lv_GI.Columns.Add("Jenis Produksi", 170, HorizontalAlignment.Center)
        Lv_GI.Columns.Add("Jumlah Batch", 120, HorizontalAlignment.Right)
        Lv_GI.Columns.Add("Batch On Production", 150, HorizontalAlignment.Right)
        'HIDE
        Lv_GI.Columns.Add("KdBarang", 0, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("Satuan", 0, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("JmlhKg", 0, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("SatuanKG", 0, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("noPO", 0, HorizontalAlignment.Left)
        Lv_GI.View = View.Details

        Lv_GR.Columns.Clear()
        Lv_GR.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Tanggal Produksi", 0, HorizontalAlignment.Center)
        Lv_GR.Columns.Add("Jam", 0, HorizontalAlignment.Center)
        Lv_GR.Columns.Add("Nama Barang", 220, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
        Lv_GR.Columns.Add("Jenis Produksi", 150, HorizontalAlignment.Center)
        Lv_GR.Columns.Add("Jumlah Batch", 100, HorizontalAlignment.Right)
        Lv_GR.Columns.Add("Batch Selesai", 100, HorizontalAlignment.Right)
        'HIDE
        Lv_GR.Columns.Add("KdBarang", 0, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Satuan", 0, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("JmlhKg", 0, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("SatuanKG", 0, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("noPO", 0, HorizontalAlignment.Left)
        Lv_GR.View = View.Details

        LvGR_Det.Columns.Clear()
        LvGR_Det.Columns.Add("No Batch", 70, HorizontalAlignment.Center)
        LvGR_Det.Columns.Add("Jumlah Dosing (Pcs)", 150, HorizontalAlignment.Right)
        LvGR_Det.Columns.Add("Jumlah Selesai (Pcs)", 150, HorizontalAlignment.Right)
        LvGR_Det.Columns.Add("Selisih (Pcs)", 150, HorizontalAlignment.Right)
        'HIDe
        LvGR_Det.Columns.Add("No Faktur", 0, HorizontalAlignment.Right)
        LvGR_Det.View = View.Details

        '==================
        '=     FILTER     =
        '==================
        Cmb_Filter.Items.Clear() : arrCari.Clear()
        Cmb_Filter.Items.Add("No Faktur") : arrCari.Add("a.No_Transaksi")
        Cmb_Filter.Items.Add("Nama Barang") : arrCari.Add("c.Nama")
        Cmb_Filter.Items.Add("Jenis Produksi") : arrCari.Add("d.Keterangan")


        If asal = "VALIDASI HPP" Then
            Btn_Cari.Enabled = False
            Btn_Refresh.Enabled = False
            Cmb_Filter.Enabled = False
            Txt_FilterValue.Enabled = False
        End If


        LoadLvGI()
        LoadGR()

    End Sub

    Private Sub LoadLvGI()
        Try
            OpenConn()
            Dim JumlahBatch As Double = 0


            Lv_GI.Items.Clear()
            SQL = "select a.No_Transaksi, a.No_PO, a.Tgl_Produksi, a.Jam_Produksi, a.kode_barang, c.Nama, a.Jumlah, a.satuan, "
            SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa', a.kode_barang, a.satuan, 'KG', a.Jumlah )) as Jumlah_KG, 'KG' as Satuan_KG, "
            SQL = SQL & "d.Keterangan, d.Id_Routing, isnull(a.Qty_Batch,0) as Qty_PerBatch "
            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b,Barang c,Emi_Master_routing d  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur and b.Selesai is null and b.flag_release='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Flag_Produksi = 'Y' and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Hasil_Produksi is null and b.Id_Routing = d.Id_Routing and a.status is null "

            If asal = "VALIDASI HPP" Then
                SQL = SQL & "and a.No_Transaksi = '" & NoSplit & "' "
            End If
            If Cmb_Filter.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "ThenWHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrCari.Item(Cmb_Filter.SelectedIndex) & "  like  '%" & Trim(Txt_FilterValue.Text) & "%' "
            End If
            SQL = SQL & "order by a.Tgl_Produksi,a.Jam_Produksi"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Lv As ListViewItem
                            Lv = Lv_GI.Items.Add(.Rows(i).Item("No_Transaksi"))
                            Lv.SubItems.Add(Format(.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy"))
                            Lv.SubItems.Add(.Rows(i).Item("Jam_Produksi"))
                            Lv.SubItems.Add(.Rows(i).Item("Nama"))
                            Lv.SubItems.Add(Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah"))), "N2"))
                            Lv.SubItems.Add(.Rows(i).Item("Keterangan"))

                            If .Rows(i).Item("Qty_PerBatch") = 0 Then
                                'MessageBox.Show("Jumlah Per Batch Belum di Set Untuk " & .Rows(i).Item("Keterangan") & " ! !")
                                'CloseConn()
                                'Exit Sub
                                Lv.SubItems.Add("0")
                            Else
                                JumlahBatch = Math.Ceiling(Val(HilangkanTanda(.Rows(i).Item("Jumlah_KG"))) / .Rows(i).Item("Qty_PerBatch"))
                                Lv.SubItems.Add(JumlahBatch)
                            End If




                            Dim JumlahDosing As Double = 0
                            SQL = "select a.No_Production_Order, a.No_Transaksi, "
                            SQL = SQL & "ISNULL(( select COUNT(*) from Emi_Production_Results_HPP z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Transaksi = a.No_Transaksi and z.tanggal is not null ), 0) as Jumlah_Dosing "
                            SQL = SQL & "from Emi_Production_Results a "
                            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.Status is null "
                            SQL = SQL & "and a.No_Production_Order = '" & .Rows(i).Item("No_Transaksi") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read() Then
                                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Jumlah_Dosing"))), "N0"))
                                    JumlahDosing = Format(Val(HilangkanTanda(Dr("Jumlah_Dosing"))), "N0")
                                    Lv.BackColor = Color.LightYellow
                                Else
                                    Lv.SubItems.Add(Format(0, "N0"))
                                    Lv.BackColor = Color.LightGray
                                End If
                            End Using

                            'Hide
                            Lv.SubItems.Add(.Rows(i).Item("kode_barang"))
                            Lv.SubItems.Add(.Rows(i).Item("satuan"))
                            Lv.SubItems.Add(.Rows(i).Item("Jumlah_KG"))
                            Lv.SubItems.Add(.Rows(i).Item("Satuan_KG"))
                            Lv.SubItems.Add(.Rows(i).Item("No_PO"))

                            If JumlahBatch = JumlahDosing Then
                                Lv.BackColor = Color.LightGreen
                            End If

                        Next
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

    Private Sub GetDataLvGI(ByVal index As Integer)

        Lv_NoFaktur = Lv_GI.Items(index).SubItems(item_NoFaktur).Text
        Lv_TglProduksi = Lv_GI.Items(index).SubItems(item_TglProduksi).Text
        Lv_Jam = Lv_GI.Items(index).SubItems(item_Jam).Text
        Lv_NmBarang = Lv_GI.Items(index).SubItems(item_NmBarang).Text
        Lv_Jmlh = Lv_GI.Items(index).SubItems(item_Jmlh).Text
        Lv_JnsProduksi = Lv_GI.Items(index).SubItems(item_JnsProduksi).Text
        Lv_JmlhBatch = Lv_GI.Items(index).SubItems(item_JmlhBatch).Text
        Lv_BatchDosing = Lv_GI.Items(index).SubItems(item_BatchDosing).Text
        Lv_KdBarang = Lv_GI.Items(index).SubItems(item_KdBarang).Text
        Lv_Satuan = Lv_GI.Items(index).SubItems(item_Satuan).Text
        Lv_JmlhKg = Lv_GI.Items(index).SubItems(item_JmlhKG).Text
        Lv_SatuanKg = Lv_GI.Items(index).SubItems(item_SatuanKg).Text
        Lv_NoPO = Lv_GI.Items(index).SubItems(item_NoPO).Text

    End Sub

    Private Sub LoadGR()

        Try
            OpenConn()

            Dim JumlahBatch As Double = 0

            Lv_GR.Items.Clear()
            SQL = "select a.No_Transaksi, a.No_PO, a.Tgl_Produksi, a.Jam_Produksi, a.kode_barang, c.Nama, a.Jumlah, a.satuan, "
            SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa', a.kode_barang, a.satuan, 'KG', a.Jumlah )) as Jumlah_KG, 'KG' as Satuan_KG, "
            SQL = SQL & "d.Keterangan, d.Id_Routing, isnull(a.Qty_Batch,0) as Qty_PerBatch, "

            SQL = SQL & "isnull((select top(1) 'Y' from emi_production_results x, emi_production_results_hpp y where "
            SQL = SQL & "x.kode_Perusahaan=y.kode_Perusahaan and x.No_Transaksi=y.No_Transaksi "
            SQL = SQL & "and x.status is null and y.Tanggal is not null and "
            SQL = SQL & "x.kode_Perusahaan=a.kode_perusahaan and x.No_Production_Order=a.no_transaksi),'T') as Data "

            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b,Barang c,Emi_Master_routing d  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur and b.Selesai is null and b.flag_release='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Flag_Produksi = 'Y' and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Hasil_Produksi is null and b.Id_Routing = d.Id_Routing and a.status is null "

            If asal = "VALIDASI HPP" Then
                SQL = SQL & "and a.No_Transaksi = '" & NoSplit & "' "
            End If

            If Cmb_Filter.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrCari.Item(Cmb_Filter.SelectedIndex) & "  like  '%" & Trim(Txt_FilterValue.Text) & "%' "
            End If
            SQL = SQL & "order by a.Tgl_Produksi,a.Jam_Produksi"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            If .Rows(i).Item("Data") = "Y" Then

                                Dim Lv As ListViewItem
                                Lv = Lv_GR.Items.Add(.Rows(i).Item("No_Transaksi"))
                                Lv.SubItems.Add(Format(.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy"))
                                Lv.SubItems.Add(.Rows(i).Item("Jam_Produksi"))
                                Lv.SubItems.Add(.Rows(i).Item("Nama"))
                                Lv.SubItems.Add(Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah"))), "N2"))
                                Lv.SubItems.Add(.Rows(i).Item("Keterangan"))

                                If .Rows(i).Item("Qty_PerBatch") = 0 Then
                                    'MessageBox.Show("Jumlah Per Batch Belum di Set Untuk " & .Rows(i).Item("Keterangan") & " ! !")
                                    'CloseConn()
                                    'Exit Sub
                                    Lv.SubItems.Add("0")
                                Else
                                    JumlahBatch = Math.Ceiling(Val(HilangkanTanda(.Rows(i).Item("Jumlah_KG"))) / .Rows(i).Item("Qty_PerBatch"))
                                    Lv.SubItems.Add(JumlahBatch)
                                End If






                                Dim JumlahDosing As Double = 0
                                SQL = "select a.No_Production_Order, a.No_Transaksi, "
                                SQL = SQL & "ISNULL(( select COUNT(*) from Emi_Production_Results_HPP z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Transaksi = a.No_Transaksi and z.Jumlah_Dosing - z.Jumlah_Terpakai = 0 ), 0) as Jumlah_Dosing "
                                SQL = SQL & "from Emi_Production_Results a "
                                SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                                SQL = SQL & "and a.Status is null "
                                SQL = SQL & "and a.No_Production_Order = '" & .Rows(i).Item("No_Transaksi") & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read() Then
                                        Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Jumlah_Dosing"))), "N0"))
                                        JumlahDosing = Format(Val(HilangkanTanda(Dr("Jumlah_Dosing"))), "N0")
                                    Else
                                        Lv.SubItems.Add(Format(0, "N0"))
                                    End If
                                End Using

                                'Hide
                                Lv.SubItems.Add(.Rows(i).Item("kode_barang"))
                                Lv.SubItems.Add(.Rows(i).Item("satuan"))
                                Lv.SubItems.Add(.Rows(i).Item("Jumlah_KG"))
                                Lv.SubItems.Add(.Rows(i).Item("Satuan_KG"))
                                Lv.SubItems.Add(.Rows(i).Item("No_PO"))

                            End If



                        Next
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

    Private Sub GetDataGR(ByVal index As Integer)

        LvGR_NoFaktur = Lv_GR.Items(index).SubItems(itemGR_NoFaktur).Text
        LvGR_TglProduksi = Lv_GR.Items(index).SubItems(itemGR_TglProduksi).Text
        LvGR_Jam = Lv_GR.Items(index).SubItems(itemGR_Jam).Text
        LvGR_NmBarang = Lv_GR.Items(index).SubItems(itemGR_NmBarang).Text
        LvGR_Jmlh = Lv_GR.Items(index).SubItems(itemGR_Jmlh).Text
        LvGR_JnsProduksi = Lv_GR.Items(index).SubItems(itemGR_JnsProduksi).Text
        LvGR_JmlhBatch = Lv_GR.Items(index).SubItems(itemGR_JmlhBatch).Text
        LvGR_BatchDosing = Lv_GR.Items(index).SubItems(itemGR_BatchDosing).Text
        LvGR_KdBarang = Lv_GR.Items(index).SubItems(itemGR_KdBarang).Text
        LvGR_Satuan = Lv_GR.Items(index).SubItems(itemGR_Satuan).Text
        LvGR_JmlhKg = Lv_GR.Items(index).SubItems(itemGR_JmlhKG).Text
        LvGR_SatuanKg = Lv_GR.Items(index).SubItems(itemGR_SatuanKg).Text
        LvGR_NoPO = Lv_GR.Items(index).SubItems(itemGR_NoPO).Text

    End Sub

    Private Sub Lv_GR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_GR.SelectedIndexChanged
        If Lv_GR.Items.Count = 0 Or Lv_GR.SelectedItems.Count = 0 Then Exit Sub

        Try
            OpenConn()

            GetDataGR(Lv_GR.FocusedItem.Index)

            LvGR_Det.Items.Clear()
            SQL = "select a.No_Transaksi, a.No_PO, b.No_Transaksi as No_Result, c.Proses as Batch_Number, c.Jumlah_Dosing_Pcs, "
            SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.kode_barang, c.satuan, 'PCS', c.Jumlah_Terpakai )) as Jumlah_Dosing_Terpakai "
            SQL = SQL & "from Emi_Split_Production_Order a , Emi_Production_Results b, Emi_Production_Results_HPP c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = No_Production_Order "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            SQL = SQL & "and a.status is null  "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.Tanggal is not null "
            SQL = SQL & "and a.No_Transaksi = '" & LvGR_NoFaktur & "' "
            SQL = SQL & "order by c.Proses "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Dim Lv As ListViewItem
                    Lv = LvGR_Det.Items.Add(Dr("Batch_Number"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Jumlah_Dosing_Pcs"))), "N0"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Jumlah_Dosing_Terpakai"))), "N0"))

                    Dim Selisih As Double = Val(HilangkanTanda(Dr("Jumlah_Dosing_Pcs"))) - Val(HilangkanTanda(Dr("Jumlah_Dosing_Terpakai")))
                    Lv.SubItems.Add(Format(Selisih, "N0"))
                    Lv.SubItems.Add(Dr("No_Transaksi"))

                    If Dr("Jumlah_Dosing_Pcs") = Dr("Jumlah_Dosing_Terpakai") Then
                        Lv.BackColor = Color.LightGreen
                    Else
                        Lv.BackColor = Color.White
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


    Private Sub Lv_GI_DoubleClick(sender As Object, e As EventArgs) Handles Lv_GI.DoubleClick
        If Lv_GI.Items.Count = 0 Or Lv_GI.SelectedItems.Count = 0 Then
            MessageBox.Show("Tidak Ada Data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            If CekButtonRole("Pengeluaran_Bahan_Baku") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        GetDataLvGI(Lv_GI.FocusedItem.Index)
        EMI_Hasil_Pengeluaran_Bahan_Baku.TextBox4.Text = Lv_NoFaktur
        EMI_Hasil_Pengeluaran_Bahan_Baku.DateTimePicker1.Value = CDate(Lv_TglProduksi)
        EMI_Hasil_Pengeluaran_Bahan_Baku.TextBox1.Text = Lv_Jam
        'Dim a As Double = Val(HilangkanTanda(LvJumlah)) - Val(HilangkanTanda(LvJumlah_Pro))

        EMI_Hasil_Pengeluaran_Bahan_Baku.TextBox6.Text = Lv_NmBarang
        EMI_Hasil_Pengeluaran_Bahan_Baku.fno_po = Lv_NoPO

        EMI_Hasil_Pengeluaran_Bahan_Baku.ShowDialog()
    End Sub

    Private Sub Lv_GR_DoubleClick(sender As Object, e As EventArgs) Handles Lv_GR.DoubleClick

        If Lv_GR.Items.Count = 0 Or Lv_GR.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih Split Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        GetDataGR(Lv_GR.FocusedItem.Index)

        Try
            OpenConn()

            If CekButtonRole("Hasil_FG") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            SQL = "select a.No_Transaksi, a.No_PO, b.No_Transaksi as No_Result, c.Proses as Batch_Number, c.Jumlah_Dosing_Pcs, "
            SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.kode_barang, c.satuan, 'PCS', c.Jumlah_Terpakai )) as Jumlah_Dosing_Terpakai "
            SQL = SQL & "from Emi_Split_Production_Order a , Emi_Production_Results b, Emi_Production_Results_HPP c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = No_Production_Order "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
            SQL = SQL & "and a.status is null  "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.Tanggal is not null "
            SQL = SQL & "and a.No_Transaksi = '" & LvGR_NoFaktur & "' "
            SQL = SQL & "order by c.Proses "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count = 0 Then
                        CloseConn()
                        MessageBox.Show("Data Tidak Ada", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using


            Emi_Production_Barcode.Txt_NoSplit.Text = LvGR_NoFaktur
            Emi_Production_Barcode.ShowDialog()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Jenis Filter Dahulu", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Filter.Focus() : Exit Sub
        ElseIf Cmb_Filter.SelectedIndex <> -1 Then
            If Txt_FilterValue.Text.Trim.Length = 0 Then
                MessageBox.Show("Isi Value Filter Dahulu", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_FilterValue.Focus() : Exit Sub
            End If
        End If

        LoadLvGI()
    End Sub

    Private Sub DetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailToolStripMenuItem.Click
        If Lv_GI.Items.Count = 0 Or Lv_GI.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih Data Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        GetDataLvGI(Lv_GI.FocusedItem.Index)

        SD_Detail_Batch.noSplit = Lv_NoFaktur
        SD_Detail_Batch.Kosong()

        SD_Detail_Batch.ShowDialog()




    End Sub
End Class
