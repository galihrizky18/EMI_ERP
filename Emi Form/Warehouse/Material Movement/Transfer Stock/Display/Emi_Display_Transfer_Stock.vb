Imports System.IO

Public Class Emi_Display_Transfer_Stock

    Dim JudulForm As String = "Display Transfer Stock"

    Dim arrCari As New ArrayList
    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList

    Dim Lv_KdTransfer As String

    Dim item_KdTransfer As Integer = 0

    Dim itemDetail_NoFaktur As Integer = 0
    Dim itemDetail_KdBarang As Integer = 1

    Dim itemLvAwal_Lokasi As Integer = 0
    Dim itemLvAwal_NoPallet As Integer = 1
    Dim itemLvAwal_QrCode As Integer = 2
    Dim itemLvAwal_Jumlah As Integer = 3
    Dim itemLvAwal_JumlahAktual As Integer = 4
    Dim itemLvAwal_Satuan As Integer = 5
    Dim itemLvAwal_JmlhBags As Integer = 6
    Dim itemLvAwal_NoFak As Integer = 7
    Dim itemLvAwal_Urut As Integer = 8

    Dim itemLvAkhir_Lokasi As Integer = 0
    Dim itemLvAkhir_NoPallet As Integer = 1
    Dim itemLvAkhir_QrCode As Integer = 2
    Dim itemLvAkhir_Jumlah As Integer = 3
    Dim itemLvAkhir_Satuan As Integer = 4
    Dim itemLvAkhir_JumlahBags As Integer = 5

    Dim Random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

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
        LvwAwal.Items.Clear() : LvwAkhir.Items.Clear()

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1 : Arr1.Clear()
            CmbSO_Asal.Items.Add("--- SELURUH ---")
            SQL = "Select a.kode_stock_owner, a.inisial_faktur, a.pending_persediaan, a.persediaan, a.Keterangan "
            SQL = SQL & "From Stock_Owner_Gudang a, Binding_Lokasi_Gudang b where "
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

        CmbSO_Asal.Focus()

    End Sub

    Private Sub Intial_ListView_Stock()

        Lv_Stock.Columns.Clear()
        Lv_Stock.Columns.Add("Kode Transfer", 130, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Jenis Transfer", 120, HorizontalAlignment.Center)
        'Lv_Stock.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        'Lv_Stock.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Lokasi Awal", 150, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Lokasi Akhir", 150, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Keterangan", 390, HorizontalAlignment.Left)
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
        Lv_Stock_Detail.Columns.Add("NoFaktur", 0, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Kode Barang", 250, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Nama", 0, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Total", 150, HorizontalAlignment.Right)
        Lv_Stock_Detail.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_Stock_Detail.Columns.Add("Total Bags", 130, HorizontalAlignment.Right)

        Lv_Stock_Detail.View = View.Details

        LvwAwal.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        LvwAwal.Columns.Add("No Pallet", 0, HorizontalAlignment.Center)
        LvwAwal.Columns.Add("Qr Code", 200, HorizontalAlignment.Left)
        LvwAwal.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        LvwAwal.Columns.Add("Jumlah Aktual", 130, HorizontalAlignment.Right)
        LvwAwal.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        LvwAwal.Columns.Add("Jumlah Bags", 130, HorizontalAlignment.Right)
        'HIDE
        LvwAwal.Columns.Add("NoFak", 0, HorizontalAlignment.Right)
        LvwAwal.Columns.Add("UrutOTO", 0, HorizontalAlignment.Right)

        LvwAwal.View = View.Details

        LvwAkhir.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        LvwAkhir.Columns.Add("No Pallet", 0, HorizontalAlignment.Center)
        LvwAkhir.Columns.Add("Qr Code", 200, HorizontalAlignment.Left)
        LvwAkhir.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        LvwAkhir.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        LvwAkhir.Columns.Add("Jumlah Bags", 130, HorizontalAlignment.Right)

        LvwAkhir.View = View.Details

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
            SQL = "select a.no_faktur, b.Kode_Barang,c.Nama,b.total,b.satuan,b.total_bags "
            SQL = SQL & "From Tf_Stock_Parent a, Tf_Stock b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Perusahaan  = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.so_awal = c.Kode_Stock_Owner "
            'SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_Stock.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem

                    lv = Lv_Stock_Detail.Items.Add(Dr("no_faktur"))
                    lv.SubItems.Add(Dr("kode_barang"))
                    lv.SubItems.Add("X")
                    lv.SubItems.Add(Format(Dr("total"), "N4"))
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

            LvwAwal.Items.Clear() : LvwAkhir.Items.Clear()
            SQL = "select b.Labeling_WMS_Position,a.No_Pallet_Awal as No_Pallet, a.Selesai, "
            SQL = SQL & "c.Qr_Code + '-' + c.Kode_Unik_Berjalan as Qr_Code, a.Jumlah,  "

            SQL = SQL & "ISNULL(( dbo.ubah_satuan(a.kode_perusahaan, 'masa', d.kode_barang, d.satuan_barang, d.satuan, "
            SQL = SQL & "isnull(( select SUM(z.jumlah) from TF_Stock_Det2 z where z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur = a.No_Faktur and z.Urut_Det = a.Urut_Oto ), 0))), 0) as Jumlah_Actual,"

            SQL = SQL & "d.Satuan,a.Jumlah_Bags, a.No_Faktur, a.Urut_Oto "
            SQL = SQL & "from Tf_Stock_det a, View_Warehouse_Position b, Barang_SN c, Tf_Stock d where   "
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
                    lv.SubItems.Add(Format(Dr("jumlah"), "N4"))
                    lv.SubItems.Add(Format(Dr("Jumlah_Actual"), "N4"))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Format(Dr("jumlah_bags"), "N2"))
                    'HIDE
                    lv.SubItems.Add(Dr("No_Faktur"))
                    lv.SubItems.Add(Dr("Urut_Oto"))

                    If General_Class.CekNULL(Dr("Selesai")) = "" Then
                        lv.BackColor = Color.LightGray
                    ElseIf General_Class.CekNULL(Dr("Selesai")) = "Y" Then
                        lv.BackColor = Color.LightGreen
                    Else
                        lv.BackColor = Color.LightYellow
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

    Private Sub LvwAwal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LvwAwal.SelectedIndexChanged
        If LvwAwal.Items.Count = 0 Then Exit Sub

        Dim SelectedIndex As Integer = LvwAwal.FocusedItem.Index
        Dim noFaktur As String = LvwAwal.Items(SelectedIndex).SubItems(itemLvAwal_NoFak).Text
        Dim urutDet As String = LvwAwal.Items(SelectedIndex).SubItems(itemLvAwal_Urut).Text

        Try
            OpenConn()

            LvwAkhir.Items.Clear()
            SQL = "select d.Labeling_WMS_Position,a.No_Pallet as No_Pallet, "
            SQL = SQL & "c.Qr_Code + '-' + c.Kode_Unik_Berjalan as Qr_Code, "
            SQL = SQL & "dbo.Ubah_Satuan(a.Kode_Perusahaan,'MASA',e.Kode_Barang,e.Satuan_Barang, e.Satuan, a.Jumlah) as Jumlah, "
            SQL = SQL & "e.Satuan,a.Jumlah_Bags "
            SQL = SQL & "from TF_Stock_Det2 a, Tf_Stock_det b, Barang_SN c, View_Warehouse_Position d, Tf_Stock e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Urut_Det = b.Urut_Oto "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Serial_Number = c.Serial_Number "
            SQL = SQL & "and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Id_Wms_Tujuan = d.Id_WMS_Warehouse_Position "
            SQL = SQL & "and  b.Kode_Perusahaan = e.Kode_Perusahaan and b.Urut_TF = e.Urut_Oto "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & noFaktur & "' and a.urut_Det = '" & urutDet & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem

                    lv = LvwAkhir.Items.Add(Dr("Labeling_WMS_Position"))
                    lv.SubItems.Add(Dr("no_pallet"))
                    lv.SubItems.Add(Dr("Qr_Code"))
                    lv.SubItems.Add(Format(Dr("jumlah"), "N4"))
                    lv.SubItems.Add(Dr("satuan"))
                    If General_Class.CekNULL(Dr("jumlah_bags")) = "" Then
                        lv.SubItems.Add("0")
                    Else
                        lv.SubItems.Add(Format(Dr("jumlah_bags"), "N2"))
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
            SQL = "select a.No_Faktur, a.Jenis_Transfer, a.SO_Awal, a.SO_Tujuan, a.Keterangan, a.Tanggal, a.Jam, a.UserID, a.status  "
            SQL = SQL & "from Tf_Stock_Parent a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.Status is null "

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

                SQL = SQL & "a.SO_Awal = '" & CmbSO_Asal.Text & "' "
            End If

            SQL = SQL & "order by a.Tanggal asc, a.Jam asc "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_Stock.Items.Add(Dr("No_Faktur"))
                    lv.SubItems.Add(Dr("Jenis_Transfer"))
                    'lv.SubItems.Add(Dr("Kode_Barang"))
                    'lv.SubItems.Add(Dr("Nama"))
                    lv.SubItems.Add(Dr("SO_Awal"))
                    lv.SubItems.Add(Dr("SO_Tujuan"))
                    lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan")) = "", "", Dr("Keterangan")))
                    lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("Jam"))
                    lv.SubItems.Add(Dr("UserID"))

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        lv.BackColor = Color.DarkRed
                        lv.ForeColor = Color.White
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

    Private Sub CetakUlangBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeToolStripMenuItem.Click
        If Lv_Stock_Detail.Items.Count = 0 Then Exit Sub

        Dim selectedIndex As Integer = LvwAkhir.FocusedItem.Index
        Dim QrCode As String = LvwAkhir.Items(selectedIndex).SubItems(itemLvAkhir_QrCode).Text

        Dim kode_unik_print As String = ""

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '====================
            '=     GET DATA     =
            '====================
            Dim Qr_Code As String = ""
            Dim Kode_Unik_Berjalan As String = ""
            Dim Tgl_expired As String = ""
            Dim BatchNumber As String = ""
            Dim TglMasuk As String = ""
            Dim MetodePengeluaranStok As String = ""
            Dim serialNumber As String = ""

            Dim KdBarang As String = ""
            Dim NmBarang As String = ""

            '===========================
            '=       GET DATA QR       =
            '===========================
            SQL = "select a.Kode_Perusahaan, a.Qr_Code, a.Kode_Unik_Berjalan, a.Tgl_Expired, a.Batch_Number, a.Tgl_Masuk, a.Serial_Number, "
            SQL = SQL & "ISNULL(( select z.Metode_Pengeluaran_Stok from barang z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Kode_Stock_Owner = a.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang "
            SQL = SQL & "), '-') as Metode_Pengeluaran_Stok "
            SQL = SQL & "from barang_sn a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.Qr_Code + '-' + a.Kode_Unik_Berjalan = '" & QrCode & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Qr_Code = Dr("Qr_Code")
                    Kode_Unik_Berjalan = Dr("Kode_Unik_Berjalan")
                    BatchNumber = Dr("Batch_Number")

                    If General_Class.CekNULL(Dr("Tgl_Expired")) = "" Then
                        Tgl_expired = "NULL"
                    Else
                        Tgl_expired = Format(Dr("Tgl_Expired"), "yyyy-MM-dd")
                    End If

                    If General_Class.CekNULL(Dr("Tgl_Masuk")) = "" Then
                        TglMasuk = "NULL"
                    Else
                        TglMasuk = Format(Dr("Tgl_Masuk"), "yyyy-MM-dd")
                    End If

                    MetodePengeluaranStok = Dr("Metode_Pengeluaran_Stok")
                    serialNumber = Dr("Serial_Number")
                Else
                    Dr.Close()
                    CloseTrans()
                    MessageBox.Show("Data QR tidak ditemukan!", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    CloseConn()
                    Exit Sub
                End If
            End Using

            '===============================
            '=       GET DATA BARANG       =
            '===============================
            SQL = "select top 1 b.Kode_Barang, b.Nama "
            SQL = SQL & "from Barang_SN a, Barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Serial_Number = '" & serialNumber & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    KdBarang = Dr("Kode_Barang")
                    NmBarang = Dr("Nama")
                Else
                    Dr.Close()
                    CloseTrans()
                    MessageBox.Show("Data Barang tidak ditemukan!", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    CloseConn()
                    Exit Sub
                End If
            End Using

            '=====================================
            '=       GENERATE BARCODE BARU       =
            '=====================================
            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
            Dim fullNewQr As String = Qr_Code & "-" & Kode_Unik_Berjalan

            Barcode.Image = Generate_QR(fullNewQr)

            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")
            'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
            Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
            'End If

            fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
            FileSize1 = fs1.Length
            rawData1 = New Byte(FileSize1) {}
            fs1.Read(rawData1, 0, FileSize1)
            fs1.Close()
            Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1

            '===================================
            '=       INSERT BARCODE BARU       =
            '===================================
            Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

            SQL = "delete from Cetak_TransferStock where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            ExecuteTrans(SQL)

            SQL = "insert into Cetak_TransferStock (kode_perusahaan, kode_barang, Barcode, Nama, QrUtuh, Qr, Tgl_Expired, batch, tanggal_cetak, kode_unik_print,tanggal_masuk,metode_pengeluaran_stok) values "
            SQL = SQL & "('" & KodePerusahaan & "', '" & KdBarang & "', @newBarcode, '" & NmBarang & "', '" & fullNewQr & "', '" & Qr_Code & "', "
            SQL = SQL & If(Tgl_expired = "NULL", "NULL", $"'{Tgl_expired}'") & ", '" & BatchNumber & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "','" & kode_unik_print & "' , "
            SQL = SQL & If(TglMasuk = "NULL", "NULL", $"'{TglMasuk}'") & ", '" & MetodePengeluaranStok & "') "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '=================
        '=     CETAK     =
        '=================
        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            Dim kertasBarcodeBesar = "BarcodeFG"

            SQL = "select a.Kode_Perusahaan "
            SQL = SQL & "from Cetak_TransferStock a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Kode_Unik_Print = '" & kode_unik_print & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New NewBarcodeTransferStock

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unik_print & "'"

                    '    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    '    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    '    Dim rawKind As Integer
                    '    Dim isPaperFound As Boolean = False
                    '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    '    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertasBarcodeBesar Then
                    '            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '            CrDoc.PrintOptions.PaperSize = rawKind
                    '            isPaperFound = True
                    '            Exit For
                    '        End If
                    '    Next

                    '    If Not isPaperFound Then
                    '        CloseConn()
                    '        MessageBox.Show("Kertas Tidak DiTemukan", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If

                    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Transfer Stock"
                    '    .Text = "New Barcode Transfer Stock"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '==============================================================================

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{Cetak_TransferStock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_TransferStock.kode_unik_print} = '" & kode_unik_print & "' "

                    CrDoc.PrintOptions.PrinterName = PrinterBarcode

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    Dim rawKind As Integer
                    Dim isPaperFound As Boolean = False
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertasBarcodeBesar Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            isPaperFound = True
                            Exit For
                        End If
                    Next

                    If Not isPaperFound Then
                        'CloseConn()
                        MessageBox.Show("Kertas Tidak DiTemukan, Kertas di set ke default", JudulForm, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'Exit Sub
                    End If

                    CrDoc.PrintToPrinter(1, False, 1, 2500)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    '==================================================================================================================================================
    '=     MENU STRIP
    '==================================================================================================================================================

    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If Lv_Stock.Items.Count = 0 Or Lv_Stock.SelectedItems.Count = 0 Or Lv_Stock.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Stock.FocusedItem.Text)
    End Sub

    Private Sub CetakUlangFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangFakturToolStripMenuItem.Click
        If Lv_Stock.Items.Count = 0 Or Lv_Stock.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try

            OpenConn()

            SQL = "select c.No_Faktur "
            SQL = SQL & "from Tf_Stock a, barang b, Tf_Stock_parent c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = c.No_Faktur "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and c.so_awal = B.Kode_Stock_Owner "
            SQL = SQL & "and c.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.No_Faktur='" & Lv_Stock.FocusedItem.Text & "' "
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

    Private Sub BatalTransferStockToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalTransferStockToolStripMenuItem.Click
        If Lv_Stock.Items.Count = 0 Or Lv_Stock.FocusedItem.Index = -1 Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Transfer Stock"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Transfer_Stock2") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Transfer Stock", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan No Transfer Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim NoTransfer As String = Lv_Stock.FocusedItem.Text

            '===================================================
            '=     CEK APAKAH NO TRANSFER SUDAH DIBATALKAN     =
            '===================================================
            SQL = "select Kode_Perusahaan from Tf_Stock_Parent "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoTransfer & "' and status = 'Y'  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Transfer Stock tidak dapat dilakukan karena No Transfer Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=====================================================
            '=     CEK APAKAH SEMUA BARANG SUDAH DI VALIDASI     =
            '=====================================================
            SQL = "select a.Kode_Perusahaan from Tf_Stock_Parent a, Tf_Stock b, Tf_Stock_det c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.status is null and c.Selesai is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Transfer Stock tidak dapat dilakukan karena Barang Pada No Transfer Belum Di Validasi Sepenuhnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '============================================
            '=     CEK APAKAH BARANG SUDAH DI PAKAI     =
            '============================================
            SQL = "select a.No_Faktur, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang, c.Serial_Number_Awal, d.Serial_Number as Serial_Number_Tujuan, d.Jumlah, d.Jumlah_Bags, "
            SQL = SQL & "ISNULL(( select z.Jumlah from Barang_SN z where d.kode_perusahaan = z.kode_perusahaan and d.Serial_Number = z.Serial_Number ), NULL) as Jumlah_SN, "
            SQL = SQL & "ISNULL (( select z.Jumlah_Bags from Barang_SN z where d.kode_perusahaan = z.kode_perusahaan and d.Serial_Number = z.Serial_Number  ), NULL) as Jumlah_Bags_SN "
            SQL = SQL & "from Tf_Stock_Parent a, Tf_Stock b, Tf_Stock_det c, Tf_Stock_det2 d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            SQL = SQL & "and a.Status is null and c.Selesai ='Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & NoTransfer & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            If .Rows(i).Item("Jumlah") <> .Rows(i).Item("Jumlah_SN") Or .Rows(i).Item("Jumlah_Bags") <> .Rows(i).Item("Jumlah_Bags_SN") Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembatalan Transfer Stock tidak dapat dilakukan karena Kode Barang : " & .Rows(i).Item("Kode_Barang") & " Sudah Digunakan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Transfer Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '========================================
            '=     CEK APAKAH SUDAH TUTUP SALDO     =
            '========================================
            Dim HasData As Boolean = False
            Dim TglTfStock As DateTime
            SQL = "select Tanggal from Tf_Stock_Parent where status is null and Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoTransfer & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    HasData = True
                    TglTfStock = Dr("Tanggal")
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Transfer Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If HasData Then
                If CekSudahTutupSaldo(TglTfStock) = "Y" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Transfer Stock tidak dapat dilakukan karena No Transaksi Sudah Tutup Saldo", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If

            '=====================
            '=     CEK BULAN     =
            '=====================
            If Not tgl_skg.Month = TglTfStock.Month Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Pembatalan Transfer Stock tidak dapat dilakukan karena Sudah Melewati Bulan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If



            '=========================
            '=     ROLLBACK DATA     =
            '=========================

            '== CEK APAKAH DATA SEIMBANG =='
            SQL = "select a.No_Faktur, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang "
            SQL = SQL & "from Tf_Stock_Parent a, Tf_Stock b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & .Rows(i).Item("SO_Tujuan") & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & .Rows(i).Item("Kode_Barang") & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan, Data Tidak Seimbang . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Transfer Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '== ROLLBACK DATA =='
            SQL = "select a.No_Faktur, b.Flag_Timbang, a.SO_Awal, a.SO_Tujuan, b.Kode_Barang, c.Serial_Number_Awal, d.Serial_Number as Serial_Number_Tujuan, "
            SQL = SQL & "d.Jumlah as Jumlah_Kecil, d.Jumlah_Bags, d.Kode_Voucher, c.Urut_Oto as Urut_TF "
            SQL = SQL & "from Tf_Stock_Parent a, Tf_Stock b, Tf_Stock_det c, Tf_Stock_det2 d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            SQL = SQL & "and a.status is null and c.Selesai = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoTransfer & "' "
            SQL = SQL & "order by b.Flag_Timbang "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim JumlahRollBack As Double = .Rows(i).Item("Jumlah_Kecil")
                            Dim JumlahBagsRollBack As Double = .Rows(i).Item("Jumlah_Bags")
                            Dim Kd_Barang As String = .Rows(i).Item("Kode_Barang")
                            Dim So_Awal As String = .Rows(i).Item("SO_Awal")
                            Dim So_Tujuan As String = .Rows(i).Item("SO_Tujuan")
                            Dim Sn_Awal As String = .Rows(i).Item("Serial_Number_Awal")
                            Dim SN_Tujuan As String = .Rows(i).Item("Serial_Number_Tujuan")
                            Dim KodeVoucher As String = .Rows(i).Item("Kode_Voucher")
                            Dim UrutTF As Integer = .Rows(i).Item("Urut_TF")

                            '== ROLLBACK BARANG SN =='
                            ' PENGURANGAN STOCK
                            SQL = "select Jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Tujuan & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    If Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah"))) < JumlahRollBack Or Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("Jumlah_Bags"))) < JumlahBagsRollBack Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback Data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else

                                        SQL = "update Barang_SN set Jumlah = jumlah - " & JumlahRollBack & ", "
                                        SQL = SQL & "Jumlah_Bags = Jumlah_Bags - " & JumlahBagsRollBack & "  "
                                        SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Tujuan & "' "
                                        ExecuteTrans(SQL)

                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '== UPDATE BARANG =='
                            SQL = "select good_stock, jumlah_bags from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & So_Tujuan & "'  and kode_barang = '" & Kd_Barang & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    If Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("good_stock"))) < JumlahRollBack Or Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags"))) < JumlahBagsRollBack Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan Saat Rollback Data", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    Else

                                        SQL = "update Barang set good_stock = good_stock - " & JumlahRollBack & ", "
                                        SQL = SQL & "jumlah_bags = jumlah_bags - " & JumlahBagsRollBack & "  "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & So_Tujuan & "'  and kode_barang = '" & Kd_Barang & "' "
                                        ExecuteTrans(SQL)

                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & So_Tujuan & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds2.Tables("MyTable").Rows(0).Item("good_stock") <> Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            'PENAMBAHAN STOCK
                            SQL = "select Jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & Sn_Awal & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "update Barang_SN set Jumlah = jumlah + " & JumlahRollBack & ", "
                                    SQL = SQL & "Jumlah_Bags = Jumlah_Bags + " & JumlahBagsRollBack & "  "
                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & Sn_Awal & "' "
                                    ExecuteTrans(SQL)
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '== UPDATE BARANG =='
                            SQL = "select good_stock, jumlah_bags from Barang where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & So_Awal & "'  and kode_barang = '" & Kd_Barang & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "update Barang set good_stock = good_stock + " & JumlahRollBack & ", "
                                    SQL = SQL & "jumlah_bags = jumlah_bags + " & JumlahBagsRollBack & "  "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_Stock_owner = '" & So_Awal & "'  and kode_barang = '" & Kd_Barang & "' "
                                    ExecuteTrans(SQL)
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang SN Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & So_Awal & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds2.Tables("MyTable").Rows(0).Item("good_stock") <> Ds2.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds2.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '===========================
                            '=     ROLLBACK JURNAL     =
                            '===========================
                            SQL = "select Kode_Perusahaan from Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & KodeVoucher & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "delete Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & KodeVoucher & "' "
                                    ExecuteTrans(SQL)
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Jurnal Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            Dim abc As String = ""
                            '==================================
                            '=     UPDATE TF STOCK DETAIL     =
                            '==================================
                            'SQL = "select Kode_Perusahaan from Tf_Stock_det where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            'SQL = SQL & "and No_Faktur = '" & NoTransfer & "' and urut_oto = '" & UrutTF & "' "
                            'Using Dr = OpenTrans(SQL)
                            '    If Dr.Read Then

                            '        Dr.Close()
                            '        SQL = "update Tf_Stock_det set Selesai = NULL where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            '        SQL = SQL & "and No_Faktur = '" & NoTransfer & "' and urut_oto = '" & UrutTF & "' "
                            '        ExecuteTrans(SQL)
                            '    Else
                            '        CloseTrans()
                            '        CloseConn()
                            '        MessageBox.Show("Data TF Stock Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '        Exit Sub
                            '    End If
                            'End Using

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Transfer Barang Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '===========================
            '=     UPDATE TF STOCK     =
            '===========================
            SQL = "select Kode_Perusahaan from Tf_Stock_Parent where Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and No_Faktur = '" & NoTransfer & "' and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = "update Tf_Stock_Parent set Status = 'Y', UserID_Batal = '" & UserID & "', Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoTransfer & "' and status is null"
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Transfer Stock Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Transfer Stock Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(e, New EventArgs)

    End Sub

    '===========================================================================================================================================================
    '=     HANDLE KEYPRESS
    '===========================================================================================================================================================
    Private Sub CmbSO_Asal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSO_Asal.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Transaksi_HariIni.Focus()
    End Sub

    Private Sub Chk_Transaksi_HariIni_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Transaksi_HariIni.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Tanggal.Focus()
    End Sub

    Private Sub Chk_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_Tanggal.Checked Then
                Cmb_FIlterTanggal.DroppedDown = True
                Cmb_FIlterTanggal.Focus()
            Else
                Chk_Param_Lain.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_FIlterTanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_FIlterTanggal.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Param_Lain.Focus()
    End Sub

    Private Sub Chk_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Param_Lain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_Param_Lain.Checked Then
                Cmb_Filter_ParamLain.DroppedDown = True
                Cmb_Filter_ParamLain.Focus()
            Else
                BtnBarangMasuk_Cari.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Filter_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then Txt_ParamLain.Focus()
    End Sub

    Private Sub Txt_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then BtnBarangMasuk_Cari.Focus()
    End Sub

End Class