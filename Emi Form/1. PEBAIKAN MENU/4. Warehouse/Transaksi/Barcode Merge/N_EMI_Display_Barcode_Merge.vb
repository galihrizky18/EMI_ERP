Imports System.IO

Public Class N_EMI_Display_Barcode_Merge

    Dim arrFilterLokasiGudang, arrFilterTanggal, arrFilterParamLain As New ArrayList

    Dim Random As New Random()

    Dim LvParent_NoFaktur, LvParent_Tanggal, LvParent_Jam, LvParent_KdSo, LvParent_Keterangan, LvParent_User As String

    Dim item_NoFaktur As Integer = 0
    Dim item_Tanggal As Integer = 1
    Dim item_Jam As Integer = 2
    Dim item_KdSo As Integer = 3
    Dim item_Keterangan As Integer = 4
    Dim item_User As Integer = 5

    Dim Lv2_Barcode_Kosong, Lv2_Barcode_NoBarcode, Lv2_Barcode_KodeBarang, Lv2_Barcode_NamaBarang, Lv2_Barcode_Barcode, Lv2_Barcode_Total, Lv2_Barcode_Satuan, Lv2_Barcode_Urut As String

    Dim item2_Kosong As Integer = 0
    Dim item2_NoBarcode As Integer = 1
    Dim item2_KodeBarang As Integer = 2
    Dim item2_NamaBarang As Integer = 3
    Dim item2_Barcode As Integer = 4
    Dim item2_Total As Integer = 5
    Dim item2_Satuan As Integer = 6
    Dim item2_Urut As Integer = 7

    Dim Lv3_BarcodeAwal, Lv3_BatchAwal, Lv3_BatchAkhir, Lv3_Jumlah, Lv3_Satuan As String

    Dim item_3_BarcodeAwal As Integer = 0
    Dim item_3_BatchAwal As Integer = 1
    Dim item_3_BatchAkhir As Integer = 2
    Dim item_3_Jumlah As Integer = 3
    Dim item_3_Satuan As Integer = 4



    Private Sub N_EMI_Display_Barcode_Merge_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Parent.Columns.Clear()
        Lv_Parent.Columns.Add("No Faktur", 170, HorizontalAlignment.Left) '0
        Lv_Parent.Columns.Add("Tanggal", 130, HorizontalAlignment.Center) '1
        Lv_Parent.Columns.Add("Jam", 100, HorizontalAlignment.Center) '2
        Lv_Parent.Columns.Add("Lokasi Barcode", 180, HorizontalAlignment.Left) '3
        Lv_Parent.Columns.Add("Keterangan", 400, HorizontalAlignment.Left) '4
        Lv_Parent.Columns.Add("User", 150, HorizontalAlignment.Left) '5
        Lv_Parent.View = View.Details

        Lv_Barcode_Merge.Columns.Clear()
        Lv_Barcode_Merge.Columns.Add("", 0, HorizontalAlignment.Center) '0
        Lv_Barcode_Merge.Columns.Add("No", 50, HorizontalAlignment.Center) '1
        Lv_Barcode_Merge.Columns.Add("Kode Barang", 110, HorizontalAlignment.Left) '2
        Lv_Barcode_Merge.Columns.Add("Nama Barang", 250, HorizontalAlignment.Left) '3
        Lv_Barcode_Merge.Columns.Add("Barcode", 250, HorizontalAlignment.Left) '4
        Lv_Barcode_Merge.Columns.Add("Total", 130, HorizontalAlignment.Right) '5
        Lv_Barcode_Merge.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '6
        'Hide
        Lv_Barcode_Merge.Columns.Add("Urut", 0, HorizontalAlignment.Center) '7
        Lv_Barcode_Merge.View = View.Details

        Lv_Detail_Barcode_Merge.Columns.Clear()
        Lv_Detail_Barcode_Merge.Columns.Add("Barcode Awal", 230, HorizontalAlignment.Left) '0
        Lv_Detail_Barcode_Merge.Columns.Add("Batch Awal", 150, HorizontalAlignment.Left) '1
        Lv_Detail_Barcode_Merge.Columns.Add("Batch Akhir", 150, HorizontalAlignment.Left) '2
        Lv_Detail_Barcode_Merge.Columns.Add("Jumlah", 120, HorizontalAlignment.Right) '3
        Lv_Detail_Barcode_Merge.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '4
        Lv_Detail_Barcode_Merge.View = View.Details




        Try
            OpenConn()

            '===========================================
            '=     LOAD COMBO FILTER LOKASI GUDANG     =
            '===========================================
            Cmb_Lokasi.Items.Clear() : arrFilterLokasiGudang.Clear()
            Cmb_Lokasi.Items.Add(OpsiSeluruh) : arrFilterLokasiGudang.Add(OpsiSeluruh)
            SQL = "select Kode_Stock_Owner, Keterangan "
            SQL &= $"from Stock_Owner_Gudang "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"order by Kode_Stock_Owner "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("Keterangan")) : arrFilterLokasiGudang.Add(Dr("Kode_Stock_Owner"))
                Loop
            End Using

            If Cmb_Lokasi.Items.Count = 0 Then
                CloseConn()
                MessageBox.Show("Terjadi kesalahan, data gudang tidak ditemukan. Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_Tanggal.Items.Clear() : arrFilterTanggal.Clear()
        Cmb_Tanggal.Items.Add("Tanggal Transaksi") : arrFilterTanggal.Add("a.Tanggal")

        Cmb_Param_Lain.Items.Clear() : arrFilterParamLain.Clear()
        Cmb_Param_Lain.Items.Add("No Faktur") : arrFilterParamLain.Add("a.No_Faktur")
        Cmb_Param_Lain.Items.Add("Keterangan") : arrFilterParamLain.Add("a.Keterangan")
        Cmb_Param_Lain.Items.Add("User") : arrFilterParamLain.Add("a.UserID")




        Kosong()

    End Sub



    Private Sub Kosong()

        Cmb_Lokasi.SelectedIndex = 0
        Cb_Hari_Ini.Checked = False
        Cb_Tanggal.Checked = False
        Cb_Param_Lain.Checked = False


        Load_Lv_1()

    End Sub



    Private Sub Get_Lv_1(ByVal index As Integer)
        LvParent_NoFaktur = Lv_Parent.Items(index).SubItems(item_NoFaktur).Text
        LvParent_Tanggal = Lv_Parent.Items(index).SubItems(item_Tanggal).Text
        LvParent_Jam = Lv_Parent.Items(index).SubItems(item_Jam).Text
        LvParent_KdSo = Lv_Parent.Items(index).SubItems(item_KdSo).Text
        LvParent_Keterangan = Lv_Parent.Items(index).SubItems(item_Keterangan).Text
        LvParent_User = Lv_Parent.Items(index).SubItems(item_User).Text
    End Sub



    Private Sub Get_Lv_2(ByVal index As Integer)
        Lv2_Barcode_Kosong = Lv_Barcode_Merge.Items(index).SubItems(item2_Kosong).Text
        Lv2_Barcode_NoBarcode = Lv_Barcode_Merge.Items(index).SubItems(item2_NoBarcode).Text
        Lv2_Barcode_KodeBarang = Lv_Barcode_Merge.Items(index).SubItems(item2_KodeBarang).Text
        Lv2_Barcode_NamaBarang = Lv_Barcode_Merge.Items(index).SubItems(item2_NamaBarang).Text
        Lv2_Barcode_Barcode = Lv_Barcode_Merge.Items(index).SubItems(item2_Barcode).Text
        Lv2_Barcode_Total = Lv_Barcode_Merge.Items(index).SubItems(item2_Total).Text
        Lv2_Barcode_Satuan = Lv_Barcode_Merge.Items(index).SubItems(item2_Satuan).Text
        Lv2_Barcode_Urut = Lv_Barcode_Merge.Items(index).SubItems(item2_Urut).Text
    End Sub



    Private Sub Get_Lv_3(ByVal index As Integer)
        Lv3_BarcodeAwal = Lv_Detail_Barcode_Merge.Items(index).SubItems(item_3_BarcodeAwal).Text
        Lv3_BatchAwal = Lv_Detail_Barcode_Merge.Items(index).SubItems(item_3_BatchAwal).Text
        Lv3_BatchAkhir = Lv_Detail_Barcode_Merge.Items(index).SubItems(item_3_BatchAkhir).Text
        Lv3_Jumlah = Lv_Detail_Barcode_Merge.Items(index).SubItems(item_3_Jumlah).Text
        Lv3_Satuan = Lv_Detail_Barcode_Merge.Items(index).SubItems(item_3_Satuan).Text
    End Sub



    Private Sub Load_Lv_1()

        Try
            OpenConn()

            Lv_Parent.Items.Clear() : Lv_Barcode_Merge.Items.Clear() : Lv_Detail_Barcode_Merge.Items.Clear()
            SQL = "select a.No_Faktur, a.Tanggal, a.Jam, a.Kode_Stock_Owner, a.Keterangan, a.UserID, a.status "
            SQL &= $"from N_EMI_Transaksi_Barcode_Merge a "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "

            If Cmb_Lokasi.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "
                SQL &= $" a.Kode_Stock_Owner = '{arrFilterLokasiGudang.Item(Cmb_Lokasi.SelectedIndex)}' "
            End If

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

            SQL &= $"order by a.tanggal, a.jam, a.No_Faktur "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Parent.Items.Add(Dr("No_Faktur")) '0
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy")) '1
                    Lv.SubItems.Add(Dr("Jam")) '2
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner")) '3
                    Lv.SubItems.Add(Dr("Keterangan")) '4
                    Lv.SubItems.Add(Dr("UserID")) '5

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
                    Else
                        Lv.BackColor = Color.White
                        Lv.ForeColor = Color.Black
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



    Private Sub Lv_Parent_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Parent.SelectedIndexChanged
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Parent.FocusedItem.SubItems(item_NoFaktur).Text

            Lv_Barcode_Merge.Items.Clear() : Lv_Detail_Barcode_Merge.Items.Clear()
            SQL = "select b.Nomor_Barcode, b.Kode_Barang, d.Nama as Nama_Barang, (c.Qr_Code+'-'+c.Kode_Unik_Berjalan) as Barcode, b.Total, b.Satuan, b.Urut_Oto, b.status "
            SQL &= $"from N_EMI_Transaksi_Barcode_Merge a "
            SQL &= $"inner join N_EMI_Transaksi_Barcode_Merge_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join Barang_SN c on b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Qr_Code = c.Qr_Code and b.Kode_Unik_Berjalan = c.Kode_Unik_Berjalan "
            SQL &= $"inner join barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur.Trim}' "
            SQL &= $"group by b.Nomor_Barcode, b.Kode_Barang, d.Nama, (c.Qr_Code+'-'+c.Kode_Unik_Berjalan), b.Total, b.Satuan, a.tanggal, a.Jam, a.No_Faktur, b.Urut_Oto, b.status "
            SQL &= $"order by b.Nomor_Barcode, a.No_Faktur"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Barcode_Merge.Items.Add("") '0
                    Lv.SubItems.Add(Dr("Nomor_Barcode")) '1
                    Lv.SubItems.Add(Dr("Kode_Barang")) '2
                    Lv.SubItems.Add(Dr("Nama_Barang")) '3
                    Lv.SubItems.Add(Dr("Barcode")) '4
                    Lv.SubItems.Add(Format(Dr("Total"), "N4")) '5
                    Lv.SubItems.Add(Dr("Satuan")) '6
                    Lv.SubItems.Add(Dr("Urut_Oto")) '7

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Lv.BackColor = Color.DarkRed
                        Lv.ForeColor = Color.White
                    Else
                        Lv.BackColor = Color.White
                        Lv.ForeColor = Color.Black
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



    Private Sub Lv_Barcode_Merge_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Barcode_Merge.SelectedIndexChanged
        If Lv_Barcode_Merge.Items.Count = 0 Or Lv_Barcode_Merge.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()

            Dim SelectedFaktur As String = Lv_Parent.FocusedItem.SubItems(item_NoFaktur).Text
            Dim SelectedUrutDetail As String = Lv_Barcode_Merge.FocusedItem.SubItems(item2_Urut).Text

            Lv_Detail_Barcode_Merge.Items.Clear()
            SQL = "select (d.Qr_Code+'-'+d.Kode_Unik_Berjalan) as Barcode_Awal, (e.Qr_Code+'-'+e.Kode_Unik_Berjalan) as Barcode_Akhir, "
            SQL &= $"c.Batch_Number_Awal, c.Batch_Number_Akhir, c.Jumlah, c.Satuan "
            SQL &= $"from N_EMI_Transaksi_Barcode_Merge a "
            SQL &= $"inner join N_EMI_Transaksi_Barcode_Merge_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join N_EMI_Transaksi_Barcode_Merge_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail "
            SQL &= $"inner join barang_sn d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Serial_Number_Awal = d.Serial_Number "
            SQL &= $"inner join barang_sn e on c.Kode_Perusahaan = e.Kode_Perusahaan and c.Serial_Number_Akhir = e.Serial_Number "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            SQL &= $"and b.Urut_Oto = '{SelectedUrutDetail}' "
            SQL &= $"order by a.tanggal, a.jam, a.No_Faktur "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail_Barcode_Merge.Items.Add(Dr("Barcode_Awal")) '0
                    Lv.SubItems.Add(Dr("Batch_Number_Awal")) '1
                    Lv.SubItems.Add(Dr("Batch_Number_Akhir")) '2
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N4")) '3
                    Lv.SubItems.Add(Dr("Satuan")) '4
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub





    '=================================================================================================================================================================================
    '=     HANDLE FILTER
    '=================================================================================================================================================================================
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

        Load_Lv_1()
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



    '=============================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '=============================================================================================================================================================================
    Private Sub Cmb_Lokasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
        If e.KeyChar = Chr(13) Then Cb_Hari_Ini.Focus()
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
    Private Sub Cmb_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then Tgl_1.Focus()
    End Sub
    Private Sub Tgl_1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl_2.Focus()
    End Sub
    Private Sub Tgl_2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl_2.KeyPress
        If e.KeyChar = Chr(13) Then Cb_Param_Lain.Focus()
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

    Private Sub Cmb_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Param_Lain.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Param_Lain.Focus()
    End Sub
    Private Sub Txt_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Param_Lain.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub



    '=================================================================================================================================================================================
    '=     HANDLE CONTEXT MENU
    '=================================================================================================================================================================================


    Private Sub BatalkanTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanTransaksiToolStripMenuItem.Click
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then Exit Sub

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim SelectedFaktur As String = Lv_Parent.FocusedItem.SubItems(item_NoFaktur).Text

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Transaksi_Barcode_Merge") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Pembatalan Penggabungan Barcode", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If MessageBox.Show($"Yakin Ingin Membatalkan Transaksi Penggabungan Barcode {SelectedFaktur}?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub



            '============================================================
            '=     CEK APAKAH TRANSAKSI SUDAH DIBATALKAN SEBELUMNYA     =
            '============================================================
            SQL = "select Kode_Perusahaan, status from N_EMI_Transaksi_Barcode_Merge "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and no_faktur = '{SelectedFaktur}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Proses pembatalan tidak dapat dilanjutkan karena No Faktur {SelectedFaktur} sudah dibatalkan sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Proses pembatalan tidak dapat dilanjutkan karena No Faktur {SelectedFaktur} tidak ada. Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===========================================================
            '=     CEK APAKAH DATA SN PADA BARCODE SUDAH DIGUNAKAN     =
            '===========================================================
            SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, b.Qr_Code, b.Kode_Unik_Berjalan, c.Serial_Number_Awal, "
            SQL &= $"c.Serial_Number_Akhir, c.Batch_Number_Awal, c.Batch_Number_Akhir, c.Jumlah, c.Satuan, c.Urut_Oto, b.Urut_Oto as Urut_Detail "
            SQL &= $"from N_EMI_Transaksi_Barcode_Merge a "
            SQL &= $"inner join N_EMI_Transaksi_Barcode_Merge_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join N_EMI_Transaksi_Barcode_Merge_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            SQL &= $"and b.Status is null "
            SQL &= $"and a.status is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim KdSO As String = .Rows(i).Item("Kode_Stock_Owner")
                            Dim KdBarang As String = .Rows(i).Item("Kode_Barang")
                            Dim SnAwal As String = .Rows(i).Item("Serial_Number_Awal")
                            Dim SnAkhir As String = .Rows(i).Item("Serial_Number_Akhir")
                            Dim BatchAwal As String = .Rows(i).Item("Batch_Number_Awal")
                            Dim batchAkhir As String = .Rows(i).Item("Batch_Number_Akhir")
                            Dim Jumlah As Double = Val(HilangkanTanda(.Rows(i).Item("Jumlah")))
                            Dim Satuan As String = .Rows(i).Item("Satuan")
                            Dim UrutDetail As String = .Rows(i).Item("Urut_Detail")
                            Dim UrutDet As String = .Rows(i).Item("Urut_Oto")

                            '===================================
                            '=     CEK JUMLAH STOCK PER SN     =
                            '===================================
                            SQL = "select Jumlah, (Qr_Code+'-'+Kode_Unik_Berjalan) as Barcode from Barang_SN "
                            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                            SQL &= $"and kode_barang = '{KdBarang}' "
                            SQL &= $"and Serial_Number = '{SnAkhir}' "
                            ' SQL &= $"and Batch_Number = '{batchAkhir}' "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                    Dim StockRealSn As Double = Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")))
                                    Dim BarcodeSn As String = Ds2.Tables("MyTable").Rows(0).Item("Barcode")

                                    If Jumlah <> StockRealSn Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"Proses tidak dapat dilanjutkan karena stock pada barcode {BarcodeSn} sudah digunakan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If


                                    '============================
                                    '=     LAKUKAN ROLLBACK     =
                                    '============================
                                    ' POTONG STOCK SN AKHIR
                                    SQL = $"update Barang_SN set jumlah = Round((jumlah - {Val(HilangkanTanda(Jumlah))}), 4) "
                                    SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                                    SQL &= $"and kode_barang = '{KdBarang}' "
                                    SQL &= $"and Serial_Number = '{SnAkhir}' "
                                    'SQL &= $"and Batch_Number = '{batchAkhir}' "
                                    ExecuteTrans(SQL)

                                    SQL = $"update barang set Good_Stock = ROUND((Good_Stock - {Val(HilangkanTanda(Jumlah))}), 4) "
                                    SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                                    SQL &= $"and kode_barang = '{KdBarang}' "
                                    ExecuteTrans(SQL)


                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show($"Data Barang SN Akhir pada urut Det {UrutDet} tidak ditemukan. Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            ' CEK KESESUAIAN STOCK SN AKHIR
                            SQL = "select Jumlah from Barang_SN "
                            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                            SQL &= $"and kode_barang = '{KdBarang}' "
                            SQL &= $"and Serial_Number = '{SnAkhir}' "
                            'SQL &= $"and Batch_Number = '{batchAkhir}' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(HilangkanTanda(Dr("Jumlah"))) <> 0 Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"Proses pembatalan tidak dapat dilanjutkan karena stock pada barcode akhir tidak nol (0). Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 4) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSO & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds9 = BindingTrans(SQL)
                                With Ds9.Tables("MyTable")
                                    If Ds9.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds9.Tables("MyTable").Rows(0).Item("good_stock") <> Ds9.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show($"Terjadi Kesalahan . . ! ! {vbCrLf} Stock Kode Barang {KdBarang} Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"Data Kode barang {KdBarang} tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End With
                            End Using


                            '=======================================
                            '=     KEMBALIKAN STOCK KE SN AWAL     =
                            '=======================================
                            SQL = "select Jumlah, (Qr_Code+'-'+Kode_Unik_Berjalan) as Barcode from Barang_SN "
                            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                            SQL &= $"and kode_barang = '{KdBarang}' "
                            SQL &= $"and Serial_Number = '{SnAwal}' "
                            'SQL &= $"and Batch_Number = '{BatchAwal}' "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                    Dim BarcodeSn As String = Ds2.Tables("MyTable").Rows(0).Item("Barcode")

                                    '============================
                                    '=     LAKUKAN ROLLBACK     =
                                    '============================
                                    ' TAMBAH STOCK SN AWAL
                                    SQL = $"update Barang_SN set jumlah = Round((jumlah + {Val(HilangkanTanda(Jumlah))}), 4) "
                                    SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                                    SQL &= $"and kode_barang = '{KdBarang}' "
                                    SQL &= $"and Serial_Number = '{SnAwal}' "
                                    'SQL &= $"and Batch_Number = '{BatchAwal}' "
                                    ExecuteTrans(SQL)

                                    SQL = $"update barang set Good_Stock = ROUND((Good_Stock + {Val(HilangkanTanda(Jumlah))}), 4) "
                                    SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                                    SQL &= $"and kode_barang = '{KdBarang}' "
                                    ExecuteTrans(SQL)


                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show($"Data Barang SN Awal pada urut Det {UrutDet} tidak ditemukan. Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 4) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSO & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds9 = BindingTrans(SQL)
                                With Ds9.Tables("MyTable")
                                    If Ds9.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds9.Tables("MyTable").Rows(0).Item("good_stock") <> Ds9.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show($"Terjadi Kesalahan . . ! ! {vbCrLf} Stock Kode Barang {KdBarang} Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"Data Kode barang {KdBarang} tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End With
                            End Using

                            '====================================
                            '=       UPDATE STATUS DETAIL       =
                            '====================================
                            SQL = "update N_EMI_Transaksi_Barcode_Merge_Detail set Status = 'Y', "
                            SQL &= $"UserID_Batal = '{UserID}', Tanggal_Batal = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Batal = '{Format(tgl_skg, "HH:mm:ss")}' "
                            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and status is NULL "
                            SQL &= $"and No_Faktur = '{SelectedFaktur}' "
                            SQL &= $"and Urut_Oto = '{UrutDetail}' "
                            ExecuteTrans(SQL)


                        Next

                    End If
                End With
            End Using

            '=======================================
            '=       UPDATE STATUS TRANSAKSI       =
            '=======================================
            SQL = "update N_EMI_Transaksi_Barcode_Merge set status = 'Y', "
            SQL &= $"UserID_Batal = '{UserID}', Tanggal_Batal = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Batal = '{Format(tgl_skg, "HH:mm:ss")}' "
            SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
            SQL &= $"and no_Faktur = '{SelectedFaktur}' "
            SQL &= $"and status is null "
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseTrans()
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

    Private Sub BatalkanBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanBarcodeToolStripMenuItem.Click
        If Lv_Barcode_Merge.Items.Count = 0 Or Lv_Barcode_Merge.FocusedItem Is Nothing Then Exit Sub

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim SelectedFaktur As String = Lv_Parent.FocusedItem.SubItems(item_NoFaktur).Text
            Dim SelectedUrutDetail As String = Lv_Barcode_Merge.FocusedItem.SubItems(item2_Urut).Text
            Dim selectedBarcode As String = Lv_Barcode_Merge.FocusedItem.SubItems(item2_Barcode).Text

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Transaksi_Barcode_Merge_Per_Barcode") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Pembatalan Penggabungan Barcode", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If MessageBox.Show($"Yakin Ingin Membatalkan Penggabungan Barcode {selectedBarcode}?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question) = vbNo Then Exit Sub



            '============================================================
            '=     CEK APAKAH TRANSAKSI SUDAH DIBATALKAN SEBELUMNYA     =
            '============================================================
            SQL = "select Kode_Perusahaan, status from N_EMI_Transaksi_Barcode_Merge_Detail "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and no_faktur = '{SelectedFaktur}' "
            SQL &= $"and Urut_Oto = '{SelectedUrutDetail}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Proses pembatalan tidak dapat dilanjutkan karena Barcoode {selectedBarcode} sudah dibatalkan sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Proses pembatalan tidak dapat dilanjutkan karena Barcode {selectedBarcode} tidak ditemukan. Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===========================================================
            '=     CEK APAKAH DATA SN PADA BARCODE SUDAH DIGUNAKAN     =
            '===========================================================
            SQL = "select a.No_Faktur, a.Kode_Stock_Owner, b.Kode_Barang, b.Qr_Code, b.Kode_Unik_Berjalan, c.Serial_Number_Awal, "
            SQL &= $"c.Serial_Number_Akhir, c.Batch_Number_Awal, c.Batch_Number_Akhir, c.Jumlah, c.Satuan, c.Urut_Oto, b.Urut_Oto as Urut_Detail "
            SQL &= $"from N_EMI_Transaksi_Barcode_Merge a "
            SQL &= $"inner join N_EMI_Transaksi_Barcode_Merge_Detail b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur "
            SQL &= $"inner join N_EMI_Transaksi_Barcode_Merge_Det c on b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Faktur = c.No_Faktur and b.Urut_Oto = c.Urut_Detail "
            SQL &= $"where a.Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and a.No_Faktur = '{SelectedFaktur}' "
            SQL &= $"and b.Urut_Oto = '{SelectedUrutDetail}' "
            SQL &= $"and b.Status is null "
            SQL &= $"and a.status is null "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim KdSO As String = .Rows(i).Item("Kode_Stock_Owner")
                            Dim KdBarang As String = .Rows(i).Item("Kode_Barang")
                            Dim SnAwal As String = .Rows(i).Item("Serial_Number_Awal")
                            Dim SnAkhir As String = .Rows(i).Item("Serial_Number_Akhir")
                            Dim BatchAwal As String = .Rows(i).Item("Batch_Number_Awal")
                            Dim batchAkhir As String = .Rows(i).Item("Batch_Number_Akhir")
                            Dim Jumlah As Double = Val(HilangkanTanda(.Rows(i).Item("Jumlah")))
                            Dim Satuan As String = .Rows(i).Item("Satuan")
                            Dim UrutDet As String = .Rows(i).Item("Urut_Oto")

                            '===================================
                            '=     CEK JUMLAH STOCK PER SN     =
                            '===================================
                            SQL = "select Jumlah, (Qr_Code+'-'+Kode_Unik_Berjalan) as Barcode from Barang_SN "
                            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                            SQL &= $"and kode_barang = '{KdBarang}' "
                            SQL &= $"and Serial_Number = '{SnAkhir}' "
                            'SQL &= $"and Batch_Number = '{batchAkhir}' "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                    Dim StockRealSn As Double = Val(HilangkanTanda(Ds2.Tables("MyTable").Rows(0).Item("Jumlah")))
                                    Dim BarcodeSn As String = Ds2.Tables("MyTable").Rows(0).Item("Barcode")

                                    If Jumlah <> StockRealSn Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"Proses tidak dapat dilanjutkan karena stock pada barcode {BarcodeSn} sudah digunakan ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If


                                    '============================
                                    '=     LAKUKAN ROLLBACK     =
                                    '============================
                                    ' POTONG STOCK SN AKHIR
                                    SQL = $"update Barang_SN set jumlah = Round((jumlah - {Val(HilangkanTanda(Jumlah))}), 4) "
                                    SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                                    SQL &= $"and kode_barang = '{KdBarang}' "
                                    SQL &= $"and Serial_Number = '{SnAkhir}' "
                                    'SQL &= $"and Batch_Number = '{batchAkhir}' "
                                    ExecuteTrans(SQL)

                                    SQL = $"update barang set Good_Stock = ROUND((Good_Stock - {Val(HilangkanTanda(Jumlah))}), 4) "
                                    SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                                    SQL &= $"and kode_barang = '{KdBarang}' "
                                    ExecuteTrans(SQL)


                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show($"Data Barang SN Akhir pada urut Det {UrutDet} tidak ditemukan. Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            ' CEK KESESUAIAN STOCK SN AKHIR
                            SQL = "select Jumlah from Barang_SN "
                            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                            SQL &= $"and kode_barang = '{KdBarang}' "
                            SQL &= $"and Serial_Number = '{SnAkhir}' "
                            ' SQL &= $"and Batch_Number = '{batchAkhir}' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(HilangkanTanda(Dr("Jumlah"))) <> 0 Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"Proses pembatalan tidak dapat dilanjutkan karena stock pada barcode akhir tidak nol (0). Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 4) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSO & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds9 = BindingTrans(SQL)
                                With Ds9.Tables("MyTable")
                                    If Ds9.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds9.Tables("MyTable").Rows(0).Item("good_stock") <> Ds9.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show($"Terjadi Kesalahan . . ! ! {vbCrLf} Stock Kode Barang {KdBarang} Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"Data Kode barang {KdBarang} tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End With
                            End Using


                            '=======================================
                            '=     KEMBALIKAN STOCK KE SN AWAL     =
                            '=======================================
                            SQL = "select Jumlah, (Qr_Code+'-'+Kode_Unik_Berjalan) as Barcode from Barang_SN "
                            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                            SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                            SQL &= $"and kode_barang = '{KdBarang}' "
                            SQL &= $"and Serial_Number = '{SnAwal}' "
                            'SQL &= $"and Batch_Number = '{BatchAwal}' "
                            Using Ds2 = BindingTrans(SQL)
                                If Ds2.Tables("MyTable").Rows.Count <> 0 Then

                                    Dim BarcodeSn As String = Ds2.Tables("MyTable").Rows(0).Item("Barcode")

                                    '============================
                                    '=     LAKUKAN ROLLBACK     =
                                    '============================
                                    ' TAMBAH STOCK SN AWAL
                                    SQL = $"update Barang_SN set jumlah = Round((jumlah + {Val(HilangkanTanda(Jumlah))}), 4) "
                                    SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                                    SQL &= $"and kode_barang = '{KdBarang}' "
                                    SQL &= $"and Serial_Number = '{SnAwal}' "
                                    SQL &= $"and Batch_Number = '{BatchAwal}' "
                                    ExecuteTrans(SQL)

                                    SQL = $"update barang set Good_Stock = ROUND((Good_Stock + {Val(HilangkanTanda(Jumlah))}), 4) "
                                    SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
                                    SQL &= $"and Kode_Stock_Owner = '{KdSO}' "
                                    SQL &= $"and kode_barang = '{KdBarang}' "
                                    ExecuteTrans(SQL)


                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show($"Data Barang SN Awal pada urut Det {UrutDet} tidak ditemukan. Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using


                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),4) AS good_stock, isnull((select round(sum(jumlah),4) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 4), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 4) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSO & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds9 = BindingTrans(SQL)
                                With Ds9.Tables("MyTable")
                                    If Ds9.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds9.Tables("MyTable").Rows(0).Item("good_stock") <> Ds9.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds9.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show($"Terjadi Kesalahan . . ! ! {vbCrLf} Stock Kode Barang {KdBarang} Tidak Sesuai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"Data Kode barang {KdBarang} tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End With
                            End Using




                        Next

                    End If
                End With
            End Using

            '====================================
            '=       UPDATE STATUS DETAIL       =
            '====================================
            SQL = "update N_EMI_Transaksi_Barcode_Merge_Detail set Status = 'Y', "
            SQL &= $"UserID_Batal = '{UserID}', Tanggal_Batal = '{Format(tgl_skg, "yyyy-MM-dd")}', Jam_Batal = '{Format(tgl_skg, "HH:mm:ss")}' "
            SQL &= $"where Kode_Perusahaan = '{KodePerusahaan}' "
            SQL &= $"and status is NULL "
            SQL &= $"and No_Faktur = '{SelectedFaktur}' "
            SQL &= $"and Urut_Oto = '{SelectedUrutDetail}' "
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show($"Barcode {selectedBarcode} Berhasil Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
    End Sub

    Private Sub CetakUlangBarcodeMergeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeMergeToolStripMenuItem.Click
        If Lv_Barcode_Merge.Items.Count = 0 Or Lv_Barcode_Merge.FocusedItem Is Nothing Then Exit Sub


        Dim kode_unik_print As String = ""

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim SelectedFaktur As String = Lv_Parent.FocusedItem.SubItems(item_NoFaktur).Text
            Dim SelectedKdSo As String = Lv_Parent.FocusedItem.SubItems(item_KdSo).Text
            Dim SelectedUrutDetail As String = Lv_Barcode_Merge.FocusedItem.SubItems(item2_Urut).Text
            Dim selectedBarcode As String = Lv_Barcode_Merge.FocusedItem.SubItems(item2_Barcode).Text
            Dim SelectedKdBarang As String = Lv_Barcode_Merge.FocusedItem.SubItems(item2_KodeBarang).Text
            Dim SelectedNmBarang As String = Lv_Barcode_Merge.FocusedItem.SubItems(item2_NamaBarang).Text
            Dim SelectedNoBarcode As String = Lv_Barcode_Merge.FocusedItem.SubItems(item2_NoBarcode).Text
            Dim Selectedjumlah As Double = Val(HilangkanTanda(Lv_Barcode_Merge.FocusedItem.SubItems(item2_Total).Text))
            Dim SelectedSatuan As String = Lv_Barcode_Merge.FocusedItem.SubItems(item2_Satuan).Text

            '=========================================
            '=     CEK APAKAH BARCODE DIBATALKAN     =
            '=========================================
            SQL = "select status "
            SQL &= $"from N_EMI_Transaksi_Barcode_Merge_Detail "
            SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
            SQL &= $"and no_faktur = '{SelectedFaktur}' "
            SQL &= $"and Urut_Oto = '{SelectedUrutDetail}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Proses cetak ulang tidak dapat dilanjutkan karena Barcode {selectedBarcode} sudah dibatalkan sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Proses cetak ulang tidak dapat dilanjutkan karena Barcode {selectedBarcode} tidak ditemukan. Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '===============================
            '=       GET DETAIL DATA       =
            '===============================
            Dim tgl_produksi As Date
            Dim tgl_expired As Date
            Dim tgl_msk As Date
            Dim Batch_Number_Akhir As String = ""
            Dim Qr_Code As String = ""
            SQL = "select (Qr_Code+'-'+Kode_Unik_Berjalan) as Barcode, Kode_Barang,  "
            SQL &= $"Tgl_Produksi, Tgl_Expired, Tgl_Masuk, "
            SQL &= $"Batch_Number, Qr_Code "
            SQL &= $"from Barang_SN "
            SQL &= $"where kode_perusahaan = '{KodePerusahaan}' "
            SQL &= $"and kode_Stock_owner = '{SelectedKdSo}' "
            SQL &= $"and kode_barang = '{SelectedKdBarang}' "
            SQL &= $"and (qr_code+'-'+Kode_Unik_Berjalan) = '{selectedBarcode}' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    tgl_produksi = General_Class.CekNULL(Dr("Tgl_Produksi"))
                    tgl_expired = General_Class.CekNULL(Dr("Tgl_Expired"))
                    tgl_msk = General_Class.CekNULL(Dr("Tgl_Masuk"))
                    Batch_Number_Akhir = Dr("Batch_Number")
                    Qr_Code = Dr("Qr_Code")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Proses cetak ulang tidak dapat dilanjutkan karena Barcode {selectedBarcode} tidak ditemukan di BarangSN. Harap hubungi tim IT", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=====================================
            '=       GENERATE BARCODE BARU       =
            '=====================================
            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(Random.Next(0, 10000), "00000")
            Dim fullNewQr As String = selectedBarcode.Trim

            Cmd.Parameters.Clear()
            Using ImgBarcode1 As Image = Generate_QR(fullNewQr)
                Using ms1 As New MemoryStream()
                    ImgBarcode1.Save(ms1, Imaging.ImageFormat.Jpeg)
                    Dim rawData1 As Byte() = ms1.ToArray()

                    Dim param1 As String = "@newBarcode" & kode_unik_print
                    Cmd.Parameters.Add(param1, SqlDbType.Image).Value = rawData1
                End Using
            End Using

            Dim barcode As String = "@newBarcode" & kode_unik_print


            Dim tglDuaHariSebelum As DateTime = tgl_skg.AddDays(-2)

            SQL = "delete from N_EMI_Cetak_Transaksi_Barcode_Merge where Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Tanggal_Cetak between '" & Format(tglDuaHariSebelum, "yyyy-MM-dd") & "' and '" & Format(tgl_skg, "yyyy-MM-dd") & "' "
            ExecuteTrans(SQL)

            '=============================================
            '=       INSERT KE TABEL CETAK BARCODE       =
            '=============================================
            SQL = "insert into N_EMI_Cetak_Transaksi_Barcode_Merge (Kode_Perusahaan, Barcode, Kode_Barang, NamaBarang, Tgl_Produksi, "
            SQL &= $"Tgl_Expired, Tgl_Masuk, Batch_Number, Jumlah_Input, Satuan_Input, No_Barcode, QrUtuh, "
            SQL &= $"Qr, Tanggal_Cetak, Jam_Cetak, Kode_Unik_Print) "
            SQL &= $"values ('{KodePerusahaan}', {barcode}, '{SelectedKdBarang}', '{SelectedNmBarang}', "
            SQL &= $"'{Format(tgl_produksi, "yyyy-MM-dd")}', '{Format(tgl_expired, "yyyy-MM-dd")}', '{Format(tgl_msk, "yyyy-MM-dd")}', "
            SQL &= $"'{Batch_Number_Akhir}', '{Val(HilangkanTanda(Selectedjumlah))}', '{SelectedSatuan}', '{SelectedNoBarcode}', "
            SQL &= $"'{fullNewQr}', '{Qr_Code}', '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{kode_unik_print}')"
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


        '=========================
        '=     CETAK BARCODE     =
        '=========================

        Try
            OpenConn()

            Dim CrDoc As New Object

            Dim kertasBarcode As String = "BarcodeFG"

            SQL = "select Kode_Perusahaan from N_EMI_Cetak_Transaksi_Barcode_Merge where Kode_Perusahaan='" & KodePerusahaan & "' and kode_unik_print='" & kode_unik_print & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    CrDoc = New N_EMI_CR_Barcode_Transaksi_Barcode_Merge

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Transaksi_Barcode_Merge.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Cetak_Transaksi_Barcode_Merge.kode_unik_print} = '" & kode_unik_print & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = ""
                    '    .Text = ""
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '=================================================================================================
                    '=================================================================================================


                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{N_EMI_Cetak_Transaksi_Barcode_Merge.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Cetak_Transaksi_Barcode_Merge.kode_unik_print} = '" & kode_unik_print & "' "

                    CrDoc.PrintOptions.PrinterName = PrinterBarcode

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    Dim rawKind As Integer
                    Dim isPaperFound As Boolean = False
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertasBarcode Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            isPaperFound = True
                            Exit For
                        End If
                    Next

                    If Not isPaperFound Then
                        'CloseConn()
                        MessageBox.Show("Kertas Tidak DiTemukan, Kertas di set ke default", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        'Exit Sub
                    End If

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

                    CrDoc.PrintToPrinter(1, False, 1, 2500)

                End If
            End Using

            CloseConn()
            MessageBox.Show("Berhasil Cetak Ulang Barcode", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub



    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If Lv_Parent.Items.Count = 0 Or Lv_Parent.SelectedItems.Count = 0 Or Lv_Parent.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Parent.FocusedItem.SubItems(item_NoFaktur).Text)
    End Sub


    '=================================================================================================================================================================================
    '=     UTILITY
    '=================================================================================================================================================================================
    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = &HA3 Then
            Return
        End If

        MyBase.WndProc(m)
    End Sub

    'Bentuk Mouse berubah saat berada di atas item ListView
    Private Sub Lv_Parent_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Parent.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Parent.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            Lv_Parent.Cursor = Cursors.Hand
        Else
            Lv_Parent.Cursor = Cursors.Default
        End If
    End Sub
    Private Sub Lv_Parent_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Parent.MouseLeave
        Lv_Parent.Cursor = Cursors.Default
    End Sub

    Private Sub Lv_Barcode_Merge_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Barcode_Merge.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Barcode_Merge.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            Lv_Barcode_Merge.Cursor = Cursors.Hand
        Else
            Lv_Barcode_Merge.Cursor = Cursors.Default
        End If
    End Sub
    Private Sub Lv_Barcode_Merge_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Barcode_Merge.MouseLeave
        Lv_Barcode_Merge.Cursor = Cursors.Default
    End Sub

    Private Sub CM_Lv_1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CM_Lv_1.Opening
        If Lv_Parent.Items.Count = 0 Then
            e.Cancel = True
            Exit Sub
        End If

        '=========================================================
        '=     CEK APAKAH MOUSE BERADA DI ATAS ROWS LISTVIEW     =
        '=========================================================
        Dim mousePos As Point = Lv_Parent.PointToClient(Cursor.Position)
        Dim info As ListViewHitTestInfo = Lv_Parent.HitTest(mousePos)

        If info.Item Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        Lv_Parent.FocusedItem = info.Item
        info.Item.Selected = True
    End Sub



End Class