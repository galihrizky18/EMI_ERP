Public Class N_EMI_Transaksi_Validasi_Formula

    Dim ArrFilter As New ArrayList

    Dim Lv_NoFaktur, Lv_KdBarang, Lv_NmBarang, Lv_Jumlah, Lv_Satuan As String

    Dim Item_NoFaktur As Integer = 0
    Dim Item_KdBarang As Integer = 1
    Dim Item_NmBarang As Integer = 2
    Dim Item_Jumlah As Integer = 3
    Dim Item_Satuan As Integer = 4



    Private Sub N_EMI_Transaksi_Validasi_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Cmb_Kolom_Filter.Items.Clear() : ArrFilter.Clear()
        Cmb_Kolom_Filter.Items.Add("No Faktur") : ArrFilter.Add("a.No_Faktur")
        Cmb_Kolom_Filter.Items.Add("Kode Barang") : ArrFilter.Add("a.Kode_Barang")
        Cmb_Kolom_Filter.Items.Add("Nama Barangs") : ArrFilter.Add("b.Nama")

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Faktur", 150, HorizontalAlignment.Left) ' 0
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Nama Barang", 360, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Jumlah", 150, HorizontalAlignment.Right) '3
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '4
        Lv_Data.View = View.Details


        Kosong()
    End Sub

    Public Sub Kosong()

        Cmb_Kolom_Filter.SelectedIndex = -1
        Txt_Value_Filter.Text = ""

        LoadData()
    End Sub


    Private Sub GetDataLv(ByVal index As Integer)
        Lv_NoFaktur = Lv_Data.Items(index).SubItems(Item_NoFaktur).Text
        Lv_KdBarang = Lv_Data.Items(index).SubItems(Item_KdBarang).Text
        Lv_NmBarang = Lv_Data.Items(index).SubItems(Item_NmBarang).Text
        Lv_Jumlah = Lv_Data.Items(index).SubItems(Item_Jumlah).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(Item_Satuan).Text
    End Sub

    Private Sub LoadData()

        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select a.No_Faktur, a.Kode_Barang, b.Nama as Nama_Barang, a.Hasil, a.Satuan_Hasil "
            SQL &= $"from Emi_Transaksi_Formulator a "
            SQL &= $"inner join barang b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.Status is NULL "
            SQL &= $"and a.Flag_Validasi is null "
            If Cmb_Kolom_Filter.SelectedIndex <> -1 And Txt_Value_Filter.Text <> "" Then
                SQL &= $"and {ArrFilter(Cmb_Kolom_Filter.SelectedIndex)} like '%{Txt_Value_Filter.Text}%' "
            End If
            SQL &= $"order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Hasil"))), "N4"))
                    Lv.SubItems.Add(Dr("Satuan_Hasil"))

                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        LoadData()
    End Sub



    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Dim No_faktur As String = Lv_Data.FocusedItem.SubItems(Item_NoFaktur).Text

        N_EMI_SD_Transaksi_Validasi_Formula.Kosong()
        N_EMI_SD_Transaksi_Validasi_Formula.TxtFormulator_NoFaktur.Text = No_faktur
        N_EMI_SD_Transaksi_Validasi_Formula.TxtFormulator_NoFaktur_Leave(sender, New EventArgs)
        N_EMI_SD_Transaksi_Validasi_Formula.ShowDialog()



    End Sub



    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub Lv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Data.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            ' Mouse sedang berada di atas row
            Lv_Data.Cursor = Cursors.Hand
        Else
            ' Mouse tidak mengenai row
            Lv_Data.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Lv_Data_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data.MouseLeave
        Lv_Data.Cursor = Cursors.Default
    End Sub

End Class