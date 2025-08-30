Imports System.IO

Public Class N_EMI_Display_Request_Material_QC_Summary2

    Dim JudulForm As String = "Display - Request Material Quality Control"
    Dim arrTgl, arrLain As New ArrayList

    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Private imageBytes2 As Byte = Nothing
    Private FileSize2 As UInt32
    Private rawData2() As Byte
    Private fs2 As FileStream

    Dim kode_unik_print As String = ""
    Dim Random As New Random()


    Private Sub N_EMI_Display_Request_Material_QC_Summary_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("No Split", 130, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Tanggal", 110, HorizontalAlignment.Center) '2
        Lv_Data.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '3
        Lv_Data.Columns.Add("Barang", 250, HorizontalAlignment.Left) '4
        Lv_Data.Columns.Add("Keterangan", 280, HorizontalAlignment.Left) '5
        Lv_Data.Columns.Add("User ID", 110, HorizontalAlignment.Center) '6
        Lv_Data.View = View.Details

        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("", 0, HorizontalAlignment.Left) '0
        Lv_Detail.Columns.Add("Batch", 80, HorizontalAlignment.Center) '1
        Lv_Detail.Columns.Add("Jumlah PerBatch", 120, HorizontalAlignment.Right) '2
        Lv_Detail.Columns.Add("Jumlah Tambah", 120, HorizontalAlignment.Right) '3
        Lv_Detail.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '4
        Lv_Detail.Columns.Add("NoFaktur", 0, HorizontalAlignment.Left) '5
        Lv_Detail.Columns.Add("UrutDetail", 0, HorizontalAlignment.Left) '6
        Lv_Detail.View = View.Details

        Lv_Det.Columns.Clear()
        Lv_Det.Columns.Add("Lokasi Barang", 130, HorizontalAlignment.Left) '0
        Lv_Det.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '1
        Lv_Det.Columns.Add("Nama Barang ", 200, HorizontalAlignment.Left) '2
        Lv_Det.Columns.Add("Jumlah Kebutuhan", 130, HorizontalAlignment.Right) '3
        Lv_Det.Columns.Add("Jumlah Transfer", 130, HorizontalAlignment.Right) '4
        Lv_Det.Columns.Add("Jumlah Transfer Bags", 130, HorizontalAlignment.Right) '5
        Lv_Det.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_Det.View = View.Details

        '=============================================================================================

        Lv_ParentQC.Columns.Clear()
        Lv_ParentQC.Columns.Add("No Faktur", 130, HorizontalAlignment.Left) '0
        Lv_ParentQC.Columns.Add("No Split", 130, HorizontalAlignment.Left) '1
        Lv_ParentQC.Columns.Add("Tanggal", 110, HorizontalAlignment.Center) '2
        Lv_ParentQC.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left) '3
        Lv_ParentQC.Columns.Add("Barang", 250, HorizontalAlignment.Left) '4
        Lv_ParentQC.Columns.Add("Keterangan", 280, HorizontalAlignment.Left) '5
        Lv_ParentQC.Columns.Add("User ID", 110, HorizontalAlignment.Center) '6
        Lv_ParentQC.View = View.Details

        Lv_DetailQC.Columns.Clear()
        Lv_DetailQC.Columns.Add("", 0, HorizontalAlignment.Left) '0
        Lv_DetailQC.Columns.Add("Batch", 80, HorizontalAlignment.Center) '1
        Lv_DetailQC.Columns.Add("Jumlah PerBatch", 120, HorizontalAlignment.Right) '2
        Lv_DetailQC.Columns.Add("Jumlah Tambah", 120, HorizontalAlignment.Right) '3
        Lv_DetailQC.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '4
        Lv_DetailQC.Columns.Add("NoFaktur", 0, HorizontalAlignment.Left) '5
        Lv_DetailQC.Columns.Add("UrutDetail", 0, HorizontalAlignment.Left) '6
        Lv_Detail.View = View.Details

        Lv_DetQC.Columns.Clear()
        Lv_DetQC.Columns.Add("No Faktur", 120, HorizontalAlignment.Left) '0
        Lv_DetQC.Columns.Add("Tangal", 100, HorizontalAlignment.Center) '1
        Lv_DetQC.Columns.Add("Lokasi", 120, HorizontalAlignment.Left) '2
        Lv_DetQC.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '3
        Lv_DetQC.Columns.Add("Nama", 200, HorizontalAlignment.Left) '4
        Lv_DetQC.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '5
        Lv_DetQC.Columns.Add("Jumlah Bags", 120, HorizontalAlignment.Right) '6
        Lv_DetQC.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '7
        Lv_DetQC.Columns.Add("UrutDet", 0, HorizontalAlignment.Center) '8
        Lv_DetQC.Columns.Add("UrutOTO", 0, HorizontalAlignment.Center) '9
        Lv_DetQC.Columns.Add("SN", 0, HorizontalAlignment.Center) '10
        Lv_DetQC.View = View.Details




        Kosong()

    End Sub


    Private Sub Kosong()


        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear() : ComboBox1.Items.Clear()
            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner")) : ComboBox1.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            Cmb_Lokasi.Text = Lokasi : ComboBox1.Text = Lokasi

            If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
                Cmb_Lokasi.Enabled = False
                ComboBox1.Enabled = False
            Else
                Cmb_Lokasi.Enabled = True
                ComboBox1.Enabled = True
            End If

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Pembatalan_RM_QC") = "T" Then
                PembatalanRequestToolStripMenuItem.Visible = False
            Else
                PembatalanRequestToolStripMenuItem.Visible = True
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_Tanggal.Items.Clear() : arrTgl.Clear()
        Cmb_Tanggal.Items.Add("Tanggal Request") : arrTgl.Add("a.Tanggal")

        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Tanggal Request")

        Cmb_Lain.Items.Clear() : arrLain.Clear()
        Cmb_Lain.Items.Add("Lokasi") : arrLain.Add("a.Kode_Stock_Owner")
        Cmb_Lain.Items.Add("Kode Barang") : arrLain.Add("a.Kode_Barang")
        Cmb_Lain.Items.Add("Nama Barang") : arrLain.Add("b.Nama")
        Cmb_Lain.Items.Add("User ID") : arrLain.Add("a.UserId")
        Cmb_Lain.Items.Add("Keterangan") : arrLain.Add("a.Keterangan")

        ComboBox3.Items.Clear()
        ComboBox3.Items.Add("Lokasi")
        ComboBox3.Items.Add("Kode Barang")
        ComboBox3.Items.Add("Nama Barang")
        ComboBox3.Items.Add("User ID")
        ComboBox3.Items.Add("Keterangan")



        LoadParent()
        LoadParentQC()

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        LoadParent()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        LoadParentQC()
    End Sub

    Private Sub LoadParent()

        If Chk_Tanggal.Checked Then
            If Cmb_Tanggal.SelectedIndex = -1 Then
                MessageBox.Show("Harap Pilih Dahulu Parameter Tanggal", Judul, MessageBoxButtons.OK)
                Cmb_Tanggal.DroppedDown = True
                Cmb_Tanggal.Focus()
                Exit Sub
            End If
        End If

        If Chk_Lain.Checked Then
            If Cmb_Lain.SelectedIndex = -1 Then
                MessageBox.Show("Harap Pilih Dahulu Parameter Lain", Judul, MessageBoxButtons.OK)
                Cmb_Lain.DroppedDown = True
                Cmb_Lain.Focus()
                Exit Sub

            Else
                If Txt_Lain.Text.Trim.Length = 0 Then
                    MessageBox.Show("Value Parameter Lain Tidak Boleh Kosong", Judul, MessageBoxButtons.OK)
                    Txt_Lain.Focus()
                    Exit Sub
                End If
            End If
        End If

        Try
            OpenConn()

            Lv_Data.Items.Clear() : Lv_Detail.Items.Clear() : Lv_Det.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.No_Faktur, a.No_Faktur_Order, a.Tanggal, a.Jam, a.Kode_Barang, b.Nama as Nm_Barang, a.Keterangan, a.UserId, a.Status, a.Flag_Selesai "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, Barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            If Chk_HriIni.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.Tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If Chk_Tanggal.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrTgl(Cmb_Tanggal.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If Chk_Lain.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrLain.Item(Cmb_Lain.SelectedIndex) & " like '%" & Txt_Lain.Text & "%' "
            End If

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_Faktur_Order"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nm_Barang"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("UserId"))

                    If General_Class.CekNULL(Dr("Flag_Selesai")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
                        Lv.ForeColor = Color.Black
                    End If

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
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

    Private Sub LoadParentQC()

        If CheckBox2.Checked Then
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show("Harap Pilih Dahulu Parameter Tanggal", Judul, MessageBoxButtons.OK)
                ComboBox2.DroppedDown = True
                ComboBox2.Focus()
                Exit Sub
            End If
        End If

        If CheckBox3.Checked Then
            If ComboBox3.SelectedIndex = -1 Then
                MessageBox.Show("Harap Pilih Dahulu Parameter Lain", Judul, MessageBoxButtons.OK)
                ComboBox3.DroppedDown = True
                ComboBox3.Focus()
                Exit Sub

            Else
                If Txt_LainQC.Text.Trim.Length = 0 Then
                    MessageBox.Show("Value Parameter Lain Tidak Boleh Kosong", Judul, MessageBoxButtons.OK)
                    Txt_LainQC.Focus()
                    Exit Sub
                End If
            End If
        End If

        Try
            OpenConn()

            Lv_ParentQC.Items.Clear() : Lv_DetailQC.Items.Clear() : Lv_DetQC.Items.Clear()
            SQL = "select a.Kode_Perusahaan, a.No_Faktur, a.No_Faktur_Order, a.Tanggal, a.Jam, a.Kode_Barang, b.Nama as Nm_Barang, a.Keterangan, a.UserId, a.Status, a.Flag_Selesai "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, Barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.Tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrTgl(ComboBox2.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker3.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker4.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrLain.Item(ComboBox3.SelectedIndex) & " like '%" & Txt_LainQC.Text & "%' "
            End If

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_ParentQC.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_Faktur_Order"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nm_Barang"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("UserId"))

                    If General_Class.CekNULL(Dr("Flag_Selesai")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
                        Lv.ForeColor = Color.Black
                    End If

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
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

    Private Sub Lv_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data.SelectedIndexChanged

        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Data.FocusedItem.SubItems(0).Text

            Lv_Detail.Items.Clear() : Lv_Det.Items.Clear()
            SQL = "select a.No_Faktur, b.Batch, b.Qty_PerBatch, b.Total_Tambah, b.Flag_Terpenuhi, b.Urut_Oto, "
            SQL = SQL & "isnull(( select top 1 Satuan from N_EMI_Transaksi_Material_Requisition_QC_Det z "
            SQL = SQL & "where b.Kode_Perusahaan = z.Kode_Perusahaan and b.No_Faktur = z.No_Faktur and z.Urut_Detail = b.Urut_Oto ), '-') as Satuan "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.no_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' "
            SQL = SQL & "order by b.Batch"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add("")
                    Lv.SubItems.Add(Format(Dr("Batch"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Qty_PerBatch"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Total_Tambah"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    'Hide
                    Lv.SubItems.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Urut_Oto"))

                    If General_Class.CekNULL(Dr("Flag_Terpenuhi")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
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

    Private Sub Lv_Detail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Detail.SelectedIndexChanged
        If Lv_Detail.Items.Count = 0 Or Lv_Detail.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Detail.FocusedItem.SubItems(5).Text
            Dim SelectedUrutDetail As String = Lv_Detail.FocusedItem.SubItems(6).Text

            Lv_Det.Items.Clear()
            SQL = "select a.No_Faktur, c.Kode_Stock_Owner_Tujuan, c.Kode_Barang, d.Nama as Nama_Barang, c.Kebutuhan as Kebutuhan_Request, "
            SQL = SQL & "(isnull(sum(c.Jumlah_Per_Batch), 0) + isnull((sum(c.Jumlah_Tambah)), 0)) as Jumlah_Kebutuhan_Barang_PerBatch, "
            SQL = SQL & "isnull((select sum(z.Jumlah) from N_EMI_Transaksi_Material_Requisition_QC_Validasi z "
            SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan and z.No_Faktur_RM = c.No_Faktur and z.Urut_Det_RM = c.Urut_Oto "
            SQL = SQL & "), 0) as Jumlah_Transfer, "
            SQL = SQL & "isnull((select sum(z.Jumlah_Bags) from N_EMI_Transaksi_Material_Requisition_QC_Validasi z "
            SQL = SQL & "where z.Kode_Perusahaan = c.Kode_Perusahaan and z.No_Faktur_RM = c.No_Faktur and z.Urut_Det_RM = c.Urut_Oto "
            SQL = SQL & "), 0) as Jumlah_Transfer_Bags, c.Satuan "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b, N_EMI_Transaksi_Material_Requisition_QC_Det c, Barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail "
            SQL = SQL & "and c.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & SelectedFaktur & "' and b.Urut_Oto = '" & SelectedUrutDetail & "' "
            SQL = SQL & "group by a.No_Faktur, c.Kode_Stock_Owner_Tujuan, c.Kode_Barang, d.Nama, c.Kebutuhan, c.Kode_Perusahaan, c.No_Faktur, c.Urut_Oto, c.Satuan "
            SQL = SQL & "order by c.Kode_Barang"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Det.Items.Add(Dr("Kode_Stock_Owner_Tujuan"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Kebutuhan_Barang_PerBatch"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Transfer"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Transfer_Bags"), "N4"))
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

    '==============================================================================================================================================
    '=     HANDLE CONTEX MENU     
    '==============================================================================================================================================
    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Data.FocusedItem.Text)
    End Sub

    Private Sub CetakUlangBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeToolStripMenuItem.Click
        If Lv_Detail.Items.Count = 0 Or Lv_Detail.SelectedItems.Count = 0 Or Lv_Detail.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih Dahulu Batch yang akan Dicetak Ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim SelectedFaktur As String = Lv_Detail.FocusedItem.SubItems(5).Text
        Dim SelectedUrutDetail As String = Lv_Detail.FocusedItem.SubItems(6).Text

        Dim isCetak As Boolean = False

        Try
            OpenConn()

            '=======================================
            '=     CEK APAKAH DATA DI BATALKAN     =
            '=======================================
            SQL = "select Status from N_EMI_Transaksi_Material_Requisition_QC "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & SelectedFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Gagal Cetak Ulang Karena No Requestt Material Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Gagal Cetak Ulang Karena No Request Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '=================================================
            '=     CEK APAKAH DATA BATCH SUDAH TERPENUHI     =
            '=================================================
            SQL = "select b.Flag_Terpenuhi "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' "
            SQL = SQL & "and b.Urut_Oto = '" & SelectedUrutDetail & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Flag_Terpenuhi")) = "" Then
                        Dr.Close()
                        CloseConn()
                        MessageBox.Show("Gagal Cetak Ulang Karena Batch Belum Terpenuhi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Gagal Cetak Ulang Karena Batch Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            '=============================================
            '=     CEK APAKAH ADA DATA BATCH TERKAIT     =
            '=============================================
            SQL = "select b.Flag_Terpenuhi "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.status is null and b.Flag_Terpenuhi = 'Y' "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "'  "
            SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' "
            SQL = SQL & "and b.Urut_Oto = '" & SelectedUrutDetail & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    isCetak = True
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Gagal Cetak Ulang Karena Batch Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If isCetak Then

            Try
                OpenConn()

                Dim CrDoc As New Object
                Dim kertasBarcode As String = ""
                kertasBarcode = "BarcodeFG"

                Dim SF As String = ""

                SQL = "select kode_perusahaan from N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & SelectedFaktur & "' "
                SQL = SQL & "and urut_detail = '" & SelectedUrutDetail & "' "

                SF = "{N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed.kode_perusahaan} = '" & KodePerusahaan & "' "
                SF = SF & "and {N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed.No_Faktur} = '" & SelectedFaktur & "'"
                SF = SF & "and {N_EMI_View_Transaksi_Material_Requisition_QC_Batch_Completed.urut_detail} = " & Val(SelectedUrutDetail) & ""
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New N_EMI_CR_Transaksi_Request_Material_QC_Premix_Label

                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.RecordSelectionFormula = SF
                            CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
                            .Text = "Faktur Premix Label"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With

                        '==============================================================================
                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.RecordSelectionFormula = SF

                        'CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        'doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        'Dim rawKind As Integer
                        'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        '    If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcode Then
                        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                        '        CrDoc.PrintOptions.PaperSize = rawKind
                        '        Exit For
                        '    End If
                        'Next

                        'If rawKind = Nothing Or rawKind = 0 Then
                        '    CloseConn()
                        '    MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Kertas Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If

                        'CrDoc.PrintToPrinter(1, False, 1, 2500)

                    End If
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



        End If


    End Sub

    Private Sub CetakUlangBarcodeToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeToolStripMenuItem1.Click
        If Lv_DetQC.Items.Count = 0 Or Lv_DetQC.SelectedItems.Count = 0 Or Lv_DetQC.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih Dahulu Batch yang akan Dicetak Ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim NmBarangProduksi As String = Lv_ParentQC.FocusedItem.SubItems(4).Text
        Dim NoSplit As String = Lv_ParentQC.FocusedItem.SubItems(1).Text

        Dim SelectedFakturRM As String = Lv_DetailQC.FocusedItem.SubItems(5).Text
        Dim SelectedUrutDetailRM As String = Lv_DetailQC.FocusedItem.SubItems(6).Text
        Dim Selectedbatch As String = Lv_DetailQC.FocusedItem.SubItems(1).Text

        Dim SelectedFaktur As String = Lv_DetQC.FocusedItem.SubItems(0).Text
        Dim SelectedUrutDet As String = Lv_DetQC.FocusedItem.SubItems(8).Text
        Dim SelectedUrutValidasi As String = Lv_DetQC.FocusedItem.SubItems(9).Text
        Dim SelectedKdSO As String = Lv_DetQC.FocusedItem.SubItems(2).Text
        Dim SelectedKDBarang As String = Lv_DetQC.FocusedItem.SubItems(3).Text
        Dim SelectedNmBarang As String = Lv_DetQC.FocusedItem.SubItems(4).Text
        Dim SelectedSN As String = Lv_DetQC.FocusedItem.SubItems(10).Text

        Dim Jumlah As Double = Lv_DetQC.FocusedItem.SubItems(5).Text
        Dim SatuanBesar As String = Lv_DetQC.FocusedItem.SubItems(7).Text

        Dim KdSOTujuan As String = ""


        Dim isCetak As Boolean = False

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '================================================
            '=     CEK APAKAH DATA Validasi DI BATALKAN     =
            '================================================
            SQL = "select Status, Kode_Stock_Owner_Tujuan from N_EMI_Transaksi_Material_Requisition_QC_Validasi "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & SelectedFaktur & "' "
            SQL = SQL & "and Urut_Det_RM = '" & SelectedUrutDet & "' and Urut_Oto = '" & SelectedUrutValidasi & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Gagal Cetak Ulang Karena No Validasi Request Material Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                    KdSOTujuan = Dr("Kode_Stock_Owner_Tujuan")
                    isCetak = True
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Gagal Cetak Ulang Karena No Validasi Request Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If isCetak Then

                Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

                SQL = "delete from N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
                ExecuteTrans(SQL)


                '=============================
                '=       GET DATA LAMA       =
                '=============================
                Dim hargaIsn As String = ""
                Dim namaBarang As String = ""
                Dim warnaLama As String = ""
                Dim QrLama As String = ""
                Dim batchLama As String = ""
                Dim expDate As String = ""
                Dim KDUnikBerajalan As String = ""
                SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
                SQL = SQL & "from barang_sn a, barang b "
                SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                SQL = SQL & "and a.Kode_Stock_Owner='" & KdSOTujuan & "' "
                SQL = SQL & "and a.Kode_Barang ='" & SelectedKDBarang & "' "
                SQL = SQL & "and a.Serial_Number='" & SelectedSN & "' "
                'SQL = SQL & "and a.Jumlah <> 0 "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                hargaIsn = Get_Harga_SN(.Rows(i).Item("Serial_Number"))
                                QrLama = General_Class.CekNULL(.Rows(i).Item("Qr_Code"))
                                batchLama = General_Class.CekNULL(.Rows(i).Item("Batch_Number"))
                                namaBarang = General_Class.CekNULL(.Rows(i).Item("Nama"))
                                expDate = General_Class.CekNULL(.Rows(i).Item("Tgl_Expired"))
                                warnaLama = General_Class.CekNULL(.Rows(i).Item("warna"))
                                KDUnikBerajalan = General_Class.CekNULL(.Rows(i).Item("Kode_Unik_Berjalan"))
                            Next

                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Gagal Cetak Ulang Karena Data Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using


                Dim rnd As New Random()
                Dim TextBarcodePSS As String = ""

                For k As Integer = 1 To 10
                    Dim randomChar As Char = Chr(rnd.Next(65, 91)) ' ASCII 65–90 = A–Z
                    TextBarcodePSS &= randomChar
                Next

                '=====================================
                '=       GENERATE BARCODE BARU       =
                '=====================================
                kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
                Dim fullNewQr As String = QrLama & "-" & KDUnikBerajalan

                Barcode.Image = Generate_QR_QC(fullNewQr)
                Barcode_PSS.Image = Generate_QR_QC(TextBarcodePSS)

                Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeValidasiRMQC" & kode_unik_print & ".jpg")
                Dim FileToSaveAs2 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeValidasiRMQCPSS" & kode_unik_print & ".jpg")
                'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
                Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
                Barcode_PSS.Image.Save(FileToSaveAs2, System.Drawing.Imaging.ImageFormat.Jpeg)
                'End If

                fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                FileSize1 = fs1.Length
                rawData1 = New Byte(FileSize1) {}
                fs1.Read(rawData1, 0, FileSize1)
                fs1.Close()
                Cmd.Parameters.Add("@newBarcode" & kode_unik_print, SqlDbType.Image).Value = rawData1

                fs2 = New FileStream(FileToSaveAs2, FileMode.Open, FileAccess.Read)
                FileSize2 = fs2.Length
                rawData2 = New Byte(FileSize2) {}
                fs2.Read(rawData2, 0, FileSize2)
                fs2.Close()
                Cmd.Parameters.Add("@newBarcodePSS" & kode_unik_print, SqlDbType.Image).Value = rawData2

                '==================================
                '=       GET URUTAN BARCODE       =
                '==================================
                Dim UrutBarcode As Double = 0
                SQL = "select count(*) as urut from N_EMI_Transaksi_Material_Requisition_QC_Validasi "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur_RM = '" & SelectedFakturRM & "' "
                SQL = SQL & "and Kode_Barang = '" & SelectedKdSO & "' and Urut_Det_RM = '" & SelectedUrutDet & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Val(Dr("urut")) = 0 Then
                            UrutBarcode = 1
                        Else
                            UrutBarcode = Dr("urut")
                        End If
                    End If
                End Using

                '===================================
                '=       INSERT BARCODE BARU       =
                '===================================
                SQL = "INSERT INTO N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak "
                SQL = SQL & "(Kode_Perusahaan, Barcode, Barcode_PSS, Kode_Bahan, NamaBahan, NamaBarang, Tgl_Input, Jam_Input, Batch_Number, Jumlah_Input, Satuan_Input, "
                SQL = SQL & "No, Dari, QrUtuh, Qr, Tanggal_Cetak, Kode_Unik_Print, No_Split, Batch) "
                SQL = SQL & "VALUES('" & KodePerusahaan & "', @newBarcode" & kode_unik_print & ", @newBarcodePSS" & kode_unik_print & ",'" & SelectedKDBarang & "', '" & SelectedNmBarang & "', "
                SQL = SQL & "'" & NmBarangProduksi & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & batchLama & "', "
                SQL = SQL & "" & HilangkanTanda(Jumlah) & ", '" & SatuanBesar & "', " & UrutBarcode & ", 0, "
                SQL = SQL & "'" & fullNewQr & "', '" & QrLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & kode_unik_print & "', "
                SQL = SQL & "'" & NoSplit & "', '" & Selectedbatch & "'); "
                ExecuteTrans(SQL)

            End If


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        If isCetak Then

            Try
                OpenConn()

                Dim CrDoc As New Object
                Dim kertas As String = ""

                Dim kertasBarcode As String = ""
                kertasBarcode = "BarcodeQC"

                SQL = "select Kode_Perusahaan from N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Unik_Print='" & kode_unik_print & "'"
                Using Ds = BindingTrans(SQL)
                    If Ds.Tables("MyTable").Rows.Count <> 0 Then

                        CrDoc = New N_EMI_CR_Transaksi_Request_Material_QC_Barcode


                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = ""
                            CrDoc.RecordSelectionFormula = "{N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Unik_Print} = '" & kode_unik_print & "' "
                            CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
                            .Text = "Faktur Premix Label"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .Refresh()
                            .Show()
                        End With


                        'CrDoc.SetDataSource(Ds)
                        'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        'CrDoc.RecordSelectionFormula = "{N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Unik_Print} = '" & arrKdUnikPrint(i) & "' "
                        'CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        'Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        'doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        'Dim rawKind As Integer
                        'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        '    If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcode Then
                        '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                        '        CrDoc.PrintOptions.PaperSize = rawKind
                        '        Exit For
                        '    End If
                        'Next

                        'If rawKind = Nothing Or rawKind = 0 Then
                        '    CloseConn()
                        '    MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Kertas Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                        'End If

                        'CrDoc.PrintToPrinter(1, False, 1, 2500)


                    End If
                End Using

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try



        End If
    End Sub

    '===========================================================================================================================================

    Private Sub Lv_ParentQC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_ParentQC.SelectedIndexChanged

        If Lv_ParentQC.Items.Count = 0 Or Lv_ParentQC.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_ParentQC.FocusedItem.SubItems(0).Text

            Lv_DetailQC.Items.Clear() : Lv_Det.Items.Clear()
            SQL = "select a.No_Faktur, b.Batch, b.Qty_PerBatch, b.Total_Tambah, b.Flag_Terpenuhi, b.Urut_Oto, "
            SQL = SQL & "isnull(( select top 1 Satuan from N_EMI_Transaksi_Material_Requisition_QC_Det z "
            SQL = SQL & "where b.Kode_Perusahaan = z.Kode_Perusahaan and b.No_Faktur = z.No_Faktur and z.Urut_Detail = b.Urut_Oto ), '-') as Satuan "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.no_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' "
            SQL = SQL & "order by b.Batch"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DetailQC.Items.Add("")
                    Lv.SubItems.Add(Format(Dr("Batch"), "N0"))
                    Lv.SubItems.Add(Format(Dr("Qty_PerBatch"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Total_Tambah"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    'Hide
                    Lv.SubItems.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Urut_Oto"))

                    If General_Class.CekNULL(Dr("Flag_Terpenuhi")) = "Y" Then
                        Lv.BackColor = Color.LightGreen
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
    Private Sub Lv_DetailQC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_DetailQC.SelectedIndexChanged
        If Lv_DetailQC.Items.Count = 0 Or Lv_DetailQC.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_DetailQC.FocusedItem.SubItems(5).Text
            Dim SelectedUrutDetail As String = Lv_DetailQC.FocusedItem.SubItems(6).Text

            Lv_DetQC.Items.Clear()
            SQL = "select a.No_Faktur, a.Tanggal, a.Kode_Stock_Owner, a.Kode_Barang, c.Nama as Nama_Barang, a.Jumlah, a.Jumlah_Bags, a.Satuan, "
            SQL = SQL & "a.Status, a.Urut_Det_RM, a.Urut_Oto, a.SN_Baru "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC_Validasi a, N_EMI_Transaksi_Material_Requisition_QC_Det b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur_RM = b.No_Faktur and a.Urut_Det_RM = b.Urut_Oto "
            SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & SelectedFaktur & "' "
            SQL = SQL & "and b.Urut_Detail = '" & SelectedUrutDetail & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DetQC.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Tanggal"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))

                    Lv.SubItems.Add(Dr("Urut_Det_RM"))
                    Lv.SubItems.Add(Dr("Urut_Oto"))
                    Lv.SubItems.Add(Dr("SN_Baru"))

                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
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



    '==============================================================================================================================================
    '=     HANDLE KEYPRESS     
    '==============================================================================================================================================

    Private Sub Chk_HriIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_HriIni.CheckedChanged
        If Chk_HriIni.Checked = True Then
            Chk_Tanggal.Checked = False
            Btn_Cari_Click(Chk_HriIni, e)
        End If
    End Sub

    Private Sub Chk_HriIni_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_HriIni.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Tanggal.Focus()
    End Sub
    Private Sub Chk_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Tanggal.CheckedChanged
        If Chk_Tanggal.Checked Then
            Cmb_Tanggal.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Chk_HriIni.Checked = False
        Else
            Cmb_Tanggal.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_Tanggal.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub Chk_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_Tanggal.Checked Then
                Cmb_Tanggal.DroppedDown = True
                Cmb_Tanggal.Focus()
            Else
                Chk_Lain.Focus()
            End If

        End If
    End Sub

    Private Sub Chk_Lain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Lain.CheckedChanged
        If Chk_Lain.Checked Then
            Cmb_Lain.Enabled = True : Txt_Lain.Enabled = True
        Else
            Cmb_Lain.Enabled = False : Txt_Lain.Enabled = False
            Cmb_Lain.SelectedIndex = -1 : Txt_Lain.Text = ""
        End If
    End Sub

    Private Sub Chk_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Lain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_Lain.Checked Then
                Cmb_Lain.DroppedDown = True
                Cmb_Lain.Focus()
            Else
                Btn_Cari.Focus()
            End If

        End If
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Lain.Focus()
    End Sub

    Private Sub Cmb_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lain.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Lain.Focus()
    End Sub

    Private Sub Txt_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Lain.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub Cmb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    '==========================================================================================================================
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            Button1_Click(CheckBox1, e)
        End If
    End Sub
    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox2.Focus()
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ComboBox2.Enabled = True : DateTimePicker3.Enabled = True : DateTimePicker4.Enabled = True
            CheckBox1.Checked = False
        Else
            ComboBox2.Enabled = False : DateTimePicker3.Enabled = False : DateTimePicker4.Enabled = False
            ComboBox2.SelectedIndex = -1 : DateTimePicker3.Value = Now.Date : DateTimePicker4.Value = Now.Date
        End If
    End Sub

    Private Sub CheckBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox2.Checked Then
                ComboBox2.DroppedDown = True
                ComboBox2.Focus()
            Else
                CheckBox3.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker3.Focus()
    End Sub
    Private Sub DateTimePicker3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker3.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker4.Focus()
    End Sub
    Private Sub DateTimePicker4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker4.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox3.Focus()
    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked Then
            ComboBox3.Enabled = True : Txt_LainQC.Enabled = True
        Else
            ComboBox3.Enabled = False : Txt_LainQC.Enabled = False
            ComboBox3.SelectedIndex = -1 : Txt_LainQC.Text = ""
        End If
    End Sub
    Private Sub CheckBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox3.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox3.Checked Then
                ComboBox3.DroppedDown = True
                ComboBox3.Focus()
            Else
                Button1.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then Txt_LainQC.Focus()
    End Sub



    Private Sub Txt_LainQC_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_LainQC.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub


    Private Function Generate_QR_QC(ByVal isi As String)

        Dim options As New ZXing.QrCode.QrCodeEncodingOptions()

        options.DisableECI = True
        options.CharacterSet = "UTF-8"
        options.Width = 80
        options.Height = 80
        options.Margin = 0

        Dim qr As New ZXing.BarcodeWriter()
        qr.Format = ZXing.BarcodeFormat.QR_CODE
        qr.Options = options

        Dim result As New Bitmap(qr.Write(isi))
        Return result
    End Function


End Class