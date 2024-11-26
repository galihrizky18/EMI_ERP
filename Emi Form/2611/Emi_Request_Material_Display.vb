'Public Class Emi_Request_Material_Display_vb
Public Class Emi_Request_Material_Display

    Dim Lv_NoFaktur, Lv_Lokasi, Lv_KdBarang, Lv_Nama, Lv_Tanggal, Lv_Jam, Lv_Jmlh, Lv_Satuan, Lv_User As String

    Dim item_NoFak As Integer = 0
    Dim item_Lokasi As Integer = 1
    Dim item_KdBarang As Integer = 2
    Dim item_Nama As Integer = 3
    Dim item_Tanggal As Integer = 4
    Dim item_Jam As Integer = 5
    Dim item_Jumlah As Integer = 6
    Dim item_Satuan As Integer = 7
    Dim item_User As Integer = 8

    Private Sub Emi_Request_Material_Display_vb_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Request_Material_Display_vb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Initial_Lv()
        kosong()
        Load_Lv()

    End Sub

    Private Sub kosong()
        Lv_Data.Items.Clear()
    End Sub

    Private Sub Initial_Lv()
        Lv_Data.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang", 270, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal Produksi", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam Produksi", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jumlah", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("User", 120, HorizontalAlignment.Left)
        Lv_Data.View = View.Details

    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)

        Lv_NoFaktur = Lv_Data.Items(index).SubItems(item_NoFak).Text
        Lv_Lokasi = Lv_Data.Items(index).SubItems(item_Lokasi).Text
        Lv_KdBarang = Lv_Data.Items(index).SubItems(item_KdBarang).Text
        Lv_Nama = Lv_Data.Items(index).SubItems(item_Nama).Text
        Lv_Tanggal = Lv_Data.Items(index).SubItems(item_Tanggal).Text
        Lv_Jam = Lv_Data.Items(index).SubItems(item_Jam).Text
        Lv_Jmlh = Lv_Data.Items(index).SubItems(item_Jumlah).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_User = Lv_Data.Items(index).SubItems(item_User).Text

    End Sub

    Private Sub Load_Lv()

        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select a.No_Faktur, a.Kode_stock_Owner, a.Kode_Barang, b.Nama, a.Tanggal_Produksi, a.Jam_Produksi, a.Jumlah, a.Satuan, a.UserId "
            SQL = SQL & "from EMI_Order_Produksi a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Selesai is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by a.Tanggal_Produksi desc "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    lv.SubItems.Add(Dr("Kode_stock_Owner"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                    If Not General_Class.CekNULL(Dr("Tanggal_Produksi")) = "" Then
                        lv.SubItems.Add(Format(Dr("Tanggal_Produksi"), "dd MMM yyyy"))
                        lv.SubItems.Add(Dr("Jam_Produksi"))
                    Else
                        lv.SubItems.Add("-")
                        lv.SubItems.Add("-")
                    End If

                    lv.SubItems.Add(Dr("Jumlah"))
                    lv.SubItems.Add(Dr("Satuan"))
                    lv.SubItems.Add(Dr("UserId"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick

        If Lv_Data.Items.Count = 0 Then Exit Sub

        Get_Data_Lv(Lv_Data.FocusedItem.Index)


        Emi_Request_Material.Txt_NoFaktur.Text = Lv_NoFaktur
        Emi_Request_Material.Txt_So.Text = Lv_Lokasi
        Emi_Request_Material.Txt_KdBarang.Text = Lv_KdBarang
        Emi_Request_Material.Txt_NamaBarang.Text = Lv_Nama

        Emi_Request_Material.ShowDialog()




    End Sub
End Class