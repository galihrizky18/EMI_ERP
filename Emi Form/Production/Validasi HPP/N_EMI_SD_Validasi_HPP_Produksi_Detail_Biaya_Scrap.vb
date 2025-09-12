Public Class N_EMI_SD_Validasi_HPP_Produksi_Detail_Biaya_Scrap

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



        Kosong()
    End Sub

    Public Sub Kosong()

        Txt_TotalBiaya.Text = ""

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
            SQL = SQL & "and c.jenis_barang <> 'FG' "
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




            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub






End Class