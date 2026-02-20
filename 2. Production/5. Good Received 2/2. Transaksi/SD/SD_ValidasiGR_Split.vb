Public Class SD_ValidasiGR_Split

    Dim arrPeriode, arrParamLain As New ArrayList

    Dim arrSelectedBarcode As New List(Of Dictionary(Of String, String))
    Public arrBarcodeFromParent As New List(Of Dictionary(Of String, String))

    Dim Fitur_Military_Sampling As Boolean = False

    Dim Lv_NoSplit, Lv_Lokasi, Lv_Barcode, Lv_Batch, Lv_KdBarang, Lv_NmBarang, Lv_Jumlah, Lv_Satuan, Lv_TglProduksi, Lv_TglExpired, Lv_Kualitas, Lv_QrCode, Lv_KdUnikBerjalan, Lv_StatMilitary As String

    Dim item_NoSplit As Integer = 0
    Dim item_Lokasi As Integer = 1
    Dim item_Barcode As Integer = 2
    Dim item_Batch As Integer = 3
    Dim item_KdBarang As Integer = 4
    Dim item_NmBarang As Integer = 5
    Dim item_Jumlah As Integer = 6
    Dim item_Satuan As Integer = 7
    Dim item_TglProduksi As Integer = 8
    Dim item_TglExpired As Integer = 9
    Dim item_Kualitas As Integer = 10
    Dim item_Nomor As Integer = 11
    Dim item_QrCode As Integer = 12
    Dim item_KdUnikBerjalan As Integer = 13
    Dim item_StatMilitarySampling As Integer = 14

    'Dim SelectedSplit As String = ""

    Dim CurrentVariant As String = ""



    Private Sub SD_ValidasiGR_Split_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Kosong()
    End Sub

    Private Sub Kosong()

        Lv_Data.Columns.Clear() : Lv_Data.Items.Clear()
        Lv_Data.Columns.Add("No Split", 120, HorizontalAlignment.Left) '0
        Lv_Data.Columns.Add("Lokasi", 140, HorizontalAlignment.Left) '1
        Lv_Data.Columns.Add("Barcode", 250, HorizontalAlignment.Left) '2
        Lv_Data.Columns.Add("Batch Number", 0, HorizontalAlignment.Left) '3
        Lv_Data.Columns.Add("Kode Barang", 150, HorizontalAlignment.Left) '4
        Lv_Data.Columns.Add("Nama Barang", 180, HorizontalAlignment.Left) '5
        Lv_Data.Columns.Add("Jumlah", 110, HorizontalAlignment.Right) '6
        Lv_Data.Columns.Add("Satuan", 80, HorizontalAlignment.Center) '7
        Lv_Data.Columns.Add("Tgl Produksi", 100, HorizontalAlignment.Center) '8
        Lv_Data.Columns.Add("Tgl Expired", 100, HorizontalAlignment.Center) '9
        Lv_Data.Columns.Add("Kualitas", 130, HorizontalAlignment.Center) '10
        Lv_Data.Columns.Add("Nomor", 90, HorizontalAlignment.Center) '11
        'Hide
        Lv_Data.Columns.Add("QrCode", 0, HorizontalAlignment.Left) '12
        Lv_Data.Columns.Add("kdUnikBerjalan", 0, HorizontalAlignment.Left) '13
        Lv_Data.Columns.Add("StatMil", 0, HorizontalAlignment.Left) '14
        Lv_Data.View = View.Details


        Lv_Data.Columns(11).DisplayIndex = 3


        Cmb_Periode.Items.Clear() : arrPeriode.Clear()
        Cmb_Periode.Items.Add(OpsiSeluruh) : arrPeriode.Add(OpsiSeluruh)
        Cmb_Periode.Items.Add("Tanggal Produksi") : arrPeriode.Add("a.Tgl_Produksi")
        Cmb_Periode.Items.Add("Tanggal Expired") : arrPeriode.Add("a.Tgl_Expired")
        Cmb_Periode.SelectedIndex = 0

        Cmb_Lain.Items.Clear() : arrParamLain.Clear()
        Cmb_Lain.Items.Add(OpsiSeluruh) : arrParamLain.Add(OpsiSeluruh)
        Cmb_Lain.Items.Add("No Split") : arrParamLain.Add("b.No_Production_Order")
        Cmb_Lain.Items.Add("Lokasi") : arrParamLain.Add("a.Lokasi_Gudang")
        Cmb_Lain.Items.Add("Barcode") : arrParamLain.Add("(a.Qr_Code + '-' + a.Kode_Unik_Berjalan)")
        Cmb_Lain.Items.Add("Kode Barang") : arrParamLain.Add("c.Kode_Barang")
        Cmb_Lain.Items.Add("Nama Barang") : arrParamLain.Add("d.Nama")
        Cmb_Lain.Items.Add("Kualitas") : arrParamLain.Add("e.Keterangan")
        Cmb_Lain.SelectedIndex = 0

        Txt_ValueLain.Text = ""
        arrSelectedBarcode.Clear()

        'SelectedSplit = ""
        CurrentVariant = ""

        Tgl1.Enabled = False : Tgl2.Enabled = False

        Btn_Cari_Click(Me, New EventArgs)

    End Sub

    Private Sub Get_Data_Listview(ByVal index As Integer)

        Lv_NoSplit = Lv_Data.Items(index).SubItems(item_NoSplit).Text
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
        Lv_StatMilitary = Lv_Data.Items(index).SubItems(item_StatMilitarySampling).Text

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



            SQL = "select top(300) b.No_Production_Order as No_Split, a.Lokasi_Gudang as Kode_Stock_Owner, a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) as Barcode, a.Batch_Number,  "
            SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama as Nama_Barang, a.Nomor, "
            'SQL = SQL & "sum(f.Jumlah) as Jumlah, "

            SQL = SQL & "isnull((sum(f.Jumlah) -  "
            SQL = SQL & "(select isnull(sum(jumlah), 0) from N_EMI_Validation_GR_Temp z "
            SQL = SQL & "where b.kode_perusahaan = z.kode_perusahaan "
            SQL = SQL & "and b.no_production_order = z.no_production_order "
            SQL = SQL & "and z.barcode = (a.Qr_Code + '-' + a.Kode_Unik_Berjalan)) ), 0) as Jumlah, "

            SQL = SQL & "a.Satuan, a.Jenis, e.Keterangan as Kualitas, a.proses, "

            SQL = SQL & "isnull(( "
            SQL = SQL & "select top 1 'Y' from N_EMI_Military_Sampling z "
            SQL = SQL & "where z.kode_perusahaan = a.Kode_Perusahaan and z.status is null "
            SQL = SQL & "and z.No_Split = b.No_Production_Order and z.No_Batch = a.tahap "
            SQL = SQL & "and z.No_GR = '1' "
            SQL = SQL & "), 'T') as Status_Military_Sampling, "

            SQL = SQL & "isnull(( "
            SQL = SQL & "select isnull(flag_commercial, 'T') "
            SQL = SQL & "from emi_split_production_order x, emi_order_produksi y where "
            SQL = SQL & "x.kode_perusahaan =y.kode_perusahaan and x.no_po=y.no_faktur and x.status is null and y.status is null "
            SQL = SQL & "and b.kode_perusahaan=x.kode_perusahaan and b.no_production_order=x.no_transaksi), null) as Flag_Commercial "

            SQL = SQL & "from Emi_Production_Results_Detail_Pallet a, Emi_Production_Results b, EMI_Production_Results_Detail_Barang c, barang d, EMI_Master_Warna e, Barang_SN f "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and a.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.No_Transaksi = c.No_Transaksi and a.Proses = c.Proses "
            SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Jenis = e.Kode_Warna "
            SQL = SQL & "and a.SN_Baru = f.Serial_Number "
            SQL = SQL & "and b.Status is null "
            SQL = SQL & "and a.Lokasi_Gudang in (select Kode_Stock_Owner from Stock_Owner_Gudang z where z.Kode_Perusahaan = a.Kode_Perusahaan and z.Flag_QI = 'Y') "

            If Cmb_Periode.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrPeriode(Cmb_Periode.SelectedIndex) & " between '"
                SQL = SQL & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "
            End If

            If Cmb_Lain.SelectedIndex <> 0 Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrParamLain.Item(Cmb_Lain.SelectedIndex) & " like '%" & Trim(Txt_ValueLain.Text) & "%' "
            End If

            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' and f.jumlah<>0 "
            SQL = SQL & "group by  b.No_Production_Order, a.Lokasi_Gudang , a.Qr_Code, a.Kode_Unik_Berjalan, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan) , a.Batch_Number, "
            SQL = SQL & "a.Tgl_Produksi, a.Tgl_Expired, b.UserID, c.Kode_Barang, d.Nama, a.Satuan, a.Jenis, e.Keterangan, b.kode_perusahaan, a.Nomor, a.Kode_Perusahaan, a.Proses, a.tahap "
            SQL = SQL & "order by  b.No_Production_Order, a.Nomor, a.Lokasi_Gudang, (a.Qr_Code + '-' + a.Kode_Unik_Berjalan), a.Tgl_Expired ASC "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read

                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("No_Split")) '0
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner")) '1
                    Lv.SubItems.Add(Dr("Barcode")) '2
                    Lv.SubItems.Add(Dr("Batch_Number")) '3
                    Lv.SubItems.Add(Dr("Kode_Barang")) '4
                    Lv.SubItems.Add(Dr("Nama_Barang")) '5
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2")) '6
                    Lv.SubItems.Add(Dr("Satuan")) '7
                    Lv.SubItems.Add(Format(Dr("Tgl_Produksi"), "dd MMM yyyy")) '8
                    Lv.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy")) '9
                    Lv.SubItems.Add(Dr("Kualitas")) '10
                    Lv.SubItems.Add(Dr("Nomor")) '11

                    'Hide
                    Lv.SubItems.Add(Dr("Qr_Code")) '12
                    Lv.SubItems.Add(Dr("Kode_Unik_Berjalan")) '13
                    If Dr("Flag_Commercial") = "Y" Then

                        Lv.SubItems.Add(Dr("Status_Military_Sampling")) '14
                        If Dr("Status_Military_Sampling") = "Y" Then
                            Lv.BackColor = Color.LightBlue
                        End If

                    Else
                        Lv.SubItems.Add("Y")
                        Lv.BackColor = Color.Tan
                    End If



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
            'SelectedSplit = ""
            CurrentVariant = ""
        End If



        'Dim SelectedSplitParent As String = EMI_Validasi_GR.Txt_NoSplit.Text
        Dim SelectedVarian As String = EMI_Validasi_GR.SelectedVariant


        If Not Lv_Data.FocusedItem Is Nothing AndAlso Lv_Data.FocusedItem.Checked Then

            Dim CheckedVarian As String = Lv_Data.FocusedItem.SubItems(item_KdBarang).Text

            If Fitur_Military_Sampling Then
                If Lv_Data.FocusedItem.SubItems(item_StatMilitarySampling).Text = "T" Then
                    Lv_Data.FocusedItem.Checked = False
                    Exit Sub
                End If
            End If

            'If SelectedSplitParent = "" Then
            '    If SelectedSplit = "" Then
            '        SelectedSplit = Lv_Data.FocusedItem.Text
            '    Else
            '        If Lv_Data.FocusedItem.Text <> SelectedSplit Then
            '            Lv_Data.FocusedItem.Checked = False
            '        End If
            '    End If
            'Else
            '    If Lv_Data.FocusedItem.Text <> SelectedSplitParent Then
            '        Lv_Data.FocusedItem.Checked = False
            '    End If
            'End If

            If SelectedVarian = "" Then
                If CurrentVariant = "" Then
                    CurrentVariant = CheckedVarian
                Else
                    If CheckedVarian <> CurrentVariant Then
                        Lv_Data.FocusedItem.Checked = False
                    End If
                End If
            Else
                If CheckedVarian <> SelectedVarian Then
                    Lv_Data.FocusedItem.Checked = False
                End If
            End If

        End If

    End Sub

    Private Sub Btn_Tambah_Click(sender As Object, e As EventArgs) Handles Btn_Tambah.Click

        If Lv_Data.Items.Count = 0 Then Exit Sub

        Dim hasData As Boolean = False
        arrSelectedBarcode.Clear()
        For i As Integer = 0 To Lv_Data.Items.Count - 1
            If Lv_Data.Items(i).Checked = True Then
                Get_Data_Listview(i)

                'If EMI_Validasi_GR.Txt_NoSplit.Text.Trim.Length = 0 Then
                '    EMI_Validasi_GR.Txt_NoSplit.Text = Lv_NoSplit
                'Else
                '    If EMI_Validasi_GR.Txt_NoSplit.Text.Trim.ToUpper <> Lv_NoSplit.Trim.ToUpper Then
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

        EMI_Validasi_GR.arrBarcodeFromSD = arrSelectedBarcode
        EMI_Validasi_GR.LoadFromSD()

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
        If e.KeyChar = Chr(13) Then
            Btn_Cari.Focus()
            e.Handled = True
        End If
    End Sub

End Class