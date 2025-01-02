
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class EMI_Display_Hasil_HPP
    Dim Jenis = "Display_Production_Order"
    Public asal As String
    Dim arrcari As New ArrayList
    Public filter_tambahan As String

    Dim LvNo_Faktur As String
    Dim LvTgl_Produksi As String
    Dim LvJam_Produksi As String
    Dim LvNm_Barang As String
    Dim LvJumlah As String
    Dim LvJumlah_Pro As String
    Dim LvGood_Stock As String
    Dim LvBad_Stock As String
    Dim LvLine As String
    Dim LvId_Routing As String
    Dim LvNo_PO As String
    Dim LvQtyHslPrd As String
    Dim LvQtyGoodStock As String
    Dim LvQtyBadStock As String
    Dim LvNilaiHslProdRAW As String
    Dim LvNilaiHslProdPack As String

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvNo_Faktur = ListView1.Items(NoIndex).Text
        LvTgl_Produksi = ListView1.Items(NoIndex).SubItems(1).Text
        LvJam_Produksi = ListView1.Items(NoIndex).SubItems(2).Text
        LvNm_Barang = ListView1.Items(NoIndex).SubItems(3).Text
        LvJumlah = ListView1.Items(NoIndex).SubItems(4).Text
        LvJumlah_Pro = ListView1.Items(NoIndex).SubItems(5).Text
        LvGood_Stock = ListView1.Items(NoIndex).SubItems(6).Text
        LvBad_Stock = ListView1.Items(NoIndex).SubItems(7).Text
        LvLine = ListView1.Items(NoIndex).SubItems(8).Text
        LvId_Routing = ListView1.Items(NoIndex).SubItems(9).Text
        LvNo_PO = ListView1.Items(NoIndex).SubItems(10).Text
        LvQtyHslPrd = ListView1.Items(NoIndex).SubItems(11).Text
        LvQtyGoodStock = ListView1.Items(NoIndex).SubItems(12).Text
        LvQtyBadStock = ListView1.Items(NoIndex).SubItems(13).Text
        LvNilaiHslProdRAW = ListView1.Items(NoIndex).SubItems(14).Text
        LvNilaiHslProdPack = ListView1.Items(NoIndex).SubItems(15).Text
    End Sub

    Private Sub EMI_Production_Order_Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong3()
    End Sub

    Private Sub isi_lv()
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.No_Transaksi,a.Tgl_Produksi,a.Jam_Produksi,c.Nama,a.Jumlah,d.Keterangan,d.Id_Routing,a.No_PO, "
            'SQL = SQL & "isnull((select sum(Qty_Hasil_Produksi) from Emi_Production_Results z where z.No_Production_Order = a.No_Transaksi "
            'SQL = SQL & "and a.Kode_Perusahaan = z.Kode_Perusahaan),0) as jumlah_produksi,"
            'SQL = SQL & "isnull((select sum(Qty_Good_Stock) from Emi_Production_Results z where z.No_Production_Order = a.No_Transaksi "
            'SQL = SQL & "and a.Kode_Perusahaan = z.Kode_Perusahaan),0) as good_stock,"
            'SQL = SQL & "isnull((select sum(Qty_Bad_Stock) from Emi_Production_Results z where z.No_Production_Order = a.No_Transaksi "
            'SQL = SQL & "and a.Kode_Perusahaan = z.Kode_Perusahaan),0) as bad_stock "
            SQL = SQL & "isnull((select sum(f.Qty_Hasil_Produksi) from Emi_Production_Results e, EMI_Production_Results_Detail_Barang f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi and e.status is null)"
            SQL = SQL & ",0) as Qty_Hasil_Produksi, "
            SQL = SQL & "isnull((select sum(f.Qty_Good_Stock) from Emi_Production_Results e, EMI_Production_Results_Detail_Barang f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi and e.status is null)"
            SQL = SQL & ",0) as Qty_Good_Stock, "
            SQL = SQL & "isnull((select sum(f.Qty_Bad_Stock) from Emi_Production_Results e, EMI_Production_Results_Detail_Barang f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi and e.status is null)"
            SQL = SQL & ",0) as Qty_Bad_Stock, "
            SQL = SQL & "isnull((select sum(f.Nilai_Produksi) from Emi_Production_Results e, Emi_Production_Results_Detail f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi and e.status is null) "
            SQL = SQL & ",0) as Hasil_Prod_RAW, "
            SQL = SQL & "isnull((select sum(f.Nilai_Produksi) from Emi_Production_Results e, Emi_Production_Results_Packaging_Detail f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi and e.status is null) "
            SQL = SQL & ",0) as Hasil_Prod_PACK "

            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b,Barang c,Emi_Master_routing d,Emi_Production_Results e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur and b.Selesai is null and b.flag_release ='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Flag_Produksi = 'Y' and b.status is null  "
            SQL = SQL & "and a.Kode_Perusahaan = e.Kode_Perusahaan and e.No_Production_Order = a.No_Transaksi and e.Flag_Hpp is null and e.status is null "
            SQL = SQL & "and a.Flag_Hasil_Produksi ='Y' and b.Id_Routing = d.Id_Routing order by a.Tgl_Produksi,a.Jam_Produksi "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(dr("No_Transaksi"))
                    lvw.SubItems.Add(Format(dr("Tgl_Produksi"), "dd-MMM-yyyy"))
                    lvw.SubItems.Add(dr("Jam_Produksi"))
                    lvw.SubItems.Add(dr("Nama"))
                    lvw.SubItems.Add(Format(dr("Jumlah"), "N2"))
                    lvw.SubItems.Add(Format(0, "N2"))
                    lvw.SubItems.Add(Format(0, "N2"))
                    lvw.SubItems.Add(Format(0, "N2"))
                    lvw.SubItems.Add(dr("Keterangan"))
                    lvw.SubItems.Add(dr("Id_Routing"))
                    lvw.SubItems.Add(dr("No_PO"))

                    lvw.SubItems.Add(dr("Qty_Hasil_Produksi"))
                    lvw.SubItems.Add(dr("Qty_Good_Stock"))
                    lvw.SubItems.Add(dr("Qty_Bad_Stock"))
                    lvw.SubItems.Add(dr("Hasil_Prod_RAW"))
                    lvw.SubItems.Add(dr("Hasil_Prod_PACK"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong3()
        'Label1.Text = "Display Production Order"
        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            '''Label1.Text = Base_Language.Lang_Display_Production_Order_Judul
            Label1.Text = "Display Hasil Produksi"
            Label3.Text = Base_Language.Lang_Global_Jenis
            Btn_Cari.Text = Base_Language.Lang_Global_Cari

            ComboBox3.Items.Clear() : arrcari.Clear()
            ComboBox3.Items.Add(Base_Language.Lang_Global_NoFaktur) : arrcari.Add("a.No_Faktur")
            ComboBox3.Items.Add("Line") : arrcari.Add("d.Id_Routing")
            ComboBox3.Items.Add("Nama Barang") : arrcari.Add("c.Nama")
            ComboBox3.SelectedIndex = -1
            TextBox3.Text = ""

            ListView1.Columns.Clear()
            ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 125, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal_Produksi, 125, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Jam, 100, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 350, HorizontalAlignment.Left)
            ListView1.Columns.Add("Jumlah Split PO", 200, HorizontalAlignment.Right)
            ListView1.Columns.Add(Base_Language.Lang_GLOBAL_Jumlah_Produksi, 0, HorizontalAlignment.Right)
            ListView1.Columns.Add("-Qty Good Stock", 0, HorizontalAlignment.Right)
            ListView1.Columns.Add("-Qty Bad Stock", 0, HorizontalAlignment.Right)
            ListView1.Columns.Add("Routing", 220, HorizontalAlignment.Center)
            ListView1.Columns.Add("id_routing", 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("no po", 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("Qty Hasil Produksi", 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("Qty Good Stock", 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("Qty Bad Stock", 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("Nilai Hasil Produksi RAW", 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("Nilai Hasil Produksi PACK", 0, HorizontalAlignment.Center)
            ListView1.View = System.Windows.Forms.View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        isi_lv()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Try
            OpenConn()

            ListView1.Items.Clear()
            ''SQL = "select a.No_Transaksi,a.Tgl_Produksi,a.Jam_Produksi,c.Nama,a.Jumlah,d.Keterangan,d.Id_Routing,a.No_PO,"
            ''SQL = SQL & "isnull((select sum(Qty_Hasil_Produksi) from Emi_Production_Results z where z.No_Production_Order = a.No_Transaksi "
            ''SQL = SQL & "and a.Kode_Perusahaan = z.Kode_Perusahaan),0) as hasil_produksi,"
            ''SQL = SQL & "isnull((select sum(Qty_Good_Stock) from Emi_Production_Results z where z.No_Production_Order = a.No_Transaksi "
            ''SQL = SQL & "and a.Kode_Perusahaan = z.Kode_Perusahaan),0) as good_stock,"
            ''SQL = SQL & "isnull((select sum(Qty_Bad_Stock) from Emi_Production_Results z where z.No_Production_Order = a.No_Transaksi "
            ''SQL = SQL & "and a.Kode_Perusahaan = z.Kode_Perusahaan),0) as bad_stock "
            ''SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b,Barang c,Emi_Master_routing d "
            ''SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur and b.Selesai is null and b.flag_release='Y' "
            ''SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            ''SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Flag_Produksi = 'Y' and a.Flag_Selesai_Produksi = 'Y' "
            ''SQL = SQL & "and a.Flag_Hasil_Produksi is null and b.Id_Routing = d.Id_Routing "

            SQL = "select a.No_Transaksi,a.Tgl_Produksi,a.Jam_Produksi,c.Nama,a.Jumlah,d.Keterangan,d.Id_Routing,a.No_PO, "
            SQL = SQL & "isnull((select sum(Qty_Hasil_Produksi) from Emi_Production_Results z where z.No_Production_Order = a.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = z.Kode_Perusahaan),0) as jumlah_produksi,"
            SQL = SQL & "isnull((select sum(Qty_Good_Stock) from Emi_Production_Results z where z.No_Production_Order = a.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = z.Kode_Perusahaan),0) as good_stock,"
            SQL = SQL & "isnull((select sum(Qty_Bad_Stock) from Emi_Production_Results z where z.No_Production_Order = a.No_Transaksi "
            SQL = SQL & "and a.Kode_Perusahaan = z.Kode_Perusahaan),0) as bad_stock, "
            SQL = SQL & "isnull((select sum(f.Qty_Hasil_Produksi) from Emi_Production_Results e, EMI_Production_Results_Detail_Barang f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi)"
            SQL = SQL & ",0) as Qty_Hasil_Produksi, "
            SQL = SQL & "isnull((select sum(f.Qty_Good_Stock) from Emi_Production_Results e, EMI_Production_Results_Detail_Barang f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi)"
            SQL = SQL & ",0) as Qty_Good_Stock, "
            SQL = SQL & "isnull((select sum(f.Qty_Bad_Stock) from Emi_Production_Results e, EMI_Production_Results_Detail_Barang f "
            SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi)"
            SQL = SQL & ",0) as Qty_Bad_Stock "
            '''SQL = SQL & "isnull((select sum(f.Nilai_Produksi) from Emi_Production_Results e, Emi_Production_Results_Detail f "
            '''SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            '''SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi) "
            '''SQL = SQL & ",0) as Hasil_Prod_RAW, "
            '''SQL = SQL & "isnull((select sum(f.Nilai_Produksi) from Emi_Production_Results e, Emi_Production_Results_Packaging_Detail f "
            '''SQL = SQL & "where e.Kode_Perusahaan = a.Kode_Perusahaan and f.Kode_Perusahaan = a.Kode_Perusahaan "
            '''SQL = SQL & "and e.No_Transaksi = f.No_Transaksi and e.No_Production_Order = a.No_Transaksi) "
            '''SQL = SQL & ",0) as Hasil_Prod_PACK "
            SQL = SQL & "from Emi_Split_Production_Order a,EMI_Order_Produksi b,Barang c,Emi_Master_routing d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_PO = b.No_Faktur and b.Selesai is null and b.flag_release='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Flag_Produksi = 'Y' and a.Flag_Selesai_Produksi = 'Y' "
            SQL = SQL & "and a.Flag_Hasil_Produksi is null and b.Id_Routing = d.Id_Routing "
            If ComboBox3.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrcari.Item(ComboBox3.SelectedIndex) & "  like  '%" & Trim(TextBox3.Text) & "%' "
            End If
            SQL = SQL & "order by a.Tgl_Produksi,a.Jam_Produksi"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = ListView1.Items.Add(dr("No_Transaksi"))
                    lvw.SubItems.Add(Format(dr("Tgl_Produksi"), "dd-MMM-yyyy"))
                    lvw.SubItems.Add(dr("Jam_Produksi"))
                    lvw.SubItems.Add(dr("Nama"))
                    lvw.SubItems.Add(Format(dr("Jumlah"), "N2"))
                    lvw.SubItems.Add(Format(dr("jumlah_produksi"), "N2"))
                    lvw.SubItems.Add(Format(dr("good_stock"), "N2"))
                    lvw.SubItems.Add(Format(dr("bad_stock"), "N2"))
                    lvw.SubItems.Add(dr("Keterangan"))
                    lvw.SubItems.Add(dr("Id_Routing"))
                    lvw.SubItems.Add(dr("No_PO"))

                    lvw.SubItems.Add(dr("Qty_Hasil_Produksi"))
                    lvw.SubItems.Add(dr("Qty_Good_Stock"))
                    lvw.SubItems.Add(dr("Qty_Bad_Stock"))
                    lvw.SubItems.Add(dr("Hasil_Prod_RAW"))
                    lvw.SubItems.Add(dr("Hasil_Prod_PACK"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Production_Order_Error_Pilih, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Get_Isi_ListView(ListView1.FocusedItem.Index)
        Dim txt As String = ""
        Try
            OpenConn()

            SQL = "select No_Transaksi from Emi_Production_Results where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & LvNo_Faktur & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    txt = dr("No_Transaksi")
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try




        EMI_HPP_Production.TextBox4.Text = txt
        EMI_HPP_Production.ShowDialog()

    End Sub

    Public Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        isi_lv()
    End Sub

End Class