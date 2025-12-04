Imports System.IO


Public Class N_EMI_Display_Request_Material_QC_Summary

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
        Lv_DetailQC.Columns.Add("Batch", 55, HorizontalAlignment.Center) '1
        Lv_DetailQC.Columns.Add("Jumlah PerBatch", 0, HorizontalAlignment.Right) '2
        Lv_DetailQC.Columns.Add("Jumlah Tambah", 0, HorizontalAlignment.Right) '3
        Lv_DetailQC.Columns.Add("Satuan", 0, HorizontalAlignment.Center) '4
        Lv_DetailQC.Columns.Add("NoFaktur", 0, HorizontalAlignment.Left) '5
        Lv_DetailQC.Columns.Add("UrutDetail", 0, HorizontalAlignment.Left) '6
        Lv_Detail.View = View.Details

        Lv_BahanQC.Columns.Clear()
        Lv_BahanQC.Columns.Add("Lokasi", 0, HorizontalAlignment.Left) '0
        Lv_BahanQC.Columns.Add("Kode Barang", 100, HorizontalAlignment.Left) '1
        Lv_BahanQC.Columns.Add("Nama Barang", 230, HorizontalAlignment.Left) '2
        Lv_BahanQC.Columns.Add("No Faktur", 0, HorizontalAlignment.Right) '3
        Lv_BahanQC.Columns.Add("Urut Detail", 0, HorizontalAlignment.Center) '4
        Lv_BahanQC.View = View.Details

        Lv_DetQC.Columns.Clear()
        Lv_DetQC.Columns.Add("No Faktur", 120, HorizontalAlignment.Left) '0
        Lv_DetQC.Columns.Add("Tangal", 100, HorizontalAlignment.Center) '1
        Lv_DetQC.Columns.Add("Lokasi", 120, HorizontalAlignment.Left) '2
        Lv_DetQC.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left) '3
        Lv_DetQC.Columns.Add("Nama", 200, HorizontalAlignment.Left) '4
        Lv_DetQC.Columns.Add("Barcode", 200, HorizontalAlignment.Left) '5
        Lv_DetQC.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '6
        Lv_DetQC.Columns.Add("Jumlah Bags", 120, HorizontalAlignment.Right) '7
        Lv_DetQC.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '8
        Lv_DetQC.Columns.Add("UrutDet", 0, HorizontalAlignment.Center) '9
        Lv_DetQC.Columns.Add("UrutOTO", 0, HorizontalAlignment.Center) '10
        Lv_DetQC.Columns.Add("SN", 0, HorizontalAlignment.Center) '11
        Lv_DetQC.Columns.Add("Flag Retur", 0, HorizontalAlignment.Center) '12
        Lv_DetQC.Columns.Add("SN Retur", 0, HorizontalAlignment.Center) '13
        Lv_DetQC.Columns.Add("Barcode Retur", 200, HorizontalAlignment.Center) '14
        Lv_DetQC.View = View.Details

        Lv_DetQC.Columns(14).DisplayIndex = 6



        Me.Size = New Size(1200, 650)
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


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()


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

        Txt_ActiveMenu.Text = ""

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


        HandleMenuClick("PRODUCTION")

        Panel_Production.Size = New Size(1149, 518)
        Panel_QC.Size = New Size(1149, 518)


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

            Lv_ParentQC.Items.Clear() : Lv_DetailQC.Items.Clear() : Lv_BahanQC.Items.Clear() :
            Lv_DetQC.Items.Clear()
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
    Private Sub SalinNoFakturToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem2.Click
        If Lv_DetQC.Items.Count = 0 Or Lv_DetQC.SelectedItems.Count = 0 Or Lv_DetQC.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_DetQC.FocusedItem.Text)
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

                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = SF
                        '    CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
                        '    .Text = "Faktur Premix Label"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        '==============================================================================
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = SF

                        CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcode Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        If rawKind = Nothing Or rawKind = 0 Then
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Kertas Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
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
        Dim SelectedUrutDet As String = Lv_DetQC.FocusedItem.SubItems(9).Text
        Dim SelectedUrutValidasi As String = Lv_DetQC.FocusedItem.SubItems(10).Text
        Dim SelectedKdSO As String = Lv_DetQC.FocusedItem.SubItems(2).Text
        Dim SelectedKDBarang As String = Lv_DetQC.FocusedItem.SubItems(3).Text
        Dim SelectedNmBarang As String = Lv_DetQC.FocusedItem.SubItems(4).Text
        Dim SelectedSN As String = Lv_DetQC.FocusedItem.SubItems(11).Text
        Dim SelectedFlagRetur As String = Lv_DetQC.FocusedItem.SubItems(12).Text
        Dim SelectedSNRetur As String = Lv_DetQC.FocusedItem.SubItems(13).Text
        Dim Jumlah As Double = Lv_DetQC.FocusedItem.SubItems(6).Text
        Dim SatuanBesar As String = Lv_DetQC.FocusedItem.SubItems(8).Text

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

                If SelectedFlagRetur = "Y" Then
                    SQL = SQL & "and a.Serial_Number='" & SelectedSNRetur & "' "
                Else
                    SQL = SQL & "and a.Serial_Number='" & SelectedSN & "' "
                End If
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


                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = "{N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Unik_Print} = '" & kode_unik_print & "' "
                        '    CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
                        '    .Text = "Faktur Premix Label"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With


                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Unik_Print} = '" & kode_unik_print & "' "
                        CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        doctoprint.PrinterSettings.PrinterName = PrinterBarcodeQC

                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcode Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        If rawKind = Nothing Or rawKind = 0 Then
                            CloseConn()
                            MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Kertas Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
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



        End If
    End Sub

    Private Sub CetakUlangFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangFakturToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim SF As String = ""
            Dim kertas As String = "Faktur"

            Dim NoFakturCetak As String = Lv_Data.FocusedItem.Text

            SQL = "select Kode_Perusahaan from N_EMI_View_Faktur_Material_Requisition_QC "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoFakturCetak & "' "
            SF = "{N_EMI_View_Faktur_Material_Requisition_QC.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {N_EMI_View_Faktur_Material_Requisition_QC.No_Faktur} = '" & NoFakturCetak & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            CrDoc = New N_EMI_CR_Faktur_Request_Material_QC


                            'With A_Place_For_Printing2
                            '    CrDoc.SetDataSource(Ds)
                            '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            '    'CrDoc.PrintOptions.PrinterName = ""
                            '    CrDoc.RecordSelectionFormula = SF
                            '    CrDoc.SummaryInfo.ReportTitle = "Faktur Request Material Quality Control"
                            '    .Text = "Faktur Request Material Quality Control"
                            '    .CrystalReportViewer1.ReportSource = CrDoc
                            '    .Refresh()
                            '    .Show()
                            'End With

                            '=====================================

                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.PrintOptions.PrinterName = PrinterNameSPB
                            CrDoc.RecordSelectionFormula = SF
                            'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            doctoprint.PrinterSettings.PrinterName = PrinterNameSPB
                            Dim rawKind As Integer
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertas Then
                                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                    CrDoc.PrintOptions.PaperSize = rawKind
                                    Exit For
                                End If
                            Next

                            'CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)

                            '=======================================
                            '=     CEK APAKAH KERTAS DITEMUKAN     =
                            '=======================================
                            If rawKind <> -1 Then
                                CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                            Else
                                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                                Debug.Print("Ukuran kertas tidak ditemukan, menggunakan default.")
                            End If

                            CrDoc.PrintToPrinter(1, False, 1, 99)

                            MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                        Next

                    Else
                        CloseConn()
                        MessageBox.Show("No Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

                    End If
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    '===========================================================================================================================================

    Private Sub Lv_ParentQC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_ParentQC.SelectedIndexChanged

        If Lv_ParentQC.Items.Count = 0 Or Lv_ParentQC.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_ParentQC.FocusedItem.SubItems(0).Text

            Lv_DetailQC.Items.Clear() : Lv_BahanQC.Items.Clear() : Lv_DetQC.Items.Clear()
            'SQL = "select a.No_Faktur, b.Batch, b.Qty_PerBatch, b.Total_Tambah, b.Flag_Terpenuhi, b.Urut_Oto, "
            'SQL = SQL & "isnull(( select top 1 Satuan from N_EMI_Transaksi_Material_Requisition_QC_Det z "
            'SQL = SQL & "where b.Kode_Perusahaan = z.Kode_Perusahaan and b.No_Faktur = z.No_Faktur and z.Urut_Detail = b.Urut_Oto ), '-') as Satuan "
            'SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC a, N_EMI_Transaksi_Material_Requisition_QC_Detail b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.no_Faktur = b.No_Faktur "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur = '" & SelectedFaktur & "' "
            'SQL = SQL & "order by b.Batch"

            SQL = "select a.No_Faktur, b.Batch, b.Urut_Oto, b.Flag_Terpenuhi "
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
                    Lv.SubItems.Add("-")
                    Lv.SubItems.Add("-")
                    Lv.SubItems.Add("-")
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

            Lv_DetQC.Items.Clear() : Lv_BahanQC.Items.Clear()
            'SQL = "select a.No_Faktur, a.Tanggal, a.Kode_Stock_Owner, a.Kode_Barang, c.Nama as Nama_Barang, a.Jumlah, a.Jumlah_Bags, a.Satuan, "
            'SQL = SQL & "a.Status, a.Urut_Det_RM, a.Urut_Oto, a.SN_Baru "
            'SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC_Validasi a, N_EMI_Transaksi_Material_Requisition_QC_Det b, barang c "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Faktur_RM = b.No_Faktur and a.Urut_Det_RM = b.Urut_Oto "
            'SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            'SQL = SQL & "and a.Status is null "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and b.No_Faktur = '" & SelectedFaktur & "' "
            'SQL = SQL & "and b.Urut_Detail = '" & SelectedUrutDetail & "' "


            SQL = "select distinct b.No_Faktur, b.Urut_Detail, a.Kode_Stock_Owner, a.Kode_Barang, c.Nama as Nama_Barang, a.Status "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC_Validasi a, N_EMI_Transaksi_Material_Requisition_QC_Det b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur_RM = b.No_Faktur and a.Urut_Det_RM = b.Urut_Oto "
            SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            'SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & SelectedFaktur & "' "
            SQL = SQL & "and b.Urut_Detail = '" & SelectedUrutDetail & "' "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_BahanQC.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))

                    Lv.SubItems.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Urut_Detail"))

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
    Private Sub Lv_BahanQC_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_BahanQC.SelectedIndexChanged
        If Lv_BahanQC.Items.Count = 0 Or Lv_BahanQC.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_BahanQC.FocusedItem.SubItems(4).Text
            Dim SelectedUrutDetail As String = Lv_BahanQC.FocusedItem.SubItems(5).Text
            Dim SelectedKdBarang As String = Lv_BahanQC.FocusedItem.SubItems(1).Text

            Lv_DetQC.Items.Clear()
            SQL = "select a.No_Faktur, a.Tanggal, a.Kode_Stock_Owner, a.Kode_Barang, c.Nama as Nama_Barang, a.Jumlah, a.Jumlah_Bags, a.Satuan, "
            SQL = SQL & "a.Status, a.Urut_Det_RM, a.Urut_Oto, a.SN_Baru, "
            SQL = SQL & "ISNULL((select (z.Qr_Code + '-' + z.Kode_Unik_Berjalan) from Barang_SN z where a.kode_Perusahaan = z.kode_perusahaan "
            SQL = SQL & "and a.Kode_Stock_Owner_Tujuan = z.Kode_Stock_Owner and a.Kode_Barang = z.Kode_Barang and a.SN_Baru = z.Serial_Number "
            SQL = SQL & "), '-') as Barcode, "
            SQL = SQL & "isnull(( select (z.qr_code+'-'+z.kode_unik_berjalan) from Barang_SN z "
            SQL = SQL & "where a.kode_perusahaan = z.kode_perusahaan and a.Sn_Regenerate = z.Serial_Number "
            SQL = SQL & "), '-') as Barcode_Retur, "
            SQL = SQL & "a.Flag_Retur, a.Sn_Regenerate "
            SQL = SQL & "from N_EMI_Transaksi_Material_Requisition_QC_Validasi a, N_EMI_Transaksi_Material_Requisition_QC_Det b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur_RM = b.No_Faktur and a.Urut_Det_RM = b.Urut_Oto "
            SQL = SQL & "and a.Kode_Stock_Owner = c.Kode_Stock_Owner and a.Kode_Barang = c.Kode_Barang "
            'SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Faktur = '" & SelectedFaktur & "' "
            SQL = SQL & "and b.Urut_Detail = '" & SelectedUrutDetail & "' "
            SQL = SQL & "and b.Kode_Barang = '" & SelectedKdBarang & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DetQC.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Barcode"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    Lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Dr("Urut_Det_RM"))
                    Lv.SubItems.Add(Dr("Urut_Oto"))
                    Lv.SubItems.Add(Dr("SN_Baru"))

                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Flag_Retur")) = "", "-", Dr("Flag_Retur")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Sn_Regenerate")) = "", "-", Dr("Sn_Regenerate")))
                    Lv.SubItems.Add(Dr("Barcode_Retur"))

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

    Private Sub BatalBatchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalBatchToolStripMenuItem.Click
        If Lv_DetailQC.Items.Count = 0 Then Exit Sub


        Dim No_RM_QC As String = Lv_DetailQC.FocusedItem.SubItems(5).Text
        Dim No_Batch As String = Lv_DetailQC.FocusedItem.SubItems(1).Text

        If MessageBox.Show($"Yakin Ingin Membatalkan {No_RM_QC} Batch {No_Batch} ???", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

        get_jam()

        Dim arrKdUnikPrint, arrUrutValidasi As New ArrayList

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Request Material Quality Control"

            arrKdUnikPrint.Clear()
            arrUrutValidasi.Clear()

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_RM_QC") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Request Material", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '=================================================
            '=     CEK APAKAH TRANSAKSI SUDAH DIBATALKAN     =
            '=================================================
            SQL = $"
                select Kode_Perusahaan
                from N_EMI_Transaksi_Material_Requisition_QC
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status = 'Y'
                and No_Faktur = '{No_RM_QC}'
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"No Faktur {No_RM_QC} sudah dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==================================================
            '=     CEK APAKAH TRANSAKSI SUDAH DI TRANSFER     =
            '==================================================
            SQL = $"
                select a.Kode_Perusahaan
                from N_EMI_Transaksi_Material_Requisition_QC a
	                inner join N_EMI_Transaksi_Material_Requisition_QC_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
	                inner join N_EMI_Transaksi_Material_Requisition_QC_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail
	                inner join N_EMI_Transaksi_Material_Requisition_QC_Validasi d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Urut_Oto = d.Urut_Det_RM and a.No_Faktur = d.No_Faktur_RM
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Status is null and d.Status is null
                and a.No_Faktur = '{No_RM_QC}'
                and b.Batch = {No_Batch}
            "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"No Faktur {No_RM_QC} belum melakukan validasi transfer", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===================================
            '=     DELETE DATA TABEL CETAK     =
            '===================================
            Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)
            SQL = "delete from N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            ExecuteTrans(SQL)

            '====================
            '=     GET DATA     =
            '====================
            SQL = $"
                select a.No_Faktur, a.No_Faktur_Order, d.No_Faktur as Faktur_Validasi, d.Status, d.Kode_Stock_Owner, d.Kode_Stock_Owner_Tujuan, d.Kode_Barang, e.Nama as Nm_Barang, 
                d.SN_Lama, d.SN_Baru, d.Jumlah, d.Jumlah_Bags, d.Satuan, d.Barcode_PSS, d.Urut_Det_RM, a.Nama as Nm_Barang_Produksi, d.Urut_Oto as Urut_Validasi
                from N_EMI_Transaksi_Material_Requisition_QC a
	                inner join N_EMI_Transaksi_Material_Requisition_QC_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur
	                inner join N_EMI_Transaksi_Material_Requisition_QC_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail
	                inner join N_EMI_Transaksi_Material_Requisition_QC_Validasi d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Urut_Oto = d.Urut_Det_RM and a.No_Faktur = d.No_Faktur_RM
                    inner join barang e on d.Kode_Perusahaan = e.Kode_Perusahaan and d.Kode_Stock_Owner_Tujuan = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Status is null and d.Status is null
                and a.No_Faktur = '{No_RM_QC}'
                and b.Batch = {No_Batch}
                order by d.Kode_Stock_Owner_Tujuan, d.Kode_Barang
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            If General_Class.CekNULL(.Rows(i).Item("Status")) = "Y" Then Continue For

                            Dim No_Split_Req As String = .Rows(i).Item("No_Faktur_Order")
                            Dim No_Faktur_Validasi As String = .Rows(i).Item("Faktur_Validasi")
                            Dim So_Awal As String = .Rows(i).Item("Kode_Stock_Owner")
                            Dim So_Tujuan As String = .Rows(i).Item("Kode_Stock_Owner_Tujuan")
                            Dim Kd_Barang As String = .Rows(i).Item("Kode_Barang")
                            Dim Nm_Barang As String = .Rows(i).Item("Nm_Barang")
                            Dim Nm_Barang_Produksi As String = .Rows(i).Item("Nm_Barang_Produksi")
                            Dim Sn_Lama As String = .Rows(i).Item("SN_Lama")
                            Dim Sn_Baru As String = .Rows(i).Item("SN_Baru")
                            Dim Jumlah As Double = .Rows(i).Item("Jumlah")
                            Dim Jumlah_Bags As Double = .Rows(i).Item("Jumlah_Bags")
                            Dim Satuan As String = .Rows(i).Item("Satuan")
                            Dim Urut_Det_RM As String = .Rows(i).Item("Urut_Det_RM")
                            Dim Urut_Validasi As String = .Rows(i).Item("Urut_Validasi")

                            '================================
                            '=     CEK STOCK DI SN BARU     =
                            '================================
                            SQL = $"
                                select jumlah, Jumlah_Bags from Barang_SN 
                                where kode_perusahaan = '{KodePerusahaan}'
                                and kode_Stock_owner = '{So_Tujuan}'
                                and kode_barang = '{Kd_Barang}'
                                and serial_number = '{Sn_Baru}'
                            "
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    If Val(HilangkanTanda(dr("jumlah"))) <> Val(HilangkanTanda(Jumlah)) Or Val(HilangkanTanda(dr("Jumlah_Bags"))) <> Val(HilangkanTanda(Jumlah_Bags)) Then
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"Stock pada Barang {Kd_Barang} sudah digunakan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show($"Data Barang {Kd_Barang} Tidak ditemukan di Barang SN", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '============================
                            '=       POTONG STOCK       =
                            '============================
                            Dim Nama As String = ""
                            SQL = "select Nama,round(good_stock,4) as good_stock, Jumlah_Bags from Barang where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & So_Tujuan & "' "
                            SQL = SQL & "and Kode_Barang='" & Kd_Barang & "' "
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    Nama = dr("nama")
                                    If dr("good_stock") < Jumlah Then
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    ElseIf dr("Jumlah_Bags") < Val(HilangkanTanda(Jumlah_Bags)) Then
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    Else
                                        dr.Close()
                                        SQL = "update barang set Good_Stock = ROUND((Good_Stock - " & Jumlah & "), 4) , "
                                        SQL = SQL & "Jumlah_Bags =  ROUND((Jumlah_Bags - " & Jumlah_Bags & "), 4) "
                                        SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & So_Tujuan & "' "
                                        SQL = SQL & " and Kode_Barang='" & Kd_Barang & "'"
                                        ExecuteTrans(SQL)
                                    End If
                                Else
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "select round(jumlah,4) as jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & So_Tujuan & "' "
                            SQL = SQL & "and Kode_Barang='" & Kd_Barang & "' "
                            SQL = SQL & "and Serial_Number='" & Sn_Baru & "'"
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    If dr("jumlah") < Jumlah Then
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    ElseIf dr("Jumlah_Bags") < Val(HilangkanTanda(Jumlah_Bags)) Then
                                        dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat jumlah bags " & Nama & " menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    Else
                                        dr.Close()
                                        SQL = "update barang_sn set jumlah = ROUND((jumlah - " & Jumlah & "), 4) , "
                                        SQL = SQL & "Jumlah_Bags = ROUND((Jumlah_Bags - " & Jumlah_Bags & "), 4) "
                                        SQL = SQL & "where Kode_Stock_Owner='" & So_Tujuan & "' and Kode_Barang='" & Kd_Barang & "' "
                                        SQL = SQL & "and Serial_Number='" & Sn_Baru & "'"
                                        ExecuteTrans(SQL)
                                    End If
                                Else
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang " & Nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
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

                            '============================
                            '=       TAMBAH STOCK       =
                            '============================
                            Dim hargaIsn As String = ""
                            Dim namaBarang As String = ""
                            Dim warnaLama As String = ""
                            Dim QrLama As String = ""
                            Dim Kd_Unik_Berjalan_Lama As String = ""
                            Dim batchLama As String = ""
                            Dim expDate As String = ""

                            'Ambil Data Lama
                            SQL = "select a.Serial_Number, a.Qr_Code, a.Kode_Unik_Berjalan, b.Nama, a.Batch_Number, a.Tgl_Expired, a.warna "
                            SQL = SQL & "from barang_sn a, barang b "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
                            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
                            SQL = SQL & "and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "and a.Kode_Stock_Owner='" & So_Tujuan & "' "
                            SQL = SQL & "and a.Kode_Barang ='" & Kd_Barang & "' "
                            SQL = SQL & "and a.Serial_Number='" & Sn_Baru & "' "
                            Using Dr = OpenTrans(SQL)
                                Do While Dr.Read
                                    hargaIsn = Get_Harga_SN(Dr("Serial_Number"))
                                    QrLama = General_Class.CekNULL(Dr("Qr_Code"))
                                    Kd_Unik_Berjalan_Lama = General_Class.CekNULL(Dr("Kode_Unik_Berjalan"))
                                    batchLama = General_Class.CekNULL(Dr("Batch_Number"))
                                    namaBarang = General_Class.CekNULL(Dr("Nama"))
                                    expDate = General_Class.CekNULL(Dr("Tgl_Expired"))
                                    warnaLama = General_Class.CekNULL(Dr("warna"))
                                Loop
                            End Using

                            '==========================
                            '=     GET RAK KOSONG     =
                            '==========================
                            Dim available_Id_Warehouse As String = ""
                            Dim available_NoPallet As String = ""
                            SQL = "select top 1 id_wms_warehouse_position, 0 as nomor_urut from view_warehouse_position where "
                            SQL = SQL & "kode_stock_Owner='" & So_Awal & "' "
                            Using Dr2 = OpenTrans(SQL)
                                If Dr2.Read Then
                                    available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                                    available_NoPallet = Dr2("nomor_urut")
                                Else
                                    Dr2.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Rak Sudah Penuh. . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            'GENERATE SN BARU
                            Dim str As String = Format(Random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                            Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                            Dim SN_Regenerate As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & hargaIsn & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                            Dim newKodeUnikBerjalan As String = Generate_Random_Kode(10)


                            '===========================================
                            '=       PASTIKAN BARCODE TIDAK SAMA       =
                            '===========================================
                            If (QrLama + "-" + Kd_Unik_Berjalan_Lama).Trim = (QrLama + "-" + newKodeUnikBerjalan).Trim Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan . . ! !, Harap Ulangi Transaksi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If


                            'INSERT BARANG SN BARU
                            SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah,  Jumlah_Bags, "
                            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, id_Susunan, Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN) "
                            SQL = SQL & "select Kode_Perusahaan, '" & So_Awal & "', Kode_Barang, '" & SN_Regenerate & "', '" & Jumlah & "', " & Jumlah_Bags & ", "
                            SQL = SQL & "Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, '" & available_Id_Warehouse & "', id_Susunan , Qr_Code, '" & newKodeUnikBerjalan & "', "
                            SQL = SQL & "Kode_Unik_Asal, '" & available_NoPallet & "', batch_number, '" & warnaLama & "', Tgl_Masuk, NULL "
                            SQL = SQL & "from Barang_SN "
                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner='" & So_Tujuan & "' "
                            SQL = SQL & "and Kode_Barang='" & Kd_Barang & "' "
                            SQL = SQL & "and Serial_Number='" & Sn_Baru & "' "
                            ExecuteTrans(SQL)


                            SQL = "update barang set Good_Stock = Good_Stock + " & Jumlah & ", "
                            SQL = SQL & "Jumlah_Bags = Jumlah_Bags + " & Jumlah_Bags & " "
                            SQL = SQL & "where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Stock_Owner='" & So_Awal & "' "
                            SQL = SQL & " and Kode_Barang='" & Kd_Barang & "'"
                            ExecuteTrans(SQL)

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
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
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



                            '======================
                            '=       JURNAL       =
                            '======================
#Region "Jurnal"
                            Dim akun_persediaan_dari As String = ""
                            Dim akun_persediaan_tujuan As String = ""
                            Dim inisial_faktur_dari As String = ""


                            SQL = "select inisial_faktur,Persediaan_Bahan_Baku,Persediaan,Persediaan_Bahan_Setengah_Jadi,Persediaan_Scrap, Persediaan_Packaging from stock_owner_gudang "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & So_Tujuan & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    'akun_persediaan_dari = Dr("persediaan")
                                    inisial_faktur_dari = Dr("inisial_faktur")

                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "select c.akun_Persediaan "
                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and b.kode_stock_owner = '" & So_Tujuan & "' and b.Kode_Barang='" & Kd_Barang & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    akun_persediaan_dari = Dr("akun_Persediaan")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "select c.akun_Persediaan "
                            SQL = SQL & "from EMI_Group_Jenis a, Barang b, EMI_Group_Jenis_Akun c where "
                            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Group_Jenis = b.Id_Group_Jenis and "
                            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Group_Jenis = c.Id_Group_Jenis and "
                            SQL = SQL & "b.kode_stock_owner = c.kode_stock_owner and b.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and b.kode_stock_owner = '" & So_Awal & "' and b.Kode_Barang='" & Kd_Barang & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    akun_persediaan_tujuan = Dr("akun_Persediaan")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data akun tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            Dim nilai_persediaan_min As Double = 0
                            SQL = "select round(dbo.get_hpp(serial_number) * " & Jumlah & ", 2) as rp_persediaan_min from barang_sn where "
                            SQL = SQL & "Kode_Stock_Owner='" & So_Tujuan & "' and Kode_Barang='" & Kd_Barang & "' "
                            SQL = SQL & "and Serial_Number='" & Sn_Baru & "'"
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    nilai_persediaan_min = dr("rp_persediaan_min")
                                Else
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data SN tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            Dim Kode_voucher As String = ""
                            Kode_voucher = GetLastNumberJurnal(Format(tgl_skg, "yyyyMM"), "JS" & inisial_faktur_dari, KodePerusahaan)
                            Dim pagenumber As Integer = 1

                            SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                            SQL = SQL & "Keterangan, JudulBank, KetDK, userid) values("
                            SQL = SQL & "'" & Kode_voucher & "', "
                            SQL = SQL & "'" & Format(tgl_skg, "yyyy-MM-dd") & "', "
                            SQL = SQL & "'" & Format(tgl_skg, "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                            SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock " & No_Faktur_Validasi.Trim & "', '', "
                            SQL = SQL & "'-', '" & UserID & "')"
                            ExecuteTrans(SQL)

                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_dari, 1),
                                  Strings.Mid(akun_persediaan_dari, 2, 1),
                                  Strings.Mid(Ganti(akun_persediaan_dari), 3),
                                  KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Validasi.Trim, "0", nilai_persediaan_min, pagenumber, So_Tujuan, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                            SQL = Get_Detail_Jurnal(Kode_voucher, Strings.Left(akun_persediaan_tujuan, 1),
                                 Strings.Mid(akun_persediaan_tujuan, 2, 1),
                                 Strings.Mid(Ganti(akun_persediaan_tujuan), 3),
                                 KodePerusahaan, KodeProyek, "Persedian " & No_Faktur_Validasi.Trim, nilai_persediaan_min, "0", pagenumber, So_Awal, Bahasa_Pilihan, Ket_Cost_Center_HO)
                            ExecuteTrans(SQL)
                            pagenumber = pagenumber + 1

                            SQL = "select sum(debit) as debit, sum(kredit) as kredit from detail_jurnal where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_voucher = '" & Kode_voucher & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Dr("debit") <> Dr("kredit") Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Jurnal salah!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data jurnal tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


#End Region



                            '====================================
                            '=       GENERATE BARCODE PSS       =
                            '====================================
                            Dim rnd As New Random()
                            Dim TextBarcodePSSBaru As String = ""

                            For k As Integer = 1 To 10
                                Dim randomChar As Char = Chr(rnd.Next(65, 91)) ' ASCII 65–90 = A–Z
                                TextBarcodePSSBaru &= randomChar
                            Next

                            '=====================================
                            '=       GENERATE BARCODE BARU       =
                            '=====================================
                            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
                            Dim fullNewQr As String = QrLama & "-" & newKodeUnikBerjalan

                            Barcode.Image = Generate_QR_QC(fullNewQr)
                            Barcode_PSS.Image = Generate_QR_QC(TextBarcodePSSBaru)

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
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur_RM = '" & No_RM_QC & "' "
                            SQL = SQL & "and Kode_Barang = '" & Kd_Barang & "' and Urut_Det_RM = '" & Urut_Det_RM & "' "
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
                            SQL = SQL & "No, Dari, QrUtuh, Qr, Tanggal_Cetak, Kode_Unik_Print, No_Split, Batch, isRetur) "
                            SQL = SQL & "VALUES('" & KodePerusahaan & "', @newBarcode" & kode_unik_print & ", @newBarcodePSS" & kode_unik_print & ",'" & Kd_Barang & "', '" & Nm_Barang & "', "
                            SQL = SQL & "'" & Nm_Barang_Produksi & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & batchLama & "', "
                            SQL = SQL & "" & HilangkanTanda(Jumlah) & ", '" & Satuan & "', " & UrutBarcode & ", 0, "
                            SQL = SQL & "'" & fullNewQr & "', '" & QrLama & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & kode_unik_print & "', "
                            SQL = SQL & "'" & No_Split_Req & "', '" & No_Batch & "', 'Y'); "
                            ExecuteTrans(SQL)

                            '=================================
                            '=     UPDATE BATAL VALIDASI     =
                            '=================================
                            SQL = $"
                                select Kode_Perusahaan
                                from N_EMI_Transaksi_Material_Requisition_QC_Validasi
                                where Kode_Perusahaan = '{KodePerusahaan}'
                                and status is null
                                and No_Faktur = '{No_Faktur_Validasi}' and Urut_Oto = {Urut_Validasi}
                            "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()
                                    SQL = $"
                                        update N_EMI_Transaksi_Material_Requisition_QC_Validasi set Status  = 'Y', Flag_Retur = 'Y',
	                                        Tanggal_Batal = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Batal = '{Format(tgl_skg, "HH:mm:ss")}', 
                                            User_Batal = '{UserID}', Sn_Regenerate = '{SN_Regenerate}'
                                        where Kode_Perusahaan = '{KodePerusahaan}'
                                        and status is null
                                        and No_Faktur = '{No_Faktur_Validasi}' and Urut_Oto = {Urut_Validasi}
                                    "
                                    ExecuteTrans(SQL)

                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show($"Faktur Validasi {No_Faktur_Validasi} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using




                            arrKdUnikPrint.Add(kode_unik_print)
                            arrUrutValidasi.Add(Urut_Validasi)

                        Next
                    End If
                End With
            End Using






            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        CetakBarcode(arrKdUnikPrint, arrUrutValidasi)
        Kosong()

    End Sub



    Private Sub CetakBarcode(ByVal arrKdUnikPrint As ArrayList, ByVal arrUrutValidasi As ArrayList)
        Try
            OpenConn()

            '=================================
            '=     CETAK FAKTUR TF STOCK     =
            '=================================
            Dim CrDoc As New Object
            Dim kertas As String = ""

            '=================================
            '=     CETAK FAKTUR BARCODE     =
            '=================================
            If arrKdUnikPrint.Count <> 0 Then

                Dim kertasBarcode As String = ""
                kertasBarcode = "BarcodeQC"


                For i As Integer = 0 To arrKdUnikPrint.Count - 1
                    SQL = "select Kode_Perusahaan from N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Unik_Print='" & arrKdUnikPrint(i) & "'"
                    Using Ds = BindingTrans(SQL)
                        If Ds.Tables("MyTable").Rows.Count <> 0 Then

                            CrDoc = New N_EMI_CR_Transaksi_Request_Material_QC_Barcode
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.RecordSelectionFormula = "{N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_CR_Transaksi_Request_Material_QC_Barcode_Cetak.Kode_Unik_Print} = '" & arrKdUnikPrint(i) & "' "

                            'CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC
                            CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC

                            Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                            'doctoprint.PrinterSettings.PrinterName = PrinterBarcodeQC
                            doctoprint.PrinterSettings.PrinterName = PrinterBarcodeQC


                            Dim rawKind As Integer
                            CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                            For j = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                                If doctoprint.PrinterSettings.PaperSizes(j).PaperName = kertasBarcode Then
                                    rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(j).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(j)))
                                    CrDoc.PrintOptions.PaperSize = rawKind
                                    Exit For
                                End If
                            Next

                            If rawKind = Nothing Or rawKind = 0 Then
                                CloseConn()
                                MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Kertas Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            CrDoc.PrintToPrinter(1, False, 1, 2500)

                        End If
                    End Using


                    '===================================
                    '=     UPDATE FLAG SUDAH CETAK     =
                    '===================================
                    SQL = $"
                        update N_EMI_Transaksi_Material_Requisition_QC_Validasi set Flag_Sudah_Cetak = 'Y'
                        where Kode_Perusahaan = '{KodePerusahaan}'
                        and status is null
                        and Urut_Oto = {arrUrutValidasi(i)}
                    "
                    ExecuteTrans(SQL)

                Next
            Else
                CloseConn()
                MessageBox.Show("Terjadi Kesalahan Saat Cetak Barcode. Data Barcode Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    '============================================================================================================================
    '=     UI SECTION
    '============================================================================================================================

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &HA3 Then
            Return
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub LblSatuan_MouseEnter(sender As Object, e As EventArgs) Handles Lbl_TabProd.MouseEnter
        Lbl_TabProd.Font = New Font(Lbl_TabProd.Font.FontFamily, Lbl_TabProd.Font.Size, FontStyle.Bold)
        Border_Bottom_Prod.BackColor = Color.DarkGray
        Cursor = Cursors.Hand

        CekActiveMenu()
    End Sub
    Private Sub LblSatuan_MouseLeave(sender As Object, e As EventArgs) Handles Lbl_TabProd.MouseLeave
        Lbl_TabProd.Font = New Font(Lbl_TabProd.Font.FontFamily, Lbl_TabProd.Font.Size, FontStyle.Regular)
        Border_Bottom_Prod.BackColor = Color.LightGray
        Cursor = Cursors.Default

        CekActiveMenu()
    End Sub
    Private Sub Label10_MouseEnter(sender As Object, e As EventArgs) Handles Lbl_TabQC.MouseEnter
        Lbl_TabQC.Font = New Font(Lbl_TabQC.Font.FontFamily, Lbl_TabQC.Font.Size, FontStyle.Bold)
        Border_Bottom_QC.BackColor = Color.DarkGray
        Cursor = Cursors.Hand

        CekActiveMenu()
    End Sub
    Private Sub Lbl_TabQC_MouseLeave(sender As Object, e As EventArgs) Handles Lbl_TabQC.MouseLeave
        Lbl_TabQC.Font = New Font(Lbl_TabQC.Font.FontFamily, Lbl_TabQC.Font.Size, FontStyle.Regular)
        Border_Bottom_QC.BackColor = Color.LightGray
        Cursor = Cursors.Default

        CekActiveMenu()
    End Sub

    Private Sub Lbl_TabProd_Click(sender As Object, e As EventArgs) Handles Lbl_TabProd.Click
        HandleMenuClick(Lbl_TabProd.Tag)
    End Sub

    Private Sub Lbl_TabQC_Click(sender As Object, e As EventArgs) Handles Lbl_TabQC.Click
        HandleMenuClick(Lbl_TabQC.Tag)
    End Sub

    Private Sub HandleMenuClick(ByVal Tag As String)

        If Tag = "PRODUCTION" Then

            Txt_ActiveMenu.Text = "PRODUCTION"

        ElseIf Tag = "QC" Then

            Txt_ActiveMenu.Text = "QC"
        Else
            MessageBox.Show("Menu Tidak Ditemuukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        CekActiveMenu()
        KosongMenuProd()
        KosongMenuQC()
    End Sub


    Private Sub CekActiveMenu()

        If Txt_ActiveMenu.Text = "PRODUCTION" Then

            ' Tanda Menu Aktif
            Lbl_TabProd.Font = New Font(Lbl_TabProd.Font.FontFamily, Lbl_TabProd.Font.Size, FontStyle.Bold)
            Border_Bottom_Prod.BackColor = Color.DarkGray
            Lbl_TabQC.Font = New Font(Lbl_TabQC.Font.FontFamily, Lbl_TabQC.Font.Size, FontStyle.Regular)
            Border_Bottom_QC.BackColor = Color.LightGray


            Panel_Production.Visible = True
            Panel_Production.Location = New Point(20, 83)
            Panel_QC.Visible = False
            Panel_QC.Location = New Point(1130, 83)

        ElseIf Txt_ActiveMenu.Text = "QC" Then
            ' Tanda Menu Aktif
            Lbl_TabQC.Font = New Font(Lbl_TabProd.Font.FontFamily, Lbl_TabProd.Font.Size, FontStyle.Bold)
            Border_Bottom_QC.BackColor = Color.DarkGray
            Lbl_TabProd.Font = New Font(Lbl_TabQC.Font.FontFamily, Lbl_TabQC.Font.Size, FontStyle.Regular)
            Border_Bottom_Prod.BackColor = Color.LightGray


            Panel_QC.Visible = True
            Panel_QC.Location = New Point(20, 83)
            Panel_Production.Visible = False
            Panel_Production.Location = New Point(1130, 83)
        End If

    End Sub

    Private Sub KosongMenuProd()

        Chk_HriIni.Checked = False
        Chk_Tanggal.Checked = False
        Chk_Lain.Checked = False

        LoadParent()
    End Sub

    Private Sub CM_Batal_TF_Material_QC_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CM_Batal_TF_Material_QC.Opening
        If Lv_DetailQC.Items.Count = 0 Then
            e.Cancel = True
            Exit Sub
        End If


        '=========================================================
        '=     CEK APAKAH MOUSE BERADA DI ATAS ROWS LISTVIEW     =
        '=========================================================
        Dim mousePos As Point = Lv_DetailQC.PointToClient(Cursor.Position)
        Dim info As ListViewHitTestInfo = Lv_DetailQC.HitTest(mousePos)

        If info.Item Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        Lv_DetailQC.FocusedItem = info.Item
        info.Item.Selected = True
    End Sub

    Private Sub KosongMenuQC()
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False

        LoadParentQC()
    End Sub



End Class
