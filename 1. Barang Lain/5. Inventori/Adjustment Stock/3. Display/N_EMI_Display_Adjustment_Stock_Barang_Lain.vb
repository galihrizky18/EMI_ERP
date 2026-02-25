Public Class N_EMI_Display_Adjustment_Stock_Barang_Lain

    Dim arrLokasi, arrTanggal, arrParamLain As New ArrayList

    Private Sub Emi_Display_Adjusment_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()
    End Sub



    Private Sub Kosong()
        Lv_Adjustment.Columns.Clear()
        Lv_Adjustment.Columns.Add("No Faktur", 200, HorizontalAlignment.Left)
        Lv_Adjustment.Columns.Add("Lokasi", 200, HorizontalAlignment.Left)
        Lv_Adjustment.Columns.Add("Kode SO", 200, HorizontalAlignment.Left)
        Lv_Adjustment.Columns.Add("Keterangan", 350, HorizontalAlignment.Left)
        Lv_Adjustment.Columns.Add("Tanggal", 180, HorizontalAlignment.Center)
        Lv_Adjustment.Columns.Add("User ID", 150, HorizontalAlignment.Center)
        Lv_Adjustment.View = View.Details

        Lv_Retur_Detail.Columns.Clear()
        Lv_Retur_Detail.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Retur_Detail.Columns.Add("Nama", 400, HorizontalAlignment.Left)
        Lv_Retur_Detail.Columns.Add("Total Tambah", 150, HorizontalAlignment.Right)
        Lv_Retur_Detail.Columns.Add("Total Kurang", 150, HorizontalAlignment.Right)
        Lv_Retur_Detail.Columns.Add("Total Bags Tambah", 150, HorizontalAlignment.Right)
        Lv_Retur_Detail.Columns.Add("Total Bags Kurang", 150, HorizontalAlignment.Right)
        Lv_Retur_Detail.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        'Hide
        Lv_Retur_Detail.Columns.Add("urut", 0, HorizontalAlignment.Center)
        Lv_Retur_Detail.View = View.Details

        Lv_Retur_Mobil.Columns.Clear()
        Lv_Retur_Mobil.Columns.Add("Rak", 150, HorizontalAlignment.Left)
        Lv_Retur_Mobil.Columns.Add("Jumlah", 150, HorizontalAlignment.Right)
        Lv_Retur_Mobil.Columns.Add("Jumlah Bags", 140, HorizontalAlignment.Right)
        Lv_Retur_Mobil.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Retur_Mobil.Columns.Add("Kualitas", 150, HorizontalAlignment.Center)
        Lv_Retur_Mobil.View = View.Details

        Try
            OpenConn()

            '============================
            '=     LOAD DATA FILTER     =
            '============================
            Cmb1.Items.Clear() : arrLokasi.Clear()
            SQL = "select kode_stock_owner, keterangan from Stock_Owner "
            Using Dr = OpenTrans(SQL)
                Cmb1.Items.Add("--- SELURUH ---") : arrLokasi.Add("--- SELURUH ---")
                Do While Dr.Read
                    Cmb1.Items.Add(Dr("keterangan")) : arrLokasi.Add(Dr("kode_stock_owner"))
                Loop
                Cmb1.SelectedIndex = 0
            End Using

            Cmb_Tanggal.Items.Clear() : arrTanggal.Clear() : DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
            Cmb_Tanggal.Items.Add("Tanggal") : arrTanggal.Add("a.Tanggal")

            Cmb_ParamLain.Items.Clear() : arrParamLain.Clear() : Txt_ParamLain.Text = ""
            Cmb_ParamLain.Items.Add("No Transaksi") : arrParamLain.Add("a.No_Faktur")
            Cmb_ParamLain.Items.Add("Lokasi") : arrParamLain.Add("a.Kode_Stock_Owner")
            Cmb_ParamLain.Items.Add("User ID") : arrParamLain.Add("a.UserID")
            Cmb_ParamLain.Items.Add("Keterangan") : arrParamLain.Add("a.Keterangan")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Btn_Cari_Click(Btn_Cari, New EventArgs)


    End Sub



    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Try
            OpenConn()

            Lv_Adjustment.Items.Clear() : Lv_Retur_Detail.Items.Clear() : Lv_Retur_Mobil.Items.Clear()
            SQL = "Select a.Kode_Perusahaan, a.Lokasi, a.No_Faktur, a.Kode_Stock_Owner, a.Tanggal, a.Jam, a.Keterangan, a.UserID "
            SQL = SQL & "from Emi_Adjustment_Stock a "
            SQL = SQL & "where  a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.status is null "

            If Not Cmb1.SelectedIndex = 0 Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Lokasi = '" & arrLokasi(Cmb1.SelectedIndex) & "' "
            End If

            If Chk_HariIni.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal Between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(DateAdd(DateInterval.Day, 1, Now), "yyyy-MM-dd") & "' "

            End If

            If Chk_Tanggal.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If Chk_ParamLain.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrParamLain.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
            End If

            SQL = SQL & "order by a.No_Faktur, a.Tanggal, a.Jam"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Adjustment.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
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

    Private Sub Lv_Adjustment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Adjustment.SelectedIndexChanged

        If Lv_Adjustment.Items.Count = 0 OrElse Lv_Adjustment.FocusedItem.Index = -1 OrElse Lv_Adjustment.FocusedItem.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Dim NoTransaksi As String = Lv_Adjustment.FocusedItem.Text

            Lv_Retur_Detail.Items.Clear() : Lv_Retur_Mobil.Items.Clear()

            SQL = "select a.No_Faktur, b.Kode_Barang, c.Nama, b.Total_Tambah, b.Total_Minus, b.Total_Bags_Tambah, b.Total_Bags_Minus, b.Satuan, b.Urut "
            SQL = SQL & "from Emi_Adjustment_Stock a, Emi_Adjustment_Stock_Detail b, barang c "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransaksi & "' "
            SQL = SQL & "and a.Status is null "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Retur_Detail.Items.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Format(Dr("Total_Tambah"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Total_Minus"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Total_Bags_Tambah"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Total_Bags_Minus"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    'hide
                    Lv.SubItems.Add(Dr("Urut"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Retur_Detail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Retur_Detail.SelectedIndexChanged
        If Lv_Retur_Detail.Items.Count = 0 OrElse Lv_Retur_Detail.FocusedItem.Index = -1 OrElse Lv_Retur_Detail.FocusedItem.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Dim NoTransaksi As String = Lv_Adjustment.FocusedItem.Text
            Dim Urut As String = Lv_Retur_Detail.FocusedItem.SubItems(7).Text

            Lv_Retur_Mobil.Items.Clear()
            SQL = "select a.No_Faktur, c.Id_Wms, d.Keterangan as Rak, c.Jumlah, c.Jumlah_Bags, e.Keterangan as Kualitas, b.Satuan "
            SQL = SQL & "from Emi_Adjustment_Stock a, Emi_Adjustment_Stock_Detail b, Emi_Adjustment_Stock_Det c, View_Warehouse_Position d, EMI_Master_Warna e "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut = c.Urut_Adj "
            SQL = SQL & "and c.Id_Wms = d.Id_WMS_Warehouse_Position "
            SQL = SQL & "and c.Warna = e.Kode_Warna "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransaksi & "' "
            SQL = SQL & "and b.Urut = '" & Urut & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "order by a.No_Faktur, c.Id_Wms, c.Warna"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem

                    Lv = Lv_Retur_Mobil.Items.Add(Dr("Rak"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Kualitas"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Chk_HariIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_HariIni.CheckedChanged
        If Chk_HariIni.Checked = True Then
            Chk_Tanggal.Checked = False
            Btn_Cari_Click(Chk_Tanggal, e)
        End If
    End Sub

    Private Sub Chk_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Tanggal.CheckedChanged

        If Chk_Tanggal.Checked Then
            Cmb_Tanggal.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Chk_HariIni.Checked = False
        Else
            Cmb_Tanggal.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_Tanggal.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub
    Private Sub Chk_ParamLain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_ParamLain.CheckedChanged
        If Chk_ParamLain.Checked = True Then
            Cmb_ParamLain.Enabled = True : Txt_ParamLain.Enabled = True
        Else
            Cmb_ParamLain.Enabled = False : Txt_ParamLain.Enabled = False
            Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamLain.Text = ""
        End If
    End Sub


End Class
