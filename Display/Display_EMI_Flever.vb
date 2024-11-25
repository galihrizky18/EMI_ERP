Public Class Display_EMI_Flever

    Dim kodePerusahaan = "001"
    Dim arrFilterKodeStock As New ArrayList

    Private Sub Display_EMI_Flever_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Lv_Flever.Columns.Add("No Faktur", 180, HorizontalAlignment.Center)
        Lv_Flever.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_Flever.Columns.Add("Jam", 130, HorizontalAlignment.Center)
        Lv_Flever.Columns.Add("Lokasi", 180, HorizontalAlignment.Center)
        Lv_Flever.Columns.Add("Keterangan", 180, HorizontalAlignment.Center)
        Lv_Flever.Columns.Add("Total Jumlah", 130, HorizontalAlignment.Center)
        Lv_Flever.View = View.Details

        Lv_DetailFlever.Columns.Add("Kode SO Min", 180, HorizontalAlignment.Center)
        Lv_DetailFlever.Columns.Add("Kode SO Plus", 180, HorizontalAlignment.Center)
        Lv_DetailFlever.Columns.Add("Nama Barang Min", 165, HorizontalAlignment.Center)
        Lv_DetailFlever.Columns.Add("Nama Barang Plus", 165, HorizontalAlignment.Center)
        Lv_DetailFlever.Columns.Add("Jumlah Min", 120, HorizontalAlignment.Center)
        Lv_DetailFlever.Columns.Add("Jumlah Plus", 120, HorizontalAlignment.Center)
        Lv_DetailFlever.View = View.Details

        Kosong()

        ComboBox3.Items.Add("Tgl Flever")

    End Sub

    Private Sub Kosong()
        Load_Data_Flever()

        ComboBox6.Items.Clear()
        ComboBox6.Text = String.Empty

        Try
            OpenConn()

            SQL = "select kode_stock_owner, keterangan from Stock_Owner_Gudang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    ComboBox6.Items.Add(Dr("keterangan")) : arrFilterKodeStock.Add(Dr("kode_stock_owner"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Load_Data_Flever()
        Try
            OpenConn()

            Lv_Flever.Items.Clear()

            SQL = "select No_Faktur, Tanggal, Jam, Lokasi, Keterangan, Total_Jml "
            SQL = SQL & "from EMI_Flever where kode_perusahaan='" & kodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Flever.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "yyyy-MM-dd"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jam")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Lokasi")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Keterangan")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Total_Jml")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Flever_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Flever.SelectedIndexChanged
        Try
            OpenConn()

            Lv_DetailFlever.Items.Clear()

            SQL = "select a.No_Faktur, a.Kode_Stock_Owner_Min, a.Kode_Stock_Owner_Plus, "
            SQL = SQL & "(SELECT TOP 1 b.nama FROM barang b WHERE b.Kode_Stock_Owner = a.Kode_Stock_Owner_Min AND b.Kode_Barang = a.Kode_Barang_Min) AS Barang_Min, "
            SQL = SQL & "(SELECT TOP 1 b.nama FROM barang b WHERE b.Kode_Stock_Owner = a.Kode_Stock_Owner_Plus AND b.Kode_Barang = a.Kode_Barang_Plus) AS Barang_Plus, "
            SQL = SQL & "a.Jumlah_Min, a.Jumlah_Plus from EMI_Detail_Flever a "
            SQL = SQL & "where a.No_Faktur='" & Lv_Flever.FocusedItem.Text & "' "
            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DetailFlever.Items.Add(Dr("Kode_Stock_Owner_Min"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Kode_Stock_Owner_Plus")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Barang_Min")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Barang_Plus")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jumlah_Min")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jumlah_Plus")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_FilterCari_Click(sender As Object, e As EventArgs) Handles Btn_FilterCari.Click
        Try
            OpenConn()

            Lv_Flever.Items.Clear()

            SQL = "select No_Faktur, Tanggal, Jam, Lokasi, Keterangan, Total_Jml "
            SQL = SQL & "from EMI_Flever where kode_perusahaan='" & kodePerusahaan & "'"

            If Not ComboBox6.SelectedIndex = -1 Then
                SQL = SQL & " and lokasi = '" & arrFilterKodeStock(ComboBox6.SelectedIndex) & "' "
            End If

            If CheckBox1.Checked Then
                If ComboBox3.SelectedIndex = 0 Then
                    SQL = SQL & " and Tanggal between '"
                    SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
                End If
            End If

            If CheckBox3.Checked Then
                SQL = SQL & " and Tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Flever.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "yyyy-MM-dd"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Jam")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Lokasi")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Keterangan")))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("Total_Jml")))

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


End Class