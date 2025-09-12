Public Class N_EMI_SD_Validasi_HPP_Produksi_Detail_Biaya

    Public noSplit As String = ""
    Public Proses As String = ""

    Private Sub N_EMI_SD_Validasi_HPP_Produksi_Detail_Biaya_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("No Split", 150, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Proses", 100, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Jenis", 150, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Jenis Barang", 150, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("HPP Per PCS", 150, HorizontalAlignment.Right)
        Lv_Detail.View = View.Details

        Lv_Detail_Packaging.Columns.Clear()
        Lv_Detail_Packaging.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("No Split", 150, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Proses", 100, HorizontalAlignment.Center)
        Lv_Detail_Packaging.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Nama Barang", 200, HorizontalAlignment.Left)
        Lv_Detail_Packaging.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        Lv_Detail_Packaging.Columns.Add("Satuan", 150, HorizontalAlignment.Center)
        Lv_Detail_Packaging.Columns.Add("Hpp Per PCS", 150, HorizontalAlignment.Right)
        Lv_Detail_Packaging.View = View.Details

        Lv_Detail_Produksi.Columns.Clear()
        Lv_Detail_Produksi.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left)
        Lv_Detail_Produksi.Columns.Add("No Split", 150, HorizontalAlignment.Left)
        Lv_Detail_Produksi.Columns.Add("Proses", 100, HorizontalAlignment.Center)
        Lv_Detail_Produksi.Columns.Add("Work Center", 150, HorizontalAlignment.Center)
        Lv_Detail_Produksi.Columns.Add("Jenis Biaya", 150, HorizontalAlignment.Center)
        Lv_Detail_Produksi.Columns.Add("Hpp Per PCS", 150, HorizontalAlignment.Right)
        Lv_Detail_Produksi.View = View.Details


        Kosong()
    End Sub

    Public Sub Kosong()

        Txt_TotalBiaya.Text = ""
        Txt_Total_Biaya_Packaging.Text = ""
        Txt_Total_Biaya_Produksi.Text = ""

        load_Data_Lv()

    End Sub


    Private Sub load_Data_Lv()

        Try
            OpenConn()

            Dim TotalBiaya As Double = 0
            Lv_Detail.Items.Clear()
            SQL = "select a.no_transaksi, a.No_Production_Order, b.proses, c.jenis, "
            SQL = SQL & "case when c.jenis_barang = 'FG' then 'Finish Good' when c.jenis_barang = 'SCP' then 'Scrap' else 'Undefined' "
            SQL = SQL & "end as Jenis_Barang, sum(c.hpp_per_pcs) as hpp_per_pcs "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b, N_Emi_Production_Results_Detail_Biaya c "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = c.kode_perusahaan "
            SQL = SQL & "and a.no_transaksi = b.no_transaksi "
            SQL = SQL & "and a.no_transaksi = c.no_transaksi and b.proses = c.proses "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
            SQL = SQL & "and b.Proses = '" & Proses & "' "
            SQL = SQL & "and c.jenis_barang <> 'SCP' "
            SQL = SQL & "group by a.no_transaksi, a.No_Production_Order, b.proses, c.jenis, c.jenis_barang, c.hpp_per_pcs "
            SQL = SQL & "order by c.jenis, c.jenis_barang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("no_transaksi"))
                    Lv.SubItems.Add(Dr("No_Production_Order"))
                    Lv.SubItems.Add(Format(Dr("proses"), "N0"))
                    Lv.SubItems.Add(Dr("jenis"))
                    Lv.SubItems.Add(Dr("Jenis_Barang"))
                    Lv.SubItems.Add(Format(Dr("hpp_per_pcs"), "N0"))

                    TotalBiaya += Dr("hpp_per_pcs")
                Loop
            End Using

            Txt_TotalBiaya.Text = Format(TotalBiaya, "N0")

            Dim TotalBiaya_Packaging As Double = 0

            Lv_Detail_Packaging.Items.Clear()
            SQL = "select a.No_Transaksi, a.No_Production_Order as no_split, c.proses, b.Kode_Barang, d.nama as Nama_Barang, b.Serial_Number, sum(b.nilai) as Nilai, "
            SQL = SQL & "ISNULL(( select dbo.get_hpp(b.Serial_Number)), 0) as Harga_HPP, "
            SQL = SQL & "ISNULL(( sum(b.Nilai) * (select dbo.get_hpp(b.Serial_Number))), 0) as Harga_BahanBaku, c.satuan "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Packaging_Det b, Emi_Production_Results_Packaging_Detail c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.kode_perusahaan = d.kode_perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.No_Urut_Detail = c.Urut "
            SQL = SQL & "and b.kode_stock_owner = d.kode_stock_owner and b.kode_barang = d.kode_barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
            SQL = SQL & "and c.Proses = '" & Proses & "' "
            SQL = SQL & "GROUP BY a.No_Transaksi, a.No_Production_Order, b.Kode_Barang, b.Serial_Number, dbo.get_hpp(b.Serial_Number), c.satuan, d.nama, c.proses "
            SQL = SQL & "order by b.Kode_Barang, c.satuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Packaging.Items.Add(Dr("no_transaksi"))
                    Lv.SubItems.Add(Dr("no_split"))
                    Lv.SubItems.Add(Dr("proses"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Nilai"), "N0"))
                    Lv.SubItems.Add(Dr("satuan"))
                    Lv.SubItems.Add(Format(Dr("Harga_HPP"), "N0"))

                    TotalBiaya_Packaging += Dr("Harga_HPP")

                Loop
            End Using

            Txt_Total_Biaya_Packaging.Text = Format(TotalBiaya_Packaging, "N0")

            Dim TotalBiaya_Produksi As Double = 0

            Lv_Detail_Produksi.Items.Clear()
            SQL = "select a.no_transaksi, a.No_Production_Order, b.proses, d.id_work_center, d.keterangan as Work_Center, c.kode_jenis_biaya, e.keterangan as Jenis_Biaya, sum(c.nilai) as nilai "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b, N_Emi_Production_Results_Detail_Biaya_WC c, emi_master_work_center d, Emi_Jenis_Biaya_Produksi e "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = c.kode_perusahaan and c.kode_perusahaan = d.kode_perusahaan and c.kode_perusahaan = e.kode_perusahaan "
            SQL = SQL & "and a.no_transaksi = b.no_transaksi "
            SQL = SQL & "and a.no_transaksi = c.no_transaksi "
            SQL = SQL & "and c.id_work_center = d.id_work_center "
            SQL = SQL & "and c.kode_jenis_biaya = e.kode_jenis_biaya_Produksi "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & noSplit & "' "
            SQL = SQL & "and b.Proses = '" & Proses & "' "
            SQL = SQL & "group by  a.no_transaksi, a.No_Production_Order, b.proses, d.id_work_center, d.keterangan, c.kode_jenis_biaya, e.keterangan, c.nilai "
            SQL = SQL & "order by c.kode_jenis_biaya "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Produksi.Items.Add(Dr("no_transaksi"))
                    Lv.SubItems.Add(Dr("No_Production_Order"))
                    Lv.SubItems.Add(Dr("proses"))
                    Lv.SubItems.Add(Dr("Work_Center"))
                    Lv.SubItems.Add(Dr("Jenis_Biaya"))
                    Lv.SubItems.Add(Format(Dr("nilai"), "N0"))

                    TotalBiaya_Produksi += Dr("nilai")

                Loop
            End Using

            Txt_Total_Biaya_Produksi.Text = Format(TotalBiaya_Produksi, "N0")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub






End Class