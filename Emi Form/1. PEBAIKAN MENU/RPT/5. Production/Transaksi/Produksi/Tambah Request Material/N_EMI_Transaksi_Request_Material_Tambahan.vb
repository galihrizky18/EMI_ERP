Public Class N_EMI_Transaksi_Request_Material_Tambahan

    Dim Lv_NoTransaksi, Lv_KdSo, Lv_KdBarang, Lv_NmBarang, Lv_TglProduksi, Lv_JamProduksi, Lv_Jumlah, Lv_Satuan, Lv_User As String

    Dim item_NoTransaksi As Integer = 0
    Dim item_KdSo As Integer = 1
    Dim item_KdBarang As Integer = 2
    Dim item_NmBarang As Integer = 3
    Dim item_TglProduksi As Integer = 4
    Dim item_JamProduksi As Integer = 5
    Dim item_Jumlah As Integer = 6
    Dim item_Satuan As Integer = 7
    Dim item_User As Integer = 8

    Dim switch_auto_complete As Boolean = False

    Private Sub N_EMI_Transaksi_Request_Material_Tambahan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal Produksi", 130, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam Produksi", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("User", 130, HorizontalAlignment.Left)
        Lv_Data.View = View.Details

        Lv_Barang.Columns.Clear()
        Lv_Barang.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Barang.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_Barang.View = View.Details


        Kosong()

    End Sub



    Private Sub Kosong()

        Txt_NoTransaksi.Text = ""
        switch_auto_complete = False
        Txt_KdBarang.Text = OpsiSeluruh
        Txt_NmBarang.Text = OpsiSeluruh
        switch_auto_complete = True

        Load_Data()
    End Sub



    Private Sub Get_Data_Lv(ByVal index As Integer)
        Lv_NoTransaksi = Lv_Data.Items(index).SubItems(item_NoTransaksi).Text
        Lv_KdSo = Lv_Data.Items(index).SubItems(item_KdSo).Text
        Lv_KdBarang = Lv_Data.Items(index).SubItems(item_KdBarang).Text
        Lv_NmBarang = Lv_Data.Items(index).SubItems(item_NmBarang).Text
        Lv_TglProduksi = Lv_Data.Items(index).SubItems(item_TglProduksi).Text
        Lv_JamProduksi = Lv_Data.Items(index).SubItems(item_JamProduksi).Text
        Lv_Jumlah = Lv_Data.Items(index).SubItems(item_Jumlah).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_User = Lv_Data.Items(index).SubItems(item_User).Text
    End Sub



    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Load_Data(filter:=True)
    End Sub



    Private Sub Load_Data(ByVal Optional filter As Boolean = False)
        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = "select distinct a.no_transaksi, a.Kode_stock_Owner, a.Kode_Barang, b.Nama, a.Tgl_Produksi, a.Jam_Produksi, a.Jumlah, a.Satuan, a.UserId, "
            SQL = SQL & "ISNULL((select top 1 'T' from Emi_Split_Production_Order_Detail_Bahan z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.no_transaksi = z.No_Faktur and Flag_Terpenuhi is null "
            SQL = SQL & "), 'Y') as Produksi_Bahan_Terpenuhi "
            SQL = SQL & "from Emi_Split_Production_Order a, barang b, Emi_Material_Requisition c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.No_Transaksi = c.No_Faktur_Order "
            SQL = SQL & "and a.Status is null and c.Status is null "
            SQL = SQL & "and a.Selesai is null "
            SQL = SQL & "and a.Flag_Selesai_Request_Material is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If filter Then
                SQL = SQL & "and a.no_transaksi like '%" & Txt_NoTransaksi.Text & "%' "

                If Txt_KdBarang.Text.Trim.ToUpper <> OpsiSeluruh.ToUpper Then
                    SQL = SQL & "and a.Kode_Barang like '%" & Txt_KdBarang.Text & "%' "
                End If
            End If
            SQL = SQL & "order by a.tgl_produksi desc "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("no_transaksi"))
                    Lv.SubItems.Add(Dr("Kode_stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam_Produksi"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("UserId"))
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
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Get_Data_Lv(Lv_Data.FocusedItem.Index)

        N_EMI_SD_Transaksi_Request_Material_Tambahan.Txt_No_Split.Text = Lv_NoTransaksi
        N_EMI_SD_Transaksi_Request_Material_Tambahan.Txt_Kd_Barang.Text = Lv_KdBarang
        N_EMI_SD_Transaksi_Request_Material_Tambahan.Txt_Nm_Barang.Text = Lv_NmBarang
        N_EMI_SD_Transaksi_Request_Material_Tambahan.Txt_Jumlah.Text = Lv_Jumlah

        Try
            OpenConn()

            N_EMI_SD_Transaksi_Request_Material_Tambahan.Cmb_Satuan.Items.Clear()
            SQL = "select satuan from emi_satuan where kode_perusahaan = '" & KodePerusahaan & "' order by satuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    N_EMI_SD_Transaksi_Request_Material_Tambahan.Cmb_Satuan.Items.Add(Dr("satuan"))
                Loop
            End Using

            N_EMI_SD_Transaksi_Request_Material_Tambahan.Cmb_Satuan.Text = Lv_Satuan

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        N_EMI_SD_Transaksi_Request_Material_Tambahan.kd_so = Lv_KdSo
        N_EMI_SD_Transaksi_Request_Material_Tambahan.ShowDialog()


    End Sub



    Private Sub Txt_KdBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdBarang.TextChanged
        If switch_auto_complete = False Then Exit Sub
        If Txt_KdBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 132)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(131, 132)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            SQL = "select distinct Kode_Barang, Nama from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Barang like '%" & Txt_KdBarang.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub



    Private Sub Txt_KdBarang_Leave(sender As Object, e As EventArgs) Handles Txt_KdBarang.Leave
        If Txt_KdBarang.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Barang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdBarang.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = "select kode_barang, nama from barang where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_barang = '" & Txt_KdBarang.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdBarang.Text = Dr("kode_barang")
                        Txt_NmBarang.Text = Dr("nama")
                    Else
                        MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul)
                        Txt_KdBarang.Text = "" : Txt_NmBarang.Text = ""
                        Txt_NmBarang.Focus()
                    End If

                    Lv_Barang.Visible = False
                    Lv_Barang.Location = New Point(1200, 132)
                End Using
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Txt_KdBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdBarang.Text.Trim.Length = 0 Then Txt_NmBarang.Focus()
            Txt_KdBarang_Leave(Txt_KdBarang, e)
            Lv_Barang.Location = New Point(1200, 132)
            Lv_Barang.Visible = False
        End If
    End Sub
    Private Sub Txt_KdBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub
    Private Sub Txt_NmBarang_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmBarang.TextChanged
        If switch_auto_complete = False Then Exit Sub
        If Txt_NmBarang.Text.Trim.Length = 0 Then
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 132)
            Txt_KdBarang.Text = ""
            Txt_NmBarang.Text = ""
            Exit Sub
        Else
            Lv_Barang.Location = New Point(131, 132)
            Lv_Barang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Barang.Items.Clear()
            Dim Lv As ListViewItem
            Lv = Lv_Barang.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select distinct Kode_Barang, Nama from barang where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_NmBarang.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Lv = Lv_Barang.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_NmBarang_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmBarang.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdBarang_Leave(Txt_NmBarang, e)
            Lv_Barang.Visible = False
            Lv_Barang.Location = New Point(1200, 132)

            Btn_Cari.Focus()
        End If
    End Sub
    Private Sub Txt_NmBarang_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmBarang.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Barang.Focus()
    End Sub
    Private Sub Lv_Barang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Barang.DoubleClick

        If Lv_Barang.Items.Count = 0 Or Lv_Barang.FocusedItem.Index = -1 Then Exit Sub

        Dim KdBarang As String = Lv_Barang.FocusedItem.SubItems(0).Text
        Dim NmBarang As String = Lv_Barang.FocusedItem.SubItems(1).Text

        switch_auto_complete = False
        Txt_KdBarang.Text = KdBarang
        Txt_NmBarang.Text = NmBarang
        switch_auto_complete = True

        Lv_Barang.Visible = False
        Lv_Barang.Location = New Point(1200, 132)

        Btn_Cari.Focus()
    End Sub

    Private Sub Lv_Barang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Barang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Barang_DoubleClick(Lv_Barang, e)
        End If
    End Sub

    Private Sub Txt_NoTransaksi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NoTransaksi.KeyPress
        If e.KeyChar = Chr(13) Then Txt_KdBarang.Focus()
    End Sub


End Class