Imports System.IO

Public Class EMI_Display_Production_Result

    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Private random As New Random()
    Private imageBytes1 As Byte = Nothing
    Private FileSize1 As UInt32
    Private rawData1() As Byte
    Private fs1 As FileStream

    Dim itemPR_NoFak As Integer = 0
    Dim itemPR_NoPO As Integer = 1
    Dim itemPR_Tanggal As Integer = 2
    Dim itemPR_Jam As Integer = 3
    Dim itemPR_UserID As Integer = 4
    Dim itemPR_KdBarang As Integer = 5
    Dim itemPR_NmBarang As Integer = 6
    Dim itemPR_JumlahProduksi As Integer = 7
    Dim itemPR_Satuan As Integer = 8
    Dim itemPR_Catatan As Integer = 9
    Dim itemPR_TanggalSelesaiProduksi As Integer = 10
    Dim itemPR_JamSelesaiProduksi As Integer = 11
    Dim itemPR_FlagSelesai As Integer = 12

    Dim itemFG_NoFak As Integer = 0
    Dim itemFG_NoPO As Integer = 1
    Dim itemFG_FullQR As Integer = 2
    Dim itemFG_Jumlah As Integer = 3
    Dim itemFG_Satuan As Integer = 4
    Dim itemFG_TglExp As Integer = 5
    Dim itemFG_Kualitas As Integer = 6

    Private Sub Display_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Private Sub kosong()

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Display_Barang_Masuk")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Pembelian_Barang_Masuk")
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Lv_ProductionResult.Items.Clear()
        Lv_ProductionResult.Columns.Add("No Split", 125, HorizontalAlignment.Left)
        Lv_ProductionResult.Columns.Add("No PO", 135, HorizontalAlignment.Left)
        Lv_ProductionResult.Columns.Add(Base_Language.Lang_Global_Tanggal, 150, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add(Base_Language.Lang_Global_Jam, 100, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add("User ID", 100, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add("Kode Barang", 150, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add("Nama", 250, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add("Jumlah Produksi", 130, HorizontalAlignment.Right)
        Lv_ProductionResult.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add("Catatan", 0, HorizontalAlignment.Left)
        Lv_ProductionResult.Columns.Add("Tanggal Selesai Produksi", 150, HorizontalAlignment.Center) 'NULLable
        Lv_ProductionResult.Columns.Add("Jam Selesai Produksi", 100, HorizontalAlignment.Center) 'NULLable
        'Hide
        Lv_ProductionResult.Columns.Add("Flag Selesai Produksi", 0, HorizontalAlignment.Center) 'NULLable
        Lv_ProductionResult.View = View.Details

        Lv_DetailFinishedGood.Items.Clear()
        Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
        Lv_DetailFinishedGood.Columns.Add("No_Production_Order", 0, HorizontalAlignment.Left)
        Lv_DetailFinishedGood.Columns.Add("QR Code", 280, HorizontalAlignment.Left)
        Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_Jumlah, 150, HorizontalAlignment.Right)
        Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_DetailFinishedGood.Columns.Add("Tanggal Expired", 150, HorizontalAlignment.Center)
        Lv_DetailFinishedGood.Columns.Add("Kualitas", 200, HorizontalAlignment.Center)
        Lv_DetailFinishedGood.Columns.Add("Keranjang", 80, HorizontalAlignment.Center).DisplayIndex = 2
        Lv_DetailFinishedGood.View = View.Details

        Lv_DetailRawMaterial.Items.Clear()
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
        Lv_DetailRawMaterial.Columns.Add("Kode Stock Owner", 0, HorizontalAlignment.Left)
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_NamaBarang, 400, HorizontalAlignment.Left)
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_Nilai_Produksi, 180, HorizontalAlignment.Right)
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_DetailRawMaterial.View = View.Details

        Lv_DetailPackaging.Items.Clear()
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
        Lv_DetailPackaging.Columns.Add("Kode Stock Owner", 0, HorizontalAlignment.Left)
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_NamaBarang, 400, HorizontalAlignment.Left)
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_Nilai_Produksi, 180, HorizontalAlignment.Right)
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_DetailPackaging.View = View.Details

        Lv_DetailScrap.Items.Clear()
        Lv_DetailScrap.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
        Lv_DetailScrap.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        Lv_DetailScrap.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
        Lv_DetailScrap.Columns.Add("Jumlah", 180, HorizontalAlignment.Right)
        Lv_DetailScrap.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_DetailScrap.Columns.Add("Proses", 0, HorizontalAlignment.Center)
        'Hide
        Lv_DetailScrap.Columns.Add("Barcode", 300, HorizontalAlignment.Left)
        Lv_DetailScrap.Columns.Add("Keranjang", 80, HorizontalAlignment.Left).DisplayIndex = 1
        Lv_DetailScrap.View = View.Details

        Lv_DetailScrap.Columns(6).DisplayIndex = 0

        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear()
            Cmb_Lokasi.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)

            'xSplit = CekKotaRole().Split(", ")

            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and kode_kota in( "
            'For i As Integer = 0 To xSplit.Count - 1
            '    SQL = SQL & "'" & xSplit(i).Trim & "', "
            'Next
            'SQL = Strings.Left(SQL, Len(SQL) - 2)

            'SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            'ComboBox1.Items.Add("Seluruh")
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using

            Cmb_Lokasi.Text = Lokasi

            'If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
            '    ComboBox6.Enabled = False
            'Else
            '    ComboBox6.Enabled = True
            'End If

            'ComboBox3.Items.Add("Y") : Arr4.Add("Y")
            'ComboBox3.Items.Add("T") : Arr4.Add("T")
            'ComboBox3.SelectedIndex = 1

            Cmb_ParamTgl.Items.Clear() : Arr1.Clear()
            Cmb_ParamTgl.Items.Add("Tanggal") : Arr1.Add("a.Tanggal")
            Cmb_ParamTgl.Items.Add("Tanggal Selesai") : Arr1.Add("a.Tgl_Hasil_Produksi")

            'TextBoxa.Text = "0"
            Cmb_ParamTgl.Enabled = False : Cmb_ParamLain.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Txt_ParamValue.Enabled = False

            Cmb_ParamLain.Items.Clear() : Cmb_ParamLain.Text = "" : Arr2.Clear()
            Cmb_ParamLain.Items.Add(Base_Language.Lang_Global_No_Transaksi) : Arr2.Add("a.no_transaksi")
            Cmb_ParamLain.Items.Add("No Production Order") : Arr2.Add("a.No_PO")
            Cmb_ParamLain.Items.Add("User ID") : Arr2.Add("a.UserID")
            Cmb_ParamLain.Items.Add("Kode Barang") : Arr2.Add("a.Kode_Barang")
            Cmb_ParamLain.Items.Add("Nama Barang") : Arr2.Add("b.Nama")
            Cmb_ParamLain.Items.Add("Satuan") : Arr2.Add("a.satuan")

            Label1.Text = "Display - Production Result"
            Cb_TransaksiHrIni.Text = Base_Language.Lang_Global_Hari_ini
            Cb_ParamTgl.Text = Base_Language.Lang_Global_Para_Tbl
            Cb_ParamLain.Text = Base_Language.Lang_Global_Para_lain
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            CloseConn()
        Catch ex As Exception
            Cmb_Lokasi.Items.Clear()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_TransaksiHrIni.CheckedChanged
        If Cb_TransaksiHrIni.Checked = True Then
            Cb_ParamTgl.Checked = False
            BtnBarangMasuk_Cari_Click(Cb_TransaksiHrIni, e)
        End If
    End Sub

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Try
            pertama = 1

            If Cb_ParamTgl.Checked = False And Cb_ParamLain.Checked = False And Cb_TransaksiHrIni.Checked = False Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Judul)
                Cb_ParamTgl.Focus() : Exit Sub
            End If

            If Cb_ParamTgl.Checked Then
                If Cmb_ParamTgl.SelectedIndex = -1 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Judul)
                    Cmb_ParamTgl.Focus() : Exit Sub
                ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                    MessageBox.Show("Periode I " & Base_Language.Lang_Global_TidakBolehLebihDari & " periode II!", Judul)
                    DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                    Exit Sub
                End If
            ElseIf Cb_ParamLain.Checked Then
                If Cmb_ParamLain.SelectedIndex = -1 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain, Judul)
                    Cmb_ParamLain.Focus() : Exit Sub
                ElseIf Txt_ParamValue.Text.Trim.Length = 0 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Judul)
                    Txt_ParamValue.Focus() : Exit Sub
                End If
            End If

            OpenConn()

            Lv_ProductionResult.Items.Clear()
            Lv_DetailFinishedGood.Items.Clear()
            Lv_DetailRawMaterial.Items.Clear()
            Lv_DetailPackaging.Items.Clear()
            Lv_DetailScrap.Items.Clear()

            SQL = "select a.No_Transaksi, a.No_PO, a.Tanggal, a.Jam, a.UserID, a.Kode_Barang, b.Nama, a.Jumlah, a.satuan, a.Catatan,  a.Flag_Hasil_Produksi, a.Tgl_Hasil_Produksi, a.Jam_Hasil_Produksi "
            SQL = SQL & "from Emi_Split_Production_Order a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan  "
            SQL = SQL & "and a.Kode_Stock_Owner = b.Kode_Stock_Owner and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' and a.status is null "

            If Cb_TransaksiHrIni.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If Cb_ParamTgl.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "and "

                SQL = SQL & Arr1.Item(Cmb_ParamTgl.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If Cb_ParamLain.Checked Then
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr2.Item(Cmb_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamValue.Text) & "%' "
            End If

            SQL = SQL & "order by No_PO, No_Transaksi, Tanggal, Jam, Flag_Hasil_Produksi"

            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = Lv_ProductionResult.Items.Add(.Rows(i).Item("No_Transaksi"))
                            Lvw.SubItems.Add(.Rows(i).Item("No_PO"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            Lvw.SubItems.Add(.Rows(i).Item("Jam"))
                            Lvw.SubItems.Add(.Rows(i).Item("UserID"))
                            Lvw.SubItems.Add(.Rows(i).Item("Kode_Barang"))
                            Lvw.SubItems.Add(.Rows(i).Item("Nama"))
                            Lvw.SubItems.Add(.Rows(i).Item("Jumlah"))
                            Lvw.SubItems.Add(.Rows(i).Item("satuan"))
                            Lvw.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Catatan")) = "", "-", General_Class.CekNULL(.Rows(i).Item("Catatan"))))
                            Lvw.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Tgl_Hasil_Produksi")) = "", "-", Format(.Rows(i).Item("Tgl_Hasil_Produksi"), "dd MMM yyyy")))
                            Lvw.SubItems.Add(If(General_Class.CekNULL(.Rows(i).Item("Jam_Hasil_Produksi")) = "", "-", General_Class.CekNULL(.Rows(i).Item("Jam_Hasil_Produksi"))))
                            'Hide
                            Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("Flag_Hasil_Produksi")))

                            If General_Class.CekNULL(.Rows(i).Item("Flag_Hasil_Produksi")) = "Y" Then
                                Lvw.BackColor = Color.LightGreen
                            Else
                                Lvw.BackColor = Color.LightYellow
                            End If

                        Next
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

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_ProductionResult.SelectedIndexChanged

        If Lv_ProductionResult.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()
            Lv_DetailFinishedGood.Items.Clear()
            Lv_DetailRawMaterial.Items.Clear()
            Lv_DetailPackaging.Items.Clear()
            Lv_DetailScrap.Items.Clear()

            'Finished Good
            SQL = "select b.No_Transaksi, a.No_Production_Order, sum(b.Jumlah) as Jumlah, b.Satuan, c.Keterangan as Kualitas, "
            SQL = SQL & "b.Kode_Unik_Berjalan, b.Qr_Code, b.Tgl_Expired, b.Nomor "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_detail_Pallet b, EMI_Master_Warna c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Jenis = c.Kode_Warna "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text & "' "
            SQL = SQL & "group by b.No_Transaksi, a.No_Production_Order, b.Satuan, c.Keterangan, "
            SQL = SQL & "b.Kode_Unik_Berjalan, b.Qr_Code, b.Tgl_Expired, b.Nomor "
            SQL = SQL & "order by b.nomor"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_DetailFinishedGood.Items.Add(Dr("No_Transaksi"))
                    lvw.SubItems.Add(Dr("No_Production_Order"))
                    lvw.SubItems.Add(Dr("Qr_Code") + "-" + Dr("Kode_Unik_Berjalan"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    lvw.SubItems.Add(Dr("Satuan"))
                    lvw.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Dr("Kualitas"))
                    lvw.SubItems.Add(Dr("Nomor"))
                Loop
            End Using

            'Raw Material
            SQL = "select b.No_Transaksi, b.kode_stock_owner, b.Kode_Barang, c.Nama as Nama_Barang, "
            SQL = SQL & "b.Nilai_Formula, b.Nilai_Produksi, b.Satuan "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and  b.Kode_Barang = c.Kode_Barang and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_DetailRawMaterial.Items.Add(Dr("No_Transaksi"))
                    lvw.SubItems.Add(Dr("kode_stock_owner"))
                    lvw.SubItems.Add(Dr("Kode_Barang"))
                    lvw.SubItems.Add(Dr("Nama_Barang"))
                    lvw.SubItems.Add(Format(Dr("Nilai_Produksi"), "N4"))
                    lvw.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            'Packaging
            SQL = "select b.No_Transaksi, b.kode_stock_owner, b.Kode_Barang, c.Nama as Nama_Barang, "
            SQL = SQL & "b.Nilai_Formula, b.Nilai_Produksi, b.Satuan "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Packaging_detail b, barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi and  b.Kode_Barang = c.Kode_Barang and b.Kode_Stock_Owner = c.Kode_Stock_Owner "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_DetailPackaging.Items.Add(Dr("No_Transaksi"))
                    lvw.SubItems.Add(Dr("kode_stock_owner"))
                    lvw.SubItems.Add(Dr("Kode_Barang"))
                    lvw.SubItems.Add(Dr("Nama_Barang"))
                    lvw.SubItems.Add(Format(Dr("Nilai_Produksi"), "N4"))
                    lvw.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            SQL = "select b.No_Transaksi, d.Kode_Barang, d.Nama as Nama_Barang, sum(b.Jumlah) as Jumlah, b.Satuan, b.proses,(c.Qr_Code + '-' + c.Kode_Unik_Berjalan) as Barcode, b.nomor "
            SQL = SQL & "from Emi_Production_Results a, EMI_Production_Results_Detail_Scrap b, Barang_SN c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Serial_Number = c.Serial_Number "
            SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text & "' "
            SQL = SQL & "group by b.No_Transaksi, d.Kode_Barang, d.Nama, b.Satuan, b.proses, (c.Qr_Code + '-' + c.Kode_Unik_Berjalan), b.nomor "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_DetailScrap.Items.Add(Dr("No_Transaksi"))
                    Lvw.SubItems.Add(Dr("Kode_Barang"))
                    Lvw.SubItems.Add(Dr("Nama_Barang"))
                    Lvw.SubItems.Add(Format(Dr("Jumlah"), "N4"))
                    Lvw.SubItems.Add(Dr("Satuan"))
                    Lvw.SubItems.Add(Dr("Proses"))
                    Lvw.SubItems.Add(Dr("Barcode"))
                    Lvw.SubItems.Add(Dr("nomor"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CopyNoTransaksiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyNoTransaksiToolStripMenuItem.Click
        If Lv_ProductionResult.Items.Count = 0 Or Lv_ProductionResult.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Pilih_Dahulu_No_Transaksi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_ProductionResult.FocusedItem.Text)
    End Sub

    'Private Sub LaporanDetailBatchMaterialToolStripMenuItem_Click(sender As Object, e As EventArgs)
    '    If Lv_ProductionResult.Items.Count = 0 Then Exit Sub

    '    If Lv_ProductionResult.Items.Count = 0 Or Lv_ProductionResult.SelectedItems.Count = 0 Then
    '        MessageBox.Show("Pilih dahulu data yang akan dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    End If

    '    Try
    '        OpenConn()

    '        Dim CrDoc As New Object
    '        Dim kertas As String = ""

    '        Dim SF As String = ""
    '        SQL = "select Kode_Perusahaan from View_Laporan_Hasil_QC where Kode_Perusahaan = '" & KodePerusahaan & "' and "
    '        SQL = SQL & "No_Fak_Loading_Barang = '" & ListView1.FocusedItem.Text & "' "
    '        SQL = SQL & "and kode_barang = '" & ListView2.FocusedItem.SubItems(1).Text & "' "

    '        SF = "{View_Laporan_Hasil_QC.No_Fak_Loading_Barang} = '" & ListView1.FocusedItem.Text & "' "
    '        SF = SF & "and {View_Laporan_Hasil_QC.kode_perusahaan} = '" & KodePerusahaan & "' "
    '        SF = SF & "and {View_Laporan_Hasil_QC.Kode_Barang} = '" & ListView2.FocusedItem.SubItems(1).Text & "' "
    '        Using Ds = BindingTrans(SQL)
    '            If Ds.Tables("MyTable").Rows.Count <> 0 Then
    '                CrDoc = New Rpt_Laporan_Hasil_QC

    '                'With A_Place_For_Printing2
    '                '    CrDoc.SetDataSource(Ds)
    '                '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                '    CrDoc.PrintOptions.PrinterName = ""
    '                '    CrDoc.RecordSelectionFormula = SF
    '                '    'CrDoc.SummaryInfo.ReportTitle = "Barang Masuk Per Pallet"
    '                '    .Text = "Laporan Hasil QC"
    '                '    .CrystalReportViewer1.ReportSource = CrDoc
    '                '    '.CrystalReportViewer1.DisplayGroupTree = False
    '                '    .Refresh()
    '                '    .Show()
    '                'End With

    '                '============================================================================================================================================
    '                '============================================================================================================================================

    '                kertas = "A4"

    '                CrDoc.SetDataSource(Ds)
    '                CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
    '                CrDoc.PrintOptions.PrinterName = PrinterQC
    '                CrDoc.RecordSelectionFormula = SF
    '                'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

    '                Dim doctoprint As New System.Drawing.Printing.PrintDocument()
    '                doctoprint.PrinterSettings.PrinterName = PrinterQC
    '                'doctoprint.DefaultPageSettings.Landscape = True
    '                Dim rawKind As Integer
    '                CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
    '                For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
    '                    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
    '                        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
    '                        CrDoc.PrintOptions.PaperSize = rawKind
    '                        Exit For
    '                    End If
    '                Next

    '                CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
    '                CrDoc.PrintToPrinter(1, False, 1, 99)

    '                MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            Else
    '                CloseConn()
    '                MessageBox.Show("Data Tidak diTemukan", "Cetak Ulang", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub

    '            End If
    '        End Using

    '        A_Place_For_Printing2.Focus()

    '        CloseConn()
    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    Private Sub DisplayRakToolStripMenuItem_Click(sender As Object, e As EventArgs)
        If Lv_ProductionResult.Items.Count = 0 Or Lv_ProductionResult.SelectedItems.Count = 0 Then
            Exit Sub
        End If
        EMI_Barang_Masuk_Display_Rak.TxtNoBM.Text = Lv_ProductionResult.FocusedItem.Text
        EMI_Barang_Masuk_Display_Rak.ShowDialog()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamTgl.CheckedChanged
        If Cb_ParamTgl.Checked Then
            Cmb_ParamTgl.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Cb_TransaksiHrIni.Checked = False
        Else
            Cmb_ParamTgl.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_ParamTgl.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles Cb_ParamLain.CheckedChanged
        If Cb_ParamLain.Checked Then
            Cmb_ParamLain.Enabled = True : Txt_ParamValue.Enabled = True
        Else
            Cmb_ParamLain.Enabled = False : Txt_ParamValue.Enabled = False
            Cmb_ParamLain.SelectedIndex = -1 : Txt_ParamValue.Text = ""
        End If
    End Sub

    '======= CETAK ULANG ======='

    Private Sub LaporanGIGRToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanGIGRToolStripMenuItem.Click
        If Lv_ProductionResult.Items.Count = 0 Then Exit Sub

        If Not Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_FlagSelesai).Text = "Y" Then
            MessageBox.Show("Order Produksi Belum Selesai", "Production Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            Dim NoPO As String = Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_NoPO).Text
            Dim NoTransaksi As String = Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_NoFak).Text

            SQL = "select Kode_Perusahaan from Vw_Laporan_Perfaktur_GI_GR "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_PO = '" & NoPO & "' and No_Transaksi = '" & NoTransaksi & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New Laporan_Perfaktur_GI_GR
                    kertas = "A4"

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Vw_Laporan_Perfaktur_GI_GR.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Laporan_Perfaktur_GI_GR.No_PO}='" & NoPO & "' and {Vw_Laporan_Perfaktur_GI_GR.No_Transaksi}='" & NoTransaksi & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "Laporan GI GR"
                    '    .Text = "Laporan GI GR"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '============================================================================================================================================
                    '============================================================================================================================================
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterQC
                    CrDoc.RecordSelectionFormula = "{Vw_Laporan_Perfaktur_GI_GR.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Laporan_Perfaktur_GI_GR.No_PO}='" & NoPO & "' and {Vw_Laporan_Perfaktur_GI_GR.No_Transaksi}='" & NoTransaksi & "' "
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterQC
                    doctoprint.DefaultPageSettings.Landscape = True
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 99)

                    MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub LaporanGIGRDetailToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LaporanGIGRDetailToolStripMenuItem.Click
        If Lv_ProductionResult.Items.Count = 0 Then Exit Sub

        If Not Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_FlagSelesai).Text = "Y" Then
            MessageBox.Show("Order Produksi Belum Selesai", "Production Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try
            OpenConn()

            Dim CrDoc As New Object
            Dim kertas As String = ""

            Dim NoPO As String = Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_NoPO).Text
            Dim NoTransaksi As String = Lv_ProductionResult.SelectedItems(0).SubItems(itemPR_NoFak).Text

            SQL = "select Kode_Perusahaan from Vw_Laporan_Perfaktur_GI_GR_Detail "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_PO = '" & NoPO & "' and No_Transaksi = '" & NoTransaksi & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New Laporan_Perfaktur_GI_GR_Detail
                    kertas = "A4"

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Vw_Laporan_Perfaktur_GI_GR_Detail.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Laporan_Perfaktur_GI_GR_Detail.No_PO}='" & NoPO & "' and {Vw_Laporan_Perfaktur_GI_GR_Detail.No_Transaksi}='" & NoTransaksi & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "Laporan GI GR"
                    '    .Text = "Laporan GI GR"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '============================================================================================================================================
                    '============================================================================================================================================
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterQC
                    CrDoc.RecordSelectionFormula = "{Vw_Laporan_Perfaktur_GI_GR.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Vw_Laporan_Perfaktur_GI_GR.No_PO}='" & NoPO & "' and {Vw_Laporan_Perfaktur_GI_GR.No_Transaksi}='" & NoTransaksi & "' "
                    'CrDoc.SummaryInfo.ReportTitle = "Halaman : " & min & "/" & max

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterQC
                    doctoprint.DefaultPageSettings.Landscape = True
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = kertas Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 99)

                    MessageBox.Show("Berhasil Print", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If Lv_ProductionResult.Items.Count = 0 Then
            BatalkanToolStripMenuItem.Visible = False
            BatalkanToolStripMenuItem.Enabled = False
            Exit Sub
        End If
        If Lv_ProductionResult.FocusedItem Is Nothing Then Exit Sub

        Dim selectedSplit As String = Lv_ProductionResult.FocusedItem.Text

        Try
            OpenConn()

            '================================================
            '=     CEK APAKAH SPLIT SUDAH DI GOOD ISSUE     =
            '================================================
            SQL = "select Kode_Perusahaan from Emi_Production_Results where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & selectedSplit & "' and status is null "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    BatalkanToolStripMenuItem.Visible = False
                    BatalkanToolStripMenuItem.Enabled = False
                Else
                    BatalkanToolStripMenuItem.Visible = True
                    BatalkanToolStripMenuItem.Enabled = True
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CetakUlangBarcodeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeToolStripMenuItem.Click
        If Lv_DetailFinishedGood.Items.Count = 0 Then Exit Sub
        Dim SelectedIndex As Integer = Lv_DetailFinishedGood.FocusedItem.Index
        Dim NoSplit As String = Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text


        Dim KdBarang As String = ""
        Dim Qr_Code As String = ""
        Dim TglExp As String = ""
        Dim Batch As String = ""
        Dim Tgl_Produksi As String = ""
        Dim Tgl_Masuk As String = ""
        Dim MetodePengeluaranStok As String = ""
        Dim kode_unik_print As String = ""



        get_jam()

        'Try
        '    OpenConn()
        '    Cmd.Transaction = Cn.BeginTransaction

        '    '======================
        '    '=      GET DATA      =
        '    '======================
        '    'Finished Good
        '    SQL = "select a.Kode_Perusahaan, a.Kode_Barang, b.Qr_Code, Tgl_Expired, b.Batch_Number, b.Tgl_Produksi, b.Tgl_Masuk, a.Metode_Pengeluaran_Stok "
        '    SQL = SQL & "from barang a, barang_sn b "
        '    SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
        '    SQL = SQL & "and a.kode_stock_owner = b.kode_stock_owner "
        '    SQL = SQL & "and a.kode_barang = b.kode_barang "
        '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and b.Qr_Code + '-' + b.Kode_Unik_Berjalan = '" & Lv_DetailFinishedGood.FocusedItem.SubItems(itemFG_FullQR).Text & "' "
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            KdBarang = Dr("Kode_Barang")
        '            Qr_Code = Dr("Qr_Code")
        '            TglExp = If(General_Class.CekNULL(Dr("Tgl_Expired")) = "", "", Dr("Tgl_Expired"))
        '            Batch = Dr("Batch_Number")
        '            Tgl_Produksi = If(General_Class.CekNULL(Dr("Tgl_Produksi")) = "", "", Dr("Tgl_Produksi"))
        '            Tgl_Masuk = If(General_Class.CekNULL(Dr("Tgl_Masuk")) = "", "", Dr("Tgl_Masuk"))
        '            MetodePengeluaranStok = Dr("Metode_Pengeluaran_Stok")
        '        Else
        '            Dr.Close()
        '            CloseTrans()
        '            CloseConn()
        '            MessageBox.Show("Data Tidak Ditemukan", "Production Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '            Exit Sub
        '        End If
        '    End Using

        '    '==================================
        '    '=      GENERATE NEW BARCODE      =
        '    '==================================

        '    kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

        '    Dim fullNewQr As String = Lv_DetailFinishedGood.Items(SelectedIndex).SubItems(itemFG_FullQR).Text

        '    Barcode.Image = Generate_QR_NoPadding(fullNewQr)

        '    Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")

        '    '   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

        '    'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
        '    Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
        '    'End If

        '    fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
        '    FileSize1 = fs1.Length
        '    rawData1 = New Byte(FileSize1) {}
        '    fs1.Read(rawData1, 0, FileSize1)
        '    fs1.Close()
        '    Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1

        '    ''INSERT TABEL CETAK QR
        '    'SQL = "insert into Cetak_Finish_Good (Kode_Perusahaan, Kode_Barang, Barcode, QrUtuh, Qr, Tgl_Expired, batch, tgl_produksi, kode_unik_print, tanggal_masuk, metode_pengeluaran_stok) values "
        '    ''SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_KdBarang.Text & "', @newBarcode, '" & Txt_NamaBarang.Text & "', "
        '    'SQL = SQL & "('" & KodePerusahaan & "', '" & KdBarang & "', @newBarcode, "
        '    'SQL = SQL & "'" & fullNewQr & "', '" & Qr_Code & "', '" & Format(Date.Parse(TglExp), "yyyy-MM-dd") & "', '" & Batch & "',  '" & Format(Date.Parse(Tgl_Produksi), "yyyy-MM-dd") & "', "
        '    'SQL = SQL & "'" & kode_unik_print & "', '" & If(String.IsNullOrEmpty(Tgl_Masuk), "", Format(Date.Parse(Tgl_Masuk), "yyyy-MM-dd")) & "', '" & MetodePengeluaranStok & "'"
        '    'SQL = SQL & ")"
        '    'ExecuteTrans(SQL)

        '    'HAPUS TABEL SEMENTARA
        '    SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_1"
        '    ExecuteTrans(SQL)

        '    SQL = "select a.No_Production_Order, c.Kode_Barang, d.Nama as Nama_Barang, b.proses, b.tahap, b.Jumlah, b.Satuan, b.Troli, b.nomor, e.Id_Routing, f.Keterangan as Routing "
        '    SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_detail_Pallet b, Emi_Split_Production_Order c, barang d, EMI_Order_Produksi e, EMI_Master_Routing f "
        '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and c.Kode_Perusahaan = e.Kode_Perusahaan "
        '    SQL = SQL & "and e.Kode_Perusahaan = f.Kode_Perusahaan "
        '    SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
        '    SQL = SQL & "and a.No_Production_Order = c.no_transaksi "
        '    SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
        '    SQL = SQL & "and c.No_PO = e.No_Faktur "
        '    SQL = SQL & "and e.Id_Routing = f.Id_Routing "
        '    SQL = SQL & "and a.Status is null and c.Status is null and e.Status is null "
        '    SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and a.No_Production_Order = '" & NoSplit & "' "
        '    SQL = SQL & "and (b.Qr_Code + '-' + b.Kode_Unik_Berjalan) = '" & fullNewQr & "' "
        '    Using Ds = BindingTrans(SQL)
        '        With Ds.Tables("MyTable")
        '            If .Rows.Count <> 0 Then
        '                For i As Integer = 0 To .Rows.Count - 1

        '                    SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_1 (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
        '                    SQL = SQL & "Proses, Tahap, Jumlah, Satuan, Troli, Nomor, id_routing, routing, Kode_unik_print)  "
        '                    SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("No_Production_Order") & "', @newBarcode, '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', '" & fullNewQr & "', '" & Qr_Code & "', "
        '                    SQL = SQL & "'" & Format(Date.Parse(Tgl_Produksi), "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("proses") & "', '" & .Rows(i).Item("tahap") & "', '" & .Rows(i).Item("Jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
        '                    SQL = SQL & "'" & .Rows(i).Item("Troli") & "', '" & .Rows(i).Item("nomor") & "', '" & .Rows(i).Item("Id_Routing") & "', '" & .Rows(i).Item("Routing") & "', '" & kode_unik_print & "') "
        '                    ExecuteTrans(SQL)

        '                Next
        '            Else
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show("Production Result Tidak Ditemukann", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        End With
        '    End Using

        '    Cmd.Transaction.Commit()
        '    CloseTrans()
        '    CloseConn()
        'Catch ex As Exception
        '    CloseTrans()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        '===========================
        '=      CETAK BARCODE      =
        '===========================

        Try
            OpenConn()
            Dim CrDoc As New Object

            Dim KertasBesar As String = "BarcodeFG"
            Dim KertasKecil As String = "BarcodeQC"

            'SQL = "select Kode_Perusahaan from N_EMI_Barcode_Label_Barcode_GR_1 where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & KdBarang & "' and Kode_Unik_Print = '" & kode_unik_print & "' "
            SQL = "select Kode_Perusahaan from N_EMI_Barcode_Label_Barcode_GR_1 where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='BRG08240005' and Kode_Unik_Print = '030609493900193' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New N_EMI_Label_Barcode_GR_1

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "

                    '    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    '    Dim rawKind As Integer
                    '    Dim foundPaper As Boolean = False
                    '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    '    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                    '            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '            CrDoc.PrintOptions.PaperSize = rawKind
                    '            foundPaper = True
                    '            Exit For
                    '        End If
                    '    Next

                    '    If Not foundPaper Then
                    '        CloseConn()
                    '        MessageBox.Show("Kertas Tidak Ditemukan", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If

                    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                    '    .Text = "New Barcode Finish Good"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '========================================================================================================================================================================================

                    With A_Place_For_Printing2
                        CrDoc.SetDataSource(Ds)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.PrintOptions.PrinterName = ""
                        'CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_1.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Barang} = '" & KdBarang & "' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Unik_Print} = '" & kode_unik_print & "' "
                        CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_1.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Barang} = 'BRG08240005' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Unik_Print} = '030609493900193' "
                        CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
                        .Text = "Faktur Premix Label"
                        .CrystalReportViewer1.ReportSource = CrDoc
                        .Refresh()
                        .Show()
                    End With

                    '========================================================================================================================================================================================

                    '==========================
                    '=     BARCODEE BESAR     =
                    '==========================
                    'Dim doctoprint As New System.Drawing.Printing.PrintDocument()

                    'CrDoc.SetDataSource(Ds)
                    'CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    ''CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_1.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Barang} = '" & KdBarang & "' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Unik_Print} = '" & kode_unik_print & "' "
                    'CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_1.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Barang} = 'BRG08240005' and {N_EMI_Barcode_Label_Barcode_GR_1.Kode_Unik_Print} = '030609493900193' "
                    'CrDoc.PrintOptions.PrinterName = PrinterBarcode

                    'doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    'Dim rawKind As Integer
                    'Dim foundPaper As Boolean = False
                    'CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    'For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '    If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                    '        rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '        CrDoc.PrintOptions.PaperSize = rawKind
                    '        foundPaper = True
                    '        Exit For
                    '    End If
                    'Next

                    'If Not foundPaper Then
                    '    'CloseConn()
                    '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    '    MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    'Exit Sub
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

        MessageBox.Show("Behasil Cetak", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Sub

    Private Sub CetakUlangBarcodeFGToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeFGToolStripMenuItem.Click
        If Lv_DetailScrap.Items.Count = 0 Then Exit Sub

        Dim kode_unik_print As String
        Dim SelectedIndex As Integer = Lv_DetailScrap.FocusedItem.Index
        Dim NoSplit As String = Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text
        Dim Selected_Barcode As String = Lv_DetailScrap.FocusedItem.SubItems(6).Text

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '======================
            '=      GET DATA      =
            '======================

            'HAPUS TABEL SEMENTARA
            SQL = "truncate table N_EMI_Barcode_Label_Barcode_GR_1_Scrap "
            ExecuteTrans(SQL)

            SQL = "select a.No_Production_Order, d.Kode_Barang, e.Nama as Nama_Barang, (c.Qr_Code +'-' + c.Kode_Unik_Berjalan) as Barcode, c.Qr_Code,  "
            SQL = SQL & "d.Tgl_Produksi, c.Proses, sum(c.jumlah) as jumlah, c.Satuan, c.nomor, f.Id_Routing, g.Keterangan as Routing "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_detail_barang b, Emi_Production_Results_Detail_Scrap c, Emi_Split_Production_Order d, Barang e, EMI_Order_Produksi f, EMI_Master_Routing g "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and a.Kode_Perusahaan = d.Kode_Perusahaan and b.Kode_Perusahaan = e.kode_perusahaan "
            SQL = SQL & "and d.Kode_Perusahaan = f.Kode_Perusahaan and g.Kode_Perusahaan = g.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
            SQL = SQL & "and a.No_Production_Order = d.No_Transaksi "
            SQL = SQL & "and b.Kode_Stock_Owner = e.Kode_Stock_Owner and b.Kode_Barang_scrap = e.Kode_Barang "
            SQL = SQL & "and d.No_PO = f.No_Faktur "
            SQL = SQL & "and g.Id_Routing = f.Id_Routing "
            SQL = SQL & "and a.Status is null and d.Status is null and f.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & NoSplit & "' "
            SQL = SQL & "and (c.Qr_Code +'-' + c.Kode_Unik_Berjalan) = '" & Selected_Barcode & "' "
            SQL = SQL & "group by a.No_Production_Order, d.Kode_Barang, e.Nama, (c.Qr_Code +'-' + c.Kode_Unik_Berjalan), c.Qr_Code,  "
            SQL = SQL & "d.Tgl_Produksi, c.Proses, c.Satuan, c.nomor, f.Id_Routing, g.Keterangan"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            '==================================
                            '=      GENERATE New BARCODE      =
                            '==================================
                            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

                            Dim fullNewQr As String = .Rows(i).Item("Barcode")

                            Barcode.Image = Generate_QR_NoPadding(fullNewQr)

                            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")

                            '   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

                            'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
                            Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
                            'End If

                            fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
                            FileSize1 = fs1.Length
                            rawData1 = New Byte(FileSize1) {}
                            fs1.Read(rawData1, 0, FileSize1)
                            fs1.Close()
                            Cmd.Parameters.Add("@newBarcodescrap" & kode_unik_print, SqlDbType.Image).Value = rawData1

                            SQL = "insert into N_EMI_Barcode_Label_Barcode_GR_1_Scrap (kode_perusahaan, no_split, Barcode, Kode_barang, Nama_Barang, QrUtuh, Qr, Tgl_Produksi, Jam_Produksi, "
                            SQL = SQL & "Proses, Jumlah, Satuan, Nomor, id_routing, routing, Kode_unik_print)  "
                            SQL = SQL & "values ('" & KodePerusahaan & "', '" & .Rows(i).Item("No_Production_Order") & "', @newBarcodescrap" & kode_unik_print & ", '" & .Rows(i).Item("Kode_Barang") & "', '" & .Rows(i).Item("Nama_Barang") & "', "
                            SQL = SQL & "'" & fullNewQr & "', '" & .Rows(i).Item("Qr_Code") & "',  "
                            SQL = SQL & "'" & Format(.Rows(i).Item("Tgl_Produksi"), "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "', '" & .Rows(i).Item("Proses") & "', '" & .Rows(i).Item("jumlah") & "', '" & .Rows(i).Item("Satuan") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("nomor") & "', '" & .Rows(i).Item("Id_Routing") & "', '" & .Rows(i).Item("Routing") & "', '" & kode_unik_print & "') "
                            ExecuteTrans(SQL)

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Production Result Tidak Ditemukann", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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

        '===========================
        '=      CETAK BARCODE      =
        '===========================

        Try
            OpenConn()
            Dim CrDoc As New Object

            Dim KertasBesar As String = "BarcodeFG"
            Dim KertasKecil As String = "BarcodeQC"

            SQL = "select Kode_Perusahaan from N_EMI_Barcode_Label_Barcode_GR_1_Scrap where Kode_Perusahaan='" & KodePerusahaan & "'  and Kode_Unik_Print = '" & kode_unik_print & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New N_EMI_Label_Barcode_GR_1_Scrap

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "

                    '    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    '    Dim rawKind As Integer
                    '    Dim foundPaper As Boolean = False
                    '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    '    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                    '        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                    '            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                    '            CrDoc.PrintOptions.PaperSize = rawKind
                    '            foundPaper = True
                    '            Exit For
                    '        End If
                    '    Next

                    '    If Not foundPaper Then
                    '        CloseConn()
                    '        MessageBox.Show("Kertas Tidak Ditemukan", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If

                    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                    '    .Text = "New Barcode Finish Good"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '========================================================================================================================================================================================

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
                    '    .Text = "Faktur Premix Label"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '========================================================================================================================================================================================

                    '==========================
                    '=     BARCODEE BESAR     =
                    '==========================
                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{N_EMI_Barcode_Label_Barcode_GR_1_Scrap.Kode_Perusahaan} = '" & KodePerusahaan & "' and {N_EMI_Barcode_Label_Barcode_GR_1_Scrap.Kode_Unik_Print} = '" & kode_unik_print & "' "
                    CrDoc.PrintOptions.PrinterName = PrinterBarcode

                    doctoprint.PrinterSettings.PrinterName = PrinterBarcode

                    Dim rawKind As Integer
                    Dim foundPaper As Boolean = False
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = KertasBesar Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            foundPaper = True
                            Exit For
                        End If
                    Next

                    If Not foundPaper Then
                        'CloseConn()
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        MessageBox.Show("Kertas Tidak Ditemukan, Menggunakan Kertas Default", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'Exit Sub
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
    End Sub

    Private Sub CetakUlangBarcodeQCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangBarcodeQCToolStripMenuItem.Click

        Exit Sub

        If Lv_DetailFinishedGood.Items.Count = 0 Then Exit Sub

        Dim KdBarang, Qr_Code, TglExp, Tgl_Produksi, Tgl_Masuk, Batch, MetodePengeluaranStok As String
        Dim kode_unik_print As String
        Dim SelectedIndex As Integer = Lv_DetailFinishedGood.FocusedItem.Index

        Try
            OpenConn()

            '======================
            '=      GET DATA      =
            '======================
            'Finished Good
            SQL = "select a.Kode_Perusahaan, a.Kode_Barang, b.Qr_Code, Tgl_Expired, b.Batch_Number, b.Tgl_Produksi, b.Tgl_Masuk, a.Metode_Pengeluaran_Stok "
            SQL = SQL & "from barang a, barang_sn b "
            SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.kode_stock_owner = b.kode_stock_owner "
            SQL = SQL & "and a.kode_barang = b.kode_barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.Qr_Code + '-' + b.Kode_Unik_Berjalan = '" & Lv_DetailFinishedGood.FocusedItem.SubItems(itemFG_FullQR).Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    KdBarang = Dr("Kode_Barang")
                    Qr_Code = Dr("Qr_Code")
                    TglExp = If(General_Class.CekNULL(Dr("Tgl_Expired")) = "", "", Dr("Tgl_Expired"))
                    Batch = Dr("Batch_Number")
                    Tgl_Produksi = If(General_Class.CekNULL(Dr("Tgl_Produksi")) = "", "", Dr("Tgl_Produksi"))
                    Tgl_Masuk = If(General_Class.CekNULL(Dr("Tgl_Masuk")) = "", "", Dr("Tgl_Masuk"))
                    MetodePengeluaranStok = Dr("Metode_Pengeluaran_Stok")
                Else
                    Dr.Close()
                    CloseConn()
                    MessageBox.Show("Data Tidak Ditemukan", "Production Result", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '==================================
            '=      GENERATE NEW BARCODE      =
            '==================================
            'HAPUS TABEL SEMENTARA
            SQL = "truncate table Cetak_Finish_Good "
            ExecuteTrans(SQL)

            kode_unik_print = Format(tgl_skg, "MMddHHmmss") & Format(random.Next(0, 10000), "00000")

            Dim fullNewQr As String = Lv_DetailFinishedGood.Items(SelectedIndex).SubItems(itemFG_FullQR).Text

            Barcode.Image = Generate_QR(fullNewQr)

            Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeTfStock" & kode_unik_print & ".jpg")

            '   Dim FileToSaveAs1 As String = System.IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "newBarcodeFinishGood.jpg")

            'If Not (System.IO.File.Exists(FileToSaveAs1)) Then
            Barcode.Image.Save(FileToSaveAs1, System.Drawing.Imaging.ImageFormat.Jpeg)
            'End If

            fs1 = New FileStream(FileToSaveAs1, FileMode.Open, FileAccess.Read)
            FileSize1 = fs1.Length
            rawData1 = New Byte(FileSize1) {}
            fs1.Read(rawData1, 0, FileSize1)
            fs1.Close()
            Cmd.Parameters.Add("@newBarcode", SqlDbType.Image).Value = rawData1

            'INSERT TABEL CETAK QR
            SQL = "insert into Cetak_Finish_Good (Kode_Perusahaan, Kode_Barang, Barcode, QrUtuh, Qr, Tgl_Expired, batch, tgl_produksi, kode_unik_print, tanggal_masuk, metode_pengeluaran_stok) values "
            'SQL = SQL & "('" & KodePerusahaan & "', '" & Txt_KdBarang.Text & "', @newBarcode, '" & Txt_NamaBarang.Text & "', "
            SQL = SQL & "('" & KodePerusahaan & "', '" & KdBarang & "', @newBarcode, "
            SQL = SQL & "'" & fullNewQr & "', '" & Qr_Code & "', '" & Format(Date.Parse(TglExp), "yyyy-MM-dd") & "', '" & Batch & "',  '" & Format(Date.Parse(Tgl_Produksi), "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & kode_unik_print & "', '" & If(String.IsNullOrEmpty(Tgl_Masuk), "", Format(Date.Parse(Tgl_Masuk), "yyyy-MM-dd")) & "', '" & MetodePengeluaranStok & "'"
            SQL = SQL & ")"
            ExecuteTrans(SQL)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        '===========================
        '=      CETAK BARCODE      =
        '===========================

        Try
            OpenConn()
            Dim CrDoc As New Object

            Dim KertasBesar As String = "BarcodeFG"
            Dim KertasKecil As String = "BarcodeQC"

            SQL = "select Kode_Perusahaan from Cetak_Finish_Good where Kode_Perusahaan='" & KodePerusahaan & "' and Kode_Barang='" & KdBarang & "' and Kode_Unik_Print = '" & kode_unik_print & "' "
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then

                    CrDoc = New NewBarcodeFinishGoodKecil

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "

                    '    Dim doctoprint2 As New System.Drawing.Printing.PrintDocument()
                    '    doctoprint2.PrinterSettings.PrinterName = PrinterBarcodeQC
                    '    Dim rawKind2 As Integer
                    '    Dim foundPaper As Boolean = False
                    '    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    '    For i = 0 To doctoprint2.PrinterSettings.PaperSizes.Count - 1
                    '        If doctoprint2.PrinterSettings.PaperSizes(i).PaperName = KertasKecil Then
                    '            rawKind2 = CInt(doctoprint2.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint2.PrinterSettings.PaperSizes(i)))
                    '            CrDoc.PrintOptions.PaperSize = rawKind2
                    '            foundPaper = True
                    '            Exit For
                    '        End If
                    '    Next

                    '    If Not foundPaper Then
                    '        CloseConn()
                    '        MessageBox.Show("Kertas Tidak Ditemukan", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '        Exit Sub
                    '    End If

                    '    CrDoc.SummaryInfo.ReportTitle = "New Barcode Finish Good"
                    '    .Text = "New Barcode Finish Good"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '========================================================================================================================================================================================

                    'With A_Place_For_Printing2
                    '    CrDoc.SetDataSource(Ds)
                    '    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    '    CrDoc.PrintOptions.PrinterName = ""
                    '    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                    '    CrDoc.SummaryInfo.ReportTitle = "Faktur Premix Label"
                    '    .Text = "Faktur Premix Label"
                    '    .CrystalReportViewer1.ReportSource = CrDoc
                    '    .Refresh()
                    '    .Show()
                    'End With

                    '========================================================================================================================================================================================

                    '==========================
                    '=     BARCODEE KECIL     =
                    '==========================

                    Dim doctoprint2 As New System.Drawing.Printing.PrintDocument()

                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.RecordSelectionFormula = "{Cetak_Finish_Good.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Cetak_Finish_Good.Kode_Barang} = '" & KdBarang & "' and {Cetak_Finish_Good.Kode_Unik_Print} = '" & kode_unik_print & "' "
                    CrDoc.PrintOptions.PrinterName = PrinterBarcodeQC

                    doctoprint2.PrinterSettings.PrinterName = PrinterBarcodeQC

                    Dim rawKind2 As Integer
                    Dim foundPaper As Boolean = False
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = 0 To doctoprint2.PrinterSettings.PaperSizes.Count - 1
                        If doctoprint2.PrinterSettings.PaperSizes(i).PaperName = KertasKecil Then
                            rawKind2 = CInt(doctoprint2.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint2.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind2
                            foundPaper = True
                            Exit For
                        End If
                    Next

                    If Not foundPaper Then
                        'CloseConn()
                        CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                        MessageBox.Show("Kertas Tidak Ditemukan", "Cetak Ulang Barcode", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        'Exit Sub
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

    End Sub

    '========================================================================
    '=     PEMBATALAN
    '========================================================================

    Private Sub BatalkanToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanToolStripMenuItem.Click
        If Lv_ProductionResult.Items.Count = 0 Or Lv_ProductionResult.FocusedItem.Index = -1 Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Production Result"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Good_Issue") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Production Result", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Transaksi Production Result Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim No_Split As String = Lv_ProductionResult.FocusedItem.Text

            '=========================================================
            '=     CEK APAKAH SPLIT SUDAH DI BATALKAN SEBELUMNYA     =
            '=========================================================
            SQL = "select status from Emi_Split_Production_Order where kode_perusahaan = '" & KodePerusahaan & "' and no_transaksi = '" & No_Split & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan Karena No Split Sudah Dibatalkan Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("No Split Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '================================================
            '=     CEK APAKAH SPLIT SUDAH DI GOOD ISSUE     =
            '================================================
            SQL = "select Kode_Perusahaan from Emi_Production_Results where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & No_Split & "' and status is null "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan Karena No Split Belum pada Step Good Issue", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '================================================
            '=     CEK APAKAH SPLIT SUDAH GOOD RECEIVED     =
            '================================================
            SQL = "select b.Kode_Perusahaan "
            SQL = SQL & "from emi_production_results a, emi_production_results_detail_barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null and b.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Tidak Bisa Dilanjutkan karena No Split Sudah Masuk Ketahap Good Received", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '====================================
            '=     MASUK KE STEP PEMBATALAN     =
            '====================================

            'PEMBATALAN PRODUCTION RESULT DETAIL
            SQL = "select a.No_Transaksi from Emi_Production_Results a, Emi_Production_Results_Detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Transaksi = b.No_Transaksi and a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.No_Production_Order = '" & No_Split & "' and a.Status is null  "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dim NoPR As String = Dr("No_Transaksi")
                    Dr.Close()

                    SQL = "update Emi_Production_Results_Detail set status = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & NoPR & "'"
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Production Detail Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            SQL = "select Kode_Perusahaan from Emi_Production_Results where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & No_Split & "' and status is null "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    SQL = "update Emi_Production_Results set Status = 'Y' where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Production_Order = '" & No_Split & "' and status is null "
                    ExecuteTrans(SQL)
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Production Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Pembatalan Production Result Berhasil", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(e, sender)

    End Sub

    Private Sub BatalkanScrapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanScrapToolStripMenuItem.Click
        If Lv_DetailFinishedGood.Items.Count = 0 Or Lv_DetailFinishedGood.FocusedItem.Index = -1 Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Scrap"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Scrap_GR") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Scrap!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Scrap Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim No_Split As String = Lv_ProductionResult.FocusedItem.Text
            Dim No_Transaksi As String = Lv_DetailScrap.FocusedItem.SubItems(0).Text
            Dim Kd_Barang As String = Lv_DetailScrap.FocusedItem.SubItems(1).Text
            Dim Jumlah As String = Lv_DetailScrap.FocusedItem.SubItems(3).Text
            Dim Satuan As String = Lv_DetailScrap.FocusedItem.SubItems(4).Text
            Dim Proses As String = Lv_DetailScrap.FocusedItem.SubItems(5).Text
            Dim Barcode As String = Lv_DetailScrap.FocusedItem.SubItems(6).Text
            Dim No_Keranjang As String = Lv_DetailScrap.FocusedItem.SubItems(7).Text

            '==========================
            '=     CEK DATA SCRAP     =
            '==========================
            SQL = "select top 1 b.Serial_Number, b.Urut_HPP "
            SQL = SQL & "from Emi_Production_Results a, EMI_Production_Results_Detail_Scrap b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Proses = '" & Proses & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code + '-' + b.kode_unik_berjalan) = '" & Barcode & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Scrap Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '========================
            '=     ROLLBACK HPP     =
            '========================
            SQL = "select b.No_Transaksi, b.Proses, b.Nomor, b.Jumlah, b.Serial_Number, b.Urut_HPP "
            SQL = SQL & "from Emi_Production_Results a, EMI_Production_Results_Detail_Scrap b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Proses = '" & Proses & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code + '-' + b.kode_unik_berjalan) = '" & Barcode & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "select satuan, urut "
                            SQL = SQL & "from Emi_Production_Results_hpp "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                            SQL = SQL & "and Urut = '" & .Rows(i).Item("Urut_HPP") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                        Dim Jumlah_Kurang As Double = 0

                                        SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Kd_Barang & "', '" & Satuan & "', "
                                        SQL = SQL & "'" & Ds1.Tables("MyTable").Rows(j).Item("satuan") & "', '" & .Rows(i).Item("Jumlah") & "' ) as hasil"
                                        Using Dr = OpenTrans(SQL)
                                            If Dr.Read Then
                                                Jumlah_Kurang = Dr("hasil")
                                            End If
                                        End Using

                                        'SQL = "insert into N_EMI_LOG_Production_Results_HPP (Kode_Perusahaan, No_Transaksi, Proses, Tanggal, jam, Jumlah_Formula, Jumlah_Dosing, Satuan, Jumlah_Dosing_Pcs, "
                                        'SQL = SQL & "Total_Bahan_Baku, Total_Packaging, Total_Biaya_Produksi, Nilai_Loss_Production, Persen_Loss_Production, Jumlah_Terpakai, Urut, userid_batal, tanggal_batal, jam_batal) "
                                        'SQL = SQL & "select Kode_Perusahaan, No_Transaksi, Proses, Tanggal, jam, Jumlah_Formula, Jumlah_Dosing, Satuan, Jumlah_Dosing_Pcs,  "
                                        'SQL = SQL & "Total_Bahan_Baku, Total_Packaging, Total_Biaya_Produksi, Nilai_Loss_Production, Persen_Loss_Production, Jumlah_Terpakai, Urut, "
                                        'SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "' "
                                        'SQL = SQL & "from Emi_Production_Results_hpp "
                                        'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        'SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                                        'SQL = SQL & "and Urut = '" & .Rows(i).Item("Urut_HPP") & "' "
                                        'ExecuteTrans(SQL)

                                        '==================================
                                        '=     PENGURANGAN RESULT HPP     =
                                        '==================================
                                        SQL = "update Emi_Production_Results_hpp set Jumlah_Terpakai = Jumlah_Terpakai - " & HilangkanTanda(Jumlah_Kurang) & " "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' and Urut = '" & .Rows(i).Item("Urut_HPP") & "' "
                                        ExecuteTrans(SQL)

                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Production Result HPP Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                        Next
                    End If
                End With
            End Using

            '==========================
            '=     ROLLBACK SCRAP     =
            '==========================
            SQL = "select b.No_Transaksi, b.Proses, b.Nomor, sum(b.Jumlah) as Jumlah, b.Serial_Number "
            SQL = SQL & "from Emi_Production_Results a, EMI_Production_Results_Detail_Scrap b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Proses = '" & Proses & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code + '-' + b.kode_unik_berjalan) = '" & Barcode & "' "
            SQL = SQL & "group by b.No_Transaksi, b.Proses, b.Nomor, b.Serial_Number, b.Urut_HPP "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim SN_Potong As String = .Rows(i).Item("Serial_Number")
                            Dim jumlah_Potong As Double = HilangkanTanda(.Rows(i).Item("Jumlah"))

                            '============================
                            '=     UPDATE BARANG SN     =
                            '============================
                            SQL = "select Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Potong & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(HilangkanTanda(Dr("Jumlah"))) >= jumlah_Potong Then

                                        Dr.Close()
                                        SQL = "update Barang_SN set jumlah -= " & HilangkanTanda(jumlah_Potong) & " where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Potong & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            '=========================
                            '=     UPDATE BARANG     =
                            '=========================
                            Dim kdSO As String = ""
                            Dim KdBarang As String = ""
                            SQL = "select Kode_Stock_Owner, Kode_Barang from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Potong & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    kdSO = Dr("Kode_Stock_Owner")
                                    KdBarang = Dr("Kode_Barang")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "select Good_Stock from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner = '" & kdSO & "' and Kode_Barang = '" & KdBarang & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(HilangkanTanda(Dr("Good_Stock"))) >= jumlah_Potong Then

                                        Dr.Close()
                                        SQL = "update barang set Good_Stock -= " & HilangkanTanda(jumlah_Potong) & " where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & kdSO & "' and Kode_Barang = '" & KdBarang & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & kdSO & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds1 = BindingTrans(SQL)
                                With Ds1.Tables("MyTable")
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or
                                            Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
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
                                End With
                            End Using

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Pallet Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            SQL = "select top 1 Kode_Perusahaan "
            SQL = SQL & "from EMI_Production_Results_Detail_Scrap "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and Proses = '" & Proses & "' "
            SQL = SQL & "and nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (Qr_Code + '-' + kode_unik_berjalan) = '" & Barcode & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    Dr.Close()
                    SQL = "insert into N_EMI_LOG_Production_Results_Detail_Scrap "
                    SQL = SQL & "(Kode_Perusahaan,No_Transaksi,Proses,Kode_Unik_Berjalan,Kode_Unik_Asal,Qr_Code,Jumlah,Satuan,NIlai_Barang,Satuan_Barang,Batch_Number, "
                    SQL = SQL & "Id_Warehouse,Nomor_Pallet,Serial_Number,Urut_HPP,Tgl_Produksi,Tgl_Expired,Lokasi_Gudang_Sisa,Nomor,UserID_Batal,Tanggal_Batal,Jam_Batal) "
                    SQL = SQL & "select Kode_Perusahaan,No_Transaksi,Proses,Kode_Unik_Berjalan,Kode_Unik_Asal,Qr_Code,Jumlah,Satuan,NIlai_Barang,Satuan_Barang,Batch_Number, "
                    SQL = SQL & "Id_Warehouse,Nomor_Pallet,Serial_Number,Urut_HPP,Tgl_Produksi,Tgl_Expired,Lokasi_Gudang_Sisa,Nomor, '', '', '' "
                    SQL = SQL & "from EMI_Production_Results_Detail_Scrap "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Transaksi = '" & No_Transaksi & "' "
                    SQL = SQL & "and Proses = '" & Proses & "' "
                    SQL = SQL & "and nomor = '" & No_Keranjang & "' "
                    SQL = SQL & "and (Qr_Code + '-' + kode_unik_berjalan) = '" & Barcode & "' "
                    ExecuteTrans(SQL)

                    SQL = SQL & "Delete from EMI_Production_Results_Detail_Scrap "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & "and No_Transaksi = '" & No_Transaksi & "' "
                    SQL = SQL & "and Proses = '" & Proses & "' "
                    SQL = SQL & "and nomor = '" & No_Keranjang & "' "
                    SQL = SQL & "and (Qr_Code + '-' + kode_unik_berjalan) = '" & Barcode & "' "
                    ExecuteTrans(SQL)
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Scrap tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Scrap Berhasil Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(e, sender)

    End Sub

    Private Sub BatalkanKeranjangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalkanKeranjangToolStripMenuItem.Click
        If Lv_DetailFinishedGood.Items.Count = 0 Or Lv_DetailFinishedGood.FocusedItem.Index = -1 Then Exit Sub

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Production Result"

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Keranjang_GR_1") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Keranjang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan keranjang Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            Dim No_Split As String = Lv_ProductionResult.FocusedItem.Text
            Dim Kd_Barang As String = Lv_ProductionResult.FocusedItem.SubItems(itemPR_KdBarang).Text
            Dim Satuan As String = Lv_ProductionResult.FocusedItem.SubItems(itemPR_Satuan).Text
            Dim No_Transaksi As String = Lv_DetailFinishedGood.FocusedItem.SubItems(0).Text
            Dim No_Keranjang As String = Lv_DetailFinishedGood.FocusedItem.SubItems(7).Text
            Dim Barcode As String = Lv_DetailFinishedGood.FocusedItem.SubItems(2).Text

            '======================================
            '=     CEK DATA PRODUCTION RESLUT     =
            '======================================
            SQL = "select Kode_Perusahaan from Emi_Production_Results where Kode_Perusahaan = '" & KodePerusahaan & "' and Status is null and no_production_order = '" & No_Split & "' "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data Production Result Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '======================================
            '=     CEK DATA PRODUCTION RESULT     =
            '======================================
            SQL = "select a.No_Transaksi, b.Urut_HPP, b.Jumlah "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code +'-' + b.Kode_Unik_Berjalan ) = '" & Barcode & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            '========================
                            '=     ROLLBACK HPP     =
                            '========================
                            SQL = "select satuan, urut "
                            SQL = SQL & "from Emi_Production_Results_hpp "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                            SQL = SQL & "and Urut = '" & .Rows(i).Item("Urut_HPP") & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    For j As Integer = 0 To Ds1.Tables("MyTable").Rows.Count - 1

                                        Dim Jumlah_Kurang As Double = 0

                                        SQL = "select dbo.ubah_satuan('" & KodePerusahaan & "', 'masa','" & Kd_Barang & "', '" & Satuan & "', "
                                        SQL = SQL & "'" & Ds1.Tables("MyTable").Rows(j).Item("satuan") & "', '" & .Rows(i).Item("Jumlah") & "' ) as hasil"
                                        Using Dr = OpenTrans(SQL)
                                            If Dr.Read Then
                                                Jumlah_Kurang = Dr("hasil")
                                            End If
                                        End Using

                                        'SQL = "insert into N_EMI_LOG_Production_Results_HPP (Kode_Perusahaan, No_Transaksi, Proses, Tanggal, jam, Jumlah_Formula, Jumlah_Dosing, Satuan, Jumlah_Dosing_Pcs, "
                                        'SQL = SQL & "Total_Bahan_Baku, Total_Packaging, Total_Biaya_Produksi, Nilai_Loss_Production, Persen_Loss_Production, Jumlah_Terpakai, Urut, userid_batal, tanggal_batal, jam_batal) "
                                        'SQL = SQL & "select Kode_Perusahaan, No_Transaksi, Proses, Tanggal, jam, Jumlah_Formula, Jumlah_Dosing, Satuan, Jumlah_Dosing_Pcs,  "
                                        'SQL = SQL & "Total_Bahan_Baku, Total_Packaging, Total_Biaya_Produksi, Nilai_Loss_Production, Persen_Loss_Production, Jumlah_Terpakai, Urut, "
                                        'SQL = SQL & "'" & UserID & "','" & Format(tgl_skg, "yyyy-MM-dd") & "','" & Format(tgl_skg, "HH:mm:ss") & "' "
                                        'SQL = SQL & "from Emi_Production_Results_hpp "
                                        'SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        'SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                                        'SQL = SQL & "and Urut = '" & .Rows(i).Item("Urut_HPP") & "' "
                                        'ExecuteTrans(SQL)

                                        '==================================
                                        '=     PENGURANGAN RESULT HPP     =
                                        '==================================
                                        SQL = "update Emi_Production_Results_hpp set Jumlah_Terpakai = Jumlah_Terpakai - " & HilangkanTanda(Jumlah_Kurang) & " "
                                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' and Urut = '" & .Rows(i).Item("Urut_HPP") & "' "
                                        ExecuteTrans(SQL)

                                    Next
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data Production Result HPP Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Production Result Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '================================
            '=     ROLLBACK FINISH GOOD     =
            '================================
            SQL = "select a.No_Transaksi, b.nomor, b.Serial_Number, isnull(sum(b.Jumlah), 0) as Jumlah, b.Satuan "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code +'-' + b.Kode_Unik_Berjalan ) = '" & Barcode & "' "
            SQL = SQL & "group by a.No_Transaksi, b.Serial_Number, b.nomor, b.Satuan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim SN_Potong As String = .Rows(i).Item("Serial_Number")
                            Dim jumlah_Potong As Double = HilangkanTanda(.Rows(i).Item("Jumlah"))

                            '============================
                            '=     UPDATE BARANG SN     =
                            '============================
                            SQL = "select Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Potong & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(HilangkanTanda(Dr("Jumlah"))) >= jumlah_Potong Then

                                        Dr.Close()
                                        SQL = "update Barang_SN set jumlah -= " & HilangkanTanda(jumlah_Potong) & " where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Potong & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            '=========================
                            '=     UPDATE BARANG     =
                            '=========================
                            Dim kdSO As String = ""
                            Dim KdBarang As String = ""
                            SQL = "select Kode_Stock_Owner, Kode_Barang from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Potong & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    kdSO = Dr("Kode_Stock_Owner")
                                    KdBarang = Dr("Kode_Barang")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "select Good_Stock from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner = '" & kdSO & "' and Kode_Barang = '" & KdBarang & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(HilangkanTanda(Dr("Good_Stock"))) >= jumlah_Potong Then

                                        Dr.Close()
                                        SQL = "update barang set Good_Stock -= " & HilangkanTanda(jumlah_Potong) & " where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & kdSO & "' and Kode_Barang = '" & KdBarang & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & kdSO & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds1 = BindingTrans(SQL)
                                With Ds1.Tables("MyTable")
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or
                                            Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
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
                                End With
                            End Using

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Pallet Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '==============================
            '=     ROLLBACK PACKAGING     =
            '==============================
            SQL = "select distinct a.No_Transaksi, b.nomor, d.Serial_Number, isnull(sum(d.Nilai), 0) as Jumlah, e.Satuan "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Emi_Production_Results_Packaging_Detail c, Emi_Production_Results_Packaging_Det d, Barang e "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan and d.Kode_Perusahaan = e.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
            SQL = SQL & "and c.No_Transaksi = d.No_Transaksi and c.Urut = d.No_Urut_Detail "
            SQL = SQL & "and d.Kode_Stock_Owner = e.Kode_Stock_Owner and d.Kode_Barang = e.Kode_Barang "
            SQL = SQL & "and a.Status is null and c.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code +'-' + b.Kode_Unik_Berjalan ) = '" & Barcode & "' "
            SQL = SQL & "group by a.No_Transaksi, b.Nomor, d.Serial_Number, e.Satuan "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Dim SN_Potong As String = .Rows(i).Item("Serial_Number")
                            Dim jumlah_Potong As Double = HilangkanTanda(.Rows(i).Item("Jumlah"))

                            If jumlah_Potong = 0 Then Continue For

                            '============================
                            '=     UPDATE BARANG SN     =
                            '============================
                            SQL = "select Jumlah from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Potong & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(HilangkanTanda(Dr("Jumlah"))) >= jumlah_Potong Then

                                        Dr.Close()
                                        SQL = "update Barang_SN set jumlah -= " & HilangkanTanda(jumlah_Potong) & " where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Potong & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            '=========================
                            '=     UPDATE BARANG     =
                            '=========================
                            Dim kdSO As String = ""
                            Dim KdBarang As String = ""
                            SQL = "select Kode_Stock_Owner, Kode_Barang from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN_Potong & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    kdSO = Dr("Kode_Stock_Owner")
                                    KdBarang = Dr("Kode_Barang")
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            SQL = "select Good_Stock from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner = '" & kdSO & "' and Kode_Barang = '" & KdBarang & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If Val(HilangkanTanda(Dr("Good_Stock"))) >= jumlah_Potong Then

                                        Dr.Close()
                                        SQL = "update barang set Good_Stock -= " & HilangkanTanda(jumlah_Potong) & " where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                        SQL = SQL & "and Kode_Stock_Owner = '" & kdSO & "' and Kode_Barang = '" & KdBarang & "' "
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Proses tidak dapat dilanjutkan karena akan membuat stock menjadi negatif.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End If
                            End Using

                            '====================================
                            '=       CEK KESESUAIAN STOCK       =
                            '====================================
                            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & kdSO & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds1 = BindingTrans(SQL)
                                With Ds1.Tables("MyTable")
                                    If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                        If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or
                                            Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
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
                                End With
                            End Using
                        Next
                    End If
                End With
            End Using

            '=============================================
            '=     ROLLBACK JURNAL Dan RESULT JURNAL     =
            '=============================================
            SQL = "select distinct a.No_Transaksi, b.nomor, c.Kode_Voucher, c.Jenis, c.Proses "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Emi_Production_Results_Jurnal c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses  "
            SQL = SQL & "and c.Jenis not in ('GI', 'GIP') "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code +'-' + b.Kode_Unik_Berjalan ) = '" & Barcode & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            '===========================
                            '=     ROLLBACK JURNAL     =
                            '===========================
                            SQL = "select kode_perusahaan from Jurnal "
                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and kode_voucher = '" & .Rows(i).Item("Kode_Voucher") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Dr.Close()
                                    SQL = "insert into N_EMI_LOG_Jurnal "
                                    SQL = SQL & "(Kode_Perusahaan,Kode_Voucher,Kode_Proyek,Tanggal,Jam,Keterangan,JudulBank,KetDK,Jabatan,UserID,Lokasi,Jns,Ket1,Ket2,Otomatis,Sudah_Fk,Tgl_Input, "
                                    SQL = SQL & "Flag_Jenis,Validasi,Ket_Validasix, "
                                    SQL = SQL & "User_Validasi,Tgl_Validasi,Jam_Validasi,Flag_Jurnal_PBK,xxx,tgl_backup,Flag_Val,User_Val,Tgl_Val,Jam_Val,UserID_Batal,Tanggal_Batal,Jam_Batal) "
                                    SQL = SQL & "select Kode_Perusahaan,Kode_Voucher,Kode_Proyek,Tanggal,Jam,Keterangan,JudulBank,KetDK,Jabatan,UserID,Lokasi,Jns,Ket1,Ket2,Otomatis,Sudah_Fk,Tgl_Input,Flag_Jenis, "
                                    SQL = SQL & "Validasi,Ket_Validasix,User_Validasi,Tgl_Validasi,Jam_Validasi,Flag_Jurnal_PBK,xxx,tgl_backup,Flag_Val,User_Val,Tgl_Val, "
                                    SQL = SQL & "Jam_Val, '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
                                    SQL = SQL & "from Jurnal "
                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and kode_voucher = '" & .Rows(i).Item("Kode_Voucher") & "' "
                                    ExecuteTrans(SQL)

                                    SQL = "delete from Jurnal "
                                    SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and kode_voucher = '" & .Rows(i).Item("Kode_Voucher") & "' "
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Jurnal Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '==================================
                            '=     ROLLBACK RESULT JURNAL     =
                            '==================================
                            SQL = "select Kode_Perusahaan from Emi_Production_Results_Jurnal "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                            SQL = SQL & "and Proses = '" & .Rows(i).Item("Proses") & "' and Kode_Voucher = '" & .Rows(i).Item("Kode_Voucher") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Dr.Close()
                                    SQL = "insert into N_EMI_LOG_Production_Results_Jurnal "
                                    SQL = SQL & "(Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses,Jenis,UserID_Batal,Tanggal_Batal,Jam_Batal) "
                                    SQL = SQL & "select Kode_Perusahaan,No_Transaksi,Kode_Voucher,Proses,Jenis, "
                                    SQL = SQL & "'" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
                                    SQL = SQL & "from Emi_Production_Results_Jurnal "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                                    SQL = SQL & "and Proses = '" & .Rows(i).Item("Proses") & "' and Kode_Voucher = '" & .Rows(i).Item("Kode_Voucher") & "' "
                                    ExecuteTrans(SQL)

                                    SQL = "delete from Emi_Production_Results_Jurnal "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                                    SQL = SQL & "and Proses = '" & .Rows(i).Item("Proses") & "' and Kode_Voucher = '" & .Rows(i).Item("Kode_Voucher") & "' "
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Jurnal Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Jurnal Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '==================================
            '=     ROLLBACK DETAIL BARANG     =
            '==================================
            SQL = "select distinct a.No_Transaksi, b.nomor, c.Proses "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, EMI_Production_Results_Detail_Barang c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
            SQL = SQL & "and a.Status is null and c.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code +'-' + b.Kode_Unik_Berjalan ) = '" & Barcode & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "insert into N_EMI_LOG_Production_Results_Detail_Barang "
                            SQL = SQL & "(Kode_Perusahaan,No_Transaksi,Proses,Tanggal,Jam,UserID,Kode_Stock_Owner,Kode_Barang,Qty_Hasil_Produksi,Qty_Good_Stock, "
                            SQL = SQL & "Qty_Bad_Stock,Satuan,Qty_Scrap,Satuan_Scrap,Kode_Barang_Scrap,status,UserID_Batal,Tanggal_Batal,Jam_Batal) "
                            SQL = SQL & "select Kode_Perusahaan,No_Transaksi,Proses,Tanggal,Jam,UserID,Kode_Stock_Owner,Kode_Barang,Qty_Hasil_Produksi,Qty_Good_Stock, "
                            SQL = SQL & "Qty_Bad_Stock,Satuan,Qty_Scrap,Satuan_Scrap,Kode_Barang_Scrap,status, '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
                            SQL = SQL & "from EMI_Production_Results_Detail_Barang "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                            SQL = SQL & "and Proses = '" & .Rows(i).Item("Proses") & "' "
                            SQL = SQL & "and status is null "
                            ExecuteTrans(SQL)

                            SQL = "delete from EMI_Production_Results_Detail_Barang "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                            SQL = SQL & "and Proses = '" & .Rows(i).Item("Proses") & "' "
                            SQL = SQL & "and status is null "
                            ExecuteTrans(SQL)

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Detail Barang Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '=========================================
            '=     ROLLBACK DETAIL PACKAGING DET     =
            '=========================================
            SQL = "select distinct a.No_Transaksi, b.nomor, d.Serial_Number, d.Urut "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Emi_Production_Results_Packaging_Detail c, Emi_Production_Results_Packaging_Det d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan  "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
            SQL = SQL & "and c.No_Transaksi = d.No_Transaksi and c.Urut = d.No_Urut_Detail "
            SQL = SQL & "and a.Status is null and c.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code +'-' + b.Kode_Unik_Berjalan ) = '" & Barcode & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Dim Sn As String = .Rows(i).Item("Serial_Number")
                            Dim urut As String = .Rows(i).Item("Urut")

                            SQL = "select Kode_Perusahaan from Emi_Production_Results_Packaging_Det "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                            SQL = SQL & "and Serial_Number = '" & Sn & "' "
                            SQL = SQL & "and Urut = '" & urut & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Dr.Close()
                                    SQL = "insert into N_EMI_LOG_Production_Results_Packaging_Det "
                                    SQL = SQL & "(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai,Serial_Number,No_Urut_Detail,jumlah_pakai,UserID_Batal,Tanggal_Batal,Jam_Batal) "
                                    SQL = SQL & "select Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai,Serial_Number,No_Urut_Detail,jumlah_pakai, '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
                                    SQL = SQL & "from Emi_Production_Results_Packaging_Det "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                                    SQL = SQL & "and Serial_Number = '" & Sn & "' "
                                    SQL = SQL & "and Urut = '" & urut & "' "
                                    ExecuteTrans(SQL)

                                    SQL = SQL & "Delete from Emi_Production_Results_Packaging_Det "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                                    SQL = SQL & "and Serial_Number = '" & Sn & "' "
                                    SQL = SQL & "and Urut = '" & urut & "' "
                                    ExecuteTrans(SQL)

                                End If
                            End Using

                        Next
                    End If
                End With
            End Using

            '=====================================
            '=     ROLLBACK DETAIL PACKAGING     =
            '=====================================
            SQL = "select distinct a.No_Transaksi, b.nomor, c.Proses, c.Urut "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b, Emi_Production_Results_Packaging_Detail c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.No_Transaksi = c.No_Transaksi and b.Proses = c.Proses "
            SQL = SQL & "and a.Status is null and c.status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code +'-' + b.Kode_Unik_Berjalan ) = '" & Barcode & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "select Kode_Perusahaan from Emi_Production_Results_Packaging_Detail "
                            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                            SQL = SQL & "and Proses = '" & .Rows(i).Item("Proses") & "' "
                            SQL = SQL & "and Urut = '" & .Rows(i).Item("Urut") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then

                                    Dr.Close()
                                    SQL = "insert into N_EMI_LOG_Production_Results_Packaging_Detail "
                                    SQL = SQL & "(Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan,Nilai_Barang,Satuan_Barang,Proses,UserID, "
                                    SQL = SQL & "Tanggal,Jam,status,UserID_Batal,Tanggal_Batal,Jam_Batal) "
                                    SQL = SQL & "select Kode_Perusahaan,No_Transaksi,Kode_Stock_Owner,Kode_Barang,Nilai_Formula,Nilai_Produksi,Satuan,Nilai_Barang,Satuan_Barang,Proses,UserID, "
                                    SQL = SQL & "Tanggal,Jam,status, '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
                                    SQL = SQL & "from Emi_Production_Results_Packaging_Detail "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                                    SQL = SQL & "and Proses = '" & .Rows(i).Item("Proses") & "' "
                                    SQL = SQL & "and Urut = '" & .Rows(i).Item("Urut") & "' "
                                    ExecuteTrans(SQL)

                                    SQL = SQL & "Delete from Emi_Production_Results_Packaging_Detail "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
                                    SQL = SQL & "and No_Transaksi = '" & .Rows(i).Item("No_Transaksi") & "' "
                                    SQL = SQL & "and Proses = '" & .Rows(i).Item("Proses") & "' "
                                    SQL = SQL & "and Urut = '" & .Rows(i).Item("Urut") & "' "
                                    ExecuteTrans(SQL)

                                End If
                            End Using

                        Next
                    End If
                End With
            End Using

            '==================================
            '=     ROLLBACK DETAIL PALLET     =
            '==================================
            SQL = "select top 1 a.No_Transaksi, b.nomor "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
            SQL = SQL & "and (b.Qr_Code +'-' + b.Kode_Unik_Berjalan ) = '" & Barcode & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            SQL = "insert into N_EMI_LOG_Production_Results_detail_pallet "
                            SQL = SQL & "(Kode_Perusahaan,No_Transaksi,Proses,Kode_Unik_Berjalan,Kode_Unik_Asal,Qr_Code,Jumlah,Satuan,NIlai_Barang,Satuan_Barang,Batch_Number, "
                            SQL = SQL & "Id_Warehouse,Nomor_Pallet,Serial_Number,Jenis,SN_Baru,Warna_QI,No_Faktur_QC,Flag_Sudah_QI,Tgl_Produksi,Tgl_Expired,Flag_Simpan_Sementara, "
                            SQL = SQL & "Lokasi_Gudang,SN_HPP,Urut_HPP,Flag_Military_Sampling,Tahap,Nomor,Troli,UserID_Batal,Tanggal_Batal,Jam_Batal) "
                            SQL = SQL & "select b.Kode_Perusahaan,b.No_Transaksi,b.Proses,b.Kode_Unik_Berjalan,b.Kode_Unik_Asal,b.Qr_Code,b.Jumlah,b.Satuan,b.NIlai_Barang,b.Satuan_Barang, "
                            SQL = SQL & "b.Batch_Number,b.Id_Warehouse,b.Nomor_Pallet,b.Serial_Number,b.Jenis,b.SN_Baru,b.Warna_QI,b.No_Faktur_QC, "
                            SQL = SQL & "b.Flag_Sudah_QI,b.Tgl_Produksi,b.Tgl_Expired,b.Flag_Simpan_Sementara,b.Lokasi_Gudang,b.SN_HPP,b.Urut_HPP, "
                            SQL = SQL & "b.Flag_Military_Sampling,b.Tahap,b.Nomor,b.Troli, '" & UserID & "', '" & Format(tgl_skg, "yyyy-MM-dd") & "', '" & Format(tgl_skg, "HH:mm:ss") & "' "
                            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_Detail_Pallet b "
                            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
                            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
                            SQL = SQL & "and a.Status is null "
                            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and a.No_Production_Order = '" & No_Split & "' "
                            SQL = SQL & "and a.No_Transaksi = '" & No_Transaksi & "' "
                            SQL = SQL & "and b.Nomor = '" & No_Keranjang & "' "
                            SQL = SQL & "and (b.Qr_Code +'-' + b.Kode_Unik_Berjalan ) = '" & Barcode & "' "
                            ExecuteTrans(SQL)

                            SQL = "delete from Emi_Production_Results_Detail_Pallet  "
                            SQL = SQL & "where  "
                            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and No_Transaksi = '" & No_Transaksi & "' "
                            SQL = SQL & "and Nomor = '" & No_Keranjang & "' "
                            SQL = SQL & "and (Qr_Code +'-' + Kode_Unik_Berjalan ) = '" & Barcode & "' "
                            ExecuteTrans(SQL)

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Pallet Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Data Berhasil Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(e, sender)

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