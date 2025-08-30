Public Class N_EMI_Display_Selisih_Barang_Masuk


    Dim arr_tanggal, arr_lain As New ArrayList

    Dim Lv_Lokasi, Lv_NoFaktur, Lv_NoFakturBM, Lv_Tanggal, Lv_Supplier, Lv_jenis_Selisih, Lv_Total_Selisih, Lv_Satuan, Lv_Tot_Harga, Lv_User, Lv_Kd_supplier, Lv_Id_Selisih As String

    Dim item_Lokasi As Integer = 0
    Dim item_No_Faktur As Integer = 1
    Dim item_No_Faktur_BM As Integer = 2
    Dim item_Tanggal As Integer = 3
    Dim item_Supplier As Integer = 4
    Dim item_Jenis_Selisih As Integer = 5
    Dim item_Total_Selisih As Integer = 6
    Dim item_Satuan As Integer = 7
    Dim item_Total_Harga As Integer = 8
    Dim item_User As Integer = 9
    Dim item_Kd_Supplier As Integer = 10
    Dim item_Id_Selisih As Integer = 11



    Private Sub N_EMI_Display_Selisih_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Lokasi", 110, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("No Faktur", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("No Faktur BM", 130, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Tanggal", 110, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Supplier", 180, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jenis Selisih", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Total Selisih", 150, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Total Harga Selisih", 150, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("User", 130, HorizontalAlignment.Center)
        'hide
        Lv_Data.Columns.Add("kd_Supplier", 0, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("id_selisih", 0, HorizontalAlignment.Center)
        Lv_Data.View = View.Details


        Lv_Detail.Columns.Clear()
        Lv_Detail.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Kode Barang", 120, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Nama", 300, HorizontalAlignment.Left)
        Lv_Detail.Columns.Add("Harga Barang", 130, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Jumlah PL", 150, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Jumlah BM", 150, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Jumlah Hutang", 150, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Selisih", 150, HorizontalAlignment.Right)
        Lv_Detail.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Detail.Columns.Add("Selisih (Rp)", 150, HorizontalAlignment.Right)
        Lv_Detail.View = View.Details

        ComboBox3.Items.Clear() : arr_tanggal.Clear()
        ComboBox3.Items.Add("Tanggal") : arr_tanggal.Add("a.Tanggal")

        ComboBox2.Items.Clear() : arr_lain.Clear()
        ComboBox2.Items.Add("Supplier") : arr_lain.Add("b.Nama")
        ComboBox2.Items.Add("Jenis Selisih") : arr_lain.Add("d.Kode_Jenis_Selisih")
        ComboBox2.Items.Add("User ID") : arr_lain.Add("a.UserID")

        Try
            OpenConn()

            ComboBox6.Items.Clear()
            ComboBox6.Items.Add(OpsiSeluruh)
            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            ComboBox6.Text = Lokasi


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()

    End Sub



    Private Sub Kosong()

        Lv_Data.Items.Clear() : Lv_Detail.Items.Clear()
        ComboBox6.Text = Lokasi


        Load_Data()

    End Sub



    Private Sub Get_Data_LV(ByVal index As Integer)

        Lv_Lokasi = Lv_Data.Items(index).SubItems(item_Lokasi).Text
        Lv_NoFaktur = Lv_Data.Items(index).SubItems(item_No_Faktur).Text
        Lv_NoFakturBM = Lv_Data.Items(index).SubItems(item_No_Faktur_BM).Text
        Lv_Tanggal = Lv_Data.Items(index).SubItems(item_Tanggal).Text
        Lv_Supplier = Lv_Data.Items(index).SubItems(item_Supplier).Text
        Lv_jenis_Selisih = Lv_Data.Items(index).SubItems(item_Jenis_Selisih).Text
        Lv_Total_Selisih = Lv_Data.Items(index).SubItems(item_Total_Selisih).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_Tot_Harga = Lv_Data.Items(index).SubItems(item_Total_Harga).Text
        Lv_User = Lv_Data.Items(index).SubItems(item_User).Text
        Lv_Kd_supplier = Lv_Data.Items(index).SubItems(item_Kd_Supplier).Text
        Lv_Id_Selisih = Lv_Data.Items(index).SubItems(item_Id_Selisih).Text

    End Sub



    Private Sub Load_Data()
        Try
            OpenConn()


            Lv_Data.Items.Clear() : Lv_Detail.Items.Clear()
            SQL = "select c.Lokasi, a.No_Faktur, a.No_Faktur_BM, a.Tanggal, a.jam, a.UserID, a.Kode_Supplier, b.Nama as Supplier, a.Jenis_Selisih, d.Kode_Jenis_Selisih, a.Total_Selisih, "
            SQL = SQL & "isnull((select top 1 z.Satuan from EMI_Pembelian_Selisih_Barang_Masuk_Det z where a.Kode_Perusahaan = z.Kode_Perusahaan and a.No_Faktur = z.No_Faktur "
            SQL = SQL & "), '-') as Satuan, "
            SQL = SQL & "a.Total_Harga_Selisih, a.status "
            SQL = SQL & "from EMI_Pembelian_Selisih_Barang_Masuk a, Suppliers b, EMI_Pembelian_Loading c, EMI_Master_Jenis_Selisih d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = c.kode_perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.No_Faktur_BM = c.No_Faktur "
            SQL = SQL & "and a.Jenis_Selisih = d.Id_Jenis_Selisih "
            SQL = SQL & "and c.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.No_Faktur = 'SBDS-01/01-0006' "

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arr_tanggal.Item(ComboBox3.SelectedIndex) & " between ' "
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arr_lain.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "
            End If

            'If Not ComboBox6.SelectedIndex = -1 Then
            '    SQL = SQL & " and c.Lokasi = '" & ComboBox6.Text & "' "
            'End If
            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and c.Lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and c.Lokasi = '" & ComboBox6.Text & "' "
            End If

            SQL = SQL & "order by a.No_Faktur, a.Tanggal, a.Jam "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Lokasi"))
                    Lv.SubItems.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("No_Faktur_BM"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Supplier"))
                    Lv.SubItems.Add(Dr("Kode_Jenis_Selisih"))

                    If Dr("Satuan").ToString.ToUpper = "PCS" Then
                        Lv.SubItems.Add(Format(Dr("Total_Selisih"), "N0"))
                    Else
                        Lv.SubItems.Add(Format(Dr("Total_Selisih"), "N4"))
                    End If
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Format(Dr("Total_Harga_Selisih"), "N0"))
                    Lv.SubItems.Add(Dr("UserID"))

                    Lv.SubItems.Add(Dr("Kode_Supplier"))
                    Lv.SubItems.Add(Dr("Jenis_Selisih"))

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



    Private Sub Lv_Data_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data.SelectedIndexChanged
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Dim SelectedIndex As Integer = Lv_Data.FocusedItem.Index
        Get_Data_LV(SelectedIndex)

        Try
            OpenConn()


            Lv_Detail.Items.Clear()
            SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, c.Nama as Nama_Barang, b.Harga, b.Jumlah_PL, b.Jumlah_BM, b.Jumlah_Utang, b.Selisih, b.Satuan, b.Selisih_Rp "
            SQL = SQL & "from EMI_Pembelian_Selisih_Barang_Masuk a, EMI_Pembelian_Selisih_Barang_Masuk_Det b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and b.Kode_Stock_Owner = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & Lv_NoFaktur & "' "
            SQL = SQL & "order by b.Kode_Barang "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Detail.Items.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Dr("Harga"))

                    If Dr("Satuan").ToString.ToUpper = "PCS" Then
                        Lv.SubItems.Add(Format(Dr("Jumlah_PL"), "N0"))
                        Lv.SubItems.Add(Format(Dr("Jumlah_BM"), "N0"))
                        Lv.SubItems.Add(Format(Dr("Jumlah_Utang"), "N0"))
                        Lv.SubItems.Add(Format(Dr("Selisih"), "N0"))
                    Else
                        Lv.SubItems.Add(Format(Dr("Jumlah_PL"), "N4"))
                        Lv.SubItems.Add(Format(Dr("Jumlah_BM"), "N4"))
                        Lv.SubItems.Add(Format(Dr("Jumlah_Utang"), "N4"))
                        Lv.SubItems.Add(Format(Dr("Selisih"), "N4"))
                    End If

                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Format(Dr("Selisih_Rp"), "N0"))
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

        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            MessageBox.Show("Jenis Tanggal Harus Di pilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            CheckBox1.Focus() : Exit Sub
        End If

        If CheckBox1.Checked Then
            If ComboBox3.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Tanggal Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox3.DroppedDown = True : ComboBox3.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show("Periode I Tidak Boleh Lebih Dari periode II!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                Exit Sub
            End If
        End If

        If CheckBox2.Checked Then
            If ComboBox2.SelectedIndex = -1 Then
                MessageBox.Show("Parameter Lain Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox2.DroppedDown = True : ComboBox2.Focus() : Exit Sub
            ElseIf TextBox4.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Filter Harus Dipilih", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox4.Focus() : Exit Sub
            End If
        End If

        Load_Data()

    End Sub

    Private Sub SalinNoFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinNoFakturToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.SelectedItems.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Data.FocusedItem.SubItems(1).Text)
    End Sub

    Private Sub BatalSelisihBMToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalSelisihBMToolStripMenuItem.Click
        If Lv_Data.Items.Count = 0 Or Lv_Data.FocusedItem Is Nothing Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Selisih Barang Masuk"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Selisih_BM") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Selisih Barang Masuk", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Selisih Barang Masuk Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim NoFaktur As String = Lv_Data.FocusedItem.SubItems(item_No_Faktur).Text
            Dim NoFaktur_BM As String = Lv_Data.FocusedItem.SubItems(item_No_Faktur_BM).Text

            '=========================================
            '=     CEK APAKAH SELISIH DIBATALKAN     =
            '=========================================
            SQL = "select Kode_Perusahaan from EMI_Pembelian_Selisih_Barang_Masuk where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & NoFaktur & "' and status = 'Y' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan tidak dapat dilakukan karena No Faktur Sudah Dibatalkan Sebelumnya", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '==================================================
            '=     CEK APAKAH DATA SUDAH VALIDASI ANDROID     =
            '==================================================
            SQL = "select Flag_angkut from emi_barang_masuk_perpallet "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Pembelian_Loading = '" & NoFaktur_BM & "' "
            SQL = SQL & "and Status is null "
            SQL = SQL & "and Flag_angkut is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan, Karena No Loading Belum Divalidasi Android", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim Flag_Import As String = ""
            SQL = "select a.Kode_Perusahaan, a.No_Faktur, isnull(Flag_Import,'T') as Flag_Import from emi_pembelian_po a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_Faktur = ( "
            SQL = SQL & "select distinct x.No_PO "
            SQL = SQL & "from EMI_Pembelian_Loading z, EMI_Pembelian_Loading_Detail x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur = '" & NoFaktur_BM & "' ) "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Flag_Import = Dr("Flag_Import")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Pembelian Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=======================================================
            '=     CEK APAKAH DATA SUDAH TRANSAKSI BIAYA LOKAL     =
            '=======================================================
            SQL = "select Flag_Biaya, flag_selisih_bm "
            SQL = SQL & "from EMI_Pembelian_po a "
            SQL = SQL & "where a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = ( "
            SQL = SQL & "select distinct x.No_PO from EMI_Pembelian_Loading z, EMI_Pembelian_Loading_Detail x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "and z.no_Faktur = '" & NoFaktur_BM & "')"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("flag_selisih_bm")) = "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan, Karena No Faktur Belum Validasi Selisih BM", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        If Flag_Import = "T" Then
                            If General_Class.CekNULL(Dr("Flag_Biaya")) = "Y" Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan, Karena No Faktur Sudah Melalui Step Transaksi Biaya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End If
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan, Karena Data Pembelian Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select a.Kode_Perusahaan, * from Transaksi_Biaya_Lokal a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_Po = ( "
            SQL = SQL & "select distinct x.No_PO from EMI_Pembelian_Loading z, EMI_Pembelian_Loading_Detail x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur and z.no_Faktur = '" & NoFaktur_BM & "' ) "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan, Karena No Faktur Sudah Melalui Step Transaksi Biaya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '======================
            '=     PEMBATALAN     =
            '======================



            SQL = "select a.Kode_Perusahaan, a.No_Faktur from emi_pembelian_po a "
            SQL = SQL & "where a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.No_Faktur = ( "
            SQL = SQL & "select distinct x.No_PO "
            SQL = SQL & "from EMI_Pembelian_Loading z, EMI_Pembelian_Loading_Detail x "
            SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "and z.Kode_Perusahaan = a.Kode_Perusahaan "
            SQL = SQL & "and z.No_Faktur = '" & NoFaktur_BM & "' ) "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dim NoPO As String = Dr("No_Faktur")

                    Dr.Close()
                    SQL = "update emi_pembelian_po set Flag_Selisih_BM = NULL, Flag_Biaya = NULL  "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and status is null "
                    SQL = SQL & "and No_Faktur = '" & NoPO & "' "
                    ExecuteTrans(SQL)


                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Pembelian Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Flag_Selisih_BM from EMI_Pembelian_Loading where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Faktur = '" & NoFaktur_BM & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = "update EMI_Pembelian_Loading set Flag_Selisih_BM = NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null and No_Faktur = '" & NoFaktur_BM & "'"
                    ExecuteTrans(SQL)

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Loading Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            SQL = "select Kode_Perusahaan from EMI_Pembelian_Selisih_Barang_Masuk "
            SQL = SQL & "where status is null and Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = "update EMI_Pembelian_Selisih_Barang_Masuk set Status = 'Y' "
                    SQL = SQL & "where status is null and Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoFaktur & "' "
                    ExecuteTrans(SQL)

                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Faktur Selisih Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Faktur Selisih Berhasil Dibatalkan", "Pembatalan Selisih Barang Masuk", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Load_Data()


    End Sub


    '=============================================================================================================================================================================
    '=     HANDLE KEYPRESS
    '=============================================================================================================================================================================
    Private Sub ComboBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox6.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox3.Focus()
    End Sub
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            BtnBarangMasuk_Cari_Click(CheckBox3, e)
        End If
    End Sub
    Private Sub CheckBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox3.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox1.Focus()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox3.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            CheckBox3.Checked = False
        Else
            ComboBox3.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            ComboBox3.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub
    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox1.Checked Then
                ComboBox3.DroppedDown = True
                ComboBox3.Focus()
            Else
                CheckBox2.Focus()
            End If

        End If
    End Sub
    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub
    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub
    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox2.Focus()
    End Sub
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ComboBox2.Enabled = True : TextBox4.Enabled = True
        Else
            ComboBox2.Enabled = False : TextBox4.Enabled = False
            ComboBox2.SelectedIndex = -1 : TextBox4.Text = ""
        End If
    End Sub
    Private Sub CheckBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If CheckBox2.Checked Then
                ComboBox2.DroppedDown = True
                ComboBox2.Focus()
            Else
                BtnBarangMasuk_Cari.Focus()
            End If

        End If
    End Sub
    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox4.Focus()
    End Sub
    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then BtnBarangMasuk_Cari.Focus()
    End Sub





End Class