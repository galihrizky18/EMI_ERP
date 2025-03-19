Public Class SD_Detail_Batch

    Public noSplit As String = ""

    Dim Lv_Batch, Lv_KdBarang, Lv_NmBarang, Lv_NilaiFormula, Lv_NilaiProduksi, Lv_Satuan, Lv_NoTransaksi As String

    Dim item_Batch As Integer = 0
    Dim item_KdBarang As Integer = 1
    Dim item_NmBarang As Integer = 2
    Dim item_NilaiFormula As Integer = 3
    Dim item_NilaiProduksi As Integer = 4
    Dim item_Satuan As Integer = 5
    Dim item_NoFaktur As Integer = 6


    Private Sub SD_Detail_Batch_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Kosong()
    End Sub


    Public Sub Kosong()

        Txt_TotNilaiFormula.Text = ""
        Txt_TotNilaiPRoduksi.Text = ""



        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("", 0, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Batch", 120, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Barang", 180, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nilai Produksi", 210, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Selisih %", 210, HorizontalAlignment.Right)

        'HIDE   
        Lv_Data.Columns.Add("no_Faktur", 0, HorizontalAlignment.Left)
        Lv_Data.View = View.Details

        LoadDataLv()
    End Sub


    Private Sub LoadDataLv()
        Try
            OpenConn()
            Dim Total_Formula As Double = 0
            Dim Total_Produksi As Double = 0

            Lv_Data.Items.Clear()
            SQL = "select a.No_Transaksi, e.Proses, c.Kode_Stock_Owner, c.Kode_Barang, f.nama, c.satuan, "

            SQL = SQL & "isnull(( "
            SQL = SQL & "FLOOR( "
            SQL = SQL & "(c.Jumlah / (select z.Hasil from Emi_Transaksi_Formulator z "
            SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan And z.No_Faktur = c.No_Faktur) "
            SQL = SQL & ") "
            SQL = SQL & " * "
            SQL = SQL & "(ISNULL((select z.Qty_PerBatch from EMI_Master_Routing z "
            SQL = SQL & "where z.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "And z.Id_Routing = b.Id_Routing "
            SQL = SQL & "), 0)) "
            SQL = SQL & ") "
            SQL = SQL & "), 0) as Nilai_Formula, "

            SQL = SQL & "ISNULL(( select z.Nilai_Produksi from Emi_Production_Results_Detail z  where z.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and z.No_Transaksi = d.No_Transaksi and z.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and z.Proses = e.Proses ), 0) as Nilai_Produksi "

            SQL = SQL & "from Emi_Split_Production_Order a, EMI_Order_Produksi b, EMI_Transaksi_Formulator_Detail_Bahan c, Emi_Production_Results d, Emi_Production_Results_HPP e, Barang f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and a.No_PO = b.No_Faktur "
            SQL = SQL & "and a.No_Transaksi = d.No_Production_Order "
            SQL = SQL & "and d.No_Transaksi = e.No_Transaksi "
            SQL = SQL & "and b.Kode_Formula = c.No_Faktur "
            SQL = SQL & "and c.Kode_Stock_Owner = f.Kode_Stock_Owner and c.Kode_Barang = f.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_Transaksi = '" & noSplit & "' "
            SQL = SQL & "order by e.Proses, c.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read()
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add("")
                    Lv.SubItems.Add(Dr("Proses"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Nilai_Produksi"))), "N2"))
                    Lv.SubItems.Add(Dr("satuan"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda((Dr("Nilai_Formula") - Dr("Nilai_Produksi")) / Dr("Nilai_Formula") * 100)), "N2"))


                    Total_Formula += Dr("Nilai_Formula")
                    Total_Produksi += Dr("Nilai_Produksi")
                Loop
            End Using

            Txt_TotNilaiFormula.Text = Format(Total_Formula, "N2")
            Txt_TotNilaiPRoduksi.Text = Format(Total_Produksi, "N2")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub







End Class