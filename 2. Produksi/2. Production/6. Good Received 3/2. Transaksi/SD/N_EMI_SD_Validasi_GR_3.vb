Public Class N_EMI_SD_Validasi_GR_3

    Dim arrPeriode, arrParamLain As New ArrayList

    Dim arrSelectedBarcode As New List(Of Dictionary(Of String, String))
    Public arrBarcodeFromParent As New List(Of Dictionary(Of String, String))

    Dim Lv_NoSplit, Lv_Lokasi, Lv_Barcode, Lv_Batch, Lv_KdBarang, Lv_NmBarang, Lv_Jumlah, Lv_Satuan, Lv_TglProduksi, Lv_TglExpired, Lv_Kualitas, Lv_QrCode, Lv_KdUnikBerjalan As String

    'Dim item_NoSplit As Integer = 0
    Dim item_Lokasi As Integer = 0
    Dim item_Barcode As Integer = 1
    Dim item_Batch As Integer = 2
    Dim item_KdBarang As Integer = 3
    Dim item_NmBarang As Integer = 4
    Dim item_Jumlah As Integer = 5
    Dim item_Satuan As Integer = 6
    Dim item_TglProduksi As Integer = 7
    Dim item_TglExpired As Integer = 8
    Dim item_Kualitas As Integer = 9
    Dim item_Nomor As Integer = 10
    Dim item_QrCode As Integer = 11
    Dim item_KdUnikBerjalan As Integer = 12

    Dim SelectedSplit As String = ""

    Private Sub SD_ValidasiGR_Split_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()
    End Sub

    Private Sub Kosong()

        Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
        'Lv_Data.Columns.Add("No Split", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Lokasi", 140, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Barcode", 250, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Batch Number", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Nama Barang", 180, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Jumlah", 110, HorizontalAlignment.Right)
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tgl Produksi", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Tgl Expired", 100, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Kualitas", 130, HorizontalAlignment.Center)
        Lv_Data.Columns.Add("Nomor", 90, HorizontalAlignment.Center)
        'Hide
        Lv_Data.Columns.Add("QrCode", 0, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("kdUnikBerjalan", 0, HorizontalAlignment.Left)
        Lv_Data.View = View.Details


        Lv_Data.Columns(11).DisplayIndex = 3


        Cmb_Periode.Items.Clear() : arrPeriode.Clear()
        Cmb_Periode.Items.Add(OpsiSeluruh) : arrPeriode.Add(OpsiSeluruh)
        Cmb_Periode.Items.Add("Tanggal Produksi") : arrPeriode.Add("c.Tgl_Produksi")
        Cmb_Periode.Items.Add("Tanggal Expired") : arrPeriode.Add("c.Tgl_Expired")
        Cmb_Periode.SelectedIndex = 0

        Cmb_Lain.Items.Clear() : arrParamLain.Clear()
        Cmb_Lain.Items.Add(OpsiSeluruh) : arrParamLain.Add(OpsiSeluruh)
        Cmb_Lain.Items.Add("No Split") : arrParamLain.Add("a.No_Production_Order")
        Cmb_Lain.Items.Add("Lokasi") : arrParamLain.Add("b.Kode_Stock_Owner_Tujuan")
        Cmb_Lain.Items.Add("Barcode") : arrParamLain.Add("(c.Qr_Code + '-' + c.Kode_Unik_Berjalan)")
        Cmb_Lain.Items.Add("Kode Barang") : arrParamLain.Add("b.Kode_Barang")
        Cmb_Lain.Items.Add("Nama Barang") : arrParamLain.Add("d.Nama")
        Cmb_Lain.Items.Add("Kualitas") : arrParamLain.Add("e.Keterangan")
        Cmb_Lain.SelectedIndex = 0

        Txt_ValueLain.Text = ""
        arrSelectedBarcode.Clear()

        SelectedSplit = ""

        Tgl1.Enabled = False : Tgl2.Enabled = False

        Btn_Cari_Click(Me, New EventArgs)

    End Sub

    Private Sub Get_Data_Listview(ByVal index As Integer)
        'Lv_NoSplit = Lv_Data.Items(index).SubItems(item_NoSplit).Text
        Lv_Lokasi = Lv_Data.Items(index).SubItems(item_Lokasi).Text
        Lv_Barcode = Lv_Data.Items(index).SubItems(item_Barcode).Text
        Lv_Batch = Lv_Data.Items(index).SubItems(item_Batch).Text
        Lv_KdBarang = Lv_Data.Items(index).SubItems(item_KdBarang).Text
        Lv_NmBarang = Lv_Data.Items(index).SubItems(item_NmBarang).Text
        Lv_Jumlah = Lv_Data.Items(index).SubItems(item_Jumlah).Text
        Lv_Satuan = Lv_Data.Items(index).SubItems(item_Satuan).Text
        Lv_TglProduksi = Lv_Data.Items(index).SubItems(item_TglProduksi).Text
        Lv_TglExpired = Lv_Data.Items(index).SubItems(item_TglExpired).Text
        Lv_Kualitas = Lv_Data.Items(index).SubItems(item_Kualitas).Text
        Lv_QrCode = Lv_Data.Items(index).SubItems(item_QrCode).Text
        Lv_KdUnikBerjalan = Lv_Data.Items(index).SubItems(item_KdUnikBerjalan).Text

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If Cmb_Lain.SelectedIndex <> 0 Then
            If Txt_ValueLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value Parameter Lain Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If

        Try
            OpenConn()

            Lv_Data.Items.Clear()

            'SQL = "select a.no_transaksi as No_GR2, isnull(b.No_Split, '-') as No_Split, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, "
            'SQL = SQL & "d.Nama as Nama_Barang, b.Jenis, c.Qr_Code, c.Kode_Unik_Berjalan, c.Batch_Number, "
            'SQL = SQL & "c.Tgl_Produksi, c.Tgl_Expired, a.UserID, "

            ''SQL = SQL & "isnull(((isnull(sum(c.Jumlah), 0)) - "
            ''SQL = SQL & "ISNULL((select isnull(sum(z.jumlah), 0) from N_EMI_Validation_GR_3_Detail z, N_EMI_Validation_GR_3 x "
            ''SQL = SQL & "where z.Kode_Perusahaan = x.Kode_Perusahaan and z.Kode_Perusahaan = a.Kode_Perusahaan "
            ''SQL = SQL & "and z.No_Transaksi = x.No_Transaksi and z.No_Transaksi_GR2 = a.No_Transaksi and x.status is null "
            ''SQL = SQL & "),0)), 0) as Jumlah, "

            'SQL = SQL & "isnull(sum(c.Jumlah), 0) as Jumlah, "

            'SQL = SQL & "b.Satuan, b.Warna, e.Keterangan as Kualitas, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, b.nomor "
            'SQL = SQL & "from Emi_Production_Results_Validation a, Emi_Production_Results_Validation_Detail b, Barang_SN c, barang d, EMI_Master_Warna e "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.kode_perusahaan = c.kode_perusahaan and b.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.Kode_Perusahaan "
            'SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            'SQL = SQL & "and b.kode_stock_owner_tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number "
            'SQL = SQL & "and b.Kode_Stock_Owner_Tujuan = d.Kode_Stock_Owner and b.Kode_Barang = d.Kode_Barang "
            'SQL = SQL & "and b.Warna = e.Kode_Warna "
            'SQL = SQL & "and a.Status is null and b.Flag_Validasi is null "
            'SQL = SQL & "and b.Flag_Validasi_Loading  = 'Y' "
            'SQL = SQL & "and b.Jenis = 'Finished Good' "
            'SQL = SQL & "and c.Jumlah <> 0 "

            'If Cmb_Periode.SelectedIndex <> 0 Then
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & arrPeriode(Cmb_Periode.SelectedIndex) & " between '"
            '    SQL = SQL & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
            'End If

            'If Cmb_Lain.SelectedIndex <> 0 Then
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & arrParamLain.Item(Cmb_Lain.SelectedIndex) & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
            'End If
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "group by a.kode_perusahaan, a.No_Transaksi, b.No_Split, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama, b.Jenis, c.Qr_Code, "
            'SQL = SQL & "c.Kode_Unik_Berjalan, c.Batch_Number, c.Tgl_Produksi, c.Tgl_Expired, a.UserID, b.Satuan, b.Warna, e.Keterangan, b.nomor "
            'SQL = SQL & "order by b.No_Split, b.Kode_Stock_Owner_Tujuan, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan), c.Tgl_Expired ASC "


            SQL = ";with cte as ( "
            SQL &= $"select a.no_transaksi as No_GR2, isnull(b.No_Split, '-') as No_Split, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, "
            SQL &= $"d.Nama as Nama_Barang, b.Jenis, c.Qr_Code, c.Kode_Unik_Berjalan, c.Batch_Number, "
            SQL &= $"c.Tgl_Produksi, c.Tgl_Expired, a.UserID, "
            SQL &= $"isnull(sum(c.Jumlah), 0) as Jumlah, "
            SQL &= $"b.Satuan, b.Warna, e.Keterangan as Kualitas, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, b.nomor "
            SQL &= $"from Emi_Production_Results_Validation a "
            SQL &= $"inner join Emi_Production_Results_Validation_Detail b on a.Kode_Perusahaan  = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi  "
            SQL &= $"inner join Barang_SN c on b.Kode_Perusahaan  = c.Kode_Perusahaan  and b.Kode_Stock_Owner_Tujuan = c.Kode_Stock_Owner and b.Kode_Barang = c.Kode_Barang and b.Serial_Number_Akhir = c.Serial_Number  "
            SQL &= $"inner join Barang d on c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL &= $"inner join EMI_Master_Warna e on b.Kode_Perusahaan = e.Kode_Perusahaan  and b.Warna  = e.Kode_Warna  "
            SQL &= $"where a.Kode_Perusahaan  = '{KodePerusahaan}' "
            SQL &= $"and a.Status is null "
            SQL &= $"and a.Flag_Validasi is NULl "
            SQL &= $"and b.Flag_Validasi_Loading = 'Y' "
            SQL &= $"and b.Jenis = 'Finished Good' "
            SQL &= $"and c.Jumlah <> 0 "
            If Cmb_Periode.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrPeriode(Cmb_Periode.SelectedIndex) & " between '"
                SQL = SQL & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
            End If

            If Cmb_Lain.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrParamLain.Item(Cmb_Lain.SelectedIndex) & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
            End If
            SQL &= $"group by a.kode_perusahaan, a.No_Transaksi, b.No_Split, b.Kode_Stock_Owner_Tujuan, b.Kode_Barang, d.Nama, b.Jenis, c.Qr_Code, "
            SQL &= $"c.Kode_Unik_Berjalan, c.Batch_Number, c.Tgl_Produksi, c.Tgl_Expired, a.UserID, b.Satuan, b.Warna, e.Keterangan, b.nomor ) "
            SQL &= $"select No_GR2, Kode_Stock_Owner_Tujuan, Kode_Barang, Nama_Barang, Jenis, Qr_Code, Kode_Unik_Berjalan, Batch_Number, Tgl_Produksi, Tgl_Expired, UserID, sum(Jumlah) as Jumlah, "
            SQL &= $"Satuan, Warna, Kualitas, Barcode, Nomor "
            SQL &= $"from cte "
            SQL &= $"group by No_GR2, Kode_Stock_Owner_Tujuan, Kode_Barang, Nama_Barang, Jenis, Qr_Code, Kode_Unik_Berjalan, Batch_Number, Tgl_Produksi, Tgl_Expired, UserID, "
            SQL &= $"Satuan, Warna, Kualitas, Barcode, Nomor "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    'Lv = Lv_Data.Items.Add(Dr("No_Split"))
                    Lv = Lv_Data.Items.Add(Dr("Kode_Stock_Owner_Tujuan"))
                    Lv.SubItems.Add(Dr("Barcode"))
                    Lv.SubItems.Add(Dr("Batch_Number"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Dr("Nama_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N0"))
                    Lv.SubItems.Add(Dr("Satuan"))
                    Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kualitas"))
                    Lv.SubItems.Add(Dr("nomor"))

                    'Hide
                    Lv.SubItems.Add(Dr("Qr_Code"))
                    Lv.SubItems.Add(Dr("Kode_Unik_Berjalan"))
                    Lv.SubItems.Add(Dr("No_GR2"))


                    '=========================================================
                    '=     LAKUKAN CEK APAKAH ADA SUDAH ADA DI LV PARENT     =
                    '=========================================================
                    ' Cek dengan Lamda Expression
                    ' Any akan return true jika ada 1 saja data di function bernilai true
                    ' function / sub adalah syarat yang harus ada pada scope lambda expression
                    Dim foundQrCode As Boolean = arrBarcodeFromParent.Any(Function(dict) dict.ContainsKey("QrCode") AndAlso dict("QrCode") = Dr("Qr_Code"))
                    Dim foundKodeUnik As Boolean = arrBarcodeFromParent.Any(Function(dict) dict.ContainsKey("KdUnikBerjalan") AndAlso dict("KdUnikBerjalan").Trim.ToUpper() = Dr("Kode_Unik_Berjalan").ToString.Trim.ToUpper)

                    If foundQrCode And foundKodeUnik Then
                        Lv.Checked = True
                    Else
                        Lv.Checked = False
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



    Private Sub Lv_Data_Sum_ItemChecked(sender As Object, e As ItemCheckedEventArgs) Handles Lv_Data.ItemChecked
        If Lv_Data.Items.Count = 0 Then Exit Sub

        If Lv_Data.CheckedItems.Count = 0 Then
            SelectedSplit = ""
        End If

        'Dim SelectedSplitParent As String = N_EMI_Transaksi_Validasi_GR_3.Txt_NoSplit.Text

        If Not Lv_Data.FocusedItem Is Nothing AndAlso Lv_Data.FocusedItem.Checked Then
            'If SelectedSplitParent = "" Then
            '    If SelectedSplit = "" Then
            '        SelectedSplit = Lv_Data.FocusedItem.Text
            '    Else
            '        If Lv_Data.FocusedItem.Text <> SelectedSplit Then
            '            'Lv_Data.FocusedItem.Checked = False
            '        End If
            '    End If
            'Else
            '    If Lv_Data.FocusedItem.Text <> SelectedSplitParent Then
            '        'Lv_Data.FocusedItem.Checked = False
            '    End If
            'End If

        End If

    End Sub

    Private Sub Btn_Tambah_Click(sender As Object, e As EventArgs) Handles Btn_Tambah.Click

        If Lv_Data.Items.Count = 0 Then Exit Sub

        Dim hasData As Boolean = False
        arrSelectedBarcode.Clear()
        For i As Integer = 0 To Lv_Data.Items.Count - 1
            If Lv_Data.Items(i).Checked = True Then
                Get_Data_Listview(i)

                'If N_EMI_Transaksi_Validasi_GR_3.Txt_NoSplit.Text.Trim.Length = 0 Then
                '    N_EMI_Transaksi_Validasi_GR_3.Txt_NoSplit.Text = Lv_NoSplit
                'Else
                '    If N_EMI_Transaksi_Validasi_GR_3.Txt_NoSplit.Text.Trim.ToUpper <> Lv_NoSplit.Trim.ToUpper Then
                '        Continue For
                '    End If
                'End If

                hasData = True

                Dim Dict As New Dictionary(Of String, String)
                Dict("QrCode") = Lv_QrCode
                Dict("KdUnikBerjalan") = Lv_KdUnikBerjalan

                arrSelectedBarcode.Add(Dict)

            End If
        Next

        If Not hasData Then
            MessageBox.Show("Tidak Ada Data yang Ditambahkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_Data.Focus() : Exit Sub
        End If

        N_EMI_Transaksi_Validasi_GR_3.arrBarcodeFromSD = arrSelectedBarcode
        N_EMI_Transaksi_Validasi_GR_3.LoadFromSD()

        'Kosong()
        Me.Close()

    End Sub

    Private Sub Btn_Close_Click(sender As Object, e As EventArgs) Handles Btn_Close.Click
        Kosong()
        Me.Close()
    End Sub

    '============================================================================================================================================
    '=     HANDLE KEY PRESS
    '============================================================================================================================================

    Private Sub Cmb_Lain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Lain.SelectedIndexChanged
        If Cmb_Lain.SelectedIndex = 0 Then
            Txt_ValueLain.Enabled = False
        Else
            Txt_ValueLain.Enabled = True
        End If
        Txt_ValueLain.Text = ""
    End Sub

    Private Sub Cmb_Periode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmb_Periode.SelectedIndexChanged
        If Cmb_Periode.SelectedIndex = 0 Then
            Tgl1.Enabled = False : Tgl2.Enabled = False
        Else
            Tgl1.Enabled = True : Tgl2.Enabled = True
        End If
        Tgl1.Value = Now : Tgl2.Value = Now
    End Sub

    Private Sub Cmb_Periode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Periode.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Periode.SelectedIndex = 0 Then
                Cmb_Lain.DroppedDown = True
                Cmb_Lain.Focus()
            Else
                Tgl1.Focus()
            End If
        End If
    End Sub

    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then
            Cmb_Lain.DroppedDown = True
            Cmb_Lain.Focus()
        End If
    End Sub

    Private Sub Cmb_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Lain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Cmb_Periode.SelectedIndex = 0 Then
                Btn_Cari.Focus()
            Else
                Txt_ValueLain.Focus()
            End If
        End If
    End Sub

    Private Sub Txt_ValueLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ValueLain.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

End Class