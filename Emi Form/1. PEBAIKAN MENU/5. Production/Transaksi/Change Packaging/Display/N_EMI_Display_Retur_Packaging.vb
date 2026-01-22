Imports System.Drawing.Printing
Imports System.IO

Public Class N_EMI_Display_Retur_Packaging

    Private ReadOnly BodyAlignments, BodyAlignments2 As New Dictionary(Of Integer, StringAlignment)

    Private random As New Random()

    Dim arrFilterLokasiGudang, arrFilterTanggal, arrFilterParamLain As New ArrayList

    Dim Lv_No_Transaksi, Lv_No_Split, Lv_Tgl_Produksi, Lv_Jam_Produksi, Lv_Tgl_Retur, Lv_Jam_Retur, Lv_Jumlah, Lv_Satuan As String

    Dim Item_No_Transaksi As Integer = 0
    Dim Item_No_Split As Integer = 1
    Dim Item_Tgl_Produksi As Integer = 2
    Dim Item_Jam_Produksi As Integer = 3
    Dim Item_Tgl_retur As Integer = 4
    Dim Item_Jam_Retur As Integer = 5
    Dim Item_Jumlah As Integer = 6
    Dim Item_Satuan As Integer = 7


    Private Sub N_EMI_Display_Retur_Packaging_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Parent.Columns.Clear() : BodyAlignments.Clear()
        Lv_Parent.Columns.Add("No Transaksi", 180) : BodyAlignments(0) = StringAlignment.Near
        Lv_Parent.Columns.Add("No Split", 180) : BodyAlignments(1) = StringAlignment.Near
        Lv_Parent.Columns.Add("Tanggal Produksi", 140) : BodyAlignments(2) = StringAlignment.Center
        Lv_Parent.Columns.Add("Jam Produksi", 110) : BodyAlignments(3) = StringAlignment.Center
        Lv_Parent.Columns.Add("Tanggal Retur", 140) : BodyAlignments(4) = StringAlignment.Center
        Lv_Parent.Columns.Add("Jam Retur", 110) : BodyAlignments(5) = StringAlignment.Center
        Lv_Parent.Columns.Add("Jumlah", 180) : BodyAlignments(6) = StringAlignment.Far
        Lv_Parent.Columns.Add("Satuan", 80) : BodyAlignments(7) = StringAlignment.Center
        Lv_Parent.View = View.Details

        Lv_Detail_RM.Columns.Clear() : BodyAlignments2.Clear()
        Lv_Detail_RM.Columns.Add("No Faktur", 140) : BodyAlignments2(0) = StringAlignment.Near
        Lv_Detail_RM.Columns.Add("Lokasi", 140) : BodyAlignments2(1) = StringAlignment.Near
        Lv_Detail_RM.Columns.Add("Kode Barang", 140) : BodyAlignments2(2) = StringAlignment.Near
        Lv_Detail_RM.Columns.Add("Nama Barang", 250) : BodyAlignments2(3) = StringAlignment.Near
        Lv_Detail_RM.Columns.Add("Jumlah", 130) : BodyAlignments2(4) = StringAlignment.Far
        Lv_Detail_RM.Columns.Add("Satuan", 80) : BodyAlignments2(5) = StringAlignment.Center
        Lv_Detail_RM.View = View.Details

        Cmb_Tanggal.Items.Clear() : arrFilterTanggal.Clear()
        Cmb_Tanggal.Items.Add("Tanggal Retur") : arrFilterTanggal.Add("a.Tanggal")
        Cmb_Tanggal.Items.Add("Tanggal Produksi") : arrFilterTanggal.Add("b.Tanggal")

        Cmb_Param_Lain.Items.Clear() : arrFilterParamLain.Clear()
        Cmb_Param_Lain.Items.Add("No Transakasi") : arrFilterParamLain.Add("a.No_Transaksi")
        Cmb_Param_Lain.Items.Add("No Split") : arrFilterParamLain.Add(".No_Split")


        Try
            OpenConn()

            Cmb_Detail_Satuan_Retur.Items.Clear() : Cmb_Detail_Satuan_Retur_Convert.Items.Clear()
            SQL = "select satuan from EMI_Satuan "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"order by satuan "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Detail_Satuan_Retur.Items.Add(Dr("satuan"))
                    Cmb_Detail_Satuan_Retur_Convert.Items.Add(Dr("satuan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Kosong()

    End Sub


    Private Sub Kosong()

        Cb_Hari_Ini.Checked = True
        Load_Data_Parent()

    End Sub



    Private Sub Kosong_Detail()

        Txt_Detail_Lokasi.Text = ""
        Txt_Detail_Kd_Barang_Awal.Text = ""
        Txt_Detail_Nm_Barang_Awal.Text = ""
        Txt_Detail_Kd_Barang_Tujuan.Text = ""
        Txt_Detail_Nm_Barang_Tujuan.Text = ""
        Txt_Detail_Jumlah_Retur.Text = ""
        Txt_Detail_Jumlah_RM.Text = ""
        Txt_Detail_Jumlah_Retur_convert.Text = ""

        Cmb_Detail_Satuan_Retur.SelectedIndex = -1
        Cmb_Detail_Satuan_Retur_Convert.SelectedIndex = -1

        Lv_Detail_RM.Items.Clear()


    End Sub



    Private Sub Get_Data_Parent(ByVal index As Integer)
        Lv_No_Transaksi = Lv_Parent.Items(index).SubItems(Item_No_Transaksi).Text
        Lv_No_Split = Lv_Parent.Items(index).SubItems(Item_No_Split).Text
        Lv_Tgl_Produksi = Lv_Parent.Items(index).SubItems(Item_Tgl_Produksi).Text
        Lv_Jam_Produksi = Lv_Parent.Items(index).SubItems(Item_Jam_Produksi).Text
        Lv_Tgl_Retur = Lv_Parent.Items(index).SubItems(Item_Tgl_retur).Text
        Lv_Jam_Retur = Lv_Parent.Items(index).SubItems(Item_Jam_Retur).Text
        Lv_Jumlah = Lv_Parent.Items(index).SubItems(Item_Jumlah).Text
        Lv_Satuan = Lv_Parent.Items(index).SubItems(Item_Satuan).Text
    End Sub



    Private Sub Load_Data_Parent()
        Try
            OpenConn()

            Lv_Parent.Items.Clear()
            SQL = "select a.No_Transaksi, a.No_Split, a.status, b.Tanggal as Tanggal_Produksi, b.Jam as Jam_Produksi, "
            SQL &= $"a.Tanggal as Tanggal_Retur, a.Jam as Jam_Retur, a.Jumlah, a.Satuan "
            SQL &= $"from EMI_Production_Results_Detail_Change_Packaging a "
            SQL &= $"inner join Emi_Split_Production_Order b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Split = b.No_Transaksi "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "

            If Cb_Hari_Ini.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL &= $" a.tanggal between '"
                SQL &= Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If Cb_Tanggal.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL &= arrFilterTanggal.Item(Cmb_Tanggal.SelectedIndex) & " between ' "
                SQL &= Format(Tgl_1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl_2.Value, "yyyy-MM-dd") & "' "
            End If

            If Cb_Param_Lain.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL &= arrFilterParamLain.Item(Cmb_Param_Lain.SelectedIndex) & " like '%" & Trim(Txt_Param_Lain.Text) & "%' "
            End If

            SQL &= $"order by a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Parent.Items.Add(Dr("No_Transaksi"))
                    Lv.SubItems.Add(Dr("No_Split"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Produksi")) = "", "-", Format(Dr("Tanggal_Produksi"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam_Produksi")) = "", "-", Dr("Jam_Produksi")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Tanggal_Retur")) = "", "-", Format(Dr("Tanggal_Retur"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Jam_Retur")) = "", "-", Dr("Jam_Retur")))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    Lv.SubItems.Add(Dr("Satuan"))

                    Lv.Tag = General_Class.CekNULL(Dr("status"))


                Loop
            End Using

            Kosong_Detail()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Lv_Parent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Parent.SelectedIndexChanged
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then Exit Sub

        Kosong_Detail()

        Try
            OpenConn()

            Dim Selected_No_Transaksi As String = Lv_Parent.FocusedItem.SubItems(Item_No_Transaksi).Text

            SQL = "select b.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang_Awal, c.nama As Nama_Barang_Awal, b.Kode_Barang_Tujuan, d.Nama as Nama_Barang_Tujuan,  "
            SQL &= $"b.Jumlah_Awal, b.Satuan_Awal, b.Jumlah_Tujuan, b.Satuan_Tujuan, b.Jumlah_Request "
            SQL &= $"from EMI_Production_Results_Detail_Change_Packaging a "
            SQL &= $"inner join EMI_Production_Results_Detail_Change_Packaging_Detail b on a.kode_perusahaan = b.Kode_Perusahaan and a.no_Transaksi = b.no_transaksi "
            SQL &= $"inner join barang c on a.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang_Awal = c.Kode_Barang "
            SQL &= $"inner join barang d on a.kode_perusahaan = d.kode_perusahaan and b.Kode_Stock_Owner = d.Kode_Stock_Owner and b.Kode_Barang_Tujuan = d.Kode_Barang "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.no_transaksi = '{Selected_No_Transaksi}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Txt_Detail_Lokasi.Text = Dr("Kode_Stock_Owner")
                    Txt_Detail_Kd_Barang_Awal.Text = Dr("Kode_Barang_Awal")
                    Txt_Detail_Nm_Barang_Awal.Text = Dr("Nama_Barang_Awal")
                    Txt_Detail_Kd_Barang_Tujuan.Text = Dr("Kode_Barang_Tujuan")
                    Txt_Detail_Nm_Barang_Tujuan.Text = Dr("Nama_Barang_Tujuan")
                    Txt_Detail_Jumlah_Retur.Text = Format(Dr("Jumlah_Awal"), "N4")
                    Txt_Detail_Jumlah_Retur_convert.Text = Format(Dr("Jumlah_Tujuan"), "N4")
                    Txt_Detail_Jumlah_RM.Text = If(General_Class.CekNULL(Dr("Jumlah_Request")) = "", "0", Format(Dr("Jumlah_Request"), "N4"))

                    Cmb_Detail_Satuan_Retur.SelectedItem = Dr("Satuan_Awal")
                    Cmb_Detail_Satuan_Retur_Convert.SelectedItem = Dr("Satuan_Tujuan")

                End If
            End Using

            Lv_Detail_RM.Items.Clear()
            SQL = "select a.No_Faktur, b.Kode_Stock_Owner_Tujuan, c.Kode_Barang, d.nama as Nama_Barang, c.Jumlah, c.Satuan "
            SQL &= $"from Emi_Material_Requisition a "
            SQL &= $"inner join Emi_Material_Requisition_Det b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join Emi_Material_Requisition_Det_Convert c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
            SQL &= $"inner join barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL &= $"where a.Flag_Retur_Packaging = 'Y' "
            SQL &= $"and a.No_Faktur_Retur_Packaging = '{Selected_No_Transaksi}' "
            SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim LV As ListViewItem
                    LV = Lv_Detail_RM.Items.Add(Dr("No_Faktur"))
                    LV.SubItems.Add(Dr("Kode_Stock_Owner_Tujuan"))
                    LV.SubItems.Add(Dr("Kode_Barang"))
                    LV.SubItems.Add(Dr("Nama_Barang"))
                    LV.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    LV.SubItems.Add(Dr("Satuan"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub



    Private Sub Btn_Show_RM_Click(sender As Object, e As EventArgs) Handles Btn_Show_RM.Click
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Or Txt_Detail_Lokasi.Text.Trim.Length = 0 Then Exit Sub

        'Try
        '    OpenConn()

        '    Dim Selected_No_Transaksi As String = Lv_Parent.FocusedItem.SubItems(Item_No_Transaksi).Text

        '    Dim HasData As Boolean = False

        '    Lv_Detail_RM.Items.Clear()
        '    SQL = "select a.No_Faktur, b.Kode_Stock_Owner_Tujuan, c.Kode_Barang, d.nama as Nama_Barang, c.Jumlah, c.Satuan "
        '    SQL &= $"from Emi_Material_Requisition a "
        '    SQL &= $"inner join Emi_Material_Requisition_Det b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
        '    SQL &= $"inner join Emi_Material_Requisition_Det_Convert c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.No_Urut_Det "
        '    SQL &= $"inner join barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
        '    SQL &= $"where a.Flag_Retur_Packaging = 'Y' "
        '    SQL &= $"and a.No_Faktur_Retur_Packaging = '{Selected_No_Transaksi}' "
        '    SQL &= $"and a.Kode_Perusahaan = '{KodePerusahaan}' "
        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read
        '            HasData = True
        '            Dim LV As ListViewItem
        '            LV = Lv_Detail_RM.Items.Add(Dr("No_Faktur"))
        '            LV.SubItems.Add(Dr("Kode_Stock_Owner_Tujuan"))
        '            LV.SubItems.Add(Dr("Kode_Barang"))
        '            LV.SubItems.Add(Dr("Nama_Barang"))
        '            LV.SubItems.Add(Format(Dr("Jumlah"), "N4"))
        '            LV.SubItems.Add(Dr("Satuan"))
        '        Loop
        '    End Using

        '    If Not HasData Then
        '        CloseConn()
        '        MessageBox.Show($"No Faktur Ini Tidak Melakukan Request Material", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If


        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try


    End Sub


    '====================================================================================================================================================================================
    '=     HANDLE KONTEKS MENU
    '====================================================================================================================================================================================
    Private Sub SalinNoTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoTransaksiToolStripMenuItem.Click
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.SelectedItems.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Parent.FocusedItem.SubItems(Item_No_Transaksi).Text)
    End Sub


    Private Sub BatalkanTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanTransaksiToolStripMenuItem.Click
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim SelectedFaktur As String = Lv_Parent.FocusedItem.SubItems(Item_No_Transaksi).Text

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Retur_Packaging") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Pembatalan Retur Packaging", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If MessageBox.Show($"Yakin Ingin Membatalkan Transaksi Retur Packaging{SelectedFaktur}?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub

            '============================================================
            '=     CEK APAKAH TRANSAKSI SUDAH DIBATALKAN SEBELUMNYA     =
            '============================================================
            SQL = "select status from EMI_Production_Results_Detail_Change_Packaging "
            SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
            SQL &= $"and no_transaksi = '{SelectedFaktur}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"No Transaksi Sudah Dibatalkan Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"No Transaksi Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=====================================================
            '=     CEK APAKAH DATA REQUEST SUDAH DI TRANSFER     =
            '=====================================================
            SQL = "SELECT d.Flag_Transfer "
            SQL &= $"from EMI_Production_Results_Detail_Change_Packaging a "
            SQL &= $"inner join Emi_Material_Requisition b on a.kode_perusahaan = b.kode_perusahaan and a.No_Transaksi = b.No_Faktur_Retur_Packaging and b.Flag_Retur_Packaging = 'Y' "
            SQL &= $"inner join Emi_Material_Requisition_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur "
            SQL &= $"inner join Emi_Material_Requisition_Det_Convert d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_Faktur and c.Urut_Oto = d.No_Urut_Det "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{SelectedFaktur}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Flag_Transfer")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Tidak Dapat Melakukan Pembatalan Retur, Karena Request Material Sudah Ditransfer", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End If
            End Using

            '======================================================
            '=     CEK APAKAH DATA SUDAH DILAKUKAN PEMUSNAHAN     =
            '======================================================
            SQL = "select top 1 a.Kode_Perusahaan "
            SQL &= $"from N_EMI_Binding_Transaksi_Transfer_Waste_Produk a "
            SQL &= $"inner join N_EMI_Transaksi_Transfer_Waste_Produk b on a.kode_perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and b.Status is null "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Production_Result = '{SelectedFaktur}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Tidak Dapat Melakukan Pembatalan Retur, Karena Request Material Sudah Ditransfer", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=========================
            '=     LAKUKAN RETUR     =
            '=========================
            SQL = "select a.No_Transaksi, b.Kode_Stock_Owner, b.Kode_Barang_Awal, b.Kode_Barang_Tujuan, b.Jumlah_Awal, b.Satuan_Awal, b.Jumlah_Tujuan, b.Satuan_Tujuan, b.Jumlah_Request "
            SQL &= $"from EMI_Production_Results_Detail_Change_Packaging a "
            SQL &= $"inner join EMI_Production_Results_Detail_Change_Packaging_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Transaksi = '{SelectedFaktur}' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim No_Faktur_Retur As String = .Rows(i).Item("No_Transaksi")
                            Dim Kd_So As String = .Rows(i).Item("Kode_Stock_Owner")
                            Dim Kd_Barang_Awal As String = .Rows(i).Item("Kode_Barang_Awal")
                            Dim Kd_Barang_Tujuan As String = .Rows(i).Item("Kode_Barang_Tujuan")
                            Dim Satuan_Awal As String = .Rows(i).Item("Satuan_Awal")
                            Dim Satuan_Tujuan As String = .Rows(i).Item("Satuan_Tujuan")
                            Dim Jumlah_Awal As Double = .Rows(i).Item("Jumlah_Awal")
                            Dim Jumlah_Tujuan As Double = .Rows(i).Item("Jumlah_Tujuan")
                            Dim Jumlah_Request As Double = .Rows(i).Item("Jumlah_Request")


                            '==============================
                            '=     RETUR BARANG SCRAP     =
                            '==============================
                            Dim sisa As Double = 0
                            Dim Nilai_Packaging As Double = 0
                            Dim JumlahPotong As Double = 0
                            Dim HPP_Packaging As Double = 0

                            Dim Tanggal_Expired_Pertama As String = ""
                            Dim Tanggal_Produksi_Pertama As String = ""
                            Dim Tanggal_Masuk_Pertama As String = ""
                            Dim Batch_Number As String = ""
                            Dim Kode_Unik_Asal As String = ""

                            SQL = "select kode_stock_owner, kode_barang, serial_number, dbo.get_hpp(Serial_Number) as HPP, round(jumlah,4) as jumlah, Tgl_Expired, Tgl_Produksi, Batch_Number, "
                            SQL = SQL & "Kode_Unik_Asal, Tgl_Masuk "
                            SQL = SQL & "from barang_sn where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and "
                            SQL = SQL & "kode_barang = '" & Kd_Barang_Tujuan & "' and jumlah <> 0 "
                            SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                            Using Ds1 = BindingTrans(SQL)

                                If .Rows.Count <> 0 Then
                                    sisa = Val(HilangkanTanda(Jumlah_Tujuan))

                                    For j As Integer = 0 To .Rows.Count - 1
                                        If sisa = 0 Then
                                            Exit For
                                        ElseIf sisa < 0 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terdapat Kesalahan saat Potong Barang SN", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        Dim HppPackaging As Double = Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(j).Item("HPP")))
                                        HPP_Packaging = Val(HilangkanTanda(Ds1.Tables("MyTable").Rows(j).Item("HPP")))

                                        Tanggal_Expired_Pertama = Ds1.Tables("MyTable").Rows(j).Item("Tgl_Expired")
                                        Tanggal_Produksi_Pertama = Ds1.Tables("MyTable").Rows(j).Item("Tgl_Produksi")
                                        Tanggal_Masuk_Pertama = Ds1.Tables("MyTable").Rows(j).Item("Tgl_Masuk")
                                        Batch_Number = Ds1.Tables("MyTable").Rows(j).Item("Batch_Number")
                                        Kode_Unik_Asal = Ds1.Tables("MyTable").Rows(j).Item("Kode_Unik_Asal")

                                        If sisa < Val(Ds1.Tables("MyTable").Rows(j).Item("jumlah")) Or sisa = Val(Ds1.Tables("MyTable").Rows(j).Item("jumlah")) Then
                                            SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & Ds1.Tables("MyTable").Rows(j).Item("kode_stock_owner") & "' and "
                                            SQL = SQL & "kode_barang = '" & Ds1.Tables("MyTable").Rows(j).Item("kode_barang") & "' and "
                                            SQL = SQL & "serial_number = '" & Ds1.Tables("MyTable").Rows(j).Item("serial_number") & "'"
                                            ExecuteTrans(SQL)

                                            Nilai_Packaging = Nilai_Packaging + (HppPackaging * sisa)
                                            JumlahPotong += sisa
                                            sisa = 0
                                        ElseIf sisa > Val(Ds1.Tables("MyTable").Rows(j).Item("jumlah")) Then
                                            SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "kode_stock_owner = '" & Ds1.Tables("MyTable").Rows(j).Item("kode_stock_owner") & "' and "
                                            SQL = SQL & "kode_barang = '" & Ds1.Tables("MyTable").Rows(j).Item("kode_barang") & "' and "
                                            SQL = SQL & "serial_number = '" & Ds1.Tables("MyTable").Rows(j).Item("serial_number") & "'"
                                            ExecuteTrans(SQL)

                                            Nilai_Packaging = Nilai_Packaging + (HppPackaging * Val(HilangkanTanda(Format(Ds1.Tables("MyTable").Rows(j).Item("jumlah"), "N4"))))
                                            JumlahPotong += Val(HilangkanTanda(Format(Ds1.Tables("MyTable").Rows(j).Item("jumlah"), "N4")))
                                            sisa = sisa - Val(HilangkanTanda(Format(Ds1.Tables("MyTable").Rows(j).Item("jumlah"), "N4")))
                                        Else
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terjadi Kesalaham pada Barang SN untuk Kode Barang " & Kd_Barang_Tujuan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        If Math.Round(sisa, 4) <> 0 And i = Ds.Tables("MyTable").Rows.Count - 1 Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Jumlah stock tidak mencukupi untuk kode barang " & Kd_Barang_Tujuan & "!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                    Next
                                End If
                            End Using


                            '=========================
                            '=     POTONG BARANG     =
                            '=========================
                            SQL = "Update barang set "
                            SQL = SQL & "Good_Stock = Good_Stock - " & HilangkanTanda(JumlahPotong) & " ,  Jumlah_Bags = Jumlah_Bags - 0 "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and kode_barang = '" & Kd_Barang_Tujuan & "'"
                            ExecuteTrans(SQL)

                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 4) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & Kd_So & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & Kd_Barang_Tujuan & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds9 = BindingTrans(SQL)
                                If Ds9.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds9.Tables("MyTable").Rows(0).Item("good_stock") <> Ds9.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan . . ! !, Stock Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !, Stock Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '===========================================
                            '=     GET WAREHOUSE DAN PALLET KOSONG     =
                            '===========================================
                            Dim available_Id_Warehouse As String = ""
                            Dim available_NoPallet As String = ""
                            SQL = "select top(1) a.id_wms_warehouse_position, b.nomor_urut from "
                            SQL = SQL & "view_warehouse_position a, view_warehouse_position_detail b "
                            SQL = SQL & "where a.Id_WMS_Warehouse_Position=b.Id_WMS_Warehouse_Position "
                            SQL = SQL & " And a.kode_Perusahaan = b.kode_Perusahaan And a.kode_Perusahaan ='" & KodePerusahaan & "' "
                            SQL = SQL & "and a.Kode_Stock_Owner='" & Kd_So & "' and b.Kode_Barang is null"
                            Using Dr2 = OpenTrans(SQL)
                                Do While Dr2.Read
                                    available_Id_Warehouse = Dr2("id_wms_warehouse_position")
                                    available_NoPallet = Dr2("nomor_urut")
                                Loop
                            End Using

                            Dim TotalhppLama As Double = HPP_Packaging
                            Dim hppSekarang As Double = Math.Round(TotalhppLama, 0)
                            'Dim HppBaru As Double = hppSekarang + (Math.Round(Nilai_Packaging / JumlahInsert, 0))
                            Dim HppBaru As Double = hppSekarang

                            Dim Str As String = Format(random.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                            Dim Kode_Unik As String = Str.Substring(0, 5) & "BB" & Chr(64 + Str.Substring(6, 1)) & Str.Substring(6, Len(Str) - 6)
                            Dim SN_Baru As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & HppBaru & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                            Dim newQrCode As String = Generate_QR_Batch(Kd_Barang_Awal, Batch_Number)
                            Dim Kode_Berjalan As String = Generate_Random_Kode(10)

                            Dim KualitasBarang As String = "HIJAU"


                            SQL = "insert into Barang_SN (Kode_Perusahaan, Kode_Stock_Owner, Kode_Barang, Serial_Number, Jumlah, Jumlah_Bags, Tgl_Expired, Tgl_Produksi, Stock_PO, Stock_Inquiry, Id_Warehouse, "
                            SQL = SQL & "Qr_Code, Kode_Unik_Berjalan, Kode_Unik_Asal, Nomor_Pallet, batch_number, Warna, Tgl_masuk, Blok_SN, Id_Jenis_Kategori_Produksi) "
                            SQL = SQL & "values('" & KodePerusahaan & "', '" & Kd_So & "', '" & Kd_Barang_Awal & "', '" & SN_Baru & "', "
                            SQL = SQL & "'" & HilangkanTanda(Jumlah_Awal) & "', '0', '" & Tanggal_Expired_Pertama & "', '" & Tanggal_Produksi_Pertama & "', 0, 0, "
                            SQL = SQL & "'" & available_Id_Warehouse & "', '" & newQrCode & "', '" & Kode_Berjalan & "', '" & Kode_Unik_Asal & "-" & Kode_Berjalan & "', '" & available_NoPallet & "', "
                            SQL = SQL & "'" & Batch_Number & "', '" & KualitasBarang & "', '" & Tanggal_Masuk_Pertama & "', NULL, NULL)"
                            ExecuteTrans(SQL)

                            '=========================
                            '=     INSERT BARANG     =
                            '=========================
                            SQL = "Update barang set "
                            SQL = SQL & "Good_Stock = Good_Stock + " & HilangkanTanda(Jumlah_Awal) & " ,  Jumlah_Bags = Jumlah_Bags + 0 "
                            SQL = SQL & "where kode_perusahaan   = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Kd_So & "' and kode_barang = '" & Kd_Barang_Awal & "'"
                            ExecuteTrans(SQL)


                            '========================================
                            '=     APAKAH ADA REQUEST MATERIAL?     =
                            '========================================
                            If Jumlah_Request <> 0 Then
                                SQL = "select 1 from Emi_Material_Requisition "
                                SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                SQL &= $"and status is null "
                                SQL &= $"and No_Faktur_Retur_Packaging = '{No_Faktur_Retur}' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dr.Close()
                                        SQL = "update Emi_Material_Requisition set status = 'Y' "
                                        SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                        SQL &= $"and status is null "
                                        SQL &= $"and No_Faktur_Retur_Packaging = '{No_Faktur_Retur}' "
                                        ExecuteTrans(SQL)

                                        'Else
                                        '    Dr.Close()
                                        '    CloseTrans()
                                        '    CloseConn()
                                        '    MessageBox.Show($"Terjadi Kesalaham, No Faktur {No_Faktur_Retur} pada Data Request Material Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        '    Exit Sub
                                    End If
                                End Using


                            End If


                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Terjadi Kesalahan, Tidak ada Data yang Bisa Dirollback", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            SQL = "update EMI_Production_Results_Detail_Change_Packaging set status = 'Y', "
            SQL &= $"UserID_Batal = '{UserID}', Tanggal_Batal = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Batal = '{Format(tgl_skg, "HH:mm:ss")}' "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and status is null "
            SQL &= $"and No_Transaksi = '{SelectedFaktur}' "
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show($"No Transaksi {SelectedFaktur} Berhasil Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
    End Sub


    Private Sub CetakBarcodeScrapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakBarcodeScrapToolStripMenuItem.Click
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then Exit Sub

        Dim SelectedTransaksi As String = Lv_Parent.FocusedItem.Text

        Dim kode_unik_print As String = ""

        Dim KdUnikPrint As New ArrayList

        Dim No_Split As String = ""
        Dim Kd_So As String = ""
        Dim Kd_Barang_Scrap As String = ""

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Cetak_Ulang_Barcode_Scrap_Retur_Packaging") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Cetak Ulang Barcode", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            Dim selectedFaktur As String = Lv_Parent.FocusedItem.SubItems(Item_No_Transaksi).Text
            Dim selectedSplit As String = Lv_Parent.FocusedItem.SubItems(Item_No_Split).Text

            Dim SelectedBarcode As String = ""

            SQL = "select (c.Qr_Code+'-'+c.Kode_Unik_Berjalan) as Barcode_Scrap "
            SQL &= $"from EMI_Production_Results_Detail_Change_Packaging a "
            SQL &= $"inner join EMI_Production_Results_Detail_Change_Packaging_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join barang_sn c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang_Tujuan = c.Kode_Barang and b.SN_Scrap = c.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.status is null "
            SQL &= $"and a.No_Transaksi ='{selectedFaktur}' "
            SQL &= $"and a.No_Split = '{selectedSplit}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    SelectedBarcode = Dr("Barcode_Scrap")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Barcode Scrap Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            SQL = "select Kode_Perusahaan from EMI_Production_Results_Detail_Change_Packaging "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and status = 'Y' "
            SQL = SQL & "and No_Transaksi = '" & selectedFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Cetak Barcode Tidak Bisa Dilakukan, Karena No Transaksi Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            SQL = "truncate table N_EMI_Barcode_Label_Retur_Packaging "
            ExecuteTrans(SQL)


            SQL = "select a.No_Transaksi, a.No_Split, b.Kode_Stock_Owner, b.Kode_Barang_Tujuan, b.Jumlah_Tujuan, b.Satuan_Tujuan, "
            SQL &= $"c.Qr_Code, c.Kode_Unik_Berjalan, b.Nomor_Scrap, a.Tanggal, a.Jam "
            SQL &= $"from EMI_Production_Results_Detail_Change_Packaging a "
            SQL &= $"inner join EMI_Production_Results_Detail_Change_Packaging_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi "
            SQL &= $"inner join barang_sn c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang_Tujuan = c.Kode_Barang and b.SN_Scrap = c.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.status is null "
            SQL &= $"and a.No_Transaksi ='{selectedFaktur}' "
            SQL &= $"and a.No_Split = '{selectedSplit}' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            '==================================
                            '=      GENERATE NEW BARCODE      =
                            '==================================
                            No_Split = .Rows(i).Item("No_Split")
                            Kd_So = .Rows(i).Item("Kode_Stock_Owner")
                            Kd_Barang_Scrap = .Rows(i).Item("Kode_Barang_Tujuan")
                            Dim newQrCode As String = .Rows(i).Item("Qr_Code")
                            Dim Kode_Berjalan As String = .Rows(i).Item("Kode_Unik_Berjalan")
                            Dim No_Scrap As String = .Rows(i).Item("Nomor_Scrap")
                            Dim Jumlah_Convert_Scrap As Double = .Rows(i).Item("Jumlah_Tujuan")
                            Dim satuan_Scrap As String = .Rows(i).Item("Satuan_Tujuan")
                            Dim Tanggal As Date = .Rows(i).Item("Tanggal")
                            Dim jam As String = .Rows(i).Item("Jam")


                            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")
                            Dim fullNewQr As String = newQrCode & "-" & Kode_Berjalan

                            Cmd.Parameters.Clear()
                            Using ImgBarcode1 As Image = Generate_QR_NoPadding(fullNewQr)
                                Using ms1 As New MemoryStream()
                                    ImgBarcode1.Save(ms1, Imaging.ImageFormat.Jpeg)
                                    Dim rawData1 As Byte() = ms1.ToArray()

                                    Dim param1 As String = "@newBarcode" & kode_unik_print
                                    Cmd.Parameters.Add(param1, SqlDbType.Image).Value = rawData1
                                End Using
                            End Using

                            Dim barcode As String = "@newBarcode" & kode_unik_print

                            '=============================
                            '=      GET NAMA BARANG      =
                            '=============================
                            Dim Nama_Scrap As String = ""
                            SQL = "select Nama from barang "
                            SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and kode_stock_owner = '{Kd_So}' "
                            SQL &= $"and kode_barang = '{Kd_Barang_Scrap}' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Nama_Scrap = Dr("Nama")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Barang Scrap Tidak Ditemukan di Tabel Barang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            Dim ID_Routing As String = ""
                            Dim Routing As String = ""
                            SQL = "Select a.Id_Routing, c.Keterangan as Routing "
                            SQL &= $"From EMI_Order_Produksi a "
                            SQL &= $"inner join Emi_Split_Production_Order b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_PO "
                            SQL &= $"inner join EMI_Master_Routing c on a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_Routing = c.Id_Routing "
                            SQL &= $"Where b.No_Transaksi ='{No_Split}' and a.kode_Perusahaan ='{KodePerusahaan}' "
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    ID_Routing = dr("Id_Routing")
                                    Routing = dr("Routing")
                                End If
                            End Using


                            SQL = "insert into  N_EMI_Barcode_Label_Retur_Packaging (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
                            SQL = SQL & "Proses, Jumlah, Satuan, Nomor, id_routing, routing, Kode_unik_print)  "
                            SQL = SQL & "values ('" & KodePerusahaan & "', '" & No_Split & "', " & barcode & ", '" & Kd_Barang_Scrap & "', '" & Nama_Scrap & "', '" & fullNewQr & "', '" & newQrCode & "', "
                            SQL = SQL & "'" & Format(Tanggal, "yyyy-MM-dd") & "', '" & jam & "', '0', '" & HilangkanTanda(Jumlah_Convert_Scrap) & "', '" & satuan_Scrap & "', "
                            SQL = SQL & "'" & No_Scrap & "', '" & ID_Routing & "', '" & Routing & "', '" & kode_unik_print & "') "
                            ExecuteTrans(SQL)

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Cetak Barcode Tidak Bisa Dilakukan, Karena No Transaksi Data No Transaksi {selectedFaktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
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

        Try
            OpenConn()
            Dim CrDoc As New Object

            Dim KertasBesar As String = "BarcodeFG"

            SQL = "select Kode_Perusahaan from N_EMI_Barcode_Label_Retur_Packaging where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & Kd_Barang_Scrap & "' and Kode_Unik_Print = '" & kode_unik_print & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    Dim printerDitemukan As Boolean = False
                    '==========================
                    '=     BARCODEE BESAR     =
                    '==========================
                    For Each printer As String In PrinterSettings.InstalledPrinters
                        If printer.ToLower() = PrinterBarcode.ToLower() Then
                            printerDitemukan = True
                            Exit For
                        End If
                    Next

                    CrDoc = New N_EMI_Barcode_Retur_Packaging

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Retur_Packaging.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Barang} = '" & Kd_Barang_Scrap & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Unik_Print} = '" & kode_unik_print & "'  "
                    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                    '    .Text = "New Barcode Finish Good"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With


                    If printerDitemukan Then
                        'CrDoc = New N_EMI_Barcode_Retur_Packaging

                        'With A_Place_For_Printing2
                        '    CrDoc.SetDataSource(Ds)
                        '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        '    CrDoc.PrintOptions.PrinterName = ""
                        '    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Retur_Packaging.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Barang} = '" & Kd_Barang_Scrap & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Unik_Print} = '" & kode_unik_print & "'  "
                        '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                        '    .Text = "New Barcode Finish Good"
                        '    .CrystalReportViewer1.ReportSource = CrDoc
                        '    .Refresh()
                        '    .Show()
                        'End With

                        ''============================================

                        Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Retur_Packaging.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Barang} = '" & Kd_Barang_Scrap & "' and {N_EMI_Barcode_Label_Retur_Packaging.Kode_Unik_Print} = '" & kode_unik_print & "'  "
                        CrDoc.PrintOptions.PrinterName = PrinterBarcode

                        doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                        Dim rawKind As Integer
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                            If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                                rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                                CrDoc.PrintOptions.PaperSize = rawKind
                                Exit For
                            End If
                        Next

                        CrDoc.PrintToPrinter(1, False, 1, 2500)





                    Else
                        MessageBox.Show("Printer FG Tidak ditemukan", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                    End If



                End If
            End Using

            CloseConn()
            MessageBox.Show("Barcode Berhasil Dicetak", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

    End Sub



    '====================================================================================================================================================================================
    '=     HANDLE FILTER
    '====================================================================================================================================================================================
    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cb_Hari_Ini.Checked = False And Cb_Tanggal.Checked = False And Cb_Param_Lain.Checked = False Then
            MessageBox.Show("Check salah satu filter dahulu", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cb_Hari_Ini.Focus() : Exit Sub
        End If

        If Cb_Tanggal.Checked Then
            If Cmb_Tanggal.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Tanggal Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Tanggal.DroppedDown = True : Cmb_Tanggal.Focus() : Exit Sub
            ElseIf Tgl_1.Value > Tgl_2.Value Then
                MessageBox.Show("Periode I Tidak Boleh Lebih Dari periode II!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Tgl_1.Value = Now.Date : Tgl_2.Value = Now.Date
                Exit Sub
            End If
        End If

        If Cb_Param_Lain.Checked Then
            If Cmb_Param_Lain.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Lain Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Cmb_Param_Lain.DroppedDown = True : Cmb_Param_Lain.Focus() : Exit Sub
            ElseIf Txt_Param_Lain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Txt_Param_Lain.Focus() : Exit Sub
            End If
        End If

        Load_Data_Parent()

    End Sub

    Private Sub Cb_Hari_Ini_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Hari_Ini.KeyPress
        If e.KeyChar = Chr(13) Then Cb_Tanggal.Focus()
    End Sub

    Private Sub Cb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cb_Tanggal.Checked Then
                Cmb_Tanggal.DroppedDown = True
                Cmb_Tanggal.Focus()
            Else
                Cb_Param_Lain.Focus()
            End If

        End If
    End Sub

    Private Sub Cb_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cb_Param_Lain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cb_Param_Lain.Checked Then
                Cmb_Param_Lain.DroppedDown = True
                Cmb_Param_Lain.Focus()
            Else
                Btn_Cari.Focus()
            End If

        End If
    End Sub

    Private Sub Cmb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then Tgl_1.Focus()
    End Sub

    Private Sub Tgl_1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl_2.Focus()
    End Sub

    Private Sub Tgl_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_2.KeyPress
        If e.KeyChar = Chr(13) Then Cb_Param_Lain.Focus()
    End Sub



    Private Sub Cmb_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Param_Lain.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Param_Lain.Focus()
    End Sub

    Private Sub Txt_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Param_Lain.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub Cb_Hari_Ini_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Hari_Ini.CheckedChanged
        If Cb_Hari_Ini.Checked = True Then
            Cb_Tanggal.Checked = False
            Btn_Cari_Click(Cb_Hari_Ini, e)
        End If
    End Sub

    Private Sub Cb_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Tanggal.CheckedChanged
        If Cb_Tanggal.Checked Then
            Cmb_Tanggal.Enabled = True : Tgl_1.Enabled = True : Tgl_2.Enabled = True
            Cb_Hari_Ini.Checked = False
        Else
            Cmb_Tanggal.Enabled = False : Tgl_1.Enabled = False : Tgl_2.Enabled = False
            Cmb_Tanggal.SelectedIndex = -1 : Tgl_1.Value = Now.Date : Tgl_2.Value = Now.Date
        End If
    End Sub

    Private Sub Cb_Param_Lain_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_Param_Lain.CheckedChanged
        If Cb_Param_Lain.Checked Then
            Cmb_Param_Lain.Enabled = True : Txt_Param_Lain.Enabled = True
        Else
            Cmb_Param_Lain.Enabled = False : Txt_Param_Lain.Enabled = False
            Cmb_Param_Lain.SelectedIndex = -1 : Txt_Param_Lain.Text = ""
        End If
    End Sub



    '====================================================================================================================================================================================
    '=     UTILITY
    '====================================================================================================================================================================================
    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub

    Private Sub Lv_Parent_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Parent.DrawColumnHeader
        ' Background gradient
        Using bgBrush As New Drawing2D.LinearGradientBrush(
            e.Bounds,
            Color.FromArgb(245, 245, 245),
            Color.FromArgb(220, 220, 220),
            Drawing2D.LinearGradientMode.Vertical)

            e.Graphics.FillRectangle(bgBrush, e.Bounds)
        End Using

        ' Border bawah (lebih modern dari full border)
        Using borderPen As New Pen(Color.FromArgb(180, 180, 180))
            e.Graphics.DrawLine(
                borderPen,
                e.Bounds.Left,
                e.Bounds.Bottom - 1,
                e.Bounds.Right,
                e.Bounds.Bottom - 1)
        End Using

        ' Teks header
        Using sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            sf.Trimming = StringTrimming.EllipsisCharacter

            ' Padding teks
            Dim textRect As Rectangle = Rectangle.Inflate(e.Bounds, -4, -2)

            e.Graphics.DrawString(
                e.Header.Text,
                Lv_Parent.Font,
                Brushes.Black,
                textRect,
                sf)
        End Using

    End Sub

    Private Sub Lv_Parent_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Parent.DrawSubItem
        Dim sf As New StringFormat()
        sf.LineAlignment = StringAlignment.Center

        If BodyAlignments.ContainsKey(e.ColumnIndex) Then
            sf.Alignment = BodyAlignments(e.ColumnIndex)
        Else
            sf.Alignment = StringAlignment.Near
        End If

        Dim status As String = CStr(e.Item.Tag)

        Dim bgBrush As Brush = Brushes.White
        Dim fgBrush As Brush = Brushes.Black

        If status = "Y" Then
            bgBrush = Brushes.DarkRed
            fgBrush = Brushes.White
        End If

        If e.Item.Selected Then
            bgBrush = SystemBrushes.Highlight
            fgBrush = SystemBrushes.HighlightText
        End If

        e.Graphics.FillRectangle(bgBrush, e.Bounds)
        e.Graphics.DrawString(e.SubItem.Text, Lv_Parent.Font, fgBrush, e.Bounds, sf)

        sf.Dispose()
    End Sub


    Private Sub Lv_Detail_RM_DrawColumnHeader(sender As Object, e As DrawListViewColumnHeaderEventArgs) Handles Lv_Detail_RM.DrawColumnHeader
        ' Background gradient
        Using bgBrush As New Drawing2D.LinearGradientBrush(
            e.Bounds,
            Color.FromArgb(245, 245, 245),
            Color.FromArgb(220, 220, 220),
            Drawing2D.LinearGradientMode.Vertical)

            e.Graphics.FillRectangle(bgBrush, e.Bounds)
        End Using

        ' Border bawah (lebih modern dari full border)
        Using borderPen As New Pen(Color.FromArgb(180, 180, 180))
            e.Graphics.DrawLine(
                borderPen,
                e.Bounds.Left,
                e.Bounds.Bottom - 1,
                e.Bounds.Right,
                e.Bounds.Bottom - 1)
        End Using

        ' Teks header
        Using sf As New StringFormat()
            sf.Alignment = StringAlignment.Center
            sf.LineAlignment = StringAlignment.Center
            sf.Trimming = StringTrimming.EllipsisCharacter

            ' Padding teks
            Dim textRect As Rectangle = Rectangle.Inflate(e.Bounds, -4, -2)

            e.Graphics.DrawString(
                e.Header.Text,
                Lv_Detail_RM.Font,
                Brushes.Black,
                textRect,
                sf)
        End Using
    End Sub

    Private Sub Lv_Detail_RM_DrawSubItem(sender As Object, e As DrawListViewSubItemEventArgs) Handles Lv_Detail_RM.DrawSubItem
        Dim sf As New StringFormat()
        sf.LineAlignment = StringAlignment.Center

        If BodyAlignments2.ContainsKey(e.ColumnIndex) Then
            sf.Alignment = BodyAlignments2(e.ColumnIndex)
        Else
            sf.Alignment = StringAlignment.Near
        End If

        Dim status As String = CStr(e.Item.Tag)

        Dim bgBrush As Brush = Brushes.White
        Dim fgBrush As Brush = Brushes.Black

        If status = "Y" Then
            bgBrush = Brushes.DarkRed
            fgBrush = Brushes.White
        End If

        If e.Item.Selected Then
            bgBrush = SystemBrushes.Highlight
            fgBrush = SystemBrushes.HighlightText
        End If

        e.Graphics.FillRectangle(bgBrush, e.Bounds)
        e.Graphics.DrawString(e.SubItem.Text, Lv_Detail_RM.Font, fgBrush, e.Bounds, sf)

        sf.Dispose()
    End Sub


    Private Function Generate_QR_NoPadding(ByVal isi As String)

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