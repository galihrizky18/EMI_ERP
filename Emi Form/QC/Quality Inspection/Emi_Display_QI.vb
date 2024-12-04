Public Class Emi_Display_QI

    Dim arrcari As New ArrayList

    Dim Lv_NoTrans, Lv_Nama, Lv_Rak, Lv_Tgl, Lv_Jam, Lv_Jumlah, Lv_Satuan, Lv_NoProduksiOrder, Lv_KdBarang, Lv_IdWarehouse, Lv_UrutDetailPallet, Lv_SnBaru, Lv_JumlahKecil, Lv_SatuanKecil As String

    Dim item_NoTrans As Integer = 0
    Dim item_Nama As Integer = 1
    Dim item_Rak As Integer = 2
    Dim item_Tanggal As Integer = 3
    Dim item_Jam As Integer = 4
    Dim item_Jumlah As Integer = 5
    Dim item_Satuan As Integer = 6
    Dim item_NoProduksiOrder As Integer = 7
    Dim item_KdBarang As Integer = 8
    Dim item_IdWarehouse As Integer = 9
    Dim item_UrutDetailPallet As Integer = 10
    Dim item_SnBaru As Integer = 11
    Dim item_JmlhKecil As Integer = 12
    Dim item_SatuanKecil As Integer = 13


    Private Sub Emi_Display_QI_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Display_QI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Initial_Lv()
        Kosong()
    End Sub

    Private Sub Kosong()
        Lv_Data.Items.Clear()
        arrcari.Clear()

        Txt_Filter_Value.Text = String.Empty

        Cmb_Filter_Jenis.Items.Clear()
        Cmb_Filter_Jenis.Items.Add("No Transaksi") : arrcari.Add("a.no_transaksi")
        Cmb_Filter_Jenis.Items.Add("Nama Barang") : arrcari.Add("e.nama")
        'Cmb_Filter_Jenis.Items.Add("Jumlah") : arrcari.Add("b.Jumlah")
        Cmb_Filter_Jenis.Items.Add("Satuan") : arrcari.Add("b.Satuan")

        Load_Lv()
    End Sub

    Private Sub Initial_Lv()
        Lv_Data.Columns.Clear()

        Lv_Data.Columns.Add("No Transaksi", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama", 250, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Rak", 180, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 150, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jam", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Jumlah", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Satuan", 90, HorizontalAlignment.Center)

        'HIDE
        Lv_Data.Columns.Add("No_PO", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kd_Barang", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Id_Warehouse", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Urut_Detail_Pallet", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Sn_Baru", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah_Kecil", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Satuan_KEcil", 0, HorizontalAlignment.Left)

        Lv_Data.View = View.Details
    End Sub

    Private Sub Load_Lv()

        Try
            OpenConn()

            Lv_Data.Items.Clear()

            SQL = "select a.No_Transaksi, a.No_Production_Order, c.Kode_Barang, e.Nama, b.Id_Warehouse, d.Keterangan, a.Tanggal, a.Jam, "
            SQL = SQL & "b.Jumlah, b.Satuan, b.NIlai_Barang, b.Satuan_Barang, b.Urut_Oto as urut_detail_pallet, b.SN_Baru "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Barang_SN c, View_Warehouse_Position d, barang e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.SN_Baru = c.Serial_Number "
            SQL = SQL & "and b.Id_Warehouse = d.Id_WMS_Warehouse_Position "
            SQL = SQL & "and c.Kode_Stock_Owner = e.Kode_Stock_Owner and c.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.Flag_QI = 'Y' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and b.Flag_Sudah_QI Is NULL "
            SQL = SQL & "order by a.No_Transaksi "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As New ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Jumlah"))
                    Lv.SubItems.Add(Dr("Satuan"))

                    'hide
                    Lv.SubItems.Add(Dr("No_Production_Order"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Id_Warehouse"))
                    Lv.SubItems.Add(Dr("urut_detail_pallet"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SN_Baru")))
                    Lv.SubItems.Add(Dr("NIlai_Barang"))
                    Lv.SubItems.Add(Dr("Satuan_Barang"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)

        Lv_NoTrans = Lv_Data.Items(index).SubItems(item_NoTrans).Text
        Lv_Nama = Lv_Data.Items(index).SubItems(item_Nama).Text
        Lv_Rak = Lv_Data.Items(index).SubItems(item_Rak).Text
        Lv_Tgl = Lv_Data.Items(index).SubItems(item_Tanggal).Text
        Lv_Jam = Lv_Data.Items(index).SubItems(item_Jam).Text
        Lv_Jumlah = Lv_Data.Items(index).SubItems(item_Jumlah).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_NoProduksiOrder = Lv_Data.Items(index).SubItems(item_NoProduksiOrder).Text
        Lv_KdBarang = Lv_Data.Items(index).SubItems(item_KdBarang).Text
        Lv_IdWarehouse = Lv_Data.Items(index).SubItems(item_IdWarehouse).Text
        Lv_UrutDetailPallet = Lv_Data.Items(index).SubItems(item_UrutDetailPallet).Text
        Lv_SnBaru = Lv_Data.Items(index).SubItems(item_SnBaru).Text
        Lv_JumlahKecil = Lv_Data.Items(index).SubItems(item_JmlhKecil).Text
        Lv_SatuanKecil = Lv_Data.Items(index).SubItems(item_SatuanKecil).Text

    End Sub


    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub
    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Filter_Jenis.SelectedIndex = -1 Then
            MessageBox.Show("Jenis Harus Di Pilih Dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf Txt_Filter_Value.Text.Trim.Length = 0 Then
            MessageBox.Show("Value Harus Di Isi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Lv_Data.Items.Clear()

            SQL = "select a.No_Transaksi, a.No_Production_Order, c.Kode_Barang, e.Nama, b.Id_Warehouse, d.Keterangan, a.Tanggal, a.Jam, "
            SQL = SQL & "b.Jumlah, b.Satuan, b.NIlai_Barang, b.Satuan_Barang, b.Urut_Oto as urut_detail_pallet, b.SN_Baru "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Barang_SN c, View_Warehouse_Position d, barang e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.SN_Baru = c.Serial_Number "
            SQL = SQL & "and b.Id_Warehouse = d.Id_WMS_Warehouse_Position "
            SQL = SQL & "and c.Kode_Stock_Owner = e.Kode_Stock_Owner and c.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.Flag_QI = 'Y' "
            SQL = SQL & "and b.Flag_Sudah_QI Is NULL "
            SQL = SQL & "and a.Status is null "

            If Cmb_Filter_Jenis.SelectedIndex <> -1 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL = SQL & arrcari.Item(Cmb_Filter_Jenis.SelectedIndex) & "  like  '%" & Trim(Txt_Filter_Value.Text) & "%' "
            End If

            SQL = SQL & "order by a.No_Transaksi "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As New ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Jumlah"))
                    Lv.SubItems.Add(Dr("Satuan"))

                    'hide
                    Lv.SubItems.Add(Dr("No_Production_Order"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Id_Warehouse"))
                    Lv.SubItems.Add(Dr("urut_detail_pallet"))
                    Lv.SubItems.Add(General_Class.CekNULL(Dr("SN_Baru")))
                    Lv.SubItems.Add(Dr("NIlai_Barang"))
                    Lv.SubItems.Add(Dr("Satuan_Barang"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub ReleaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReleaseToolStripMenuItem.Click

        If Lv_Data.Items.Count = 0 Then Exit Sub

        Dim Pertanyaan As String = MessageBox.Show("Anda yakin data ini akan di Release . . ? ?", "Quality Inspection", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Pertanyaan = vbNo Then
            Exit Sub
        End If


        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Get_Data_Lv(Lv_Data.FocusedItem.Index)

            'CEK DATA ADA
            SQL = "select Kode_Perusahaan from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & Lv_SnBaru & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            'UPDATE FLAG_QI BARANG SN BERDASARKAN SN_BARU
                            SQL = "update Barang_SN set Flag_QI = null where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & Lv_SnBaru & "'"
                            ExecuteTrans(SQL)


                            Dim WarnaQI As String = ""
                            Dim No_Faktur_QC As String = ""
                            'GET QC TERAKHIR BERDASARAKAN NO PRODUKSI
                            SQL = "select top 1 No_Faktur, No_Fak_Produksi_Order, Kode_Stock_Owner, Kode_Barang, Warna from EMI_Hasil_QC_Produksi "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Fak_Produksi_Order = '" & Lv_NoProduksiOrder & "' "
                            SQL = SQL & "and Status is null "
                            SQL = SQL & "order by Step Desc"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    WarnaQI = Dr("Warna")
                                    No_Faktur_QC = Dr("No_Faktur")

                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("QC Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            'UPDATE WARNA Emi_Production_Results_Detail_Pallet BERDASARKAN HASIL QC
                            SQL = "update Emi_Production_Results_Detail_Pallet set Warna_QI = '" & WarnaQI & "', No_Faktur_QC = '" & No_Faktur_QC & "', "
                            SQL = SQL & "Flag_Sudah_QI = 'Y'"
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Transaksi = '" & Lv_NoTrans & "' and Id_Warehouse = '" & Lv_IdWarehouse & "' and SN_Baru = '" & Lv_SnBaru & "' "
                            ExecuteTrans(SQL)


                        Next


                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

                    End If
                End With


            End Using



            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil DiSimpan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



















End Class