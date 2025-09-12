Public Class N_EMI_Transaksi_Bypass_Military_Sampling



    Dim Switch_Auto_Complete As Boolean = False

    Dim Lv_NoSplit, Lv_Batch, Lv_Tahapan_Military, Lv_Tanggal_GR, Lv_Hari_Lewat, Lv_Kode_Stock_Owner, Lv_Kode_Barang, Lv_Nama_Barang, Lv_Jumlah_PO, Lv_Jumlah_GR, Lv_Satuan, Lv_Nopo As String

    Dim item_NoSplit As Integer = 0
    Dim item_Batch As Integer = 1
    Dim item_Tahapan_Military As Integer = 2
    Dim item_Tanggal_GR As Integer = 3
    Dim item_Hari_Lewat As Integer = 4
    Dim item_Kode_Stock_Owner As Integer = 5
    Dim item_Kode_Barang As Integer = 6
    Dim item_Nama_Barang As Integer = 7
    Dim item_Jumlah_PO As Integer = 8
    Dim item_Jumlah_GR As Integer = 9
    Dim item_Satuan As Integer = 10
    Dim item_no_po As Integer = 11



    Private Sub N_EMI_Transaksi_Bypass_Military_Sampling_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Split", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Batch", 85, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tahapan", 85, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tanggal GR", 110, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Hari Terlewat", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang", 300, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah PO", 150, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Jumlah GR", 150, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("no_po", 0, HorizontalAlignment.Center)
        Lv_Data.View = View.Details




        Lv_PO.Columns.Clear()
        Lv_PO.Columns.Add("No PO", 130, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Tanggal", 110, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Jam", 100, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
        Lv_PO.View = View.Details

        Lv_Split.Clear()
        Lv_Split.Columns.Add("No Split", 130, HorizontalAlignment.Left)
        Lv_Split.Columns.Add("Tanggal", 110, HorizontalAlignment.Left)
        Lv_Split.Columns.Add("Jam", 100, HorizontalAlignment.Left)
        Lv_Split.Columns.Add("Keterangan", 250, HorizontalAlignment.Left)
        Lv_Split.View = View.Details



        Kosong()
    End Sub

    Public Sub Kosong()

        Switch_Auto_Complete = False
        Txt_No_PO.Text = OpsiSeluruh
        Txt_No_Spit.Text = OpsiSeluruh
        Switch_Auto_Complete = True

        Load_Data_Lv()
    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)
        Lv_NoSplit = Lv_Data.Items(index).SubItems(item_NoSplit).Text
        Lv_Batch = Lv_Data.Items(index).SubItems(item_Batch).Text
        Lv_Tahapan_Military = Lv_Data.Items(index).SubItems(item_Tahapan_Military).Text
        Lv_Tanggal_GR = Lv_Data.Items(index).SubItems(item_Tanggal_GR).Text +
        Lv_Hari_Lewat = Lv_Data.Items(index).SubItems(item_Hari_Lewat).Text
        Lv_Kode_Stock_Owner = Lv_Data.Items(index).SubItems(item_Kode_Stock_Owner).Text
        Lv_Kode_Barang = Lv_Data.Items(index).SubItems(item_Kode_Barang).Text
        Lv_Nama_Barang = Lv_Data.Items(index).SubItems(item_Nama_Barang).Text
        Lv_Jumlah_PO = Lv_Data.Items(index).SubItems(item_Jumlah_PO).Text
        Lv_Jumlah_GR = Lv_Data.Items(index).SubItems(item_Jumlah_GR).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_Nopo = Lv_Data.Items(index).SubItems(item_no_po).Text
    End Sub

    Private Sub Load_Data_Lv()
        Try
            OpenConn()

            Lv_Data.Items.Clear()
            'SQL = "select c.No_PO, a.No_Production_Order as No_Split, a.No_Transaksi, b.Tahap as Batch, e.No_Sampel, a.Tanggal, a.Jam, e.Tanggal as Tanggal_Register_Sample, e.Jam as Jam_Register_Sample, b.Lokasi_Gudang as Kode_Stock_Owner, "
            'SQL = SQL & "c.Kode_Barang, d.nama as Nama_Barang, sum(b.Jumlah) as Jumlah, b.Satuan, "
            'SQL = SQL & "CONCAT(DATEDIFF(DAY, e.Tanggal, CAST(GETDATE() AS DATE)) - 4, ' Hari') AS Hari_Lewat, "

            'SQL = SQL & "isnull(( select top 1 z.Tahap_Military_Sampling from N_EMI_Military_Sampling z "
            'SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
            'SQL = SQL & "and z.No_Split = a.No_Production_Order "
            'SQL = SQL & "and z.No_Batch = b.Tahap "
            'SQL = SQL & "and z.status is null "
            'SQL = SQL & "order by z.Tahap_Military_Sampling DESC "
            'SQL = SQL & "), 0) + 1 as Tahap_Military_Sampling "

            'SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Emi_Split_Production_Order c, barang d, N_EMI_LAB_PO_Sampel e, EMI_Master_Mesin f "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = b.Kode_Perusahaan and c.Kode_Perusahaan =d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan and e.Kode_Perusahaan = f.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            'SQL = SQL & "and a.No_Production_Order = c.No_Transaksi "
            'SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and c.No_PO = e.No_Po and a.No_Production_Order = e.No_Split_Po and b.Tahap = e.No_Batch and e.Id_Mesin = f.Id_Master_Mesin "
            'SQL = SQL & "and f.Nama_Mesin = 'AUTOCLAVE' "
            'SQL = SQL & "and a.Status is null and c.Status is null and e.Status is null "

            'If Txt_No_PO.Text.Trim.Length <> 0 And Not Txt_No_PO.Text.ToUpper = OpsiSeluruh.ToUpper Then
            '    SQL = SQL & "and c.No_PO = '" & Txt_No_PO.Text & "' "
            'End If

            'If Txt_No_Spit.Text.Trim.Length <> 0 And Not Txt_No_Spit.Text.ToUpper = OpsiSeluruh.ToUpper Then
            '    SQL = SQL & "and a.No_Production_Order = '" & Txt_No_Spit.Text & "' "
            'End If

            ''=============== UNTUK FILTER JIKA HANYA 1 KALI SAJA BYPASS ===============
            ''SQL = SQL & "and not exists (select 1 from N_EMI_Transaksi_Bypass_Military_Sampling z "
            ''SQL = SQL & "where z.No_Po = c.No_PO "
            ''SQL = SQL & "and z.No_Split = a.No_Production_Order and z.No_Batch = b.Tahap and z.status is null) "

            ''=============== JANGGAN LUPA DI UNCOMMENT ===============
            ''SQL = SQL & "and c.Flag_Hasil_Produksi_GR = 'Y' "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and DATEADD(DAY, 4, e.Tanggal) <= CAST(GETDATE() AS DATE) "
            'SQL = SQL & "and ( not exists ( select 1  from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Po = c.No_PO and z.No_Split_Po = a.No_Production_Order and z.No_Batch = b.Tahap ) "
            'SQL = SQL & "OR "
            'SQL = SQL & "exists ( select 1 from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Po = c.No_PO and z.No_Split_Po = a.No_Production_Order and z.No_Batch = b.Tahap and z.Flag_Ok IS NULL ))"

            ''=============== BATASAN HANYA SAMPAI GR 3 ===============
            'SQL = SQL & "and c.Flag_Hasil_Produksi_GR3 is null "

            'SQL = SQL & "and not ( "
            'SQL = SQL & "exists ( "
            'SQL = SQL & "select 1 "
            'SQL = SQL & "from N_EMI_Transaksi_Bypass_Military_Sampling z "
            'SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
            'SQL = SQL & "and z.No_Split = a.No_Production_Order "
            'SQL = SQL & "and z.No_Batch = b.Tahap "
            'SQL = SQL & "and z.Status IS NULL ) "
            'SQL = SQL & "and "
            'SQL = SQL & "(ISNULL(( select top 1 z.Tahapan from N_EMI_Transaksi_Bypass_Military_Sampling z "
            'SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
            'SQL = SQL & "and z.No_Split = a.No_Production_Order "
            'SQL = SQL & "and z.No_Batch = b.Tahap "
            'SQL = SQL & "and z.Status IS NULL "
            'SQL = SQL & "order by z.Tahapan DESC "
            'SQL = SQL & "), 0) "
            'SQL = SQL & "= "
            'SQL = SQL & "isnull(( "
            'SQL = SQL & "select top 1 z.Tahap_Military_Sampling "
            'SQL = SQL & "from N_EMI_Military_Sampling z "
            'SQL = SQL & "where z.Kode_Perusahaan = a.Kode_Perusahaan "
            'SQL = SQL & "and z.No_Split = a.No_Production_Order "
            'SQL = SQL & "and z.No_Batch = b.Tahap "
            'SQL = SQL & "and z.Status IS NULL "
            'SQL = SQL & "order by z.Tahap_Military_Sampling DESC "
            'SQL = SQL & "), 0) + 1)) "

            'SQL = SQL & "group by c.No_PO, a.No_Production_Order, a.No_Transaksi, b.Tahap, e.No_Sampel, a.Tanggal, a.Jam, e.Tanggal, e.Jam, b.Lokasi_Gudang, "
            'SQL = SQL & "c.Kode_Barang, d.nama, b.Satuan, e.Tanggal, a.Kode_Perusahaan"



            SQL = "select a.No_Production_Order as No_Split, b.Tahap as Batch, "
            SQL = SQL & "isnull((select top 1 z.Tahap_Military_Sampling from N_EMI_Military_Sampling z where z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Split = a.No_Production_Order and z.No_Batch = b.Tahap and z.status is null order by z.Tahap_Military_Sampling DESC "
            SQL = SQL & ")+1, 1) as Tahap_Military_Sampling, c.Tanggal as Tgl_GR1, "
            SQL = SQL & "CONCAT(DATEDIFF(DAY, c.Tanggal, CAST(GETDATE() AS DATE)) - 4, ' Hari') AS Hari_Lewat, "
            SQL = SQL & "c.Kode_Stock_Owner, c.Kode_Barang, e.nama as Nama_Barang, "
            SQL = SQL & "d.Jumlah as Jumlah_PO, d.Satuan, sum(b.jumlah) as Jumlah_GR1, c.Satuan, a.no_production_order "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, EMI_Production_Results_Detail_Barang c, Emi_Split_Production_Order d, barang e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and c.Qty_Hasil_Produksi <> 0 and c.Qty_Hasil_Produksi is not null "
            SQL = SQL & "and c.No_Transaksi = b.No_Transaksi and b.Jumlah <> 0 "
            SQL = SQL & "and b.Proses = c.Proses "
            SQL = SQL & "and a.No_Production_Order = d.No_Transaksi "
            SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and c.Kode_Stock_Owner = e.Kode_Stock_Owner and c.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and a.Status is null and c.status is null "
            SQL = SQL & "and ( not exists ( select 1  from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Po = d.No_PO and z.No_Split_Po = a.No_Production_Order and z.No_Batch = b.Tahap) "
            SQL = SQL & "or "
            SQL = SQL & "exists ( select 1 from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Po = d.No_PO and z.No_Split_Po = a.No_Production_Order and z.No_Batch = b.Tahap and z.Flag_Ok IS NULL) "
            SQL = SQL & "or "
            SQL = SQL & "exists ( select 1 from N_EMI_LAB_Hasil_Uji_Validasi_Final z where z.No_Po = d.No_PO and z.No_Split_Po = a.No_Production_Order and z.No_Batch = b.Tahap and z.Flag_Ok = 'T')) "
            SQL = SQL & "and d.Flag_Hasil_Produksi_GR3 is null "
            SQL = SQL & "AND NOT ( EXISTS ( SELECT 1 FROM N_EMI_Transaksi_Bypass_Military_Sampling z WHERE z.Kode_Perusahaan = a.Kode_Perusahaan AND z.No_Split = a.No_Production_Order "
            SQL = SQL & "AND z.No_Batch = b.Tahap "
            SQL = SQL & "and z.Tahapan = ( "
            SQL = SQL & "ISNULL(( SELECT TOP 1 x.Tahap_Military_Sampling FROM N_EMI_Military_Sampling x WHERE x.Kode_Perusahaan = a.Kode_Perusahaan AND x.No_Split = a.No_Production_Order "
            SQL = SQL & "AND x.No_Batch = b.Tahap and x.No_GR = 1 AND x.Status IS NULL ORDER BY x.Tahap_Military_Sampling DESC ), 0) + 1) "
            SQL = SQL & "AND z.Status IS NULL) "
            SQL = SQL & "AND ( ISNULL((SELECT TOP 1 z.Tahapan FROM N_EMI_Transaksi_Bypass_Military_Sampling z WHERE z.Kode_Perusahaan = a.Kode_Perusahaan AND z.No_Split = a.No_Production_Order "
            SQL = SQL & "AND z.No_Batch = b.Tahap AND z.Status IS NULL ORDER BY z.Tahapan DESC ), 0)  "
            SQL = SQL & "= "
            SQL = SQL & "ISNULL(( SELECT TOP 1 z.Tahap_Military_Sampling  FROM N_EMI_Military_Sampling z WHERE z.Kode_Perusahaan = a.Kode_Perusahaan AND z.No_Split = a.No_Production_Order "
            SQL = SQL & "AND z.No_Batch = b.Tahap and z.No_GR = 1 AND z.Status IS NULL ORDER BY z.Tahap_Military_Sampling DESC ), 0) + 1 )) "
            SQL = SQL & "group by a.Kode_Perusahaan, a.No_Production_Order, b.Tahap, c.Tanggal, c.Kode_Stock_Owner, c.Kode_Barang, e.nama, "
            SQL = SQL & "d.Jumlah, d.Satuan, c.Satuan, a.no_production_order "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Split"))
                    Lv.SubItems.Add(Dr("Batch"))
                    Lv.SubItems.Add(Dr("Tahap_Military_Sampling"))
                    Lv.SubItems.Add(Format(Dr("Tgl_GR1"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Hari_Lewat"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_PO"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_GR1"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("no_production_order"))
                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Txt_No_PO_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_PO.TextChanged
        If Switch_Auto_Complete = False Then Exit Sub

        If Txt_No_PO.Text.Trim.Length = 0 Then
            Lv_PO.Visible = False
            Lv_PO.Location = New Point(1050, 79)
            Txt_No_PO.Text = ""
            Exit Sub
        Else
            Lv_PO.Location = New Point(113, 79)
            Lv_PO.Visible = True
        End If

        Try
            OpenConn()

            Lv_PO.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_PO.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select distinct c.No_Faktur, c.Tanggal_Release, c.Jam_Release, c.Keterangan "
            SQL = SQL & "from Emi_Production_Results a, Emi_Split_Production_Order b, EMI_Order_Produksi c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and b.No_PO = c.No_Faktur "
            SQL = SQL & "and b.Flag_Hasil_Produksi_GR = 'Y' "
            SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and c.No_Faktur like '%" & Txt_No_PO.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_PO.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal_Release"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam_Release"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub Txt_No_PO_Leave(sender As Object, e As EventArgs) Handles Txt_No_PO.Leave
        If Txt_No_PO.Text.Trim.Length = 0 Then Exit Sub
        If Lv_PO.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_PO.Text.ToUpper = OpsiSeluruh.ToUpper Then

                SQL = "select distinct c.No_Faktur, c.Tanggal_Release, c.Jam_Release, c.Keterangan "
                SQL = SQL & "from Emi_Production_Results a, Emi_Split_Production_Order b, EMI_Order_Produksi c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
                SQL = SQL & "and b.No_PO = c.No_Faktur "
                SQL = SQL & "and b.Flag_Hasil_Produksi_GR = 'Y' "
                SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and c.No_Faktur = '" & Txt_No_PO.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_PO.Text = Dr("No_Faktur")
                        Txt_No_Spit.Focus()
                    Else
                        MessageBox.Show("No PO tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_PO.Text = ""
                        Txt_No_PO.Focus()
                    End If

                    Lv_PO.Visible = False
                    Lv_PO.Location = New Point(1050, 79)
                End Using
            Else
                Txt_No_Spit.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_PO_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_PO.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_PO.Text.Trim.Length = 0 Then Txt_No_PO.Focus()
            Txt_No_PO_Leave(Txt_No_PO, e)

            Lv_PO.Visible = False
            Lv_PO.Location = New Point(1050, 79)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_PO_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_PO.KeyDown
        If e.KeyCode = Keys.Down Then Lv_PO.Focus()
    End Sub

    Private Sub Txt_No_Spit_TextChanged(sender As Object, e As EventArgs) Handles Txt_No_Spit.TextChanged
        If Switch_Auto_Complete = False Then Exit Sub

        If Txt_No_Spit.Text.Trim.Length = 0 Then
            Lv_Split.Visible = False
            Lv_Split.Location = New Point(1050, 104)
            Txt_No_Spit.Text = ""
            Exit Sub
        Else
            Lv_Split.Location = New Point(113, 104)
            Lv_Split.Visible = True
        End If

        Try
            OpenConn()

            Lv_Split.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Split.Items.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)
            Lv.SubItems.Add(OpsiSeluruh)

            SQL = "select distinct b.No_Transaksi, b.Tanggal, b.Jam, b.No_Batch as Keterangan "
            SQL = SQL & "from Emi_Production_Results a, Emi_Split_Production_Order b, EMI_Order_Produksi c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
            SQL = SQL & "and b.No_PO = c.No_Faktur "
            SQL = SQL & "and b.Flag_Hasil_Produksi_GR = 'Y' "
            SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            If Not Txt_No_PO.Text.ToUpper = OpsiSeluruh.ToUpper Or Txt_No_PO.Text.Trim.Length = 0 Then
                SQL = SQL & "and c.No_Faktur = '" & Txt_No_PO.Text & "' "
            End If
            SQL = SQL & "and b.No_Transaksi like '%" & Txt_No_Spit.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Split.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Jam"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Spit_Leave(sender As Object, e As EventArgs) Handles Txt_No_Spit.Leave
        If Txt_No_Spit.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Split.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_No_Spit.Text.ToUpper = OpsiSeluruh.ToUpper Then


                SQL = "select distinct b.No_Transaksi as NoSplit, a.No_Transaksi, b.Tanggal, b.Jam, b.No_Batch as Keterangan "
                SQL = SQL & "from Emi_Production_Results a, Emi_Split_Production_Order b, EMI_Order_Produksi c "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
                SQL = SQL & "and a.No_Production_Order = b.No_Transaksi "
                SQL = SQL & "and b.No_PO = c.No_Faktur "
                SQL = SQL & "and b.Flag_Hasil_Produksi_GR = 'Y' "
                SQL = SQL & "and a.Status is null and b.Status is null and c.Status is null "
                SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                If Not Txt_No_PO.Text.ToUpper = OpsiSeluruh.ToUpper Or Txt_No_PO.Text.Trim.Length = 0 Then
                    SQL = SQL & "and c.No_Faktur = '" & Txt_No_PO.Text & "' "
                End If
                SQL = SQL & "and b.No_Transaksi = '" & Txt_No_Spit.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_No_Spit.Text = Dr("NoSplit")
                        Btn_Cari.Focus()

                    Else
                        MessageBox.Show("No Split tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_No_Spit.Text = ""
                        Txt_No_Spit.Focus()
                    End If

                    Lv_Split.Visible = False
                    Lv_Split.Location = New Point(1050, 104)
                End Using




            Else
                Btn_Cari.Focus()
            End If



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_No_Spit_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_No_Spit.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_No_Spit.Text.Trim.Length = 0 Then Txt_No_Spit.Focus()
            Txt_No_Spit_Leave(Txt_No_Spit, e)

            Lv_Split.Visible = False
            Lv_Split.Location = New Point(1050, 104)

            'Txt_KdKategori.Focus()
        End If
    End Sub

    Private Sub Txt_No_Spit_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_No_Spit.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Split.Focus()
    End Sub

    Private Sub Lv_PO_DoubleClick(sender As Object, e As EventArgs) Handles Lv_PO.DoubleClick
        If Lv_PO.Items.Count = 0 Or Lv_PO.FocusedItem.Index = -1 Then Exit Sub

        Dim No_PO As String = Lv_PO.FocusedItem.SubItems(0).Text

        Switch_Auto_Complete = False
        Txt_No_PO.Text = No_PO
        Switch_Auto_Complete = True

        Lv_PO.Location = New Point(1050, 79)
        Lv_PO.Visible = False

        Txt_No_Spit.Focus()
    End Sub

    Private Sub Lv_PO_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_PO.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_PO_DoubleClick(Lv_PO, e)
        End If
    End Sub

    Private Sub Lv_Split_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Split.DoubleClick
        If Lv_Split.Items.Count = 0 Or Lv_Split.FocusedItem.Index = -1 Then Exit Sub

        Dim No_PO As String = Lv_Split.FocusedItem.SubItems(0).Text

        Switch_Auto_Complete = False
        Txt_No_Spit.Text = No_PO
        Switch_Auto_Complete = True

        Lv_Split.Location = New Point(1050, 104)
        Lv_Split.Visible = False

        Btn_Cari.Focus()
    End Sub

    Private Sub Lv_Split_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Split.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Split_DoubleClick(Lv_Split, e)
        End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Load_Data_Lv()
    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem.Index = -1 Then
            MessageBox.Show("Pilih Dahulu Data yang Ingin Dibypass", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Bypass Military Sampling"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Bypass_Military_Sampling") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Bypass Military Sampling", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            'If MessageBox.Show("Yakin Ingin Melakukan Bypass Split ini?", JudulNotif, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then
            '    CloseTrans()
            '    CloseConn()
            '    Exit Sub
            'End If

            Get_Data_Lv(Lv_Data.FocusedItem.Index)

            Dim No_PO As String = Lv_Nopo
            Dim No_Split As String = Lv_NoSplit
            Dim Batch As String = Lv_Batch
            Dim Jumlah_PO As Double = Lv_Jumlah_PO
            Dim jumlah_GR As Double = Lv_Jumlah_GR


            '====================================================
            '=     CEK APAKAH DATA SUDAH MELEWATI STEP GR 1     =
            '====================================================
            SQL = "select top 1 1 from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, EMI_Production_Results_Detail_Barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & Lv_NoSplit & "' "
            SQL = SQL & "and b.Tahap = '" & Batch & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Belum Selesai Dari GR 1", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==================================================
            '=     CEK APAKAH DATA SUDAH TERIMA HASIL LAB     =
            '==================================================
            SQL = "select Flag_Ok from N_EMI_LAB_Hasil_Uji_Validasi_Final "
            SQL = SQL & "where No_Po = '" & Lv_Nopo & "' "
            SQL = SQL & "and No_Split_Po = '" & Lv_NoSplit & "' "
            SQL = SQL & "and No_Batch = '" & Batch & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Ok")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Sudah Menerima Hasil Uji Lab", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            '======================================================
            '=     CEK APAKAH DATA SUDAH DI BYPASS SEBELUMNYA     =
            '======================================================
            'SQL = "select 1 from N_EMI_Transaksi_Bypass_Military_Sampling "
            'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and No_PO = '" & No_PO & "' "
            'SQL = SQL & "and No_Split = '" & No_Split & "' "
            'SQL = SQL & "and No_Batch = '" & Batch & "' "
            'SQL = SQL & "and status is null "
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Data Sudah Dibypass Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using

            '==============================================
            '=     CEK APAKAH DATA SUDAH LEWAT 4 HARI     =
            '==============================================
            'SQL = "select 1 from N_EMI_LAB_PO_Sampel a, EMI_Master_Mesin b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.Id_Mesin = b.Id_Master_Mesin "
            'SQL = SQL & "and b.Nama_Mesin = 'AUTOCLAVE' "
            'SQL = SQL & "and a.Status is null "
            'SQL = SQL & "and DATEADD(DAY, 4, a.Tanggal) <= CAST(GETDATE() AS DATE)"
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.no_po = '" & No_PO & "' "
            'SQL = SQL & "and a.No_Split_Po = '" & No_Split & "' "
            'SQL = SQL & "and a.No_Batch = '" & Batch & "' "
            'Using Dr = OpenTrans(SQL)
            '    If Not Dr.Read Then
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("Data Belum di Registerasi Lab atau Belum lewat dari H+4 Hari", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '        Exit Sub
            '    End If
            'End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Get_Data_Lv(Lv_Data.FocusedItem.Index)

        N_EMI_SD_Transaksi_Bypass_Military_Sampling.Txt_No_PO.Text = Lv_Nopo
        N_EMI_SD_Transaksi_Bypass_Military_Sampling.Txt_No_Split.Text = Lv_NoSplit
        N_EMI_SD_Transaksi_Bypass_Military_Sampling.Txt_Batch.Text = Lv_Batch
        N_EMI_SD_Transaksi_Bypass_Military_Sampling.Txt_Jumlah_PO.Text = Lv_Jumlah_PO
        N_EMI_SD_Transaksi_Bypass_Military_Sampling.Txt_Jumlah_GR.Text = Lv_Jumlah_GR
        N_EMI_SD_Transaksi_Bypass_Military_Sampling.Txt_Tahapan.Text = Lv_Tahapan_Military
        N_EMI_SD_Transaksi_Bypass_Military_Sampling.ShowDialog()





    End Sub

End Class