

Public Class EMI_Display_Pengeluaran_Stock_Barang_Lain


    Dim arrCari As New ArrayList
    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList

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

            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1 : Arr1.Clear()
            CmbSO_Asal.Items.Add("--- SELURUH ---")
            SQL = "Select a.kode_stock_owner, a.inisial_faktur, a.pending_persediaan, a.persediaan, a.Keterangan "
            SQL = SQL & "From Stock_Owner_Gudang_Lain a, Binding_Lokasi_Gudang_Lain b where "
            SQL = SQL & "a.kode_Perusahaan=b.kode_Perusahaan and a.kode_stock_owner=b.kode_stock_owner_Gudang and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_stock_owner='" & Lokasi & "' "
            SQL = SQL & "and a.aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by a.kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSO_Asal.Items.Add(dr("Keterangan")) : Arr1.Add(dr("kode_stock_owner"))
                Loop
            End Using
            CmbSO_Asal.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_FIlterTanggal.Enabled = False : Cmb_Filter_ParamLain.Enabled = False
        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        Txt_ParamLain.Enabled = False


        Cmb_FIlterTanggal.Items.Clear() : Arr2.Clear()
        Cmb_FIlterTanggal.Items.Add("Tanggal") : Arr2.Add("a.Tanggal")

        Cmb_Filter_ParamLain.Items.Clear() : arrCari.Clear()
        Cmb_Filter_ParamLain.Items.Add("Kode Transfer") : arrCari.Add("a.no_faktur")
        Cmb_Filter_ParamLain.Items.Add("Jenis Transfer") : arrCari.Add("a.Jenis_Transfer")
        Cmb_Filter_ParamLain.Items.Add("Lokasi Akhir") : arrCari.Add("a.SO_Tujuan")
        Cmb_Filter_ParamLain.Items.Add("Keterangan") : arrCari.Add("a.Keterangan")
        'ComboBox1.Items.Add("Lokasi Awal") : arrCari.Add("a.SO_Awal")
        'Cmb_Filter_ParamLain.Items.Add("Kode Barang") : arrCari.Add("b.Kode_Barang")

    End Sub

    Private Sub Intial_ListView_Stock()

        Lv_Stock.Columns.Clear()
        Lv_Stock.Columns.Add("Kode Transfer", 130, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Jenis Transfer", 0, HorizontalAlignment.Center)
        'Lv_Stock.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        'Lv_Stock.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Lokasi", 150, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Lokasi Akhir", 0, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Jam", 80, HorizontalAlignment.Center)
        '   Lv_Stock.Columns.Add("Total Transfer", 100, HorizontalAlignment.Right)
        ' Lv_Stock.Columns.Add("Total Bags", 100, HorizontalAlignment.Right)
        ' Lv_Stock.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("User", 100, HorizontalAlignment.Center).DisplayIndex = 7

        Lv_Stock.View = View.Details

        Lv_Stock_Detail.Columns.Clear()
        'Lv_Stock_Detail.Columns.Add("Rak Awal", 200, HorizontalAlignment.Left)
        'Lv_Stock_Detail.Columns.Add("Rak Tujuan", 200, HorizontalAlignment.Left)
        'Lv_Stock_Detail.Columns.Add("Serial Number Awal", 0, HorizontalAlignment.Left)
        'Lv_Stock_Detail.Columns.Add("Serial Number Akhir", 0, HorizontalAlignment.Left)
        'Lv_Stock_Detail.Columns.Add("Jumlah Input", 150, HorizontalAlignment.Right)
        'Lv_Stock_Detail.Columns.Add("Jumlah Aktual", 150, HorizontalAlignment.Right)
        'Lv_Stock_Detail.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        'Lv_Stock_Detail.Columns.Add("Status", 160, HorizontalAlignment.Center)
        'Lv_Stock_Detail.Columns.Add("Tanggal", 0, HorizontalAlignment.Center)
        'Lv_Stock_Detail.Columns.Add("Jam Potong", 0, HorizontalAlignment.Center)
        'Lv_Stock_Detail.Columns.Add("User", 0, HorizontalAlignment.Left)

        Lv_Stock_Detail.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Nama", 350, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Total", 130, HorizontalAlignment.Right)
        Lv_Stock_Detail.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_Stock_Detail.Columns.Add("Total Bags", 130, HorizontalAlignment.Right)

        Lv_Stock_Detail.View = View.Details


        LvwAwal.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        LvwAwal.Columns.Add("No Pallet", 0, HorizontalAlignment.Center)
        LvwAwal.Columns.Add("Qr Code", 400, HorizontalAlignment.Left)
        LvwAwal.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        LvwAwal.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        LvwAwal.Columns.Add("Jumlah Bags", 130, HorizontalAlignment.Right)

        LvwAwal.View = View.Details





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
            'SQL = "select a.No_Faktur, "
            'SQL = SQL & "ISNULL((select z.Keterangan from View_Warehouse_Position z where a.Id_Wms_Awal = z.Id_WMS_Warehouse_Position),'-') as Rak_Awal, "
            'SQL = SQL & "ISNULL((select z.Keterangan from View_Warehouse_Position z where a.Id_Wms_Tujuan = z.Id_WMS_Warehouse_Position),'-') as Rak_Tujuan, "
            'SQL = SQL & "a.Serial_Number_Awal, a.Serial_Number_Akhir, "
            'SQL = SQL & "ISNULL((select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',b.Kode_Barang, a.Satuan, b.Satuan, a.Jumlah )), '0') as Jumlah_Input, "
            'SQL = SQL & "ISNULL((select dbo.ubah_satuan(a.Kode_Perusahaan, 'masa',b.Kode_Barang, a.Satuan, b.Satuan, a.Jumlah_Pot_Stock )), '0') as Actual_Items, "
            'SQL = SQL & "b.Satuan as satuan, "
            'SQL = SQL & "case when a.Flag_Sudah_Cetak = 'Y' then 'SUBMITTED' else 'UNSUBMITTED' end as Status_Stock, "
            'SQL = SQL & "a.Tanggal_Pot_Stock, a.Jam_Pot_Stock, a.UserId_Pot_Stock "
            'SQL = SQL & "from Tf_Stock_det a, Tf_Stock b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Faktur = b.Kode_Transfer "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur='" & Lv_KdTransfer & "' "
            'SQL = SQL & "order by a.No_Faktur desc"
            SQL = "select b.Kode_Barang,c.Nama,b.total,b.satuan,b.total_bags "
            SQL = SQL & "From EMI_Pengeluaran_Stock_Parent_Barang_lain a, EMI_Pengeluaran_Stock_Barang_lain b, barang_lain c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan  = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_Stock.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem

                    lv = Lv_Stock_Detail.Items.Add(Dr("kode_barang"))
                    lv.SubItems.Add(Dr("nama"))
                    lv.SubItems.Add(Format(Dr("total"), "N2"))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Format(Dr("total_bags"), "N2"))


                    'lv = Lv_Stock_Detail.Items.Add(Dr("Rak_Awal"))
                    'lv.SubItems.Add(Dr("Rak_Tujuan"))
                    'lv.SubItems.Add(Dr("Serial_Number_Awal"))
                    'If General_Class.CekNULL(Dr("Serial_Number_Akhir")) = "" Then
                    '    lv.SubItems.Add("-")
                    'Else
                    '    lv.SubItems.Add(Dr("Serial_Number_Akhir"))
                    'End If

                    'lv.SubItems.Add(Format(Dr("Jumlah_Input"), "N2"))
                    'lv.SubItems.Add(Format(Dr("Actual_Items"), "N2"))
                    'lv.SubItems.Add(Dr("satuan"))
                    'lv.SubItems.Add(Dr("Status_Stock"))

                    'If General_Class.CekNULL(Dr("Tanggal_Pot_Stock")) = "" Then
                    '    lv.SubItems.Add("-")
                    '    lv.SubItems.Add("-")
                    '    lv.SubItems.Add("-")
                    'Else
                    '    lv.SubItems.Add(Format(Dr("Tanggal_Pot_Stock"), "dd MMM yyyy"))
                    '    lv.SubItems.Add(General_Class.CekNULL(Dr("Jam_Pot_Stock")))
                    '    lv.SubItems.Add(General_Class.CekNULL(Dr("UserId_Pot_Stock")))
                    'End If

                Loop

            End Using

            LvwAwal.Items.Clear()
            SQL = "select b.Labeling_WMS_Position,a.No_Pallet_Awal as No_Pallet, "
            SQL = SQL & "c.Qr_Code + '-' + c.Kode_Unik_Berjalan as Qr_Code, a.Jumlah,d.Satuan,a.Jumlah_Bags "
            SQL = SQL & "from EMI_Pengeluaran_Stock_det_barang_lain a, View_Warehouse_Position_barang_lain b, Barang_lain_SN c, EMI_Pengeluaran_Stock_barang_lain d where   "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Wms_Awal = b.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Serial_Number_Awal = c.Serial_Number   "
            SQL = SQL & "and a.Kode_Perusahaan = d.Kode_Perusahaan and a.Urut_TF = d.Urut_Oto and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Lv_Stock.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem

                    lv = LvwAwal.Items.Add(Dr("Labeling_WMS_Position"))
                    lv.SubItems.Add(Dr("no_pallet"))
                    lv.SubItems.Add(Dr("Qr_Code"))
                    lv.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Format(Dr("jumlah_bags"), "N2"))
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

            'SQL = "select a.Kode_Transfer "
            'SQL = SQL & "from Pengeluaran_Stock a, barang b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            'SQL = SQL & "and a.so_awal = B.Kode_Stock_Owner "
            'SQL = SQL & "and a.Status is null "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.Kode_Transfer='" & Lv_Stock.FocusedItem.Text & "' "
            'Using Ds = BindingTrans(SQL)
            '    If Ds.Tables("MyTable").Rows.Count <> 0 Then
            '        Dim CrDoc As New Rpt_Faktur_Transfer_Stock       'Nama file CR
            '        With A_Place_For_Printing2
            '            CrDoc.SetDataSource(Ds)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            CrDoc.RecordSelectionFormula = "{Tf_Stock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Tf_Stock.Kode_Transfer} = '" & Lv_Stock.FocusedItem.Text & "'"
            '            .Text = "Faktur Transfer Stock"
            '            .CrystalReportViewer1.ReportSource = CrDoc
            '            .CrystalReportViewer1.DisplayGroupTree = False
            '            .Refresh()
            '            .Show()
            '        End With
            '    Else
            '        MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    End If
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click
        If Chk_Transaksi_HariIni.Checked = False And Chk_Tanggal.Checked = False And Chk_Param_Lain.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            Chk_Transaksi_HariIni.Focus() : Exit Sub
        ElseIf CmbSO_Asal.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi Harus harus diisi!", Judul)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        If Chk_Tanggal.Checked = True Then
            If Not Cmb_FIlterTanggal.SelectedIndex = -1 Then
                If DateTimePicker1.Value > DateTimePicker2.Value Then
                    MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                    DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                    Exit Sub
                End If
            Else
                MessageBox.Show("Pilih Dahulu Tanggal yang akan Di Filter!", Judul)
                Cmb_FIlterTanggal.Focus() : Exit Sub
            End If
        End If

        If Chk_Param_Lain.Checked = True Then
            If Cmb_Filter_ParamLain.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                Cmb_Filter_ParamLain.Focus() : Exit Sub
            ElseIf Txt_ParamLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                Txt_ParamLain.Focus() : Exit Sub
            End If

        End If

        Try
            OpenConn()

            Lv_Stock.Items.Clear() : Lv_Stock_Detail.Items.Clear()
            SQL = "select a.No_Faktur, a.Kode_Stock_Owner, a.Keterangan, a.Tanggal, a.Jam, a.UserID, b.keterangan as Cost_Center  "
            SQL = SQL & "from EMI_Pengeluaran_Stock_Parent_barang_lain a, EMI_Master_Cost_Center b "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null and a.kode_Perusahaan=b.kode_Perusahaan and a.Id_Cost_Center=b.Id_Cost_Center "

            If Chk_Transaksi_HariIni.Checked = True Then
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

            If Chk_Param_Lain.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrCari.Item(Cmb_Filter_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
            End If

            If CmbSO_Asal.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Kode_Stock_Owner = '" & CmbSO_Asal.Text & "' "
            End If

            SQL = SQL & "order by a.Tanggal asc, a.Jam asc "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_Stock.Items.Add(Dr("No_Faktur"))
                    lv.SubItems.Add(Dr("Cost_Center"))
                    'lv.SubItems.Add(Dr("Kode_Barang"))
                    'lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add("")
                    lv.SubItems.Add(Dr("Keterangan"))
                    lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("Jam"))
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



    Private Sub Chk_Transaksi_HariIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Transaksi_HariIni.CheckedChanged
        If Chk_Transaksi_HariIni.Checked = True Then
            Chk_Tanggal.Checked = False
            BtnBarangMasuk_Cari_Click(Chk_Tanggal, e)
        End If
    End Sub

    'Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBox1.Checked = True Then
    '        CheckBox2.Checked = False
    '        Btn_Cari_Click(CheckBox1, e)
    '    End If
    'End Sub


    Private Sub Chk_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Tanggal.CheckedChanged
        If Chk_Tanggal.Checked Then
            Cmb_FIlterTanggal.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Chk_Transaksi_HariIni.Checked = False
        Else
            Cmb_FIlterTanggal.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_FIlterTanggal.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub Chk_Param_Lain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Param_Lain.CheckedChanged
        If Chk_Param_Lain.Checked = True Then
            Cmb_Filter_ParamLain.Enabled = True : Txt_ParamLain.Enabled = True
        Else
            Cmb_Filter_ParamLain.Enabled = False : Txt_ParamLain.Enabled = False
            Cmb_Filter_ParamLain.SelectedIndex = -1 : Txt_ParamLain.Text = ""
        End If
    End Sub


End Class