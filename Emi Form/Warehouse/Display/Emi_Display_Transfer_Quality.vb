Public Class Emi_Display_Transfer_Quality

    Dim lv_NoFak, Lv_KdSO, Lv_Kd_Barang, Lv_Tot_Stock, Lv_Tot_Bags, Lv_Satuan, Lv_UserTransfer As String

    Dim arrCari As New ArrayList

    Dim item_NoFak As Integer = 0
    Dim item_KdSo As Integer = 1
    Dim item_KdBarang As Integer = 2
    Dim item_Tot_Stock As Integer = 9
    Dim item_Tot_Bags As Integer = 10
    Dim item_Satuan As Integer = 11
    Dim item_User_Tf As Integer = 12

    Private Sub Emi_Display_Transfer_Quality_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Display_Transfer_Quality_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Intial_ListView_Quality()
        Kosong()

    End Sub

    Private Sub Kosong()

        Lv_Quality.Items.Clear()
        Lv_Quality_Detail.Items.Clear()

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1
            SQL = "Select kode_stock_owner, inisial_faktur, pending_persediaan, persediaan, Keterangan From Stock_Owner_Gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSO_Asal.Items.Add(dr("Keterangan"))
                Loop
            End Using

            Lv_Quality.Items.Clear()
            SQL = "select a.no_faktur, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Quality_Awal, a.Quality_Tujuan, a.keterangan, a.Tanggal, a.Jam, a.Total_Stock, a.Total_Bags, a.Satuan, a.UserID "
            SQL = SQL & "from Emi_Transfer_Quality a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "ad "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by a.Tanggal asc, a.Jam asc "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_Quality.Items.Add(Dr("no_faktur"))
                    lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))

                    If Dr("Quality_Awal").ToString.ToUpper = "GOOD_STOCK" Then
                        lv.SubItems.Add("Good Stock")
                    ElseIf Dr("Quality_Awal").ToString.ToUpper = "WARNING_STOCK" Then
                        lv.SubItems.Add("Warning Stock")
                    ElseIf Dr("Quality_Awal").ToString.ToUpper = "BAD_STOCK" Then
                        lv.SubItems.Add("Bad Stock")
                    End If

                    If Dr("Quality_Tujuan").ToString.ToUpper = "GOOD_STOCK" Then
                        lv.SubItems.Add("Good Stock")
                    ElseIf Dr("Quality_Tujuan").ToString.ToUpper = "WARNING_STOCK" Then
                        lv.SubItems.Add("Warning Stock")
                    ElseIf Dr("Quality_Tujuan").ToString.ToUpper = "BAD_STOCK" Then
                        lv.SubItems.Add("Bad Stock")
                    End If

                    lv.SubItems.Add(Dr("keterangan"))
                    lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("Jam"))
                    lv.SubItems.Add(Dr("Total_Stock"))
                    lv.SubItems.Add(Dr("Total_Bags"))
                    lv.SubItems.Add(Dr("Satuan"))
                    lv.SubItems.Add(Dr("UserID"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ComboBox1.Items.Clear() : arrCari.Clear()
        ComboBox1.Items.Add("No Faktur") : arrCari.Add("a.No_Faktur")
        ComboBox1.Items.Add("Kode Barang") : arrCari.Add("a.Kode_Barang")
        ComboBox1.Items.Add("Nama Barang") : arrCari.Add("b.Nama")
        ComboBox1.Items.Add("Satuan") : arrCari.Add("a.Satuan")
        ComboBox1.Items.Add("Keterangan") : arrCari.Add("a.Keterangan")

    End Sub

    Private Sub Intial_ListView_Quality()

        Lv_Quality.Columns.Clear()
        Lv_Quality.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_Quality.Columns.Add("Lokasi", 130, HorizontalAlignment.Left)
        Lv_Quality.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Quality.Columns.Add("Nama", 250, HorizontalAlignment.Left)
        Lv_Quality.Columns.Add("Quality Awal", 120, HorizontalAlignment.Center)
        Lv_Quality.Columns.Add("Quality Akhir", 120, HorizontalAlignment.Center)
        Lv_Quality.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
        Lv_Quality.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_Quality.Columns.Add("Jam", 120, HorizontalAlignment.Center)
        Lv_Quality.Columns.Add("Total Stock", 80, HorizontalAlignment.Center)
        Lv_Quality.Columns.Add("Total Bags", 80, HorizontalAlignment.Center)
        Lv_Quality.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Quality.Columns.Add("User", 130, HorizontalAlignment.Left)

        Lv_Quality.View = View.Details

        Lv_Quality_Detail.Columns.Clear()
        Lv_Quality_Detail.Columns.Add("Serial Number", 280, HorizontalAlignment.Left)
        Lv_Quality_Detail.Columns.Add("Batch Number", 280, HorizontalAlignment.Left)
        Lv_Quality_Detail.Columns.Add("Position", 200, HorizontalAlignment.Left)
        Lv_Quality_Detail.Columns.Add("Jumlah Stock", 100, HorizontalAlignment.Center)
        Lv_Quality_Detail.Columns.Add("Jumlah Bags", 100, HorizontalAlignment.Center)

        Lv_Quality_Detail.View = View.Details

    End Sub

    Private Sub Get_Quality_Data(ByVal index As Integer)

        lv_NoFak = Lv_Quality.Items(index).SubItems(item_NoFak).Text
        Lv_KdSO = Lv_Quality.Items(index).SubItems(item_KdSo).Text
        Lv_Kd_Barang = Lv_Quality.Items(index).SubItems(item_KdBarang).Text
        Lv_Tot_Stock = Lv_Quality.Items(index).SubItems(item_Tot_Stock).Text
        Lv_Tot_Bags = Lv_Quality.Items(index).SubItems(item_Tot_Bags).Text
        Lv_Satuan = Lv_Quality.Items(index).SubItems(item_Satuan).Text
        Lv_UserTransfer = Lv_Quality.Items(index).SubItems(item_User_Tf).Text

    End Sub

    Private Sub Lv_Quality_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Quality.SelectedIndexChanged
        If Lv_Quality.Items.Count = 0 Then Exit Sub

        Get_Quality_Data(Lv_Quality.FocusedItem.Index)

        If Lv_KdSO.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Lv_Quality_Detail.Items.Clear()
            SQL = "select a.Serial_Number, a.Batch_Number, b.Keterangan, a.Jumlah_Stock, a.Jumlah_Bags "
            SQL = SQL & "from Emi_Transfer_Quality_det a, View_Warehouse_Position b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Id_Warehouse = b.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur ='" & lv_NoFak & "'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_Quality_Detail.Items.Add(Dr("Serial_Number"))
                    lv.SubItems.Add(Dr("Batch_Number"))
                    lv.SubItems.Add(Dr("Keterangan"))
                    lv.SubItems.Add(Dr("Jumlah_Stock"))
                    lv.SubItems.Add(Dr("Jumlah_Bags"))
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
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            CheckBox1.Focus() : Exit Sub
        ElseIf CmbSO_Asal.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi Harus harus diisi!", Judul)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        If CheckBox2.Checked = True Then
            If DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                Exit Sub
            End If
        End If

        If CheckBox3.Checked = True Then
            If ComboBox1.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                ComboBox1.Focus() : Exit Sub
            ElseIf TextBox1.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                TextBox1.Focus() : Exit Sub
            End If

        End If

        Try
            OpenConn()

            Lv_Quality.Items.Clear() : Lv_Quality_Detail.Items.Clear()
            SQL = "select a.no_faktur, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama, a.Quality_Awal, a.Quality_Tujuan, a.keterangan, a.Tanggal, a.Jam, a.Total_Stock, a.Total_Bags, a.Satuan, a.UserID "
            SQL = SQL & "from Emi_Transfer_Quality a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Stock_Owner='" & CmbSO_Asal.Text & "' "

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

                SQL = SQL & arrCari.Item(ComboBox1.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
            End If

            SQL = SQL & "order by a.Tanggal desc, a.Jam desc "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_Quality.Items.Add(Dr("no_faktur"))
                    lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))

                    If Dr("Quality_Awal").ToString.ToUpper = "GOOD_STOCK" Then
                        lv.SubItems.Add("Good Stock")
                    ElseIf Dr("Quality_Awal").ToString.ToUpper = "WARNING_STOCK" Then
                        lv.SubItems.Add("Warning Stock")
                    ElseIf Dr("Quality_Awal").ToString.ToUpper = "BAD_STOCK" Then
                        lv.SubItems.Add("Bad Stock")
                    End If

                    If Dr("Quality_Tujuan").ToString.ToUpper = "GOOD_STOCK" Then
                        lv.SubItems.Add("Good Stock")
                    ElseIf Dr("Quality_Tujuan").ToString.ToUpper = "WARNING_STOCK" Then
                        lv.SubItems.Add("Warning Stock")
                    ElseIf Dr("Quality_Tujuan").ToString.ToUpper = "BAD_STOCK" Then
                        lv.SubItems.Add("Bad Stock")
                    End If

                    lv.SubItems.Add(Dr("keterangan"))
                    lv.SubItems.Add(Dr("Tanggal"))
                    lv.SubItems.Add(Dr("Jam"))
                    lv.SubItems.Add(Dr("Total_Stock"))
                    lv.SubItems.Add(Dr("Total_Bags"))
                    lv.SubItems.Add(Dr("Satuan"))
                    lv.SubItems.Add(Dr("UserID"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CetakUlangFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangFakturToolStripMenuItem.Click

        If Lv_Quality.Items.Count = 0 Or Lv_Quality.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try

            OpenConn()

            SQL = "select a.No_Faktur "
            SQL = SQL & "from Emi_Transfer_Quality a, barang b, Emi_Transfer_Quality_det c, View_Warehouse_Position d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.No_Faktur = c.No_Faktur "
            SQL = SQL & "and c.Id_Warehouse = d.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & Lv_Quality.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Rpt_Faktur_Transfer_Quality       'Nama file CR
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{Emi_Transfer_Quality.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Emi_Transfer_Quality.No_Faktur} = '" & Lv_Quality.FocusedItem.Text & "'"
                        .Text = "Faktur Transfer Quality"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .CrystalReportViewer1.DisplayGroupTree = False
                        .Refresh()
                        .Show()
                    End With
                Else
                    MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            Btn_Cari_Click(CheckBox1, e)
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox1.Checked = False
            DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
        Else
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            ComboBox1.Enabled = True : TextBox1.Enabled = True
        Else
            ComboBox1.Enabled = False : TextBox1.Enabled = False
            ComboBox1.SelectedIndex = -1 : TextBox1.Text = ""
        End If
    End Sub



End Class