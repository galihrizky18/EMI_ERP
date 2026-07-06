Public Class N_EMI_Validasi_Transaksi_Request_Material_Tambahan


    Dim Lv_NoFaktur, Lv_NoSplit, Lv_Batch, Lv_Tanggal, Lv_Jam, Lv_Kd_So, Lv_Kd_Barang, Lv_Nm_Barang, Lv_Keterangan, Lv_UserID As String

    Dim Item_NoFaktur As Integer = 0
    Dim Item_NoSplit As Integer = 1
    Dim Item_Batch As Integer = 2
    Dim Item_Tanggal As Integer = 3
    Dim Item_Jam As Integer = 4
    Dim Item_Kd_So As Integer = 5
    Dim Item_Kd_Barang As Integer = 6
    Dim Item_Nm_Barang As Integer = 7
    Dim Item_Keterangan As Integer = 8
    Dim Item_UserID As Integer = 9



    Private Sub N_EMI_Validasi_Transaksi_Request_Material_Tambahan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("No Split", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Batch", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 90, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Stock Owner", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("User", 130, HorizontalAlignment.Center)
        Lv_Data.View = View.Details

        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("Lokasi", 130, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Lokasi Request", 130, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Jenis Material", 110, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Barang", 250, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_Detail.View = View.Details

        Kosong()
    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)
        Lv_NoFaktur = Lv_Data.Items(index).SubItems(Item_NoFaktur).Text
        Lv_Batch = Lv_Data.Items(index).SubItems(Item_Batch).Text
        Lv_Tanggal = Lv_Data.Items(index).SubItems(Item_Tanggal).Text
        Lv_Jam = Lv_Data.Items(index).SubItems(Item_Jam).Text
        Lv_Kd_So = Lv_Data.Items(index).SubItems(Item_Kd_So).Text
        Lv_Kd_Barang = Lv_Data.Items(index).SubItems(Item_Kd_Barang).Text
        Lv_Nm_Barang = Lv_Data.Items(index).SubItems(Item_Nm_Barang).Text
        Lv_Keterangan = Lv_Data.Items(index).SubItems(Item_Keterangan).Text
        Lv_UserID = Lv_Data.Items(index).SubItems(Item_UserID).Text
    End Sub

    Private Sub Kosong()

        Load_Data()
    End Sub

    Private Sub Load_Data()
        Try
            OpenConn()

            Lv_Data.Items.Clear() : Lv_Detail.Items.Clear()
            SQL = "select a.No_Faktur, a.No_Faktur_Order, a.Batch, a.Tanggal, a.Jam, a.Kode_Stock_Owner, a.Kode_Barang, b.Nama as Nama_Barang, a.Keterangan, a.UserId "
            SQL = SQL & "from Emi_Material_Requisition a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Flag_Tambah = 'Y' "
            SQL = SQL & "and a.Flag_Validasi_Tambah is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_Faktur_Order"))
                    Lv.SubItems.Add(Format(Dr("Batch"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("UserId"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub


    Private Sub Lv_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data.SelectedIndexChanged
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Get_Data_Lv(Lv_Data.FocusedItem.Index)
            Dim noFaktur As String = Lv_NoFaktur

            Lv_Detail.Items.Clear()
            SQL = "select b.Kode_Stock_Owner, b.Kode_Stock_Owner_Tujuan, b.Jenis_Material, b.Kode_Barang, c.Nama as Nama_Barang, b.Jumlah, b.Satuan "
            SQL = SQL & "from Emi_Material_Requisition a, Emi_Material_Requisition_Det b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Flag_Tambah = 'Y' "
            SQL = SQL & "and a.Flag_Validasi_Tambah is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & noFaktur & "' "
            SQL = SQL & "order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner_Tujuan"))
                    Lv.SubItems.Add(Dr("Jenis_Material"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
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

    Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim NoFaktur As String = Lv_Data.FocusedItem.SubItems(Item_NoFaktur).Text

            SQL = "select flag_Validasi_Tambah from Emi_Material_Requisition "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null "
            SQL = SQL & "and flag_tambah = 'Y' and No_Faktur = '" & NoFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("flag_Validasi_Tambah")) = "Y" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Data Request Sudah Validasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        Dr.Close()
                        SQL = "update Emi_Material_Requisition set flag_Validasi_Tambah = 'Y', User_Validasi_Tambah = '" & UserID & "', "
                        SQL = SQL & "Tanggal_Validasi_Tambah = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Validasi_Tambah = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null "
                        SQL = SQL & "and flag_tambah = 'Y' and No_Faktur = '" & NoFaktur & "' "
                        ExecuteTrans(SQL)

                    End If
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Request Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil DiValidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

    End Sub






End Class