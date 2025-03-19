Public Class EMI_Display_Split_Stock

    Dim arrFilterLokasi, arrFilterTgl, arrFilterLain As New ArrayList

    Dim LvP_KdTransfer, LvP_JnsTransfer, LvP_LokasiAwal, LvP_LokasiTujuan, LvP_Keterangan, LvP_Tgl, LvP_Jam, LvP_User As String

    Dim itemP_KdTransfer As Integer = 0
    Dim itemP_JnsTransfer As Integer = 1
    Dim itemP_LokasiAwal As Integer = 2
    Dim itemP_LokasiAkhir As Integer = 3
    Dim itemP_Keterangan As Integer = 4
    Dim itemP_Tgl As Integer = 5
    Dim itemP_Jam As Integer = 6
    Dim itemP_User As Integer = 7

    Dim LvC_RakAwal, LvC_RakTujuan, LvC_KdBarang, LvC_NmBarang, LvC_Total, LvC_TotBags, LvC_BeratBagi, LvC_Satuan, LvC_IdWmsAwal, LvC_IdWmsTujuan, LvC_Urut As String

    Dim itemC_RakAwal As Integer = 0
    Dim itemC_RakTujuan As Integer = 1
    Dim itemC_KdBarang As Integer = 2
    Dim itemC_NmBarang As Integer = 3
    Dim itemC_Total As Integer = 4
    Dim itemC_TotBags As Integer = 5
    Dim itemC_BeratBagi As Integer = 6
    Dim itemC_Satuan As Integer = 7
    Dim itemC_IdWmsAwal As Integer = 8
    Dim itemC_IdWmsTujuan As Integer = 9
    Dim itemC_Urut As Integer = 10

    Dim LvDP_QrAkhir, LvDP_NoPallet, LvDP_Jumlah, LvDP_Satuan As String

    Dim itemDP_QrAkhir As Integer = 0
    Dim itemDP_NoPallet As Integer = 1
    Dim itemDP_Jumlah As Integer = 2
    Dim itemDP_Satuan As Integer = 3

    Private Sub EMI_Display_Split_Barang_BackgroundImageChanged(sender As Object, e As EventArgs) Handles Me.BackgroundImageChanged
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub EMI_Display_Split_Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        InitialListView()
        Kosong()

    End Sub

    Private Sub Kosong()

        Try
            OpenConn()

            Chk_1.Checked = False
            Chk_2.Checked = False
            Chk_3.Checked = False

            Txt_ParamLainValue.Text = ""
            DateTimePicker1.Value = Date.Now
            DateTimePicker2.Value = Date.Now

            Lv_Parent.Items.Clear()
            Lv_Child.Items.Clear()
            Lv_DetailPallet.Items.Clear()

            Cmb_1.Items.Clear() : arrFilterLokasi.Clear()
            SQL = "select kode_stock_owner, keterangan from Stock_Owner where kode_perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Cmb_1.Items.Add("--- Seluruh ---") : arrFilterLokasi.Add("SELURUH")
                Do While Dr.Read
                    Cmb_1.Items.Add(Dr("keterangan")) : arrFilterLokasi.Add(Dr("kode_stock_owner"))
                Loop
                Cmb_1.SelectedIndex = 0
            End Using

            Cmb_2.Items.Clear() : arrFilterTgl.Clear()
            Cmb_2.Items.Add("Tanggal") : arrFilterTgl.Add("a.Tanggal")

            Cmb_3.Items.Clear() : arrFilterLain.Clear()
            Cmb_3.Items.Add("No Faktur") : arrFilterLain.Add("a.No_Faktur")
            Cmb_3.Items.Add("Jenis Transfer") : arrFilterLain.Add("a.Jenis_Transfer")
            Cmb_3.Items.Add("Lokasi Awal") : arrFilterLain.Add("a.SO_Awal")
            Cmb_3.Items.Add("Lokasi Akhir") : arrFilterLain.Add("a.SO_Tujuan")
            Cmb_3.Items.Add("User") : arrFilterLain.Add("a.UserID")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub InitialListView()

        Lv_Parent.Columns.Clear()
        Lv_Parent.Columns.Add("Kode Transfer", 150, HorizontalAlignment.Left)
        Lv_Parent.Columns.Add("Jenis Transfer", 120, HorizontalAlignment.Left)
        Lv_Parent.Columns.Add("Lokasi Awal", 160, HorizontalAlignment.Center)
        Lv_Parent.Columns.Add("Lokasi Akhir", 160, HorizontalAlignment.Center)
        Lv_Parent.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Parent.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        Lv_Parent.Columns.Add("Jam", 80, HorizontalAlignment.Center)
        Lv_Parent.Columns.Add("User", 100, HorizontalAlignment.Center)
        Lv_Parent.View = View.Details


        Lv_Child.Columns.Clear()
        Lv_Child.Columns.Add("Rak Awal", 120, HorizontalAlignment.Left)
        Lv_Child.Columns.Add("Rak Tujuan", 120, HorizontalAlignment.Left)
        Lv_Child.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left)
        Lv_Child.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_Child.Columns.Add("Total", 100, HorizontalAlignment.Right)
        Lv_Child.Columns.Add("Total Bags", 100, HorizontalAlignment.Right)
        Lv_Child.Columns.Add("Berat Bagi", 100, HorizontalAlignment.Right)
        Lv_Child.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        'Hide
        Lv_Child.Columns.Add("id_wmsAwal", 0, HorizontalAlignment.Center)
        Lv_Child.Columns.Add("id_wmsTujuan", 0, HorizontalAlignment.Center)
        Lv_Child.Columns.Add("urut  ", 0, HorizontalAlignment.Center)
        Lv_Child.View = View.Details

        Lv_DetailPallet.Columns.Clear()
        Lv_DetailPallet.Columns.Add("QR Akhir", 150, HorizontalAlignment.Left)
        Lv_DetailPallet.Columns.Add("No Pallet", 100, HorizontalAlignment.Center)
        Lv_DetailPallet.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
        Lv_DetailPallet.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_DetailPallet.View = View.Details

    End Sub

    Private Sub GetDataLv_Parent(ByVal index As Integer)
        LvP_KdTransfer = Lv_Parent.Items(index).SubItems(itemP_KdTransfer).Text
        LvP_JnsTransfer = Lv_Parent.Items(index).SubItems(itemP_JnsTransfer).Text
        LvP_LokasiAwal = Lv_Parent.Items(index).SubItems(itemP_LokasiAwal).Text
        LvP_LokasiTujuan = Lv_Parent.Items(index).SubItems(itemP_LokasiAkhir).Text
        LvP_Keterangan = Lv_Parent.Items(index).SubItems(itemP_Keterangan).Text
        LvP_Tgl = Lv_Parent.Items(index).SubItems(itemP_Tgl).Text
        LvP_Jam = Lv_Parent.Items(index).SubItems(itemP_Jam).Text
        LvP_User = Lv_Parent.Items(index).SubItems(itemP_User).Text
    End Sub

    Private Sub GetDataLvChild(ByVal index As Integer)

        LvC_RakAwal = Lv_Child.Items(index).SubItems(itemC_RakAwal).Text
        LvC_RakTujuan = Lv_Child.Items(index).SubItems(itemC_RakTujuan).Text
        LvC_KdBarang = Lv_Child.Items(index).SubItems(itemC_KdBarang).Text
        LvC_NmBarang = Lv_Child.Items(index).SubItems(itemC_NmBarang).Text
        LvC_Total = Lv_Child.Items(index).SubItems(itemC_Total).Text
        LvC_TotBags = Lv_Child.Items(index).SubItems(itemC_TotBags).Text
        LvC_BeratBagi = Lv_Child.Items(index).SubItems(itemC_BeratBagi).Text
        LvC_Satuan = Lv_Child.Items(index).SubItems(itemC_Satuan).Text
        LvC_IdWmsAwal = Lv_Child.Items(index).SubItems(itemC_IdWmsAwal).Text
        LvC_IdWmsTujuan = Lv_Child.Items(index).SubItems(itemC_IdWmsTujuan).Text
        LvC_Urut = Lv_Child.Items(index).SubItems(itemC_Urut).Text

    End Sub


    Private Sub Chk_1_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_1.CheckedChanged
        If Chk_1.Checked = True Then
            Chk_2.Checked = False
            Cmb_2.SelectedIndex = -1 : Cmb_2.Text = ""
            DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
            Cmb_2.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            BtnMasuk_Cari_Click(Chk_2, e)
        Else
            Cmb_2.SelectedIndex = -1 : Cmb_2.Text = ""
            DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
            Cmb_2.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
        End If
    End Sub

    Private Sub Chk_2_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_2.CheckedChanged
        If Chk_2.Checked Then
            Chk_1.Checked = False
            Cmb_2.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Cmb_2.SelectedIndex = -1 : Cmb_2.Text = ""
            DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
        Else
            Cmb_2.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_2.SelectedIndex = -1 : Cmb_2.Text = ""
            DateTimePicker1.Value = Date.Now : DateTimePicker2.Value = Date.Now
        End If
    End Sub

    Private Sub Chk_3_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_3.CheckedChanged
        If Chk_3.Checked = True Then
            Cmb_3.Enabled = True : Txt_ParamLainValue.Enabled = True
            Cmb_3.SelectedIndex = -1 : Cmb_3.Text = "" : Txt_ParamLainValue.Text = ""
        Else
            Cmb_3.Enabled = False : Txt_ParamLainValue.Enabled = False
            Cmb_3.SelectedIndex = -1 : Cmb_3.Text = "" : Txt_ParamLainValue.Text = ""
        End If
    End Sub

    Private Sub BtnMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnMasuk_Cari.Click
        If Chk_1.Checked = False And Chk_2.Checked = False And Chk_3.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            Chk_1.Focus() : Exit Sub
        ElseIf Cmb_1.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi Harus harus diisi!", Judul)
            Cmb_1.Focus() : Exit Sub
        End If

        If Chk_2.Checked = True Then
            If Not Cmb_2.SelectedIndex = -1 Then
                If DateTimePicker1.Value > DateTimePicker2.Value Then
                    MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                    DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                    Exit Sub
                End If
            Else
                MessageBox.Show("Pilih Dahulu Tanggal yang akan Di Filter!", Judul)
                Cmb_2.Focus() : Exit Sub
            End If
        End If

        If Chk_3.Checked = True Then
            If Cmb_3.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                Cmb_3.Focus() : Exit Sub
            ElseIf Txt_ParamLainValue.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                Txt_ParamLainValue.Focus() : Exit Sub
            End If

        End If

        Try
            OpenConn()

            Lv_Child.Items.Clear() : Lv_DetailPallet.Items.Clear()

            Lv_Parent.Items.Clear()
            SQL = "select a.No_Faktur, a.Jenis_Transfer, a.SO_Awal, a.SO_Tujuan, a.Keterangan, a.Tanggal, a.Jam, a.UserID "
            SQL = SQL & "from Tf_Stock_QC a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.status is null "
            If Cmb_1.SelectedIndex <> 0 Then
                SQL = SQL & "and a.lokasi = '" & arrFilterLokasi(Cmb_1.SelectedIndex) & "' "
            End If
            If Chk_1.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal Between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(DateAdd(DateInterval.Day, 1, Now), "yyyy-MM-dd") & "' "
            End If
            If Chk_2.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilterTgl(Cmb_2.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If
            If Chk_3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilterLain(Cmb_3.SelectedIndex) & " like '%" & Trim(Txt_ParamLainValue.Text) & "%' "
            End If
            SQL = SQL & "order by a.Tanggal, a.Jam, a.SO_Awal, a.Jenis_Transfer "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Parent.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Jenis_Transfer"))
                    Lv.SubItems.Add(Dr("SO_Awal"))
                    Lv.SubItems.Add(Dr("SO_Tujuan"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
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

    Private Sub Lv_Parent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Parent.SelectedIndexChanged

        If Lv_Parent.Items.Count = 0 OrElse Lv_Parent.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()

            Dim SelectedIndex As Integer = Lv_Parent.FocusedItem.Index
            GetDataLv_Parent(SelectedIndex)

            Lv_Child.Items.Clear()
            SQL = "select b.Kode_Barang, d.Nama as NamaBarang, b.Total, b.Satuan, b.Total_Bags,  "
            SQL = SQL & "(dbo.Ubah_Satuan(a.Kode_Perusahaan, 'masa', b.Kode_Barang, c.Satuan_Barang, b.Satuan, c.Berat_Bagi)) as Berat_Bagi, "
            SQL = SQL & "b.Urut_Oto, c.Id_Wms_Awal, e.Keterangan as Warehouse_Awal, c.No_Pallet_Awal, c.Id_Wms_Tujuan, f.Keterangan as Warehouse_Tujuan, c.No_Pallet_Tujuan "
            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c, barang d, View_Warehouse_Position e, View_Warehouse_Position f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan  "
            SQL = SQL & "and c.Kode_Perusahaan = e.Kode_Perusahaan and c.Kode_Perusahaan = f.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and a.SO_Awal = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Id_Wms_Awal = e.Id_WMS_Warehouse_Position "
            SQL = SQL & "and c.Id_Wms_Tujuan = f.Id_WMS_Warehouse_Position "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & LvP_KdTransfer.Trim & "'"
            SQL = SQL & "order by b.Kode_Barang, b.Urut_Oto "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Child.Items.Add(Dr("Warehouse_Awal"))
                    Lv.SubItems.Add(Dr("Warehouse_Tujuan"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("NamaBarang"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Total"))), "N2"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Total_Bags"))), "N2"))
                    Lv.SubItems.Add(Format(Val(HilangkanTanda(Dr("Berat_Bagi"))), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Id_Wms_Awal"))
                    Lv.SubItems.Add(Dr("Id_Wms_Tujuan"))
                    Lv.SubItems.Add(Dr("Urut_Oto"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Lv_Child_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Child.SelectedIndexChanged
        If Lv_Child.Items.Count = 0 OrElse Lv_Child.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()

            Dim selectedIndex As Integer = Lv_Child.FocusedItem.Index
            GetDataLvChild(selectedIndex)

            Lv_DetailPallet.Items.Clear()
            SQL = "select a.No_Faktur,  "
            SQL = SQL & "isnull(( select z.Qr_Code + '-' + z.Kode_Unik_Berjalan from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Awal = z.Kode_Stock_Owner and c.Serial_Number_Awal= z.Serial_Number ), '-') as QR_Awal, "

            SQL = SQL & "isnull(( select z.Qr_Code + '-' + z.Kode_Unik_Berjalan from Barang_SN z where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.SO_Tujuan = z.Kode_Stock_Owner and d.Serial_Number = z.Serial_Number ), '-') as QR_Akhir, "

            SQL = SQL & "d.No_Pallet, b.Satuan, "
            SQL = SQL & "(dbo.Ubah_Satuan(a.Kode_Perusahaan, 'masa', b.Kode_Barang, c.Satuan_Barang, b.Satuan, d.Jumlah)) as Jumlah "

            SQL = SQL & "from Tf_Stock_QC a, Tf_Stock_QC_Detail b, Tf_Stock_QC_det c, Tf_Stock_QC_det2 d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_TF "
            SQL = SQL & "and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.Urut_Det "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & LvP_KdTransfer & "' "
            SQL = SQL & "and b.Urut_Oto = '" & LvC_Urut & "' "
            SQL = SQL & "order by d.Urut_Oto "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DetailPallet.Items.Add(Dr("QR_Akhir"))
                    Lv.SubItems.Add(Dr("No_Pallet"))
                    Lv.SubItems.Add(Dr("Jumlah"))
                    Lv.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try






    End Sub

End Class