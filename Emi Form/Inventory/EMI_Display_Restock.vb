Public Class EMI_Display_Restock

    Dim arrFilter As New ArrayList

    Private Sub EMI_Display_Restock_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Initial_Lv()
        Kosong()

    End Sub

    Private Sub Kosong()

        Lv_Data.Items.Clear()

        Txt_Filter_Value.Text = String.Empty

        Cmb_Filter.Items.Clear() : arrFilter.Clear()
        Cmb_Filter.Items.Add("Kode Barang") : arrFilter.Add("a.Kode_Barang")
        Cmb_Filter.Items.Add("Nama Barang") : arrFilter.Add("b.Nama")
        Cmb_Filter.Items.Add("Tanggal") : arrFilter.Add("a.Tanggal")
        Cmb_Filter.Items.Add("Tanggal Produksi") : arrFilter.Add("a.tgl_produksi")
        Cmb_Filter.Items.Add("Tanggal Expired") : arrFilter.Add("a.tgl_expired")


        LoadData()

    End Sub

    Private Sub Initial_Lv()
        Lv_Data.Columns.Clear()

        Lv_Data.Columns.Add("No Faktur", 165, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode SO", 165, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Nama Barang", 370, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah", 140, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Harga Beli", 140, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Keterangan", 350, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal Produksi", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tanggal Expired", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("User Input", 110, HorizontalAlignment.Center)

        Lv_Data.Columns.Add("SN", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Satuan Kecil", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("UserID", 0, HorizontalAlignment.Center)
        Lv_Data.View = View.Details

    End Sub

    Private Sub LoadData()

        Try
            OpenConn()

            Lv_Data.Items.Clear()

            SQL = "select a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Jumlah, a.Satuan, a.Satuan_Barang, "
            SQL = SQL & "a.Keterangan, a.serial_number, a.UserID, c.UserName, a.harga_beli, a.tgl_produksi, a.tgl_expired, a.no_urut "
            SQL = SQL & "from EMI_Restock_Barang a, Barang b, users c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.UserID = c.UserID "
            SQL = SQL & "and a.Status is null "

            If Cmb_Filter.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrFilter.Item(Cmb_Filter.SelectedIndex) & "  like  '%" & Trim(Txt_Filter_Value.Text) & "%' "
            End If

            SQL = SQL & "order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As New ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Format(Dr("harga_beli"), "N2"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("tgl_produksi"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Format(Dr("tgl_expired"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("UserName"))

                    'Hidden
                    Lv.SubItems.Add(Dr("serial_number"))
                    Lv.SubItems.Add(Dr("Satuan_Barang"))
                    Lv.SubItems.Add(Dr("UserID"))

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

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click


        LoadData()
    End Sub

    Private Sub Cmb_Filter_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter.KeyPress

        If e.KeyChar = Chr(13) Then

            If Cmb_Filter.SelectedIndex = -1 Then
                MessageBox.Show("Pilih Dahulu Kategori Filter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Filter.Focus()
                Exit Sub
            End If

            Txt_Filter_Value.Focus()

        End If


    End Sub

    Private Sub Txt_Filter_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Filter_Value.KeyPress
        If e.KeyChar = Chr(13) Then

            Btn_Cari_Click(sender, e)

        End If
    End Sub
End Class