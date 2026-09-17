'Public Class Emi_Request_Material_Display_vb
Public Class N_EMI_Display_Request_Material_QC


    Dim Lv_NoFaktur As String
    Dim Lv_Routing As String
    Dim Lv_Keterangan As String
    Dim Lv_KodeBarang As String
    Dim Lv_NamaBarang As String
    Dim Lv_TglPrd As String
    Dim Lv_JamPrd As String
    Dim Lv_Jumlah As String

    Dim Cell_NoFaktur As Integer = 0
    Dim Cell_Routing As Integer = 1
    Dim Cell_Keterangan As Integer = 2
    Dim Cell_KodeBarang As Integer = 3
    Dim Cell_NamaBarang As Integer = 4
    Dim Cell_TglPrd As Integer = 5
    Dim Cell_JamPrd As Integer = 6
    Dim Cell_Jumlah As Integer = 7


    Private Sub Emi_Request_Material_Display_vb_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Request_Material_Display_vb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        Lv_ListSplit.Columns.Clear() : Lv_ListSplit.Items.Clear()
        Lv_ListSplit.Columns.Add("No Split", 130, HorizontalAlignment.Left)
        Lv_ListSplit.Columns.Add("Routing", 140, HorizontalAlignment.Left)
        Lv_ListSplit.Columns.Add("Keterangan", 180, HorizontalAlignment.Left)
        Lv_ListSplit.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_ListSplit.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        Lv_ListSplit.Columns.Add("Tanggal Produksi", 120, HorizontalAlignment.Center)
        Lv_ListSplit.Columns.Add("Jam Produksi", 120, HorizontalAlignment.Center)
        Lv_ListSplit.Columns.Add("Jumlah", 100, HorizontalAlignment.Right)
        Lv_ListSplit.View = View.Details

        kosong()
    End Sub

    Public Sub kosong()
        Lv_ListSplit.Items.Clear()
        Load_Lv()
    End Sub


    Private Sub Get_Data_Lv(ByVal Noindex As Integer)


        Lv_NoFaktur = Lv_ListSplit.Items(Noindex).SubItems(Cell_NoFaktur).Text
        Lv_Routing = Lv_ListSplit.Items(Noindex).SubItems(Cell_Routing).Text
        Lv_Keterangan = Lv_ListSplit.Items(Noindex).SubItems(Cell_Keterangan).Text
        Lv_KodeBarang = Lv_ListSplit.Items(Noindex).SubItems(Cell_KodeBarang).Text
        Lv_NamaBarang = Lv_ListSplit.Items(Noindex).SubItems(Cell_NamaBarang).Text
        Lv_TglPrd = Lv_ListSplit.Items(Noindex).SubItems(Cell_TglPrd).Text
        Lv_JamPrd = Lv_ListSplit.Items(Noindex).SubItems(Cell_JamPrd).Text
        Lv_Jumlah = Lv_ListSplit.Items(Noindex).SubItems(Cell_Jumlah).Text

    End Sub

    Private Sub Load_Lv()




        Try
            OpenConn()

            Lv_ListSplit.Items.Clear()



            SQL = "Select b.no_transaksi, d.Keterangan As Routing, a.keterangan, b.kode_barang, c.nama, b.tgl_produksi, "
            SQL = SQL & "b.Jam_Produksi, b.jumlah, b.satuan "
            SQL = SQL & "From emi_order_produksi a, Emi_Split_Production_Order b, barang c, emi_master_routing d Where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.no_faktur = b.no_po And "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.Kode_Barang = c.Kode_Barang And b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "And a.Kode_Perusahaan=d.Kode_Perusahaan And a.Id_Routing=d.Id_Routing "
            SQL = SQL & "And a.status Is null And b.status Is null And a.kode_perusahaan='" & KodePerusahaan & "' "
            SQL = SQL & "And b.Flag_Selesai_Request_Material Is null And b.Flag_Hasil_Produksi_GI Is null "
            SQL = SQL & "order by b.tgl_produksi+b.Jam_Produksi "
            Using Dr = OpenTrans(SQL)
                Dim row As Integer = 0
                Do While Dr.Read

                    Dim Lvw As ListViewItem
                    Lvw = Lv_ListSplit.Items.Add(Dr("no_transaksi"))
                    Lvw.SubItems.Add(Dr("Routing"))
                    Lvw.SubItems.Add(Dr("keterangan"))
                    Lvw.SubItems.Add(Dr("kode_barang"))
                    Lvw.SubItems.Add(Dr("nama"))
                    Lvw.SubItems.Add(Format(Dr("tgl_produksi"), "dd MMMM yyyy"))
                    Lvw.SubItems.Add(Dr("Jam_Produksi"))
                    Lvw.SubItems.Add(Format(Dr("jumlah"), "N0") + " " + Dr("satuan"))


                    row = row + 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub SelesaiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelesaiToolStripMenuItem.Click


        If Lv_ListSplit.Items.Count = 0 Then Exit Sub

        Get_Data_Lv(Lv_ListSplit.FocusedItem.Index)

        'Try
        '    OpenConn()
        '    Get_Data_Lv(Lv_Data.CurrentRow.Index)

        '    '=============================================
        '    '=     CEK APAKAH BARANG SUDAH TERPENUHI     =
        '    '=============================================

        '    If Lv_Status = "Terpenuhi" Then

        '        SQL = "update Emi_Split_Production_Order set Flag_Selesai_Request_Material = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '        SQL = SQL & "and No_transaksi = '" & Lv_NoFaktur & "' and Kode_stock_Owner = '" & Lv_Lokasi & "' and Kode_Barang = '" & Lv_KdBarang & "' "
        '        ExecuteTrans(SQL)

        '    Else

        '        Dim Validasi As String = MessageBox.Show("Requst Material Belum Terpenuhi, Tetap ingin Akhiri Request Produksi . . ? ?", "Production", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        '        If Validasi = vbYes Then

        '            SQL = "update Emi_Split_Production_Order set Flag_Selesai_Request_Material = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' "
        '            SQL = SQL & "and No_transaksi = '" & Lv_NoFaktur & "' and Kode_stock_Owner = '" & Lv_Lokasi & "' and Kode_Barang = '" & Lv_KdBarang & "' "
        '            ExecuteTrans(SQL)

        '        End If

        '    End If

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        kosong()

    End Sub


    Private Sub Lv_ListSplit_DoubleClick(sender As Object, e As EventArgs) Handles Lv_ListSplit.DoubleClick

        If Lv_ListSplit.Items.Count = 0 Then Exit Sub

        Get_Data_Lv(Lv_ListSplit.FocusedItem.Index)

        N_EMI_Transaksi_Request_Material_QC.Txt_NoFaktur.Text = Lv_NoFaktur

        N_EMI_Transaksi_Request_Material_QC.ShowDialog()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub
End Class