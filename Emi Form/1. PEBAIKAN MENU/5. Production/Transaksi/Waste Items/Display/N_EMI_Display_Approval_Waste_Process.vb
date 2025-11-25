Public Class N_EMI_Display_Approval_Waste_Process



    Dim Lv_Process_NoTransaksiApproval, Lv_Process_NoFaktur, Lv_Process_KdStock_Owner, Lv_Process_Lokasi, Lv_Process_Tanggal, Lv_Process_Jam, Lv_Process_Keterangan, Lv_Process_UserInput As String

    Dim Item_Process_NoTransaksiApproval As Integer = 0
    Dim Item_Process_NoFaktur As Integer = 1
    Dim Item_Process__KdStock_Owner As Integer = 2
    Dim Item_Process_Lokasi As Integer = 3
    Dim Item_Process_Tanggal As Integer = 4
    Dim Item_Process_Jam As Integer = 5
    Dim Item_Process_Keterangan As Integer = 6
    Dim Item_Process_UserInput As Integer = 7


    Dim Lv_User_Approve_Username, Lv_User_Approve_Level, Lv_User_Approve_Status, Lv_User_Approve_TanggalApprove, Lv_User_Approve_JamApprove, Lv_User_Approve_iduser As String

    Dim item_User_Approve_Username As Integer = 0
    Dim item_User_Approve_Level As Integer = 1
    Dim item_User_Approve_Status As Integer = 2
    Dim item_User_Approve_TanggalApprove As Integer = 3
    Dim item_User_Approve_JamApprove As Integer = 4
    Dim item_User_Approve_iduser As Integer = 5

    Dim arrFilterTab1 As New ArrayList

    Dim Lv_Product_NoTransaksiApproval, Lv_Product_NoFaktur, Lv_Product_KdStock_Owner, Lv_Product_Lokasi, Lv_Product_Tanggal, Lv_Product_Jam, Lv_Product_Keterangan, Lv_Product_UserInput As String

    Dim Item_Product_NoTransaksiApproval As Integer = 0
    Dim Item_Product_NoFaktur As Integer = 1
    Dim Item_Product__KdStock_Owner As Integer = 2
    Dim Item_Product_Lokasi As Integer = 3
    Dim Item_Product_Tanggal As Integer = 4
    Dim Item_Product_Jam As Integer = 5
    Dim Item_Product_Keterangan As Integer = 6
    Dim Item_Product_UserInput As Integer = 7



    Dim arrFilterTab2 As New ArrayList



    Private Sub N_EMI_Display_Approval_Waste_Process_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub N_EMI_Display_Approval_Waste_Process_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

#Region "WASTE PROCESS"

        Lv_Process_Data.Columns.Clear()
        Lv_Process_Data.Columns.Add("No Approval", 130, HorizontalAlignment.Left)
        Lv_Process_Data.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_Process_Data.Columns.Add("Lokasi", 130, HorizontalAlignment.Center)
        Lv_Process_Data.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        Lv_Process_Data.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_Process_Data.Columns.Add("Jam", 110, HorizontalAlignment.Center)
        Lv_Process_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Process_Data.Columns.Add("User Input", 130, HorizontalAlignment.Left)
        Lv_Process_Data.View = View.Details

        Cmb_Filter.Items.Clear() : arrFilterTab1.Clear()
        Cmb_Filter.Items.Add(OpsiSeluruh) : arrFilterTab1.Add(OpsiSeluruh)
        Cmb_Filter.Items.Add("No Approval") : arrFilterTab1.Add("b.No_Transaksi")
        Cmb_Filter.Items.Add("No Faktur") : arrFilterTab1.Add("a.No_Faktur")
        Cmb_Filter.Items.Add("Lokasi") : arrFilterTab1.Add("a.Lokasi")
        Cmb_Filter.Items.Add("Kode Stock Owner") : arrFilterTab1.Add("a.Kode_Stock_Owner")
        Cmb_Filter.Items.Add("User Input") : arrFilterTab1.Add("a.UserID")

        Lv_Process_User_Approve.Columns.Clear()
        Lv_Process_User_Approve.Columns.Add("Username", 200, HorizontalAlignment.Left)
        Lv_Process_User_Approve.Columns.Add("Approval Level", 100, HorizontalAlignment.Center)
        Lv_Process_User_Approve.Columns.Add("Status", 137, HorizontalAlignment.Center)
        Lv_Process_User_Approve.Columns.Add("Tanggal Approve", 110, HorizontalAlignment.Center)
        Lv_Process_User_Approve.Columns.Add("Jam Approve", 100, HorizontalAlignment.Center)
        Lv_Process_User_Approve.Columns.Add("id_user", 0, HorizontalAlignment.Left)
        Lv_Process_User_Approve.Columns.Add("Jabatan", 150, HorizontalAlignment.Left)
        Lv_Process_User_Approve.View = View.Details

        Lv_Process_User_Approve.Columns(6).DisplayIndex = 3

        Lv_Process_Detail_Barang.Columns.Clear()
        Lv_Process_Detail_Barang.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Left)
        Lv_Process_Detail_Barang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        Lv_Process_Detail_Barang.Columns.Add("Nama Barang", 190, HorizontalAlignment.Left)
        Lv_Process_Detail_Barang.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Process_Detail_Barang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Process_Detail_Barang.View = View.Details

#End Region


#Region "WASTE PRODUCT"

        Lv_Product_Data.Columns.Clear()
        Lv_Product_Data.Columns.Add("No Approval", 130, HorizontalAlignment.Left)
        Lv_Product_Data.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_Product_Data.Columns.Add("Lokasi", 130, HorizontalAlignment.Center)
        Lv_Product_Data.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        Lv_Product_Data.Columns.Add("Tanggal", 130, HorizontalAlignment.Center)
        Lv_Product_Data.Columns.Add("Jam", 110, HorizontalAlignment.Center)
        Lv_Product_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Product_Data.Columns.Add("User Input", 130, HorizontalAlignment.Left)
        Lv_Product_Data.View = View.Details

        Cmb_Filter_Tab_2.Items.Clear() : arrFilterTab2.Clear()
        Cmb_Filter_Tab_2.Items.Add(OpsiSeluruh) : arrFilterTab2.Add(OpsiSeluruh)
        Cmb_Filter_Tab_2.Items.Add("No Approval") : arrFilterTab2.Add("b.No_Transaksi")
        Cmb_Filter_Tab_2.Items.Add("No Faktur") : arrFilterTab2.Add("a.No_Faktur")
        Cmb_Filter_Tab_2.Items.Add("Lokasi") : arrFilterTab2.Add("a.Lokasi")
        Cmb_Filter_Tab_2.Items.Add("Kode Stock Owner") : arrFilterTab2.Add("a.Kode_Stock_Owner")
        Cmb_Filter_Tab_2.Items.Add("User Input") : arrFilterTab2.Add("a.UserID")

        Lv_Product_User_Approve.Columns.Clear()
        Lv_Product_User_Approve.Columns.Add("Username", 200, HorizontalAlignment.Left)
        Lv_Product_User_Approve.Columns.Add("Approval Level", 100, HorizontalAlignment.Center)
        Lv_Product_User_Approve.Columns.Add("Status", 137, HorizontalAlignment.Center)
        Lv_Product_User_Approve.Columns.Add("Tanggal Approve", 110, HorizontalAlignment.Center)
        Lv_Product_User_Approve.Columns.Add("Jam Approve", 100, HorizontalAlignment.Center)
        Lv_Product_User_Approve.Columns.Add("id_user", 0, HorizontalAlignment.Left)
        Lv_Product_User_Approve.Columns.Add("Jabatan", 150, HorizontalAlignment.Left)
        Lv_Product_User_Approve.View = View.Details

        Lv_Product_User_Approve.Columns(6).DisplayIndex = 3

        Lv_Product_Detail_Barang.Columns.Clear()
        Lv_Product_Detail_Barang.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Left)
        Lv_Product_Detail_Barang.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        Lv_Product_Detail_Barang.Columns.Add("Nama Barang", 190, HorizontalAlignment.Left)
        Lv_Product_Detail_Barang.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Product_Detail_Barang.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Product_Detail_Barang.View = View.Details

#End Region


        Kosong_Tab_1()
        'Kosong_Tab_2()

    End Sub

    Private Sub Tab1_Get_Lv_Process_Data(ByVal index As Integer)
        Lv_Process_NoTransaksiApproval = Lv_Process_Data.Items(index).SubItems(Item_Process_NoTransaksiApproval).Text
        Lv_Process_NoFaktur = Lv_Process_Data.Items(index).SubItems(Item_Process_NoFaktur).Text
        Lv_Process_KdStock_Owner = Lv_Process_Data.Items(index).SubItems(Item_Process__KdStock_Owner).Text
        Lv_Process_Lokasi = Lv_Process_Data.Items(index).SubItems(Item_Process_Lokasi).Text
        Lv_Process_Tanggal = Lv_Process_Data.Items(index).SubItems(Item_Process_Tanggal).Text
        Lv_Process_Jam = Lv_Process_Data.Items(index).SubItems(Item_Process_Jam).Text
        Lv_Process_Keterangan = Lv_Process_Data.Items(index).SubItems(Item_Process_Keterangan).Text
        Lv_Process_UserInput = Lv_Process_Data.Items(index).SubItems(Item_Process_UserInput).Text
    End Sub

    Private Sub Tab1_Get_Lv_Process_User(ByVal index As Integer)
        Lv_User_Approve_Username = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_Username).Text
        Lv_User_Approve_Level = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_Level).Text
        Lv_User_Approve_Status = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_Status).Text
        Lv_User_Approve_TanggalApprove = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_TanggalApprove).Text
        Lv_User_Approve_JamApprove = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_JamApprove).Text
        Lv_User_Approve_iduser = Lv_Process_User_Approve.Items(index).SubItems(item_User_Approve_iduser).Text
    End Sub

    Private Sub Tab2_Get_Lv_Product_Data(ByVal index As Integer)
        Lv_Product_NoTransaksiApproval = Lv_Product_Data.Items(index).SubItems(Item_Product_NoTransaksiApproval).Text
        Lv_Product_NoFaktur = Lv_Product_Data.Items(index).SubItems(Item_Product_NoFaktur).Text
        Lv_Product_KdStock_Owner = Lv_Product_Data.Items(index).SubItems(Item_Product__KdStock_Owner).Text
        Lv_Product_Lokasi = Lv_Product_Data.Items(index).SubItems(Item_Product_Lokasi).Text
        Lv_Product_Tanggal = Lv_Product_Data.Items(index).SubItems(Item_Product_Tanggal).Text
        Lv_Product_Jam = Lv_Product_Data.Items(index).SubItems(Item_Product_Jam).Text
        Lv_Product_Keterangan = Lv_Product_Data.Items(index).SubItems(Item_Product_Keterangan).Text
        Lv_Product_UserInput = Lv_Product_Data.Items(index).SubItems(Item_Product_UserInput).Text
    End Sub


    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            Kosong_Tab_1()
        ElseIf TabControl1.SelectedIndex = 1 Then
            Kosong_Tab_2()
        Else
            Kosong_Tab_1()
            Kosong_Tab_2()
        End If
    End Sub

    Private Sub Kosong_Tab_1()

        Lv_Process_Data.Items.Clear()
        Lv_Process_User_Approve.Items.Clear()
        Lv_Process_Detail_Barang.Items.Clear()
        Cmb_Filter.SelectedIndex = 0
        Txt_Filter.Text = ""

        Load_Data_Process_Waste()

    End Sub

    Private Sub Kosong_Tab_2()

        Lv_Product_Data.Items.Clear()
        Lv_Product_User_Approve.Items.Clear()
        Lv_Product_Detail_Barang.Items.Clear()
        Cmb_Filter_Tab_2.SelectedIndex = 0
        Txt_Filter_Tab_2.Text = ""

        Load_Data_Product_Waste()

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter.SelectedIndex = 0 Or Cmb_Filter.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu yang Mau Difilter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Filter.DroppedDown = True
            Cmb_Filter.Focus()
            Exit Sub
        Else
            If Txt_Filter.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Filter.Focus()
                Exit Sub
            End If
        End If

        Load_Data_Process_Waste(Filter:=True)

    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong_Tab_1()
    End Sub

    Private Sub Load_Data_Process_Waste(ByVal Optional Filter As Boolean = False)
        Try
            OpenConn()

            Lv_Process_Data.Items.Clear() : Lv_Process_User_Approve.Items.Clear() : Lv_Process_Detail_Barang.Items.Clear()
            SQL = "select distinct b.No_Transaksi, a.No_Faktur, a.Lokasi, a.Kode_Stock_Owner, a.Tanggal, a.Jam, a.Keterangan, a.UserID as User_Input, "
            SQL = SQL & "isnull((x.isCompleted), 'Y') as isCompleted "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "outer apply( "
            SQL = SQL & "select top 1 'T' as isCompleted "
            SQL = SQL & "from N_EMI_Transaksi_Approval_Waste z "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur_Waste = a.No_Faktur "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and z.Flag_Approve is null) x "
            SQL = SQL & "where a.Status is null and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
            If Filter Then
                SQL = SQL & "and " & arrFilterTab1(Cmb_Filter.SelectedIndex) & " like '%" & Txt_Filter.Text & "%' "
            End If
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Process_Data.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("User_Input"))

                    If Dr("isCompleted") = "T" Then
                        Lv.BackColor = Color.White
                    ElseIf Dr("isCompleted") = "Y" Then
                        Lv.BackColor = Color.LightGreen
                    Else
                        Lv.BackColor = Color.White
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

    Private Sub Lv_Process_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Process_Data.SelectedIndexChanged
        If Lv_Process_Data.Items.Count = 0 OrElse Lv_Process_Data.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()
            Tab1_Get_Lv_Process_Data(Lv_Process_Data.FocusedItem.Index)

            Dim No_faktur As String = Lv_Process_NoFaktur
            Dim No_Transaksi As String = Lv_Process_NoTransaksiApproval

            Lv_Process_User_Approve.Items.Clear()
            SQL = "select c.username, b.Approval_Level, b.Flag_Approve, b.Tanggal_Approve, b.Jam_Approve, b.Id_User_Android_Approve, b.jabatan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "inner join Emi_Users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_User_Android_Approve = c.id "
            SQL = SQL & "where a.Status is null and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_faktur & "' "
            SQL = SQL & "and b.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "order by b.Approval_Level ASC "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Process_User_Approve.Items.Add(Dr("username"))
                    Lv.SubItems.Add(Dr("Approval_Level"))

                    If General_Class.CekNULL(Dr("Flag_Approve")) = "Y" Then
                        Lv.SubItems.Add("Approved")
                        Lv.BackColor = Color.LightGreen
                    ElseIf General_Class.CekNULL(Dr("Flag_Approve")) = "T" Then
                        Lv.SubItems.Add("Rejected")
                        Lv.ForeColor = Color.White
                        Lv.BackColor = Color.DarkRed
                    Else
                        Lv.SubItems.Add("On Process")
                    End If

                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Approve")) = "", "-", Dr("Tanggal_Approve")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam_Approve")) = "", "-", Dr("Jam_Approve")))
                    Lv.SubItems.Add(Dr("Id_User_Android_Approve"))
                    Lv.SubItems.Add(Dr("jabatan"))
                Loop
            End Using

            Lv_Process_Detail_Barang.Items.Clear()
            SQL = "select a.Kode_Stock_Owner as Lokasi, b.kode_barang, c.Nama as Nama_Barang, b.Total, b.Satuan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
            SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "inner join barang c on a.kode_perusahaan = c.kode_perusahaan and a.kode_stock_owner = c.kode_Stock_owner and b.kode_barang = c.kode_barang "
            SQL = SQL & "where a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_faktur & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Process_Detail_Barang.Items.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("kode_barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Total"), "N4"))
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

    Private Sub Cmb_Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter.SelectedIndexChanged
        If Cmb_Filter.Items.Count = 0 OrElse Cmb_Filter.SelectedIndex = -1 Then Exit Sub

        If Cmb_Filter.SelectedIndex = 0 Then
            Txt_Filter.Enabled = False
        Else
            Txt_Filter.Enabled = True
        End If
        Txt_Filter.Text = ""

    End Sub

    Private Sub CetakFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakFakturToolStripMenuItem.Click
        If Lv_Process_Data.Items.Count = 0 AndAlso Lv_Process_Data.FocusedItem Is Nothing Then Exit Sub

        Tab1_Get_Lv_Process_Data(Lv_Process_Data.FocusedItem.Index)
        Dim No_Approval As String = Lv_Process_NoTransaksiApproval
        Dim No_Faktur As String = Lv_Process_NoFaktur

        Try
            OpenConn()

            '===========================
            '=     CEK BUTTON ROLE     =
            '===========================
            If CekButtonRole("Cetak_Faktur_Waste_Process") = "T" Then
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Cetak Faktur Waste Process", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            '===============================
            '=     CEK TRANSAKSI WASTE     =
            '===============================
            SQL = "select top 1 a.Kode_Perusahaan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "where a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
            SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    CloseConn()
                    MessageBox.Show("No Transaksi Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===============================================
            '=     CEK APAKAH SEMUA USER SUDAH APPROVE     =
            '===============================================
            SQL = "select top 1 a.Kode_Perusahaan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "where a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
            SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
            SQL = SQL & "and b.flag_approve is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CloseConn()
                    MessageBox.Show("Terdapat User yang Belum Melakukan Approval", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=======================================
            '=     CEK APAKAH ADA YANG DITOLAK     =
            '=======================================
            SQL = "select top 1 a.Kode_Perusahaan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "where a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Proses = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
            SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
            SQL = SQL & "and b.flag_approve = 'T' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CloseConn()
                    MessageBox.Show("Transaksi Ditolak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select kode_perusahaan from N_EMI_View_Berita_Acara_Waste_Process "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & No_Faktur & "' and Jenis_Approval = 'Waste_Process' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New N_EMI_CR_Berita_Acara_Waste_Proses
                    kertas = "Faktur"

                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Waste_Process.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Waste_Process.no_faktur}='" & No_Faktur & "' and {N_EMI_View_Berita_Acara_Waste_Process.Jenis_Approval}='Waste_Process' "
                        CrDoc.SummaryInfo.ReportTitle = "Waste Process"
                        .Text = "Waste Process"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With

                    '============================================================================================================================================
                    '============================================================================================================================================
                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.PrintOptions.PrinterName = PrinterNameTS
                    'CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Waste_Process.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Waste_Process.no_faktur}='" & Faktur_Pemusnahaan & "' "
                    ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterNameTS
                    ''doctoprint.DefaultPageSettings.Landscape = True
                    'Dim rawKind As Integer
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        Exit For
                    '    End If
                    'Next

                    'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    'CrDoc.PrintToPrinter(1, False, 1, 99)

                    'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub Btn_Cari_2_Click(sender As Object, e As EventArgs) Handles Btn_Cari_2.Click
        If Cmb_Filter_Tab_2.SelectedIndex = 0 Or Cmb_Filter_Tab_2.SelectedIndex = -1 Then
            MessageBox.Show("Pilih Dahulu yang Mau Difilter", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Filter_Tab_2.DroppedDown = True
            Cmb_Filter_Tab_2.Focus()
            Exit Sub
        Else
            If Txt_Filter_Tab_2.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Tidak Boleh Kosong", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Filter_Tab_2.Focus()
                Exit Sub
            End If
        End If

        Load_Data_Product_Waste(Filter:=True)
    End Sub

    Private Sub Btn_Refresh_2_Click(sender As Object, e As EventArgs) Handles Btn_Refresh_2.Click
        Kosong_Tab_2()
    End Sub


    Private Sub Cmb_Filter_Tab_2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Filter_Tab_2.SelectedIndexChanged
        If Cmb_Filter_Tab_2.Items.Count = 0 OrElse Cmb_Filter_Tab_2.SelectedIndex = -1 Then Exit Sub

        If Cmb_Filter_Tab_2.SelectedIndex = 0 Then
            Txt_Filter_Tab_2.Enabled = False
        Else
            Txt_Filter_Tab_2.Enabled = True
        End If
        Txt_Filter_Tab_2.Text = ""
    End Sub

    Private Sub Load_Data_Product_Waste(ByVal Optional Filter As Boolean = False)
        Try
            OpenConn()

            Lv_Product_Data.Items.Clear() : Lv_Product_User_Approve.Items.Clear() : Lv_Product_Detail_Barang.Items.Clear()

            SQL = "select distinct b.No_Transaksi, a.No_Faktur, a.Lokasi, a.Kode_Stock_Owner, a.Tanggal, a.Jam, a.Keterangan, a.UserID as User_Input, "
            SQL = SQL & "isnull((x.isCompleted), 'Y') as isCompleted "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "outer apply( "
            SQL = SQL & "select top 1 'T' as isCompleted "
            SQL = SQL & "from N_EMI_Transaksi_Approval_Waste z "
            SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur_Waste = a.No_Faktur "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and z.Flag_Approve is null "
            SQL = SQL & ") x "
            SQL = SQL & "where a.Status is null and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.Jenis_Approval = 'Waste_Produk' "
            SQL = SQL & "and a.Flag_Waste_Product = 'Y'	"

            If Filter Then
                SQL = SQL & "and " & arrFilterTab2(Cmb_Filter_Tab_2.SelectedIndex) & " like '%" & Txt_Filter_Tab_2.Text & "%' "
            End If
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Product_Data.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("User_Input"))

                    If Dr("isCompleted") = "T" Then
                        Lv.BackColor = Color.White
                    ElseIf Dr("isCompleted") = "Y" Then
                        Lv.BackColor = Color.LightGreen
                    Else
                        Lv.BackColor = Color.White
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

    Private Sub Lv_Product_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Product_Data.SelectedIndexChanged
        If Lv_Product_Data.Items.Count = 0 OrElse Lv_Product_Data.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()
            Tab2_Get_Lv_Product_Data(Lv_Product_Data.FocusedItem.Index)

            Dim No_faktur As String = Lv_Product_NoFaktur
            Dim No_Transaksi As String = Lv_Product_NoTransaksiApproval

            Lv_Product_User_Approve.Items.Clear()
            SQL = "select c.username, b.Approval_Level, b.Flag_Approve, b.Tanggal_Approve, b.Jam_Approve, b.Id_User_Android_Approve, b.jabatan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "inner join Emi_Users c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_User_Android_Approve = c.id "
            SQL = SQL & "where a.Status is null and b.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_faktur & "' "
            SQL = SQL & "and b.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "order by b.Approval_Level ASC "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Product_User_Approve.Items.Add(Dr("username"))
                    Lv.SubItems.Add(Dr("Approval_Level"))

                    If General_Class.CekNULL(Dr("Flag_Approve")) = "Y" Then
                        Lv.SubItems.Add("Approved")
                        Lv.BackColor = Color.LightGreen
                    ElseIf General_Class.CekNULL(Dr("Flag_Approve")) = "T" Then
                        Lv.SubItems.Add("Rejected")
                        Lv.ForeColor = Color.White
                        Lv.BackColor = Color.DarkRed
                    Else
                        Lv.SubItems.Add("On Process")
                    End If

                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Approve")) = "", "-", Dr("Tanggal_Approve")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam_Approve")) = "", "-", Dr("Jam_Approve")))
                    Lv.SubItems.Add(Dr("Id_User_Android_Approve"))
                    Lv.SubItems.Add(Dr("jabatan"))
                Loop
            End Using

            Lv_Product_Detail_Barang.Items.Clear()
            SQL = "select a.Kode_Stock_Owner as Lokasi, b.kode_barang, c.Nama as Nama_Barang, b.Total, b.Satuan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
            SQL = SQL & "inner join N_EMI_Transaksi_Transfer_Waste_Produk_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "inner join barang c on a.kode_perusahaan = c.kode_perusahaan and a.kode_stock_owner = c.kode_Stock_owner and b.kode_barang = c.kode_barang "
            SQL = SQL & "where a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_faktur & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Product_Detail_Barang.Items.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("kode_barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Total"), "N4"))
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
    Private Sub CetakFakturToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CetakFakturToolStripMenuItem1.Click
        If Lv_Product_Data.Items.Count = 0 AndAlso Lv_Product_Data.FocusedItem Is Nothing Then Exit Sub

        Tab2_Get_Lv_Product_Data(Lv_Product_Data.FocusedItem.Index)
        Dim No_Approval As String = Lv_Product_NoTransaksiApproval
        Dim No_Faktur As String = Lv_Product_NoFaktur

        Try
            OpenConn()

            '===========================
            '=     CEK BUTTON ROLE     =
            '===========================
            If CekButtonRole("Cetak_Faktur_Waste_Produk") = "T" Then
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Cetak Faktur Waste Product", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If


            '===============================
            '=     CEK TRANSAKSI WASTE     =
            '===============================
            SQL = "select top 1 a.Kode_Perusahaan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "where a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
            SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    CloseConn()
                    MessageBox.Show("No Transaksi Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select top 1 a.Kode_Perusahaan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "where a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
            SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    CloseConn()
                    MessageBox.Show("No Transaksi Belum Melakukan Pengajuan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===============================================
            '=     CEK APAKAH SEMUA USER SUDAH APPROVE     =
            '===============================================
            SQL = "select top 1 a.Kode_Perusahaan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "where a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
            SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
            SQL = SQL & "and b.flag_approve is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CloseConn()
                    MessageBox.Show("Terdapat User yang Belum Melakukan Approval", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            '=======================================
            '=     CEK APAKAH ADA YANG DITOLAK     =
            '=======================================
            SQL = "select top 1 a.Kode_Perusahaan "
            SQL = SQL & "from N_EMI_Transaksi_Transfer_Waste_Produk a "
            SQL = SQL & "inner join N_EMI_Transaksi_Approval_Waste b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur_Waste "
            SQL = SQL & "where a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Waste_Product = 'Y' "
            SQL = SQL & "and a.No_Faktur = '" & No_Faktur & "' "
            SQL = SQL & "and b.no_transaksi = '" & No_Approval & "' "
            SQL = SQL & "and b.flag_approve = 'T' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    CloseConn()
                    MessageBox.Show("Transaksi Ditolak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            SQL = "select kode_perusahaan from N_EMI_View_Berita_Acara_pemusnahan_waste_produk "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & No_Faktur & "' and Jenis_Approval = 'Waste_Produk' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New N_EMI_CR_Berita_Acara_Pemusnahan_Waste_Produk
                    kertas = "Faktur"

                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_pemusnahan_waste_produk.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_pemusnahan_waste_produk.no_faktur}='" & No_Faktur & "' and {N_EMI_View_Berita_Acara_pemusnahan_waste_produk.Jenis_Approval}='Waste_Produk' "
                        CrDoc.SummaryInfo.ReportTitle = "Waste Process"
                        .Text = "Waste Process"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With

                    '============================================================================================================================================
                    '============================================================================================================================================
                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    'CrDoc.PrintOptions.PrinterName = PrinterNameTS
                    'CrDoc.RecordSelectionFormula = "{N_EMI_View_Berita_Acara_Waste_Process.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_View_Berita_Acara_Waste_Process.no_faktur}='" & Faktur_Pemusnahaan & "' "
                    ''CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    'doctoprint.PrinterSettings.PrinterName = PrinterNameTS
                    ''doctoprint.DefaultPageSettings.Landscape = True
                    'Dim rawKind As Integer
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        Exit For
                    '    End If
                    'Next

                    'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    'CrDoc.PrintToPrinter(1, False, 1, 99)

                    'MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub









End Class