Public Class EMI_Barang_Masuk_Summary_Data_SD

    Public NoLoading, Asal As String

    Private Sub EMI_Barang_Masuk_Summary_Data_SD_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Intial_LV()
        LoadData()

    End Sub

    Private Sub Intial_LV()

        Lv_Timbang.Columns.Clear() : Lv_Timbang.Items.Clear()
        Lv_Timbang.Columns.Add("No Faktur", 140, HorizontalAlignment.Left)
        Lv_Timbang.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        Lv_Timbang.Columns.Add("Jam", 90, HorizontalAlignment.Center)
        Lv_Timbang.Columns.Add("Jumlah Timbang", 120, HorizontalAlignment.Right)
        Lv_Timbang.Columns.Add("Jumlah Bags", 100, HorizontalAlignment.Right)
        Lv_Timbang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Timbang.View = View.Details

        Lv_Detail.Columns.Clear() : Lv_Detail.Items.Clear()
        Lv_Detail.Columns.Add("Kode Barang", 110, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Nama Barang", 210, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Bags", 100, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Detail.View = View.Details

    End Sub

    Private Sub LoadData()
        If NoLoading Is Nothing OrElse NoLoading.Trim.Length = 0 Then
            MessageBox.Show("No Loading Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Me.Close()
        End If

        Try
            OpenConn()

            Txt_NoLoading.Text = NoLoading

            Lv_Timbang.Items.Clear() : Lv_Detail.Items.Clear()
            SQL = "select No_Faktur, Tgl_Timbang_Masuk, Jam_Timbang_Masuk, Timbang_Masuk, Satuan, Jumlah_Bags "
            SQL = SQL & "from EMI_Timbang_Unloading "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Loading = '" & NoLoading & "' "
            SQL = SQL & "and status is null "
            SQL = SQL & "order by No_Faktur "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Timbang.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tgl_Timbang_Masuk"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam_Timbang_Masuk"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Timbang_Masuk")) = "", 0, Format(Dr("Timbang_Masuk"), "N2")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jumlah_Bags")) = "", 0, Format(Dr("Jumlah_Bags"), "N2")))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Lv_Timbang_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Timbang.SelectedIndexChanged
        If Lv_Timbang.Items.Count = 0 Or Lv_Timbang.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim NoFaktur As String = Lv_Timbang.FocusedItem.Text

            Lv_Detail.Items.Clear()
            SQL = "select a.No_Faktur, a.No_Loading, b.No_PO ,a.Tgl_Timbang_Masuk, "
            SQL = SQL & "b.Kode_Barang, c.Nama as Nama_Barang, b.Jumlah, b.Satuan, b.Jumlah_Bag "
            SQL = SQL & "from EMI_Timbang_Unloading a, EMI_Timbang_Unloading_PO_Det b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Loading = '" & NoLoading & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoFaktur & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim lv As ListViewItem
                    lv = Lv_Detail.Items.Add(Dr("Kode_Barang").ToString())
                    lv.SubItems.Add(Dr("Nama_Barang").ToString())
                    lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jumlah")) = "", "0.00", Format(CDbl(Dr("Jumlah")), "N2")))
                    lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jumlah_Bag")) = "", "0.00", Format(CDbl(Dr("Jumlah_Bag")), "N2")))
                    lv.SubItems.Add(Dr("Satuan").ToString())
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Timbang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Timbang.DoubleClick
        If Lv_Timbang.Items.Count = 0 Or Lv_Timbang.FocusedItem Is Nothing Then Exit Sub

        Dim NoFaktur As String = Lv_Timbang.FocusedItem.Text
        If Asal.ToUpper = "PERINTAHBONGKAR" Then
            EMI_Barang_Masuk_Summary_Data.CetakUlangPerintahBongkar(NoFaktur, Asal)

        ElseIf Asal.ToUpper = "BATALTIMBANGKELUAR" Then

        End If


        Me.Close()

    End Sub

End Class