Public Class EMI_Controlling_Produksi

    Dim arrCari, arrCari_GR As New ArrayList
    Dim judulForm As String = "Controlling Produksi"

    Dim AsumsiIsiPerBatch As Integer = 10

    Public asal = ""
    Public NoSplit = ""

    'Dim Lv_NoFaktur, Lv_NoPO, Lv_Ket, Lv_TglProduksi, Lv_Jam, Lv_NmBarang, Lv_Jmlh, Lv_JnsProduksi, Lv_JmlhBatch, Lv_BatchDosing, Lv_KdBarang, Lv_Satuan, Lv_JmlhKg, Lv_SatuanKg, Lv_NoPO As String
    'Dim LvGR_NoFaktur, LvGR_TglProduksi, LvGR_Jam, LvGR_NmBarang, LvGR_Jmlh, LvGR_JnsProduksi, LvGR_JmlhBatch, LvGR_BatchDosing, LvGR_KdBarang, LvGR_Satuan, LvGR_JmlhKg, LvGR_SatuanKg, LvGR_NoPO As String
    Dim LvGRDET_NoBatch, LvGRDET_JmlhPcs, LvGRDET_JmlhPakai, LvGRDET_Selisih, LvGRDET_NoFaktur As String

    Dim Lv_NoFaktur As String
    Dim Lv_NoPO As String
    Dim Lv_Ket As String
    Dim Lv_Tgl As String
    Dim Lv_Jam As String
    Dim Lv_KdBarang As String
    Dim Lv_NmBarang As String
    Dim Lv_Qty As String
    Dim Lv_JmlBatch As String
    Dim Lv_BatchBerjalan As String
    Dim Lv_Routing As String
    Dim Lv_TotalDosing As String

    Dim Item_NoFaktur As Integer = 0
    Dim Item_NoPO As Integer = 1
    Dim Item_Ket As Integer = 2
    Dim Item_Tgl As Integer = 3
    Dim Item_Jam As Integer = 4
    Dim Item_KdBarang As Integer = 5
    Dim Item_NmBarang As Integer = 6
    Dim Item_Qty As Integer = 7
    Dim Item_JmlBatch As Integer = 8
    Dim Item_BatchBerjalan As Integer = 9
    Dim Item_Routing As Integer = 10
    Dim Item_TotalDosing As Integer = 11

    Dim LvGR_NoFaktur As String
    Dim LvGR_NoPO As String
    Dim LvGR_Ket As String
    Dim LvGR_Tgl As String
    Dim LvGR_Jam As String
    Dim LvGR_KdBarang As String
    Dim LvGR_NmBarang As String
    Dim LvGR_Qty As String
    Dim LvGR_JmlBatch As String
    Dim LvGR_BatchBerjalan As String
    Dim LvGR_Routing As String
    Dim LvGR_TotalDosing As String

    Dim ItemGR_NoFaktur As Integer = 0
    Dim ItemGR_NoPO As Integer = 1
    Dim ItemGR_Ket As Integer = 2
    Dim ItemGR_Tgl As Integer = 3
    Dim ItemGR_Jam As Integer = 4
    Dim ItemGR_KdBarang As Integer = 5
    Dim ItemGR_NmBarang As Integer = 6
    Dim ItemGR_Qty As Integer = 7
    Dim ItemGR_JmlBatch As Integer = 8
    Dim ItemGR_BatchBerjalan As Integer = 9
    Dim ItemGR_Routing As Integer = 10
    Dim ItemGR_TotalDosing As Integer = 11

    Dim PageSize As Integer = 20
    Dim CurrentPage As Integer = 1
    Dim CurrentPage_GR As Integer = 1
    Dim TotalRows As Integer
    Dim totalpage As Integer = 10
    Dim totalpage_GR As Integer = 10

    Dim Warna_Hover As Color = ColorHighlight
    Dim CurrentTab As Integer = -1

    Private Sub EMI_Controlling_Produksi_Activated(sender As Object, e As EventArgs) Handles Me.Activated, Lv_GI.SelectedIndexChanged
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Controlling_Produksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        If asal = "HASIL_PENGELUARAN_BAHAN_BAKU" Then
            Lbl_tab2.Visible = False
            Pnl_Tab2.Visible = False
            Lv_GI.ContextMenuStrip = Nothing
        End If

        ' LvGR_Det.Items.Clear()

        Me.Size = New Size(1200, 670)

        Lv_GI.Columns.Clear()
        Lv_GI.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("No PO", 0, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("Keterangan", 220, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("Tanggal Produksi", 120, HorizontalAlignment.Center)
        Lv_GI.Columns.Add("Jam Produksi", 100, HorizontalAlignment.Center)
        Lv_GI.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("Nama", 220, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("Qty Produksi", 130, HorizontalAlignment.Right)
        Lv_GI.Columns.Add("Total Batch", 100, HorizontalAlignment.Right)
        Lv_GI.Columns.Add("Batch Berjalan", 100, HorizontalAlignment.Right)
        Lv_GI.Columns.Add("Routing", 140, HorizontalAlignment.Left)
        Lv_GI.Columns.Add("Total Dosing", 120, HorizontalAlignment.Right)
        Lv_GI.View = View.Details

        Lv_GR.Columns.Clear()
        Lv_GR.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("No PO", 0, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Keterangan", 220, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Tanggal Produksi", 120, HorizontalAlignment.Center)
        Lv_GR.Columns.Add("Jam Produksi", 100, HorizontalAlignment.Center)
        Lv_GR.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Nama", 220, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Qty Produksi", 130, HorizontalAlignment.Right)
        Lv_GR.Columns.Add("Total Batch", 100, HorizontalAlignment.Right)
        Lv_GR.Columns.Add("Batch Berjalan", 100, HorizontalAlignment.Right)
        Lv_GR.Columns.Add("Routing", 140, HorizontalAlignment.Left)
        Lv_GR.Columns.Add("Total GR", 120, HorizontalAlignment.Right)
        Lv_GR.View = View.Details

        'LvGR_Det.Columns.Clear()
        'LvGR_Det.Columns.Add("No Batch", 70, HorizontalAlignment.Center)
        'LvGR_Det.Columns.Add("Jumlah Dosing (Pcs)", 150, HorizontalAlignment.Right)
        'LvGR_Det.Columns.Add("Jumlah Selesai (Pcs)", 150, HorizontalAlignment.Right)
        'LvGR_Det.Columns.Add("Selisih (Pcs)", 150, HorizontalAlignment.Right)
        ''HIDe
        'LvGR_Det.Columns.Add("No Faktur", 0, HorizontalAlignment.Right)
        'LvGR_Det.View = View.Details

        '==================
        '=     FILTER     =
        '==================
        Cmb_Filter.Items.Clear() : arrCari.Clear()
        Cmb_Filter.Items.Add("No Faktur") : arrCari.Add("a.No_Transaksi")
        Cmb_Filter.Items.Add("Nama Barang") : arrCari.Add("b.Nama")
        Cmb_Filter.Items.Add("Jenis Produksi") : arrCari.Add("d.Keterangan")

        Cmb_FilterGR.Items.Clear() : arrCari_GR.Clear()
        Cmb_FilterGR.Items.Add("No Faktur") : arrCari_GR.Add("a.No_Transaksi")
        'Cmb_FilterGR.Items.Add("No PO") : arrCari_GR.Add("a.no_Po")
        Cmb_FilterGR.Items.Add("Kode Barang") : arrCari_GR.Add("a.Kode_Barang")
        Cmb_FilterGR.Items.Add("Nama Barang") : arrCari_GR.Add("b.Nama")

        Try
            OpenConn()

            '=============================
            '=     GET TOTAL PAGE GI     =
            '=============================
            SQL = "select CEILING( CAST(COUNT(*) AS FLOAT) / " & PageSize & " ) AS Total_Pages "
            SQL = SQL & "From Emi_Split_Production_Order a, barang b, EMI_Order_Produksi c, emi_master_routing d Where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.Kode_Barang = b.Kode_Barang And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "And a.kode_perusahaan=c.Kode_Perusahaan And a.no_po=c.no_faktur "
            SQL = SQL & "And c.Kode_Perusahaan=d.kode_perusahaan And c.Id_Routing=d.Id_Routing "
            SQL = SQL & "And a.status Is null And c.Status Is null "
            SQL = SQL & "And a.Flag_Produksi = 'Y' and a.flag_hasil_Produksi_GI is null "
            SQL = SQL & "And a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    totalpage = Dr("Total_Pages")
                End If
            End Using

            '=============================
            '=     GET TOTAL PAGE GR     =
            '=============================
            SQL = "select CEILING( CAST(COUNT(*) AS FLOAT) / " & PageSize & " ) AS Total_Pages "
            SQL = SQL & "From Emi_Split_Production_Order a, barang b, EMI_Order_Produksi c, emi_master_routing d Where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.Kode_Barang = b.Kode_Barang And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "And a.kode_perusahaan=c.Kode_Perusahaan And a.no_po=c.no_faktur "
            SQL = SQL & "And c.Kode_Perusahaan=d.kode_perusahaan And c.Id_Routing=d.Id_Routing "
            SQL = SQL & "And a.status Is null And c.Status Is null "
            SQL = SQL & "And a.Flag_Produksi = 'Y' and a.flag_hasil_Produksi_GR is null "
            SQL = SQL & "And a.Kode_Perusahaan ='" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    totalpage_GR = Dr("Total_Pages")
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CurrentTab = 0
        Kosong()
    End Sub

    Public Sub Kosong()


        'LoadLvGI()
        'LoadGR()

        If CurrentTab = 0 Then
            Lbl_tab1.ForeColor = Warna_Hover
            Pnl_Tab1.BackColor = Warna_Hover

            Lbl_tab1_Click(Nothing, EventArgs.Empty)
        ElseIf CurrentTab = 1 Then
            Lbl_tab2.ForeColor = Warna_Hover
            Pnl_Tab2.BackColor = Warna_Hover

            Lbl_tab2_Click(Nothing, EventArgs.Empty)
        End If

    End Sub

    Private Sub LoadLvGI(Optional ByVal page As Integer = 1)
        Try
            OpenConn()
            'Dim JumlahBatch As Double = 0

            Dim Tot_Data As Double = 0
            SQL = "Select COUNT(*) AS TotalData "
            SQL = SQL & "From Emi_Split_Production_Order a, barang b, EMI_Order_Produksi c, emi_master_routing d Where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.Kode_Barang = b.Kode_Barang And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "And a.kode_perusahaan=c.Kode_Perusahaan And a.no_po=c.no_faktur "
            SQL = SQL & "And c.Kode_Perusahaan=d.kode_perusahaan And c.Id_Routing=d.Id_Routing "
            SQL = SQL & "And a.status Is null And c.Status Is null "
            SQL = SQL & "And a.Flag_Produksi = 'Y' and a.flag_hasil_Produksi_GI is null "
            SQL = SQL & "And a.Kode_Perusahaan ='" & KodePerusahaan & "' "

            If asal = "VALIDASI HPP" Then
                SQL = SQL & "and a.No_Transaksi = '" & NoSplit & "' "
            End If

            If Cmb_Filter.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "ThenWHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrCari.Item(Cmb_Filter.SelectedIndex) & "  like  '%" & Trim(Txt_FilterValue.Text) & "%' "
            End If
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Tot_Data = Dr("TotalData")
                End If
            End Using


            Dim totalPages As Integer = Math.Ceiling(Tot_Data / PageSize)
            Dim offset As Integer = (page - 1) * PageSize


            Lv_GI.Items.Clear()
            SQL = "Select a.No_Transaksi, a.no_Po, c.Keterangan, a.Tgl_Produksi, a.Jam_Produksi, a.Kode_Barang, "
            SQL = SQL & "b.Nama, a.Jumlah, a.Satuan, a.Jumlah_Batch, isnull(a.Qty_Batch, 0) As Qty_PerBatch, d.Id_Routing, d.Keterangan as routing, "
            SQL = SQL & "isnull((select sum(y.Nilai_Barang) from Emi_Production_Results x, Emi_Production_Results_Detail y where "
            SQL = SQL & "x.kode_perusahaan = y.kode_perusahaan And x.No_Transaksi = y.No_Transaksi And "
            SQL = SQL & "x.kode_perusahaan = a.kode_perusahaan And x.No_Production_Order = a.No_Transaksi ),0) As Total_Dosing "
            SQL = SQL & "From Emi_Split_Production_Order a, barang b, EMI_Order_Produksi c, emi_master_routing d Where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.Kode_Barang = b.Kode_Barang And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "And a.kode_perusahaan=c.Kode_Perusahaan And a.no_po=c.no_faktur "
            SQL = SQL & "And c.Kode_Perusahaan=d.kode_perusahaan And c.Id_Routing=d.Id_Routing "
            SQL = SQL & "And a.status Is null And c.Status Is null "
            SQL = SQL & "And a.Flag_Produksi = 'Y' and a.flag_hasil_Produksi_GI is null "
            SQL = SQL & "And a.Kode_Perusahaan ='" & KodePerusahaan & "' "

            If asal = "VALIDASI HPP" Then
                SQL = SQL & "and a.No_Transaksi = '" & NoSplit & "' "
            End If

            If Cmb_Filter.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "ThenWHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrCari.Item(Cmb_Filter.SelectedIndex) & "  like  '%" & Trim(Txt_FilterValue.Text) & "%' "
            End If

            SQL = SQL & "order by a.Tgl_Produksi, a.Jam_Produksi "
            SQL = SQL & "OFFSET " & offset & " ROWS "
            SQL = SQL & "FETCH NEXT " & PageSize & " ROWS ONLY "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Lv As ListViewItem
                            Lv = Lv_GI.Items.Add(.Rows(i).Item("No_Transaksi"))
                            Lv.SubItems.Add(.Rows(i).Item("No_PO"))
                            Lv.SubItems.Add(.Rows(i).Item("Keterangan"))
                            Lv.SubItems.Add(Format(.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy"))
                            Lv.SubItems.Add(.Rows(i).Item("Jam_Produksi"))
                            Lv.SubItems.Add(.Rows(i).Item("Kode_barang"))
                            Lv.SubItems.Add(.Rows(i).Item("Nama"))
                            Lv.SubItems.Add(Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah"))), "N2") + " " + .Rows(i).Item("satuan"))
                            Lv.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Batch")) = "", 0, General_Class.CekNULL(.Rows(i).Item("Jumlah_Batch"))))

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

                            If If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Batch")) = "", 0, .Rows(i).Item("Jumlah_Batch")) = JumlahDosing And Val(HilangkanTanda(.Rows(i).Item("Total_Dosing"))) <> 0 Then
                                Lv.BackColor = Color.LightGreen
                            End If

                            Lv.SubItems.Add(.Rows(i).Item("routing"))
                            Lv.SubItems.Add(Format(.Rows(i).Item("Total_Dosing"), "N4") + " Kg")

                        Next
                    End If
                End With
            End Using


            Txt_Pages_1.Text = $"{page} of {totalPages}"



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub GetDataLvGI(ByVal index As Integer)

        Lv_NoFaktur = Lv_GI.Items(index).SubItems(Item_NoFaktur).Text
        Lv_NoPO = Lv_GI.Items(index).SubItems(Item_NoPO).Text
        Lv_Ket = Lv_GI.Items(index).SubItems(Item_Ket).Text
        Lv_Tgl = Lv_GI.Items(index).SubItems(Item_Tgl).Text
        Lv_Jam = Lv_GI.Items(index).SubItems(Item_Jam).Text
        Lv_KdBarang = Lv_GI.Items(index).SubItems(Item_KdBarang).Text
        Lv_NmBarang = Lv_GI.Items(index).SubItems(Item_NmBarang).Text
        Lv_Qty = Lv_GI.Items(index).SubItems(Item_Qty).Text
        Lv_JmlBatch = Lv_GI.Items(index).SubItems(Item_JmlBatch).Text
        Lv_BatchBerjalan = Lv_GI.Items(index).SubItems(Item_BatchBerjalan).Text
        Lv_Routing = Lv_GI.Items(index).SubItems(Item_Routing).Text
        Lv_TotalDosing = Lv_GI.Items(index).SubItems(Item_TotalDosing).Text

    End Sub

    Private Sub GetDataLvGR(ByVal index As Integer)

        LvGR_NoFaktur = Lv_GR.Items(index).SubItems(ItemGR_NoFaktur).Text
        LvGR_NoPO = Lv_GR.Items(index).SubItems(ItemGR_NoPO).Text
        LvGR_Ket = Lv_GR.Items(index).SubItems(ItemGR_Ket).Text
        LvGR_Tgl = Lv_GR.Items(index).SubItems(ItemGR_Tgl).Text
        LvGR_Jam = Lv_GR.Items(index).SubItems(ItemGR_Jam).Text
        LvGR_KdBarang = Lv_GR.Items(index).SubItems(ItemGR_KdBarang).Text
        LvGR_NmBarang = Lv_GR.Items(index).SubItems(ItemGR_NmBarang).Text
        LvGR_Qty = Lv_GR.Items(index).SubItems(ItemGR_Qty).Text
        LvGR_JmlBatch = Lv_GR.Items(index).SubItems(ItemGR_JmlBatch).Text
        LvGR_BatchBerjalan = Lv_GR.Items(index).SubItems(ItemGR_BatchBerjalan).Text
        LvGR_Routing = Lv_GR.Items(index).SubItems(ItemGR_Routing).Text
        LvGR_TotalDosing = Lv_GR.Items(index).SubItems(ItemGR_TotalDosing).Text

    End Sub

    Private Sub LoadGR(Optional ByVal page As Integer = 1)

        Try
            OpenConn()

            Dim JumlahBatch As Double = 0



            Dim Tot_Data As Double = 0
            SQL = "Select COUNT(*) AS TotalData "
            SQL = SQL & "From Emi_Split_Production_Order a, barang b, EMI_Order_Produksi c, emi_master_routing d Where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.Kode_Barang = b.Kode_Barang And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "And a.kode_perusahaan=c.Kode_Perusahaan And a.no_po=c.no_faktur "
            SQL = SQL & "And c.Kode_Perusahaan=d.kode_perusahaan And c.Id_Routing=d.Id_Routing "
            SQL = SQL & "And a.status Is null And c.Status Is null "
            SQL = SQL & "And a.Flag_Produksi = 'Y' and a.flag_hasil_Produksi_GR is null  and (a.flag_hasil_Produksi_GI is null or a.flag_hasil_Produksi_GI='Y') "
            SQL = SQL & "And a.Kode_Perusahaan ='" & KodePerusahaan & "' "

            If asal = "VALIDASI HPP" Then
                SQL = SQL & "and a.No_Transaksi = '" & NoSplit & "' "
            End If

            'If Cmb_Filter.SelectedIndex <> -1 Then
            '    If Not Strings.Right(UCase(SQL), 6) = "ThenWHERE " Then SQL = SQL & "AND "
            '    SQL = SQL & arrCari.Item(Cmb_Filter.SelectedIndex) & "  like  '%" & Trim(Txt_FilterValue.Text) & "%' "
            'End If

            If Cmb_FilterGR.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "ThenWHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrCari_GR.Item(Cmb_FilterGR.SelectedIndex) & "  like  '%" & Trim(Txt_FilterGR.Text) & "%' "
            End If
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Tot_Data = Dr("TotalData")
                End If
            End Using


            Dim totalPages As Integer = Math.Ceiling(Tot_Data / PageSize)
            Dim offset As Integer = (page - 1) * PageSize


            Lv_GR.Items.Clear()
            SQL = "Select a.No_Transaksi, a.no_Po, c.Keterangan, a.Tgl_Produksi, a.Jam_Produksi, a.Kode_Barang, "
            SQL = SQL & "b.Nama, a.Jumlah, a.Satuan, isnull(a.Qty_Batch, 0) As Qty_PerBatch, d.Id_Routing, d.Keterangan as routing, "

            SQL = SQL & "isnull((select count(y.no_transaksi) from Emi_Production_Results x, Emi_Production_Results_HPP y where "
            SQL = SQL & "x.kode_perusahaan=y.kode_perusahaan and x.No_Transaksi=y.No_Transaksi and "
            SQL = SQL & "x.kode_perusahaan=a.kode_perusahaan and x.No_Production_Order=a.No_Transaksi and y.Tanggal is not null ),0) as Jumlah_Batch, "

            SQL = SQL & "isnull((select sum(y.Jumlah_Terpakai) from Emi_Production_Results x, Emi_Production_Results_HPP y where "
            SQL = SQL & "x.kode_perusahaan=y.kode_perusahaan and x.No_Transaksi=y.No_Transaksi and "
            SQL = SQL & "x.kode_perusahaan=a.kode_perusahaan and x.No_Production_Order=a.No_Transaksi and y.Tanggal is not null ),0) as Total_dosing "

            SQL = SQL & "From Emi_Split_Production_Order a, barang b, EMI_Order_Produksi c, emi_master_routing d Where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And a.Kode_Barang = b.Kode_Barang And a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "And a.kode_perusahaan=c.Kode_Perusahaan And a.no_po=c.no_faktur "
            SQL = SQL & "And c.Kode_Perusahaan=d.kode_perusahaan And c.Id_Routing=d.Id_Routing "
            SQL = SQL & "And a.status Is null And c.Status Is null "
            SQL = SQL & "And a.Flag_Produksi = 'Y' and a.flag_hasil_Produksi_GR is null and (a.flag_hasil_Produksi_GI is null or a.flag_hasil_Produksi_GI='Y') "
            SQL = SQL & "And a.Kode_Perusahaan ='" & KodePerusahaan & "' "

            If asal = "VALIDASI HPP" Then
                SQL = SQL & "and a.No_Transaksi = '" & NoSplit & "' "
            End If

            'If Cmb_Filter.SelectedIndex <> -1 Then
            '    If Not Strings.Right(UCase(SQL), 6) = "ThenWHERE " Then SQL = SQL & "AND "
            '    SQL = SQL & arrCari.Item(Cmb_Filter.SelectedIndex) & "  like  '%" & Trim(Txt_FilterValue.Text) & "%' "
            'End If

            If Cmb_FilterGR.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "ThenWHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrCari_GR.Item(Cmb_FilterGR.SelectedIndex) & "  like  '%" & Trim(Txt_FilterGR.Text) & "%' "
            End If

            SQL = SQL & "order by a.Tgl_Produksi, a.Jam_Produksi "
            SQL = SQL & "OFFSET " & offset & " ROWS "
            SQL = SQL & "FETCH NEXT " & PageSize & " ROWS ONLY "

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim Lv As ListViewItem
                            Lv = Lv_GR.Items.Add(.Rows(i).Item("No_Transaksi"))
                            Lv.SubItems.Add(.Rows(i).Item("No_PO"))
                            Lv.SubItems.Add(.Rows(i).Item("Keterangan"))
                            Lv.SubItems.Add(Format(.Rows(i).Item("Tgl_Produksi"), "dd MMM yyyy"))
                            Lv.SubItems.Add(.Rows(i).Item("Jam_Produksi"))
                            Lv.SubItems.Add(.Rows(i).Item("Kode_barang"))
                            Lv.SubItems.Add(.Rows(i).Item("Nama"))
                            Lv.SubItems.Add(Format(Val(HilangkanTanda(.Rows(i).Item("Jumlah"))), "N2") + " " + .Rows(i).Item("satuan"))
                            Lv.SubItems.Add(.Rows(i).Item("Jumlah_Batch"))

                            Dim JumlahDosing As Double = 0
                            SQL = "select a.No_Production_Order, a.No_Transaksi, "
                            SQL = SQL & "ISNULL(( select COUNT(*) from Emi_Production_Results_HPP z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Transaksi = a.No_Transaksi and z.tanggal is not null and jumlah_terpakai <>0 ), 0) as Jumlah_Dosing "
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

                            If If(General_Class.CekNULL(.Rows(i).Item("Jumlah_Batch")) = "", 0, .Rows(i).Item("Jumlah_Batch")) = JumlahDosing And Val(HilangkanTanda(.Rows(i).Item("Total_Dosing"))) <> 0 Then
                                If JumlahDosing = 0 Then
                                    Lv.BackColor = Color.LightGray
                                Else
                                    Lv.BackColor = Color.LightGreen
                                End If

                            End If

                            Lv.SubItems.Add(.Rows(i).Item("routing"))
                            Lv.SubItems.Add(Format(.Rows(i).Item("Total_Dosing"), "N4") + " Kg")

                        Next
                    End If
                End With
            End Using

            Txt_Pages_2.Text = $"{page} of {totalPages}"


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_GR_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_GR.SelectedIndexChanged
        If Lv_GR.Items.Count = 0 Or Lv_GR.SelectedItems.Count = 0 Then Exit Sub

        'Try
        '    OpenConn()

        '    GetDataGR(Lv_GR.FocusedItem.Index)

        '    LvGR_Det.Items.Clear()
        '    SQL = "select a.No_Transaksi, a.No_PO, b.No_Transaksi as No_Result, c.Proses as Batch_Number, c.Jumlah_Dosing_Pcs, "
        '    SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.kode_barang, c.satuan, 'PCS', c.Jumlah_Terpakai )) as Jumlah_Dosing_Terpakai "
        '    SQL = SQL & "from Emi_Split_Production_Order a , Emi_Production_Results b, Emi_Production_Results_HPP c "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
        '    SQL = SQL & "and a.No_Transaksi = No_Production_Order "
        '    SQL = SQL & "and b.No_Transaksi = c.No_Transaksi "
        '    SQL = SQL & "and a.status is null  "
        '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and c.Tanggal is not null "
        '    SQL = SQL & "and a.No_Transaksi = '" & LvGR_NoFaktur & "' "
        '    SQL = SQL & "order by c.Proses "
        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read()
        '            Dim Lv As ListViewItem
        '            Lv = LvGR_Det.Items.Add(Dr("Batch_Number"))
        '            Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Jumlah_Dosing_Pcs"))), "N0"))
        '            Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Jumlah_Dosing_Terpakai"))), "N0"))

        '            Dim Selisih As Double = Val(HilangkanTanda(Dr("Jumlah_Dosing_Pcs"))) - Val(HilangkanTanda(Dr("Jumlah_Dosing_Terpakai")))
        '            Lv.SubItems.Add(Format(Selisih, "N0"))
        '            Lv.SubItems.Add(Dr("No_Transaksi"))

        '            If Dr("Jumlah_Dosing_Pcs") = Dr("Jumlah_Dosing_Terpakai") Then
        '                Lv.BackColor = Color.LightGreen
        '            Else
        '                Lv.BackColor = Color.White
        '            End If
        '        Loop
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

    End Sub

    Private Sub Lv_GI_DoubleClick(sender As Object, e As EventArgs) Handles Lv_GI.DoubleClick
        If asal = "HASIL_PENGELUARAN_BAHAN_BAKU" Then
            Exit Sub
        End If

        If Lv_GI.Items.Count = 0 Or Lv_GI.SelectedItems.Count = 0 Then
            MessageBox.Show("Tidak Ada Data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        GetDataLvGI(Lv_GI.FocusedItem.Index)

        Try
            OpenConn()

            If CekButtonRole("Pengeluaran_Bahan_Baku") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            Dim jumlah_batch As Integer = 0
            Dim jumlah_batch_selesai As Integer = 0
            SQL = "select jumlah_batch from Emi_Split_Production_Order a "
            SQL = SQL & "where a.kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & Lv_NoFaktur & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    jumlah_batch = dr("jumlah_batch")
                End If
            End Using

            SQL = "Select count(b.Kode_Perusahaan) as Jumlah_selesai from Emi_Production_Results a, Emi_Production_Results_HPP b  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
            SQL = SQL & "And a.kode_Perusahaan='" & KodePerusahaan & "' and a.No_Production_Order='" & Lv_NoFaktur & "' and b.Tanggal is not null "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    jumlah_batch_selesai = dr("Jumlah_selesai")
                End If
            End Using

            If jumlah_batch_selesai >= jumlah_batch Then
                CloseConn()
                MessageBox.Show("Data Telah Selesai di Input . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        EMI_Hasil_Pengeluaran_Bahan_Baku.TextBox4.Text = Lv_NoFaktur
        EMI_Hasil_Pengeluaran_Bahan_Baku.DateTimePicker1.Value = CDate(Lv_Tgl)
        EMI_Hasil_Pengeluaran_Bahan_Baku.TextBox1.Text = Lv_Jam
        'Dim a As Double = Val(HilangkanTanda(LvJumlah)) - Val(HilangkanTanda(LvJumlah_Pro))

        EMI_Hasil_Pengeluaran_Bahan_Baku.TextBox6.Text = Lv_NmBarang
        EMI_Hasil_Pengeluaran_Bahan_Baku.fno_po = Lv_NoPO

        'EMI_Hasil_Pengeluaran_Bahan_Baku.asal = "CONTROLLING"
        EMI_Hasil_Pengeluaran_Bahan_Baku.ShowDialog()
    End Sub

    Private Sub Lv_GR_DoubleClick(sender As Object, e As EventArgs) Handles Lv_GR.DoubleClick

        If Lv_GR.Items.Count = 0 Or Lv_GR.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih Split Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        GetDataLvGR(Lv_GR.FocusedItem.Index)

        Try
            OpenConn()

            If CekButtonRole("Hasil_FG") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("anda tidak memiliki akses ! !")
                Exit Sub
            End If

            SQL = "select a.No_Transaksi, a.No_PO, b.No_Transaksi as No_Result, c.Proses as Batch_Number, c.Jumlah_Dosing_Pcs "
            'SQL = SQL & "(select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',a.kode_barang, c.satuan, 'PCS', c.Jumlah_Terpakai )) as Jumlah_Dosing_Terpakai "
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

    Private Sub ValidasiGIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiGIToolStripMenuItem.Click
        If Lv_GI.Items.Count = 0 Or Lv_GI.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Validasi_GI") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Penyelesaian GI", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Menyelesaikan Goods Issue ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            GetDataLvGI(Lv_GI.FocusedItem.Index)

            Dim jumlah_batch As Integer = 0
            Dim jumlah_batch_selesai As Integer = 0
            SQL = "select jumlah_batch from Emi_Split_Production_Order a "
            SQL = SQL & "where a.kode_Perusahaan='" & KodePerusahaan & "' and a.No_Transaksi='" & Lv_NoFaktur & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    jumlah_batch = dr("jumlah_batch")
                End If
            End Using

            SQL = "Select count(b.Kode_Perusahaan) as Jumlah_selesai from Emi_Production_Results a, Emi_Production_Results_HPP b  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
            SQL = SQL & "And a.kode_Perusahaan='" & KodePerusahaan & "' and a.No_Production_Order='" & Lv_NoFaktur & "' and b.Tanggal is not null "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    jumlah_batch_selesai = dr("Jumlah_selesai")
                End If
            End Using

            If jumlah_batch_selesai <> jumlah_batch Then
                Dim Kata As String = "Good Issue Ini Belum selesai. " & vbNewLine
                Kata += " - Jumlah Batch Rencana Produksi : " & jumlah_batch & vbNewLine
                Kata += " - Jumlah Batch Selesai Produksi : " & jumlah_batch_selesai & vbNewLine & vbNewLine
                Kata += " Lanjutkan . . ? "

                Dim tanya2 As String = MessageBox.Show(Kata, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If tanya2 = vbNo Then
                    CloseTrans()
                    CloseConn()
                    Exit Sub
                End If
            End If

            '=======================================
            '=     CEK APAKAH PO SUDAH SELESAI     =
            '=======================================
            SQL = "select Flag_Hasil_Produksi_GI from Emi_Split_Production_Order "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Lv_NoFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Hasil_Produksi_GI")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("GI Sudah Selesai, Tidak Bisa Diselesaikan Lagi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "update Emi_Split_Production_Order set Flag_Hasil_Produksi_GI = 'Y', UserID_Selesai_GI = '" & UserID & "', "
            SQL = SQL & "Tgl_Hasil_Produksi_GI = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Hasil_Produksi_GI = '" & Format(tgl_skg, "HH:mm:ss") & "'  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Lv_NoFaktur & "' "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("GI Berhasil Diselesaikan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Button1_Click(ValidasiGIToolStripMenuItem, e)

    End Sub

    Private Sub ValidasiGRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiGRToolStripMenuItem.Click
        If Lv_GR.Items.Count = 0 Or Lv_GR.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Validasi_GR") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Penyelesaian GR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Menyelesaikan Goods Received ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            GetDataLvGR(Lv_GR.FocusedItem.Index)

            Dim jumlah_batch As Double = 0
            Dim jumlah_batch_selesai As Double = 0
            Dim jumlah_Loss As Double = 0
            Dim Jumlah_Persentase As Double = 0
            Dim Jumlah_Presentase_Loss As Double = 0

            SQL = "Select round(isnull(sum(jumlah_dosing),0),4) as Jumlah_Dosing, round(isnull(sum(jumlah_Terpakai),0),4) as Jumlah_GR  from Emi_Production_Results a, Emi_Production_Results_HPP b  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Transaksi = b.No_Transaksi And a.status Is null "
            SQL = SQL & "And a.kode_Perusahaan='" & KodePerusahaan & "' and a.No_Production_Order='" & LvGR_NoFaktur & "' and b.Tanggal is not null "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    jumlah_batch = dr("Jumlah_Dosing")
                    jumlah_batch_selesai = dr("Jumlah_GR")
                    jumlah_Loss = dr("Jumlah_Dosing") - dr("Jumlah_GR")
                    Dim pembagi As Double = Val(HilangkanTanda(dr("Jumlah_Dosing")))
                    If pembagi > 0 Then
                        Jumlah_Persentase = (Val(HilangkanTanda(dr("Jumlah_GR"))) / pembagi) * 100
                        Jumlah_Presentase_Loss = (Val(HilangkanTanda(jumlah_Loss)) / pembagi) * 100
                    Else
                        Jumlah_Persentase = 0
                        Jumlah_Presentase_Loss = 0
                    End If
                End If
            End Using

            Dim Kata As String = "Berikut Detail GI-GR. " & vbNewLine
            Kata += " - Jumlah Goods Issue (Kg) : " & Format(jumlah_batch, "N4") & vbNewLine
            Kata += " - Jumlah Goods Received (Kg) : " & Format(jumlah_batch_selesai, "N4") & vbNewLine
            Kata += " - Jumlah Production Loss (Kg) : " & Format(jumlah_Loss, "N4") & vbNewLine
            Kata += " - Persentase Goods Received (%) : " & Format(Jumlah_Persentase, "N4") & vbNewLine
            Kata += " - Persentase Production Loss (%) : " & Format(Jumlah_Presentase_Loss, "N4") & vbNewLine & vbNewLine
            Kata += " Lanjutkan . . ? "

            Dim tanya2 As String = MessageBox.Show(Kata, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya2 = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            '=======================================
            '=     CEK APAKAH PO SUDAH SELESAI     =
            '=======================================
            SQL = "select Flag_Hasil_Produksi_GI, Flag_Hasil_Produksi_GR from Emi_Split_Production_Order "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & LvGR_NoFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Hasil_Produksi_GR")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("GR Sudah Selesai, Tidak Bisa Diselesaikan Lagi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                    If General_Class.CekNULL(Dr("Flag_Hasil_Produksi_GI")) <> "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Validasi GI Terlebih Dahulu . . ! ! ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "update Emi_Split_Production_Order set Flag_Hasil_Produksi_GR = 'Y', UserID_Selesai_GR = '" & UserID & "', "
            SQL = SQL & "Tgl_Hasil_Produksi_GR = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Hasil_Produksi_GR = '" & Format(tgl_skg, "HH:mm:ss") & "'  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & LvGR_NoFaktur & "' "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("GR Berhasil Diselesaikan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Button1_Click(ValidasiGRToolStripMenuItem, e)
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

    Private Sub Btn_CariGR_Click(sender As Object, e As EventArgs) Handles Btn_CariGR.Click

        If Cmb_FilterGR.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Jenis Filter Dahulu", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_FilterGR.Focus() : Exit Sub
        ElseIf Cmb_FilterGR.SelectedIndex <> -1 Then
            If Txt_FilterGR.Text.Trim.Length = 0 Then
                MessageBox.Show("Isi Value Filter Dahulu", judulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_FilterGR.Focus() : Exit Sub
            End If
        End If

        LoadGR()
    End Sub

    Private Sub DetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DetailToolStripMenuItem.Click
        If Lv_GI.Items.Count = 0 Or Lv_GI.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih Data Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        GetDataLvGI(Lv_GI.FocusedItem.Index)

        SD_Detail_Batch.noSplit = Lv_NoFaktur
        SD_Detail_Batch.asal = "GI"
        SD_Detail_Batch.Kosong()

        SD_Detail_Batch.ShowDialog()

    End Sub

    Private Sub DetailToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DetailToolStripMenuItem1.Click

        If Lv_GR.Items.Count = 0 Or Lv_GR.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih Data Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        GetDataLvGR(Lv_GR.FocusedItem.Index)

        SD_Detail_Batch.noSplit = LvGR_NoFaktur
        SD_Detail_Batch.asal = "GR"
        SD_Detail_Batch.Kosong()

        SD_Detail_Batch.ShowDialog()

    End Sub

    Private Sub BtnNext_GI_Click(sender As Object, e As EventArgs) Handles BtnNext_GI.Click

        If CurrentPage < totalpage Then
            CurrentPage += 1
            LoadLvGI(CurrentPage)

        End If

        If totalpage = CurrentPage Then
            BtnNext_GI.Enabled = False
        Else
            BtnNext_GI.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev_GI.Enabled = False
        Else
            BtnPrev_GI.Enabled = True
        End If
    End Sub

    Private Sub Btn_RefreshGR_Click(sender As Object, e As EventArgs) Handles Btn_RefreshGR.Click
        Kosong()
    End Sub

    Private Sub BtnPrev_GI_Click(sender As Object, e As EventArgs) Handles BtnPrev_GI.Click

        If CurrentPage > 1 Then
            CurrentPage -= 1
            LoadLvGI(CurrentPage)
        End If

        If totalpage = CurrentPage Then
            BtnNext_GI.Enabled = False
        Else
            BtnNext_GI.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev_GI.Enabled = False
        Else
            BtnPrev_GI.Enabled = True
        End If
    End Sub

    Private Sub BtnFirst_GI_Click(sender As Object, e As EventArgs) Handles BtnFirst_GI.Click

        CurrentPage = 1
        LoadLvGI(CurrentPage)

        If totalpage = CurrentPage Then
            BtnNext_GI.Enabled = False
        Else
            BtnNext_GI.Enabled = True
        End If

        If 1 = CurrentPage Then
            BtnPrev_GI.Enabled = False
        Else
            BtnPrev_GI.Enabled = True
        End If
    End Sub

    Private Sub BtnNext_GR_Click(sender As Object, e As EventArgs) Handles BtnNext_GR.Click

        If CurrentPage_GR < totalpage_GR Then
            CurrentPage_GR += 1
            LoadGR(CurrentPage_GR)

        End If

        If totalpage_GR = CurrentPage_GR Then
            BtnNext_GR.Enabled = False
        Else
            BtnNext_GR.Enabled = True
        End If

        If 1 = CurrentPage_GR Then
            BtnPrev_GR.Enabled = False
        Else
            BtnPrev_GR.Enabled = True
        End If
    End Sub

    Private Sub BtnPrev_GR_Click(sender As Object, e As EventArgs) Handles BtnPrev_GR.Click

        If CurrentPage_GR > 1 Then
            CurrentPage_GR -= 1
            LoadGR(CurrentPage_GR)
        End If

        If totalpage_GR = CurrentPage_GR Then
            BtnNext_GR.Enabled = False
        Else
            BtnNext_GR.Enabled = True
        End If

        If 1 = CurrentPage_GR Then
            BtnPrev_GR.Enabled = False
        Else
            BtnPrev_GR.Enabled = True
        End If
    End Sub

    Private Sub BtnFirst_GR_Click(sender As Object, e As EventArgs) Handles BtnFirst_GR.Click
        CurrentPage_GR = 1
        LoadGR(CurrentPage_GR)

        If totalpage_GR = CurrentPage_GR Then
            BtnNext_GR.Enabled = False
        Else
            BtnNext_GR.Enabled = True
        End If

        If 1 = CurrentPage_GR Then
            BtnPrev_GR.Enabled = False
        Else
            BtnPrev_GR.Enabled = True
        End If
    End Sub

    Private Sub CancelGIToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CancelGIToolStripMenuItem.Click
        If Lv_GI.Items.Count = 0 Or Lv_GI.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Cancel_GI") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Cancel GI", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Cancel Goods Issue ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            GetDataLvGI(Lv_GI.FocusedItem.Index)

            '===================================
            '=     CEK BATCH YANG SUDAH GI     =
            '===================================
            SQL = "select a.No_Production_Order, a.No_Transaksi, "
            SQL = SQL & "ISNULL(( select COUNT(*) from Emi_Production_Results_HPP z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.No_Transaksi = a.No_Transaksi and z.tanggal is not null ), 0) as Jumlah_Dosing "
            SQL = SQL & "from Emi_Production_Results a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_Production_Order = '" & Lv_NoFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read() Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Terjadi Kesalahan !!!, GI Tidak Bisa Dicancel Karena Terdapat Batch yang Sudah Berjalan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=======================================
            '=     CEK APAKAH PO SUDAH SELESAI     =
            '=======================================
            SQL = "select Flag_Hasil_Produksi_GI from Emi_Split_Production_Order "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Lv_NoFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Hasil_Produksi_GI")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("GI Sudah Selesai, Tidak Bisa Dilakukan Cancel", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "update Emi_Split_Production_Order set Flag_Hasil_Produksi_GI = 'T', UserID_Selesai_GI = '" & UserID & "', "
            SQL = SQL & "Tgl_Hasil_Produksi_GI = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Hasil_Produksi_GI = '" & Format(tgl_skg, "HH:mm:ss") & "',  "
            SQL = SQL & "Flag_Hasil_Produksi_GR = 'T', UserID_Selesai_GR = '" & UserID & "', "
            SQL = SQL & "Tgl_Hasil_Produksi_GR = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Hasil_Produksi_GR = '" & Format(tgl_skg, "HH:mm:ss") & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & Lv_NoFaktur & "' "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("GI Berhasil Dicancel", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Button1_Click(ValidasiGIToolStripMenuItem, e)

    End Sub

    '==================================================================================================================================================================
    '=     HANDLE UI
    '==================================================================================================================================================================


    Private Sub Lbl_tab1_MouseEnter(sender As Object, e As EventArgs) Handles Lbl_tab1.MouseEnter, Pnl_Tab1.MouseEnter
        Lbl_tab1.ForeColor = Warna_Hover
        Pnl_Tab1.BackColor = Warna_Hover
    End Sub

    Private Sub Lbl_tab1_MouseLeave(sender As Object, e As EventArgs) Handles Lbl_tab1.MouseLeave, Pnl_Tab1.MouseEnter
        If CurrentTab = 0 Then
            Lbl_tab1.ForeColor = Warna_Hover
            Pnl_Tab1.BackColor = Warna_Hover
        Else
            Lbl_tab1.ForeColor = SystemColors.ControlText
            Pnl_Tab1.BackColor = Color.LightGray
        End If
    End Sub

    Private Sub Lbl_tab2_MouseEnter(sender As Object, e As EventArgs) Handles Lbl_tab2.MouseEnter, Pnl_Tab2.MouseEnter
        Lbl_tab2.ForeColor = Warna_Hover
        Pnl_Tab2.BackColor = Warna_Hover
    End Sub

    Private Sub Lbl_tab2_MouseLeave(sender As Object, e As EventArgs) Handles Lbl_tab2.MouseLeave, Pnl_Tab2.MouseLeave
        If CurrentTab = 1 Then
            Lbl_tab2.ForeColor = Warna_Hover
            Pnl_Tab2.BackColor = Warna_Hover
        Else
            Lbl_tab2.ForeColor = SystemColors.ControlText
            Pnl_Tab2.BackColor = Color.LightGray
        End If

    End Sub

    Private Sub Lbl_tab1_Click(sender As Object, e As EventArgs) Handles Lbl_tab1.Click, Pnl_Tab1.Click
        CurrentTab = 0

        Panel_GI.Location = New Point(13, 90)
        Panel_GI.Visible = True

        Panel_GR.Visible = False
        Panel_GR.Location = New Point(1192, 90)

        Lbl_tab2.ForeColor = SystemColors.ControlText
        Pnl_Tab2.BackColor = Color.LightGray

        If asal = "VALIDASI HPP" Then
            Btn_Cari.Enabled = False
            Btn_Refresh.Enabled = False
            Cmb_Filter.Enabled = False
            Txt_FilterValue.Enabled = False
        End If

        Cmb_Filter.SelectedIndex = -1 : Cmb_Filter.Text = ""
        Txt_FilterValue.Text = ""

        BtnFirst_GI_Click(sender, e)

    End Sub

    Private Sub Lbl_tab2_Click(sender As Object, e As EventArgs) Handles Lbl_tab2.Click, Pnl_Tab2.Click
        CurrentTab = 1

        Panel_GR.Location = New Point(13, 90)
        Panel_GR.Visible = True

        Panel_GI.Visible = False
        Panel_GI.Location = New Point(1192, 90)

        Lbl_tab1.ForeColor = SystemColors.ControlText
        Pnl_Tab1.BackColor = Color.LightGray

        If asal = "VALIDASI HPP" Then
            Btn_Cari.Enabled = False
            Btn_Refresh.Enabled = False
            Cmb_Filter.Enabled = False
            Txt_FilterValue.Enabled = False
        End If

        Cmb_FilterGR.SelectedIndex = -1 : Cmb_FilterGR.Text = ""
        Txt_FilterGR.Text = ""

        BtnFirst_GR_Click(sender, e)
    End Sub

End Class