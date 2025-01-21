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
        Cmb_Filter.Items.Add("Lokasi Gudang") : arrFilter.Add("a.Kode_Stock_Owner")

        'Cmb_Filter.Items.Add("Tanggal") : arrFilter.Add("a.Tanggal")
        'Cmb_Filter.Items.Add("Tanggal Produksi") : arrFilter.Add("a.tgl_produksi")
        'Cmb_Filter.Items.Add("Tanggal Expired") : arrFilter.Add("a.tgl_expired")


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


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            CheckBox1.Focus() : Exit Sub
        End If

        If CheckBox2.Checked = True Then
            If DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                Exit Sub
            End If
        End If

        If CheckBox3.Checked = True Then
            If Cmb_Filter.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                Cmb_Filter.Focus() : Exit Sub
            ElseIf Txt_Filter_Value.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                Txt_Filter_Value.Focus() : Exit Sub
            End If

        End If

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

            If CheckBox1.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal Between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilter.Item(Cmb_Filter.SelectedIndex) & " like '%" & Trim(Txt_Filter_Value.Text) & "%' "
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Kosong()

    End Sub
End Class