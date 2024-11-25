Public Class Display_EMI_Transfer_Stock

    Dim kodePerusahaan = "001"
    Dim arrSOAsal, arrSOTujuan As New ArrayList
    Private Sub Display_EMI_Transfer_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lv_Transfer.Columns.Add("Kode Transfer", 160, HorizontalAlignment.Center)
        lv_Transfer.Columns.Add("Tanggal Kirim", 120, HorizontalAlignment.Center)
        lv_Transfer.Columns.Add("Jam Kirim", 120, HorizontalAlignment.Center)
        lv_Transfer.Columns.Add("User Kirim", 120, HorizontalAlignment.Center)
        lv_Transfer.Columns.Add("SO Asal", 150, HorizontalAlignment.Center)
        lv_Transfer.Columns.Add("SO Tujuan", 150, HorizontalAlignment.Center)
        lv_Transfer.Columns.Add("Keterangan", 220, HorizontalAlignment.Center)
        'lv_Transfer.Columns.Add("Status Terima", 150, HorizontalAlignment.Center)
        lv_Transfer.View = View.Details

        Lv_DetailTransfer.Columns.Add("Nama Barang", 200, HorizontalAlignment.Center)
        Lv_DetailTransfer.Columns.Add("Kode Kategori", 150, HorizontalAlignment.Center)
        Lv_DetailTransfer.Columns.Add("Jumlah Barang", 150, HorizontalAlignment.Center)
        Lv_DetailTransfer.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        'Lv_DetailTransfer.Columns.Add("User Kirim", 150, HorizontalAlignment.Center)
        Lv_DetailTransfer.View = View.Details

        Kosong()

        ComboBox3.Items.Add("Transfer")


    End Sub

    Private Sub Kosong()
        Load_Data_Transfer()


        Try
            OpenConn()

            SQL = "select Kode_Stock_Owner, Keterangan from stock_owner_gudang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_SOAsal.Items.Add(Dr("Keterangan")) : arrSOAsal.Add(Dr("Kode_Stock_Owner"))
                    Cmb_SOTujuan.Items.Add(Dr("Keterangan")) : arrSOTujuan.Add(Dr("Kode_Stock_Owner"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Data_Transfer()
        Try
            OpenConn()
            lv_Transfer.Items.Clear()

            SQL = "select a.Kode_Transfer, a.Tanggal as Tgl_Kirim, a.Jam as Jam_Kirim, b.Kode_SO_Awal as SO_Awal, a.UserID, "
            SQL = SQL & "a.Kode_SO_Tujuan as SO_Tujuan, a.Keterangan, a.No_Terima, a.Tgl_Terima, a.Jam_Terima, a.User_Terima, "
            SQL = SQL & "Case when a.No_Terima is null then 'Process' else 'Diterima' end as StatusKirim "
            SQL = SQL & "from Emi_Transfer_Stock a, Emi_Transfer_Stock_Detail b "
            SQL = SQL & "where a.Kode_Transfer=b.Kode_Transfer and a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Terima is null "
            SQL = SQL & "and a.Kode_Perusahaan='" & kodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = lv_Transfer.Items.Add(Dr("Kode_Transfer"))
                    Lv.SubItems.Add(Format(Dr("Tgl_Kirim"), "yyyy-MM-dd"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jam_Kirim")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("UserID")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SO_Awal")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SO_Tujuan")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Keterangan")))
                    'Lv.SubItems.Add(General_Class.CekNULL(Dr("StatusKirim")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub lv_Transfer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_Transfer.SelectedIndexChanged
        If lv_Transfer.Items.Count = 0 Then Exit Sub
        Try
            OpenConn()
            Lv_DetailTransfer.Items.Clear()

            SQL = "select a.No_Terima, a.Tgl_Terima, a.Jam_Terima, c.Nama, b.Jumlah, b.Satuan, a.User_Terima, c.kode_kategori "
            SQL = SQL & "from Emi_Transfer_Stock a,Emi_Transfer_Stock_Detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan=b.Kode_Perusahaan and a.Kode_Perusahaan=c.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Transfer=b.Kode_Transfer and b.Kode_Barang=c.Kode_Barang "
            SQL = SQL & "and a.Kode_Transfer='" & lv_Transfer.FocusedItem.Text & "' "
            SQL = SQL & "and a.Kode_Perusahaan='" & kodePerusahaan & "'"
            SQL = SQL & "group by  a.No_Terima, a.Tgl_Terima, a.Jam_Terima, c.Nama, b.Jumlah, a.User_Terima, b.Satuan, c.kode_kategori "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DetailTransfer.Items.Add(Dr("Nama"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("kode_kategori")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jumlah")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Satuan")))
                    'Lv.SubItems.Add(General_Class.CekNULL(Dr("User_Terima")))

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
        Lv_DetailTransfer.Items.Clear()
    End Sub

    Private Sub Cmb_SOAsal_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Cmb_SOAsal.SelectedIndex = -1 Then Exit Sub

        Cmb_SOTujuan.SelectedIndex = -1
        Cmb_SOTujuan.Text = String.Empty
    End Sub

    Private Sub Cmb_SOTujuan_SelectedIndexChanged(sender As Object, e As EventArgs)
        If Cmb_SOTujuan.SelectedIndex = -1 Then Exit Sub

        Cmb_SOAsal.SelectedIndex = -1
        Lbal2.Text = String.Empty
    End Sub

    Private Sub Btn_FilterCari_Click(sender As Object, e As EventArgs) Handles Btn_FilterCari.Click
        lv_Transfer.Items.Clear()
        Lv_DetailTransfer.Items.Clear()

        Try
            OpenConn()

            SQL = "select a.Kode_Transfer, a.Tanggal as Tgl_Kirim, a.Jam as Jam_Kirim, b.Kode_SO_Awal as SO_Awal, a.UserID, "
            SQL = SQL & "a.Kode_SO_Tujuan as SO_Tujuan, a.Keterangan, a.No_Terima, a.Tgl_Terima, a.Jam_Terima, a.User_Terima, "
            SQL = SQL & "Case when a.No_Terima is null then 'Process' else 'Diterima' end as StatusKirim "
            SQL = SQL & "from Emi_Transfer_Stock a, Emi_Transfer_Stock_Detail b "
            SQL = SQL & "where a.Kode_Transfer=b.Kode_Transfer and a.Kode_Perusahaan=b.Kode_Perusahaan and a.No_Terima is null "
            SQL = SQL & "and a.Kode_Perusahaan='" & kodePerusahaan & "'"

            If Not Cmb_SOAsal.SelectedIndex = -1 Then
                SQL = SQL & " and b.kode_so_awal='" & arrSOAsal(Cmb_SOAsal.SelectedIndex) & "' "
            End If

            If Not Cmb_SOTujuan.SelectedIndex = -1 Then
                SQL = SQL & " and a.kode_so_tujuan='" & arrSOTujuan(Cmb_SOTujuan.SelectedIndex) & "' "
            End If

            If CheckBox1.Checked Then
                SQL = SQL & " and a.Tanggal between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox3.Checked Then
                SQL = SQL & " and a.Tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If


            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = lv_Transfer.Items.Add(Dr("Kode_Transfer"))

                    Lv.SubItems.Add(Format(Dr("Tgl_Kirim"), "yyyy-MM-dd"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jam_Kirim")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("UserID")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SO_Awal")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SO_Tujuan")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Keterangan")))

                Loop
            End Using

            Cmb_SOAsal.SelectedIndex = -1
            Cmb_SOTujuan.SelectedIndex = -1
            Cmb_SOAsal.Text = String.Empty
            Cmb_SOTujuan.Text = String.Empty

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
End Class