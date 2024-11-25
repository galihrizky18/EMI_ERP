Public Class Emi_Display_Transfer_Stock

    Dim arrCari As New ArrayList

    Dim Lv_KdTransfer As String

    Dim item_KdTransfer As Integer = 0


    Private Sub Emi_Display_Transfer_Stock_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Display_Transfer_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")


        Intial_ListView_Stock()
        kosong()
    End Sub


    Private Sub kosong()

        Lv_Stock.Items.Clear()
        Lv_Stock_Detail.Items.Clear()


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

            Lv_Stock.Items.Clear()
            SQL = "select a.Kode_Transfer, a.Jenis_Transfer, a.Kode_Barang, b.Nama, a.SO_Awal, a.SO_Tujuan, a.Keterangan, a.Tanggal, a.Jam, a.Total, a.Total_Transfer_Bags, a.Satuan, a.UserID "
            SQL = SQL & "from Tf_Stock a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.so_awal = B.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by a.Tanggal desc, a.Jam desc "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_Stock.Items.Add(Dr("Kode_Transfer"))
                    lv.SubItems.Add(Dr("Jenis_Transfer"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("SO_Awal"))
                    lv.SubItems.Add(Dr("SO_Tujuan"))
                    lv.SubItems.Add(Dr("Keterangan"))
                    lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("Jam"))
                    lv.SubItems.Add(Dr("Total"))
                    lv.SubItems.Add(Dr("Total_Transfer_Bags"))
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
        ComboBox1.Items.Add("Kode Transfer") : arrCari.Add("a.Kode_Transfer")
        ComboBox1.Items.Add("Lokasi Awal") : arrCari.Add("a.SO_Awal")
        ComboBox1.Items.Add("Lokasi Akhir") : arrCari.Add("a.SO_Tujuan")
        ComboBox1.Items.Add("Kode Barang") : arrCari.Add("a.Kode_Barang")
        ComboBox1.Items.Add("Keterangan") : arrCari.Add("a.Keterangan")

    End Sub

    Private Sub Intial_ListView_Stock()

        Lv_Stock.Columns.Clear()
        Lv_Stock.Columns.Add("Kode Transfer", 150, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Jenis Transfer", 150, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Nama", 180, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Lokasi Awal", 150, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Lokasi Akhir", 150, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Keterangan", 330, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Tanggal", 120, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Jam", 120, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Total Transfer", 100, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Total Bags", 100, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("User", 130, HorizontalAlignment.Left)

        Lv_Stock.View = View.Details

        Lv_Stock_Detail.Columns.Clear()
        Lv_Stock_Detail.Columns.Add("Rak Awal", 200, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Rak Tujuan", 200, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Serial Number Awal", 250, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Serial Number Akhir", 250, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Jumlah Input", 100, HorizontalAlignment.Center)
        Lv_Stock_Detail.Columns.Add("Actual Items ", 100, HorizontalAlignment.Center)
        Lv_Stock_Detail.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Stock_Detail.Columns.Add("Status Stock", 140, HorizontalAlignment.Center)
        Lv_Stock_Detail.Columns.Add("Tanggal Potong", 100, HorizontalAlignment.Center)
        Lv_Stock_Detail.Columns.Add("Jam Potong", 100, HorizontalAlignment.Center)
        Lv_Stock_Detail.Columns.Add("User", 130, HorizontalAlignment.Left)

        Lv_Stock_Detail.View = View.Details

    End Sub

    Private Sub Get_Tf_Stock_Listview(ByVal index As Integer)
        Lv_KdTransfer = Lv_Stock.Items(index).SubItems(item_KdTransfer).Text

    End Sub

    Private Sub Lv_Stock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Stock.SelectedIndexChanged

        If Lv_Stock.Items.Count = 0 Then Exit Sub

        Get_Tf_Stock_Listview(Lv_Stock.FocusedItem.Index)

        If Lv_KdTransfer.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Lv_Stock_Detail.Items.Clear()
            SQL = "select a.No_Faktur, "
            SQL = SQL & "ISNULL((select z.Keterangan from View_Warehouse_Position z where a.Id_Wms_Awal = z.Id_WMS_Warehouse_Position),'-') as Rak_Awal, "
            SQL = SQL & "ISNULL((select z.Keterangan from View_Warehouse_Position z where a.Id_Wms_Tujuan = z.Id_WMS_Warehouse_Position),'-') as Rak_Tujuan, "
            SQL = SQL & "a.Serial_Number_Awal, a.Serial_Number_Akhir, "
            SQL = SQL & "ISNULL((select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',b.Kode_Barang, a.Satuan, b.Satuan, a.Jumlah )), '0') as Jumlah_Input, "
            SQL = SQL & "ISNULL((select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',b.Kode_Barang, a.Satuan, b.Satuan, a.Jumlah_Pot_Stock )), '0') as Actual_Items, "
            SQL = SQL & "b.Satuan as satuan, "
            SQL = SQL & "case when a.Flag_Pot_Stock = 'Y' then 'Stock Dipotong' else 'Belum Dipotong' end as Status_Stock, "
            SQL = SQL & "a.Tanggal_Pot_Stock, a.Jam_Pot_Stock, a.UserId_Pot_Stock "
            SQL = SQL & "from Tf_Stock_det a, Tf_Stock b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.Kode_Transfer "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur='" & Lv_KdTransfer & "' "
            SQL = SQL & "order by a.No_Faktur desc"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_Stock_Detail.Items.Add(Dr("Rak_Awal"))
                    lv.SubItems.Add(Dr("Rak_Tujuan"))
                    lv.SubItems.Add(Dr("Serial_Number_Awal"))
                    If General_Class.CekNULL(Dr("Serial_Number_Akhir")) = "" Then
                        lv.SubItems.Add("-")
                    Else
                        lv.SubItems.Add(Dr("Serial_Number_Akhir"))
                    End If

                    lv.SubItems.Add(Dr("Jumlah_Input"))
                    lv.SubItems.Add(Dr("Actual_Items"))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Dr("Status_Stock"))
                    If General_Class.CekNULL(Dr("Tanggal_Pot_Stock")) = "" Then
                        lv.SubItems.Add("-")
                        lv.SubItems.Add("-")
                        lv.SubItems.Add("-")
                    Else
                        lv.SubItems.Add(Format(Dr("Tanggal_Pot_Stock"), "dd MMM yyyy"))
                        lv.SubItems.Add(General_Class.CekNULL(Dr("Jam_Pot_Stock")))
                        lv.SubItems.Add(General_Class.CekNULL(Dr("UserId_Pot_Stock")))
                    End If

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
        If Lv_Stock.Items.Count = 0 Or Lv_Stock.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try

            OpenConn()

            SQL = "select a.Kode_Transfer "
            SQL = SQL & "from Tf_Stock a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.so_awal = B.Kode_Stock_Owner "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Transfer='" & Lv_Stock.FocusedItem.Text & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Rpt_Faktur_Transfer_Stock       'Nama file CR
                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{Tf_Stock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Tf_Stock.Kode_Transfer} = '" & Lv_Stock.FocusedItem.Text & "'"
                        .Text = "Faktur Transfer Stock"
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

            Lv_Stock.Items.Clear() : Lv_Stock_Detail.Items.Clear()
            SQL = "select a.Kode_Transfer, a.Jenis_Transfer, a.Kode_Barang, b.Nama, a.SO_Awal, a.SO_Tujuan, a.Keterangan, a.Tanggal, a.Jam, a.Total, a.Total_Transfer_Bags, a.Satuan, a.UserID "
            SQL = SQL & "from Tf_Stock a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.so_awal = B.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

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

            SQL = SQL & "order by a.Tanggal asc, a.Jam asc "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_Stock.Items.Add(Dr("Kode_Transfer"))
                    lv.SubItems.Add(Dr("Jenis_Transfer"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("SO_Awal"))
                    lv.SubItems.Add(Dr("SO_Tujuan"))
                    lv.SubItems.Add(Dr("Keterangan"))
                    lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("Jam"))
                    lv.SubItems.Add(Dr("Total"))
                    lv.SubItems.Add(Dr("Total_Transfer_Bags"))
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