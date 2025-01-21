Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports iTextSharp.text.pdf

Public Class EMI_Display_Production_Result

    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

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
        Lv_ProductionResult.Columns.Add(Base_Language.Lang_Global_NoFaktur, 125, HorizontalAlignment.Left)
        Lv_ProductionResult.Columns.Add("No Production Order", 135, HorizontalAlignment.Left)
        Lv_ProductionResult.Columns.Add(Base_Language.Lang_Global_Tanggal, 150, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add(Base_Language.Lang_Global_Jam, 100, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add("User ID", 100, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add("Kode Barang", 150, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add("Nama", 250, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add("Jumlah Produksi", 130, HorizontalAlignment.Right)
        Lv_ProductionResult.Columns.Add("Satuan", 80, HorizontalAlignment.Center)
        Lv_ProductionResult.Columns.Add("Catatan", 350, HorizontalAlignment.Left)
        Lv_ProductionResult.Columns.Add("Tanggal Selesai Produksi", 150, HorizontalAlignment.Center) 'NULLable
        Lv_ProductionResult.Columns.Add("Jam Selesai Produksi", 100, HorizontalAlignment.Center) 'NULLable
        'Hide
        Lv_ProductionResult.Columns.Add("Flag Selesai Produksi", 0, HorizontalAlignment.Center) 'NULLable
        Lv_ProductionResult.View = View.Details

        Lv_DetailFinishedGood.Items.Clear()
        Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
        Lv_DetailFinishedGood.Columns.Add("No_Production_Order", 0, HorizontalAlignment.Left)
        Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_Kode_Unik_Berjalan, 150, HorizontalAlignment.Left)
        Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_Kode_Unik_Asal, 150, HorizontalAlignment.Left)
        Lv_DetailFinishedGood.Columns.Add("QR Code", 0, HorizontalAlignment.Left)
        Lv_DetailFinishedGood.Columns.Add("Batch Number", 190, HorizontalAlignment.Left)
        Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_Jumlah, 120, HorizontalAlignment.Right)
        Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_Nilai_Barang, 120, HorizontalAlignment.Right)
        Lv_DetailFinishedGood.Columns.Add(Base_Language.Lang_Global_Satuan_Barang, 120, HorizontalAlignment.Center)
        Lv_DetailFinishedGood.Columns.Add("Harga Barang", 150, HorizontalAlignment.Right)
        Lv_DetailFinishedGood.Columns.Add("Total Harga", 150, HorizontalAlignment.Right)
        Lv_DetailFinishedGood.Columns.Add("Tanggal Expired", 150, HorizontalAlignment.Center)
        Lv_DetailFinishedGood.Columns.Add("Kualitas", 150, HorizontalAlignment.Center)
        Lv_DetailFinishedGood.View = View.Details

        Lv_DetailRawMaterial.Items.Clear()
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
        Lv_DetailRawMaterial.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_Nilai_Formula, 120, HorizontalAlignment.Right)
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_Nilai_Produksi, 120, HorizontalAlignment.Right)
        Lv_DetailRawMaterial.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_DetailRawMaterial.View = View.Details

        Lv_DetailPackaging.Items.Clear()
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
        Lv_DetailPackaging.Columns.Add("Kode Stock Owner", 150, HorizontalAlignment.Left)
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_Nilai_Formula, 120, HorizontalAlignment.Right)
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_Nilai_Produksi, 120, HorizontalAlignment.Right)
        Lv_DetailPackaging.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_DetailPackaging.View = View.Details

        Lv_DetailScrap.Items.Clear()
        Lv_DetailScrap.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Left)
        Lv_DetailScrap.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        Lv_DetailScrap.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
        Lv_DetailScrap.Columns.Add("Jumlah", 120, HorizontalAlignment.Right)
        Lv_DetailScrap.Columns.Add("Satuan", 130, HorizontalAlignment.Center)
        Lv_DetailScrap.Columns.Add("Nilai Barang", 120, HorizontalAlignment.Right)
        Lv_DetailScrap.Columns.Add("Satuan Barang", 130, HorizontalAlignment.Center)
        Lv_DetailScrap.Columns.Add("Proses", 80, HorizontalAlignment.Center)
        'Hide
        Lv_DetailScrap.Columns.Add("Urut Oto", 0, HorizontalAlignment.Center)
        Lv_DetailScrap.View = View.Details

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
            SQL = "select b.No_Transaksi, a.No_Production_Order, b.Jumlah, b.Satuan, b.Batch_Number, c.Keterangan as Kualitas, "
            SQL = SQL & "b.Kode_Unik_Asal, b.Kode_Unik_Berjalan, b.Qr_Code, b.Tgl_Expired, b.NIlai_Barang, b.Satuan_Barang, "
            SQL = SQL & "ISNULL((select dbo.get_hpp(b.SN_Baru)), '0') as Harga_Per_Barang, "
            SQL = SQL & "(b.Jumlah * ISNULL((select dbo.get_hpp(b.SN_Baru)), '0')) as Total_Harga "
            SQL = SQL & "from Emi_Production_Results a, Emi_Production_Results_detail_Pallet b, EMI_Master_Warna c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Jenis = c.Kode_Warna "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_DetailFinishedGood.Items.Add(Dr("No_Transaksi"))
                    lvw.SubItems.Add(Dr("No_Production_Order"))
                    lvw.SubItems.Add(Dr("Kode_Unik_Berjalan"))
                    lvw.SubItems.Add(Dr("Kode_Unik_Asal"))
                    lvw.SubItems.Add(Dr("Qr_Code"))
                    lvw.SubItems.Add(Dr("Batch_Number"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    lvw.SubItems.Add(Dr("Satuan"))
                    lvw.SubItems.Add(Format(Dr("NIlai_Barang"), "N0"))
                    lvw.SubItems.Add(Dr("Satuan_Barang"))
                    lvw.SubItems.Add(Dr("Harga_Per_Barang"))
                    lvw.SubItems.Add(Dr("Total_Harga"))
                    lvw.SubItems.Add(Format(Dr("Tgl_Expired"), "dd MMM yyyy"))
                    lvw.SubItems.Add(Dr("Kualitas"))
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
                    lvw.SubItems.Add(Format(Dr("Nilai_Formula"), "N0"))
                    lvw.SubItems.Add(Format(Dr("Nilai_Produksi"), "N0"))
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
                    lvw.SubItems.Add(Format(Dr("Nilai_Formula"), "N0"))
                    lvw.SubItems.Add(Format(Dr("Nilai_Produksi"), "N0"))
                    lvw.SubItems.Add(Dr("Satuan"))
                Loop
            End Using

            'Scrap
            SQL = "select b.No_Transaksi, d.Kode_Barang, d.Nama as Nama_Barang, b.Jumlah, b.Satuan, b.Nilai_Barang, b.Satuan_Barang, b.Proses, b.Urut_Oto "
            SQL = SQL & "from Emi_Production_Results a, EMI_Production_Results_Detail_Scrap b, Barang_SN c, barang d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and b.Serial_Number = c.Serial_Number "
            SQL = SQL & "and c.Kode_Stock_Owner = d.Kode_Stock_Owner and c.Kode_Barang = d.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Production_Order = '" & Lv_ProductionResult.FocusedItem.SubItems(itemPR_NoFak).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = Lv_DetailScrap.Items.Add(Dr("No_Transaksi"))
                    Lvw.SubItems.Add(Dr("Kode_Barang"))
                    Lvw.SubItems.Add(Dr("Nama_Barang"))
                    Lvw.SubItems.Add(Dr("Jumlah"))
                    Lvw.SubItems.Add(Dr("Satuan"))
                    Lvw.SubItems.Add(Dr("Nilai_Barang"))
                    Lvw.SubItems.Add(Dr("Satuan_Barang"))
                    Lvw.SubItems.Add(Dr("Proses"))
                    Lvw.SubItems.Add(Dr("Urut_Oto"))
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

End Class
