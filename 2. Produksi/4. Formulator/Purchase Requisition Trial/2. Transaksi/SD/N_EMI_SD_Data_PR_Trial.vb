Public Class N_EMI_SD_Data_PR_Trial
    Dim Jenis = "SD_Data_PR"
    Public asal As String

    Dim arr_tgl, arr_Lain As New ArrayList
    Dim arrInisialFaktur As String = ""
    Dim faktur As String = ""
    Public Tanda_SN As String = "#"
    Dim LvNo_Faktur As String
    Dim LvTanggal As String
    Dim LvJam As String
    Dim LvNoBM As String

    Dim arrBulan, arrTahun As New ArrayList


    Dim Lv2Lokasi As String
    Dim Lv2KdBrg As String
    Dim Lv2NmBrg As String
    Dim Lv2JmlOrder As String
    Dim Lv2JmlPR As String
    Dim Lv2Sisa As String
    Dim Lv2JmlInput As String
    Dim Lv2Satuan As String
    Dim Lv2Keterangan As String
    Dim Lv2TglDelivery As String


    Dim CellLokasi As Integer = 0
    Dim CellKdBrg As Integer = 1
    Dim CellNmBrg As Integer = 2
    Dim CellJmlOrder As Integer = 3
    Dim CellJmlPR As Integer = 4
    Dim CellSisa As Integer = 5
    Dim CellJmlInput As Integer = 6
    Dim CellSatuan As Integer = 7
    Dim CellTglDelivery As Integer = 8
    Dim CellKeterangan As Integer = 9


    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvNo_Faktur = ListView1.Items(No_Index).Text
        LvTanggal = ListView1.Items(No_Index).SubItems(1).Text
        LvJam = ListView1.Items(No_Index).SubItems(2).Text
        LvNoBM = ListView1.Items(No_Index).SubItems(3).Text
    End Sub

    Private Function CekNothing(ByVal str As String) As String
        Dim hasil As String = ""
        If str Is Nothing Then
            hasil = ""
        Else
            hasil = str
        End If
        Return hasil
    End Function

    Private Sub Get_Isi_Listview2(ByVal No_Index As Integer)
        'Lv2Cb = DataGridView1.Rows(No_Index).Cells(CellChkBox).Value.ToString
        Lv2Lokasi = CekNothing(DataGridView1.Rows(No_Index).Cells(CellLokasi).Value.ToString)
        Lv2KdBrg = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKdBrg).Value.ToString)
        Lv2NmBrg = CekNothing(DataGridView1.Rows(No_Index).Cells(CellNmBrg).Value.ToString)
        Lv2JmlOrder = CekNothing(DataGridView1.Rows(No_Index).Cells(CellJmlOrder).Value.ToString)
        Lv2JmlPR = CekNothing(DataGridView1.Rows(No_Index).Cells(CellJmlPR).Value.ToString)
        Lv2Sisa = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSisa).Value.ToString)
        Lv2JmlInput = CekNothing(DataGridView1.Rows(No_Index).Cells(CellJmlInput).Value)
        Lv2Satuan = CekNothing(DataGridView1.Rows(No_Index).Cells(CellSatuan).Value.ToString)
        Lv2TglDelivery = CekNothing(DataGridView1.Rows(No_Index).Cells(CellTglDelivery).Value.ToString)
        Lv2Keterangan = CekNothing(DataGridView1.Rows(No_Index).Cells(CellKeterangan).Value.ToString)

    End Sub

    Private Sub Display_Validasi_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Validasi_Barang_Masuk")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Selisih_BM")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Validasi_Selisih_BM")
            BtnBarangMasuk_Cari.Text = Base_Language.Lang_Global_Cari
            Label1.Text = "Display - Data Forecast"

            ListView1.Columns.Clear()
            DataGridView1.Rows.Clear()
            ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("Bulan", 150, HorizontalAlignment.Center)
            ListView1.Columns.Add("Tahun", 150, HorizontalAlignment.Center)
            ListView1.Columns.Add("Keterangan", 680, HorizontalAlignment.Left)
            'ListView1.Columns.Add(Base_Language.Lang_Global_No_PO, 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("bulan", 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("No Faktur", 0, HorizontalAlignment.Left)

            ValidasiToolStripMenuItem.Text = Base_Language.Lang_Global_Validasi

            ComboBox6.Items.Clear()
            xSplit = CekKotaRole().Split(",")
            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)

            SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            'ComboBox1.Items.Add("Seluruh")
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
        kosong()

    End Sub


    Private Sub Display_Validasi_Pembelian_Barang_Masuk_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub get_no_faktur()
        Txt_NoFaktur.Text = fPurchaseRequisition & Format(tgl_skg, "MMyy") & "-" &
                             General_Class.Get_Last_Number2("EMI_N_EMI_Transaksi_Purchase_Requisition_Trial", "no_Faktur", 5,
                             "Kode_perusahaan", KodePerusahaan,
                             "And", "substring(no_Faktur, 1, " & Len(fPurchaseRequisition) + 4 & ")", fPurchaseRequisition & Format(tgl_skg, "MMyy"))
    End Sub

    Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click

        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Validasi, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tny As String = MessageBox.Show(Base_Language.Lang_Validasi_Barang_Masuk_Tny_Val, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        If tny = vbNo Then Exit Sub

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Get_Isi_Listview(ListView1.FocusedItem.Index)

            'If CekButtonRole("validasi_Penyelesaian_Selisih_barang_masuk") = "T" Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If

            'ini dibuka nanti
            'SQL = "Select status from emi_pembelian_barang_masuk where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & ListView1.FocusedItem.SubItems(3).Text & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then

            '        If General_Class.CekNULL(Dr("status")) = "Y" Then
            '            Dr.Close()
            '            CloseTrans()
            '            CloseConn()
            '            MessageBox.Show("Faktur BM ini sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            Exit Sub
            '        End If

            '    Else
            '        Dr.Close()
            '        CloseTrans()
            '        CloseConn()
            '        MessageBox.Show("No Barang masuk tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            '        Exit Sub
            '    End If
            'End Using

            Dim jumlah_masuk As Double = 0
            Dim jumlah_keluar As Double = 0

            Dim Nilai_PPN_Persediaan_Plus As Double = 0
            Dim Nilai_PPN_Persediaan_Min As Double = 0

            Dim selisih_min As Double = 0
            Dim selisih_plus As Double = 0
            Dim flag_PPN As String = ""
            Dim total_baris As Integer = 0
            Dim tdk_ada_selisih_fix As Integer = 0
            Dim ada_selisih_plus_min As Integer = 0

            Dim indx As Integer = 0
            SQL = "select a.No_Faktur, a.No_Faktur_BM, b.Kode_Stock_Owner, b.Kode_Barang, b.Selisih, B.Jumlah_BM, b.Qty_PenyelesaianPlus, "
            SQL = SQL & "b.Qty_PenyelesaianMin, B.Selisih_Fix, B.Harga, b.Selisih_Rp, a.status, a.flag_validasi, b.Tgl_Produksi, b.Tgl_Expired, b.Serial_Number, a.No_PO, b.satuan "
            SQL = SQL & "from EMI_Pembelian_Selisih_Barang_Masuk a, EMI_Pembelian_Selisih_Barang_Masuk_Det b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_faktur = '" & ListView1.FocusedItem.Text & "' order by Jumlah_BM desc "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        total_baris = .Rows.Count

                        If General_Class.CekNULL(.Rows(0).Item("status")) = "Y" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi ini sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        ElseIf General_Class.CekNULL(.Rows(0).Item("flag_validasi")) = "Y" Then
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi ini sudah divalidasi sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If

                        For index As Integer = 0 To .Rows.Count - 1

                            Dim sn As String = ""

                            If .Rows(index).Item("Qty_PenyelesaianPlus") <> 0 Or .Rows(index).Item("Qty_Penyelesaianmin") <> 0 Then
                                'SQL = "Select Serial_Number from Barang_Masuk_Detail where "
                                'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                'SQL = SQL & "No_Faktur = '" & .Rows(index).Item("No_Faktur_BM") & "' "
                                'SQL = SQL & "and KOde_Stock_Owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' "
                                'SQL = SQL & "and Kode_Barang = '" & .Rows(index).Item("Kode_Barang") & "'"
                                'Using Dr = OpenTrans(SQL)
                                '    If Dr.Read Then
                                '        sn = Dr("Serial_Number")

                                '    Else
                                '        sn = "I" & Tanda_SN & "01" & Tanda_SN & Val(HilangkanTanda(.Rows(index).Item("Harga"))) & Tanda_SN & "02" & Tanda_SN & Format(Tanggal_Sekarang, "yyyy-MM-dd")
                                '    End If
                                'End Using
                                sn = General_Class.CekNULL(.Rows(index).Item("Serial_Number"))


                                If sn = "" Then
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Terjadi Kesalahan pada SN!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If

                            End If

                            Dim cek_ppn As Boolean = False


                            SQL = "Select PPN from EMI_Pembelian_PO a where "
                            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_faktur ='" & .Rows(index).Item("No_PO") & "' "
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    If indx = 0 Then
                                        If Val(Dr("PPN")) <> 0 Then
                                            flag_PPN = "Y"
                                        Else
                                            flag_PPN = ""
                                        End If
                                    End If

                                    Dim PPN_ As String = ""
                                    If Val(Dr("PPN")) <> 0 Then
                                        PPN_ = "Y"
                                    End If

                                    If flag_PPN <> PPN_ Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terdapat Flag PPN Berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    cek_ppn = True
                                Else
                                    cek_ppn = False
                                End If
                            End Using

                            If cek_ppn = False Then
                                SQL = "Select Flag_PPN from Barang where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "Kode_Stock_Owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' "
                                SQL = SQL & "and Kode_Barang = '" & .Rows(index).Item("Kode_Barang") & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        If indx = 0 Then
                                            flag_PPN = Dr("Flag_PPN")
                                        End If

                                        If flag_PPN <> Dr("Flag_PPN") Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Terdapat Flag PPN Berbeda!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi kesalahan pada barang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using
                            End If


                            indx += 1



                            If .Rows(index).Item("Qty_PenyelesaianPlus") > 0 Then

                                SQL = "select kode_barang, Serial_Number from barang_sn where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and "
                                SQL = SQL & "kode_barang = '" & .Rows(index).Item("Kode_Barang") & "' and serial_number = '" & sn & "' "
                                Using DrQ = OpenTrans(SQL)
                                    If DrQ.Read Then

                                        SQL = "Update barang_sn set jumlah = jumlah + " & .Rows(index).Item("Qty_PenyelesaianPlus") & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and kode_barang = '" & .Rows(index).Item("Kode_Barang") & "' and "
                                        SQL = SQL & "serial_number = '" & sn & "'"
                                        DrQ.Close()
                                        ExecuteTrans(SQL)

                                        'SQL = "Update detail_selisih_Barang_Masuk set Serial_Number = '" & sn & "' where "
                                        'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        'SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and kode_barang = '" & .Rows(index).Item("Kode_Barang") & "' and "
                                        'SQL = SQL & "No_Faktur = '" & .Rows(index).Item("No_Faktur") & "'"
                                        'ExecuteTrans(SQL)
                                    Else

                                        DrQ.Close()

                                        If General_Class.CekNULL(.Rows(index).Item("Tgl_Produksi")) = "" Or General_Class.CekNULL(.Rows(index).Item("Tgl_Expired")) = "" Then
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Trerjadi Kesalahan pada Expired!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        Dim IDSusunan_Barang As String = ""
                                        SQL = "Select urut from barang_detail_susunan where Kode_Barang = '" & .Rows(index).Item("Kode_Barang") & "' "
                                        SQL = SQL & "and Kode_Perusahaan='" & KodePerusahaan & "' and flag_default='Y' "
                                        Using Dr = OpenTrans(SQL)
                                            If Dr.Read Then
                                                IDSusunan_Barang = Dr("urut")
                                            Else
                                                Dr.Close()
                                                CloseConn()
                                                CloseTrans()
                                                MessageBox.Show("Susunan tidak ditemukan")
                                                Exit Sub
                                            End If
                                        End Using

                                        Dim kd_barang As String = .Rows(index).Item("Kode_Barang")
                                        Dim lks_barang As String = .Rows(index).Item("Kode_Stock_Owner")
                                        Dim satuan_barang As String = .Rows(index).Item("Satuan")
                                        Dim Nilai_Barang As String = .Rows(index).Item("Qty_PenyelesaianPlus")

                                        Dim Tgl_Produksi As String = Format(.Rows(index).Item("Tgl_Produksi"), "yyyy-MM-dd")
                                        Dim Tgl_Expired As String = Format(.Rows(index).Item("Tgl_Expired"), "yyyy-MM-dd")

                                        'cek dulu jenis susunan
                                        SQL = "select a.Kode_Barang, a.Susunan, a.Id_WMS_Pallet, a.Pjumlah,a.Ljumlah,Satuan_Jumlah,"
                                        SQL = SQL & "Urut, Tinggi_Per_Tumpukan, Total, P,L,T from barang_detail_susunan a, EMI_WMS_Pallet b where "
                                        SQL = SQL & "a.Id_WMS_Pallet=b.Id_WMS_Pallet and urut = '" & IDSusunan_Barang & "' and kode_barang = '" & kd_barang & "' "
                                        Using ds2 = BindingTrans(SQL)
                                            With ds2.Tables("MyTable")
                                                For index2 = 0 To ds2.Tables("MyTable").Rows.Count - 1
                                                    Dim Tinggi_Pallet As Double = ds2.Tables("MyTable").Rows(index2).Item("T")
                                                    Dim Panjang_Pallet As Double = ds2.Tables("MyTable").Rows(index2).Item("P")
                                                    Dim satuan_Pallet As String = ds2.Tables("MyTable").Rows(index2).Item("Satuan_Jumlah")

                                                    Dim Tinggi_Per_tumpukan As Double = ds2.Tables("MyTable").Rows(index2).Item("Tinggi_Per_Tumpukan")
                                                    Dim jumlah_Per_tumpukan As Double = ds2.Tables("MyTable").Rows(index2).Item("Total")

                                                    Dim Jumlah_satuan_Besar As Double = 0

                                                    'ubah ke satuan pallet
                                                    SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kd_barang & "',"
                                                    SQL = SQL & "'" & satuan_barang & "','" & satuan_Pallet & "',"
                                                    SQL = SQL & "" & Nilai_Barang & ") as Hasil "
                                                    Using dr = OpenTrans(SQL)
                                                        If dr.Read Then
                                                            Jumlah_satuan_Besar = Math.Round(dr("hasil"), 0)
                                                        End If
                                                    End Using

                                                    'Cek Rak Kosong
                                                    SQL = "Select * From View_Warehouse_Position_Detail Where Kode_Barang Is null "
                                                    SQL = SQL & "Order By kode_stock_Owner, Kode_WMS_Area, Kode_WMS_Row, Kode_WMS_Bay, Kode_WMS_Level, Kode_WMS_Position "
                                                    Using ds3 = BindingTrans(SQL)
                                                        For index3 = 0 To ds3.Tables("MyTable").Rows.Count - 1
                                                            Dim Panjang_level As Double = ds3.Tables("MyTable").Rows(index3).Item("Panjang_level")
                                                            Dim Tinggi_level As Double = ds3.Tables("MyTable").Rows(index3).Item("tinggi_level")

                                                            Dim id_warehouse As String = ds3.Tables("MyTable").Rows(index3).Item("Id_WMS_Warehouse_Position")

                                                            Dim Tinggi_Tumpukan As Double = Math.Floor((Tinggi_level - Tinggi_Pallet) / Tinggi_Per_tumpukan)

                                                            Dim Jumlah_PerPallet As Double = jumlah_Per_tumpukan * Tinggi_Tumpukan

                                                            Dim Jumlah_Masuk_Satuan_Besar As Double = 0
                                                            Dim Jumlah_SatuanKecil As Double = 0

                                                            'Get Jumlah Masuk Ke Pallet Dalam Satuan Besar dan Kecil
                                                            If Jumlah_satuan_Besar > Jumlah_PerPallet Then
                                                                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kd_barang & "',"
                                                                SQL = SQL & "'" & satuan_Pallet & "','" & satuan_barang & "',"
                                                                SQL = SQL & "" & Jumlah_PerPallet & ") as Hasil "
                                                                Using dr = OpenTrans(SQL)
                                                                    If dr.Read Then
                                                                        Jumlah_SatuanKecil = dr("hasil")
                                                                    End If
                                                                End Using

                                                                Jumlah_Masuk_Satuan_Besar = Jumlah_PerPallet

                                                            Else
                                                                SQL = "select dbo.Ubah_Satuan('" & KodePerusahaan & "','MASA','" & kd_barang & "',"
                                                                SQL = SQL & "'" & satuan_Pallet & "','" & satuan_barang & "',"
                                                                SQL = SQL & "" & Jumlah_satuan_Besar & ") as Hasil "
                                                                Using dr = OpenTrans(SQL)
                                                                    If dr.Read Then
                                                                        Jumlah_SatuanKecil = dr("hasil")
                                                                    End If
                                                                End Using

                                                                Jumlah_Masuk_Satuan_Besar = Jumlah_satuan_Besar

                                                            End If

                                                            Dim isi As Double = Jumlah_SatuanKecil

                                                            'Cek sisa Level
                                                            Dim Panjang_level_terisi As Double = 0
                                                            SQL = "Select ISNULL(SUM(Panjang_Pallet), 0) As panjang from "
                                                            SQL = SQL & "View_Warehouse_Position_Detail where Kode_Barang Is Not null And "
                                                            SQL = SQL & "Kode_Stock_Owner ='" & ds3.Tables("MyTable").Rows(index3).Item("kode_stock_Owner") & "' and "
                                                            SQL = SQL & "Id_WMS_Area='" & ds3.Tables("MyTable").Rows(index3).Item("Id_WMS_Area") & "' and "
                                                            SQL = SQL & "Id_WMS_Row='" & ds3.Tables("MyTable").Rows(index3).Item("Id_WMS_Row") & "' and "
                                                            SQL = SQL & "Id_WMS_Bay='" & ds3.Tables("MyTable").Rows(index3).Item("Id_WMS_Bay") & "' and "
                                                            SQL = SQL & "Id_WMS_Level='" & ds3.Tables("MyTable").Rows(index3).Item("Id_WMS_Level") & "' "
                                                            Using dr = OpenTrans(SQL)
                                                                If dr.Read Then
                                                                    Panjang_level_terisi = dr("panjang")
                                                                End If
                                                            End Using

                                                            Dim total_Ukuran = (Panjang_level - Panjang_level_terisi) - Panjang_Pallet

                                                            'jika muat
                                                            If total_Ukuran > 0 Then
                                                                If isi <> 0 Then
                                                                    'Dim Rand As New Random

                                                                    'Dim str As String = Format(Rand.Next(0, 999), "000") & Format(tgl_skg, "HHmmss")
                                                                    'Dim Kode_Unik As String = str.Substring(0, 5) & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)
                                                                    'Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & Val(HilangkanTanda(HPP)) & Tanda_SN & "02" & Tanda_SN & Format(tgl_skg, "yyyy-MM-dd")

                                                                    SQL = "select kode_barang from barang_sn where "
                                                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                                                    SQL = SQL & "kode_stock_owner = '" & lks_barang & "' and "
                                                                    SQL = SQL & "kode_barang = '" & kd_barang & "' and serial_number = '" & sn & "'"
                                                                    Using Dr = OpenTrans(SQL)
                                                                        If Dr.Read Then
                                                                            Dr.Close()
                                                                            CloseTrans()
                                                                            CloseConn()
                                                                            MessageBox.Show(Base_Language.Lang_Validasi_Barang_Masuk_Error7 & .Rows(index).Item("Kode_Barang") & "!")
                                                                            Exit Sub
                                                                        Else
                                                                            Dr.Close()
                                                                            SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                                                                            SQL = SQL & "serial_number, jumlah, Tgl_Produksi, Tgl_Expired,Id_Warehouse,id_Susunan) values('" & KodePerusahaan & "', "
                                                                            SQL = SQL & "'" & lks_barang & "', '" & kd_barang & "', "
                                                                            SQL = SQL & "'" & sn & "', " & isi & ", '" & Tgl_Produksi & "', '" & Tgl_Expired & "', '" & id_warehouse & "', '" & IDSusunan_Barang & "')"
                                                                            ExecuteTrans(SQL)

                                                                            Jumlah_satuan_Besar -= Jumlah_Masuk_Satuan_Besar

                                                                        End If
                                                                    End Using
                                                                End If
                                                            End If

                                                            If Jumlah_Masuk_Satuan_Besar = 0 Then
                                                                Exit For
                                                            End If

                                                        Next
                                                    End Using

                                                    If Jumlah_satuan_Besar <> 0 Then
                                                        CloseTrans()
                                                        CloseConn()
                                                        MessageBox.Show("Tempat Sudah Penuh . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                        Exit Sub
                                                    End If

                                                Next
                                            End With
                                        End Using

                                    End If
                                End Using


                                SQL = "select kode_barang from barang where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and "
                                SQL = SQL & "kode_barang = '" & .Rows(index).Item("Kode_Barang") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        SQL = "Update barang set good_stock = good_stock + " & .Rows(index).Item("Qty_PenyelesaianPlus") & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and "
                                        SQL = SQL & "kode_barang = '" & .Rows(index).Item("Kode_Barang") & "' "
                                        Dr.Close()
                                        ExecuteTrans(SQL)

                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                            ElseIf .Rows(index).Item("Qty_PenyelesaianMin") > 0 Then

                                SQL = "select kode_barang, Serial_Number, Jumlah from barang_sn where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and "
                                SQL = SQL & "kode_barang = '" & .Rows(index).Item("Kode_Barang") & "' and serial_number = '" & sn & "' "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        If (Dr("Jumlah") - .Rows(index).Item("Qty_PenyelesaianMin")) < 0 Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Stock " & .Rows(index).Item("Kode_Barang") & " Menjadi Minus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        SQL = "Update barang_sn set jumlah = jumlah - " & .Rows(index).Item("Qty_PenyelesaianMin") & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and kode_barang = '" & .Rows(index).Item("Kode_Barang") & "' and "
                                        SQL = SQL & "serial_number = '" & sn & "'"
                                        Dr.Close()
                                        ExecuteTrans(SQL)

                                        'SQL = "Update detail_selisih_Barang_Masuk set Serial_Number = '" & sn & "' where "
                                        'SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        'SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and kode_barang = '" & .Rows(index).Item("Kode_Barang") & "' and "
                                        'SQL = SQL & "No_Faktur = '" & .Rows(index).Item("No_Faktur") & "'"
                                        'ExecuteTrans(SQL)

                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang SN Tidak Ditemukan")
                                        Exit Sub
                                    End If
                                End Using


                                SQL = "select kode_barang, Good_Stock from barang where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and "
                                SQL = SQL & "kode_barang = '" & .Rows(index).Item("Kode_Barang") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        If (Dr("Good_Stock") - .Rows(index).Item("Qty_PenyelesaianMin")) < 0 Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Stock " & .Rows(index).Item("Kode_Barang") & " Menjadi Minus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        SQL = "Update barang set good_stock = good_stock - " & .Rows(index).Item("Qty_PenyelesaianMin") & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and "
                                        SQL = SQL & "kode_barang = '" & .Rows(index).Item("Kode_Barang") & "' "
                                        Dr.Close()
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Data barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using
                                ' Else

                            End If

                            If .Rows(index).Item("Selisih_Fix") = 0 Then
                                tdk_ada_selisih_fix = tdk_ada_selisih_fix + 1
                            End If

                            If .Rows(index).Item("Qty_PenyelesaianMin") + .Rows(index).Item("Qty_PenyelesaianMin") <> 0 Then
                                ada_selisih_plus_min = ada_selisih_plus_min + 1
                            End If

                            Dim stock_Barang As Double = 0
                            SQL = "select good_stock from Barang "
                            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Barang = '" & .Rows(index).Item("Kode_Barang") & "' and Kode_Stock_Owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "'"
                            Using dr = OpenTrans(SQL)
                                If dr.Read Then
                                    stock_Barang = dr("good_stock")
                                    dr.Close()

                                    SQL = "select isnull(sum(Jumlah),0) as jumlah "
                                    SQL = SQL & "from barang_sn where kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_barang = '" & .Rows(index).Item("Kode_Barang") & "' and Kode_Stock_Owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "'"
                                    Using dr2 = OpenTrans(SQL)
                                        If dr2.Read Then

                                            If stock_Barang <> dr2("jumlah") Then
                                                dr2.Close()
                                                CloseTrans()
                                                CloseConn()
                                                MessageBox.Show("Terdapat Selisih Barang_SN dengan Barang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                                Exit Sub
                                            End If
                                        Else
                                            dr2.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show(.Rows(index).Item("Kode_Barang") & " tidak ditemukan!")
                                            Exit Sub
                                        End If
                                    End Using
                                Else
                                    dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show(.Rows(index).Item("Kode_Barang") & " tidak ditemukan!")
                                    Exit Sub
                                End If
                            End Using

                            'jumlah_masuk = jumlah_masuk + ((.Rows(index).Item("Jumlah_BM") + .Rows(index).Item("Qty_PenyelesaianPlus") - .Rows(index).Item("Qty_PenyelesaianMin")) * .Rows(index).Item("Harga"))
                            jumlah_masuk = jumlah_masuk + (.Rows(index).Item("Qty_PenyelesaianPlus") * .Rows(index).Item("Harga"))
                            jumlah_keluar = jumlah_keluar + (.Rows(index).Item("Qty_PenyelesaianMin") * .Rows(index).Item("Harga"))

                            If .Rows(index).Item("Selisih_Fix") > 0 Then
                                selisih_plus = selisih_plus + (.Rows(index).Item("Selisih_RP"))
                            ElseIf .Rows(index).Item("Selisih_Fix") < 0 Then
                                selisih_min = selisih_min + (Math.Abs(.Rows(index).Item("Selisih_RP")))
                            End If

                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("No_Faktur Tidak ditemukan")
                        Exit Sub
                    End If

                End With
            End Using


            'Dim kontainer As Integer = Konte / 100
            'Dim jumlah As Integer = kontainer * 100
            'Dim selisih As Integer = Konte - jumlah


            'SQL = "Update EMI_Pembelian_Selisih_Barang_Masuk set flag_validasi = 'Y' "
            ''SQL = SQL & ",kode_voucher = " & kode_voucher_fix & ", "
            ''SQL = SQL & "kode_voucher2 = " & kode_voucher_fix2 & ", "
            ''SQL = SQL & "kode_voucher3 = " & kode_voucher_fix3 & ", "
            ''SQL = SQL & "Tgl_validasi = '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
            ''SQL = SQL & "jam_validasi = '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
            ''SQL = SQL & "user_validasi = '" & userid & "', "
            ''SQL = SQL & "Flag_Opm = " & flag_opm & ", "
            ''SQL = SQL & "Id_Transaksi = " & id_transaksi & " where "
            'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "No_Faktur = '" & ListView1.FocusedItem.Text & "'"
            'ExecuteTrans(SQL)

            'SQL = "Update EMI_Pembelian_PO set flag_Val_selisih_BM = 'Y' "
            'SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "No_Faktur = '" & ListView1.FocusedItem.SubItems(4).Text & "'"
            'ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()

            MessageBox.Show(Base_Language.Lang_Global_Sukses_Validasi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        BtnBarangMasuk_Cari_Click(ValidasiToolStripMenuItem, e)
    End Sub

    Private Sub get_Faktur()
        faktur = FBM & arrInisialFaktur & "-" & Format(tgl_skg, "MM/yy") & "-" &
                            General_Class.Get_Last_Number2("EMI_Pembelian_Selisih_Barang_Masuk", "No_Faktur", 4,
                            "Kode_perusahaan", KodePerusahaan,
                            "And", "substring(No_Faktur,1," & Len(FBM) + Len(arrInisialFaktur) + 6 & ")",
                             FBM & arrInisialFaktur & "-" & Format(tgl_skg, "MM/yy"))

    End Sub
    Private Sub kosong()

        Try
            OpenConn()

            cmb_bulan.Items.Clear() : arrBulan.Clear()
            cmb_tahun.Items.Clear() : arrTahun.Clear()

            cmb_bulan.Items.Add("Seluruh")
            cmb_bulan.Items.Add("January") : arrBulan.Add("01")
            cmb_bulan.Items.Add("February") : arrBulan.Add("02")
            cmb_bulan.Items.Add("Maret") : arrBulan.Add("03")
            cmb_bulan.Items.Add("April") : arrBulan.Add("04")
            cmb_bulan.Items.Add("Mei") : arrBulan.Add("05")
            cmb_bulan.Items.Add("Juni") : arrBulan.Add("06")
            cmb_bulan.Items.Add("July") : arrBulan.Add("07")
            cmb_bulan.Items.Add("Agustus") : arrBulan.Add("08")
            cmb_bulan.Items.Add("September") : arrBulan.Add("09")
            cmb_bulan.Items.Add("Oktober") : arrBulan.Add("10")
            cmb_bulan.Items.Add("November") : arrBulan.Add("11")
            cmb_bulan.Items.Add("Desember") : arrBulan.Add("12")
            'For i As Integer = 1 To 12
            '    If i < 10 Then
            '        cmb_bulan.Items.Add("0" & i)
            '    Else
            '        cmb_bulan.Items.Add(i)
            '    End If
            'Next

            'For i As Integer = 2022 To 2030
            '    cmb_tahun.Items.Add(i)
            'Next

            cmb_tahun.Items.Add("Seluruh")
            Dim tahun_awal As Integer = Date.Now.Year - 2
            Dim tahun_akhir As Integer = Date.Now.Year + 2
            For a As Integer = tahun_awal To tahun_akhir
                cmb_tahun.Items.Add(a)
            Next

            cmb_bulan.SelectedIndex = 0
            cmb_tahun.SelectedIndex = 0

            TxtNoFak_MR.Text = ""

            get_no_faktur()
            TextBox2.Text = ""
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(Me, Nothing)
    End Sub

    Private Sub BatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalToolStripMenuItem.Click

        '    MessageBox.Show(Base_Language.Lang_Global_Pilih_Batal, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '    Exit Sub
        'End If

        'Dim tny As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Batal, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
        'If tny = vbNo Then Exit Sub

        'get_jam()
        'Try
        '    OpenConn()
        '    Cmd.Transaction = Cn.BeginTransaction

        '    If CekButtonRole("Batal_barang_masuk_New") = "T" Then
        '        CloseTrans()
        '        CloseConn()
        '        MessageBox.Show(Base_Language.Lang_Global_Error_Tdk_Ada_Akses, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '        Exit Sub
        '    End If

        '    SQL = "select Flag_Validasi from EMI_Pembelian_Barang_Masuk_Sementara where "
        '    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            If General_Class.CekNULL(Dr("Flag_Validasi")) = "Y" Then
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show(Base_Language.Lang_Validasi_Barang_Masuk_Error3, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        End If
        '    End Using

        '    SQL = "select Status from EMI_Pembelian_Barang_Masuk_Sementara where "
        '    SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and no_faktur = '" & ListView1.FocusedItem.Text & "'"
        '    Using Dr = OpenTrans(SQL)
        '        If Dr.Read Then
        '            If General_Class.CekNULL(Dr("Status")) = "Y" Then
        '                CloseTrans()
        '                CloseConn()
        '                MessageBox.Show(Base_Language.Lang_Validasi_Barang_Masuk_Error2, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        '                Exit Sub
        '            End If
        '        End If
        '    End Using

        '    SQL = "Update EMI_Pembelian_Barang_Masuk_Sementara set status = 'Y' where "
        '    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
        '    SQL = SQL & "No_Faktur = '" & ListView1.FocusedItem.Text & "'"
        '    ExecuteTrans(SQL)

        '    Cmd.Transaction.Commit()
        '    CloseConn()
        'Catch ex As Exception
        '    CloseTrans()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

        'Button1_Click(ValidasiToolStripMenuItem, e)

    End Sub

    Private Sub CB_PilihSeluruh_CheckedChanged(sender As Object, e As EventArgs) Handles CB_PilihSeluruh.CheckedChanged
        If CB_PilihSeluruh.Checked = True Then
            For a As Integer = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(a).Cells(0).Value = True
            Next
        Else
            For a As Integer = 0 To DataGridView1.Rows.Count - 1
                DataGridView1.Rows(a).Cells(0).Value = False
            Next
        End If
    End Sub

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click
        Try
            OpenConn()
            CB_PilihSeluruh.Checked = False
            ListView1.Items.Clear()
            DataGridView1.Rows.Clear()

            SQL = "select a.no_faktur,a.Tahun,a.Bulan,a.Keterangan from EMI_Transaksi_Material_Requsition a "
            SQL = SQL & "where status is null and a.flag_Validasi_PPIC='Y' "
            If cmb_bulan.SelectedIndex <> 0 Then
                SQL = SQL & "and a.bulan = '" & arrBulan.Item(cmb_bulan.SelectedIndex - 1) & "' "

            End If
            If cmb_tahun.SelectedIndex <> 0 Then
                SQL = SQL & "and a.tahun = '" & cmb_tahun.Text & "' "

            End If
            SQL = SQL & "and a.status is null group by  a.no_faktur,a.Tahun,a.Bulan,a.Keterangan "
            SQL = SQL & "order by a.no_faktur, a.tahun,a.bulan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem

                    lvw = ListView1.Items.Add(Dr("no_faktur"))
                    lvw.SubItems.Add(Format(CDate(Dr("tahun") & "-" & Dr("bulan") & "-01"), "MMMM"))
                    lvw.SubItems.Add(Dr("tahun"))
                    lvw.SubItems.Add(Dr("keterangan"))
                    lvw.SubItems.Add(Dr("bulan"))
                    lvw.SubItems.Add(Dr("no_faktur"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            OpenConn()
            CB_PilihSeluruh.Checked = False
            If ListView1.Items.Count = 0 Then Exit Sub
            DataGridView1.Rows.Clear()
            TxtNoFak_MR.Text = ""
            Dim no As Integer = 0
            SQL = "select a.no_faktur,a.Kode_Stock_Owner, a.Kode_Barang, b.nama, Nilai_PPIC, "

            SQL = SQL & "isnull((select sum(y.jumlah) from EMI_N_EMI_Transaksi_Purchase_Requisition_Trial x, EMI_N_EMI_Transaksi_Purchase_Requisition_Trial_Detail y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "
            SQL = SQL & "and y.Kode_Perusahaan = a.Kode_Perusahaan and y.Kode_Stock_Owner = a.Kode_Stock_Owner and  "
            SQL = SQL & "y.Kode_Barang = a.Kode_Barang and x.Status is null and "
            SQL = SQL & "a.No_Faktur = x.no_fak_material_requisition ),0) as jumlah_pr, c.satuan as Satuan_Display "

            SQL = SQL & "from EMI_Transaksi_Material_Requsition_detail a, barang b, barang_detail_satuan c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Kode_Barang = c.Kode_Barang and c.flag_tampil_display='Y' "

            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.no_faktur = '" & ListView1.FocusedItem.Text & "'"
            SQL = SQL & "and a.bulan  = '" & ListView1.FocusedItem.SubItems(4).Text & "' "
            SQL = SQL & "and a.tahun = '" & ListView1.FocusedItem.SubItems(2).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    DataGridView1.Rows.Add(1)
                    DataGridView1.Rows.Item(no).Cells(CellLokasi).Value = Dr("Kode_Stock_Owner")
                    DataGridView1.Rows.Item(no).Cells(CellKdBrg).Value = Dr("kode_barang")
                    DataGridView1.Rows.Item(no).Cells(CellNmBrg).Value = Dr("nama")
                    DataGridView1.Rows.Item(no).Cells(CellJmlOrder).Value = Format(Dr("nilai_ppic"), "N2")
                    DataGridView1.Rows.Item(no).Cells(CellJmlPR).Value = Format(Dr("jumlah_pr"), "N2")
                    DataGridView1.Rows.Item(no).Cells(CellSisa).Value = Format(Dr("nilai_ppic") - Dr("jumlah_pr"), "N2")

                    DataGridView1.Rows.Item(no).Cells(CellJmlInput).Value = 0
                    DataGridView1.Rows.Item(no).Cells(CellSatuan).Value = Dr("Satuan_Display")
                    DataGridView1.Rows.Item(no).Cells(CellTglDelivery).Value = ""
                    DataGridView1.Rows.Item(no).Cells(CellKeterangan).Value = ""

                    If no = 0 Then
                        TxtNoFak_MR.Text = Dr("no_faktur")
                    End If

                    no = no + 1
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub DataGridView1_DoubleClick(sender As Object, e As EventArgs) Handles DataGridView1.DoubleClick
        If DataGridView1.Rows.Count = 0 Then
            MessageBox.Show("Silahkan pilih data terlebih dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        ''If DataGridView1.CurrentRow.Cells(CellJmlInput).Value = "" Then
        ''    MessageBox.Show("Silahkan isi jumlah input terlebih dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        ''    Exit Sub
        ''End If
        'Dim SD_Tambah_PR As New SD_Tambah_PR(Me)


        Try
            OpenConn()

            SD_Tambah_PR.filter_tambahan = "and c.Kode_Stock_Owner = '" & ComboBox6.Text & "'"
            SD_Tambah_PR.asal = Jenis

            Dim sendKdBrg As String = DataGridView1.CurrentRow.Cells(CellKdBrg).Value
            SD_Tambah_PR.Lbl_GetKdBrg.Text = sendKdBrg

            Dim sendNmBrg As String = DataGridView1.CurrentRow.Cells(CellNmBrg).Value
            Dim PR As String = DataGridView1.CurrentRow.Cells(CellJmlPR).Value
            Dim Order As String = DataGridView1.CurrentRow.Cells(CellJmlOrder).Value
            Dim sisa As String = DataGridView1.CurrentRow.Cells(CellSisa).Value
            Dim jmlInput As String = DataGridView1.CurrentRow.Cells(CellJmlInput).Value
            Dim sendSatuan As String = DataGridView1.CurrentRow.Cells(CellSatuan).Value
            Dim sendTglDelivery As String = DataGridView1.CurrentRow.Cells(CellTglDelivery).Value
            Dim sendKeterangan As String = DataGridView1.CurrentRow.Cells(CellKeterangan).Value

            SD_Tambah_PR.kosong()

            SD_Tambah_PR.TxtPilihBarang_KodeBarang.Enabled = False
            SD_Tambah_PR.TxtPilihBarang_Satuan.Enabled = False
            SD_Tambah_PR.TxtPilihBarang_NamaBarang.Enabled = False
            SD_Tambah_PR.txtJumlah.Enabled = False
            SD_Tambah_PR.Lbl_PR.Visible = False
            SD_Tambah_PR.Lbl_Order.Visible = False
            SD_Tambah_PR.Lbl_Sisa.Visible = False

            SD_Tambah_PR.Lbl_PR.Visible = True
            SD_Tambah_PR.Lbl_Order.Visible = True
            SD_Tambah_PR.Lbl_Sisa.Visible = True
            SD_Tambah_PR.Txt_PR.Visible = True
            SD_Tambah_PR.Txt_Order.Visible = True
            SD_Tambah_PR.Txt_Sisa.Visible = True

            ''If DataGridView1.CurrentRow.Cells(CellSatuan).Value <> "" Then
            ''End If

            SD_Tambah_PR.CmbPilihBarang_Satuan.Text = sendSatuan
            SD_Tambah_PR.DateTimePicker1.Text = sendTglDelivery
            SD_Tambah_PR.txtKeterangan.Text = sendKeterangan

            SD_Tambah_PR.TxtPilihBarang_KodeBarang.Text = sendKdBrg
            SD_Tambah_PR.TxtPilihBarang_NamaBarang.Text = sendNmBrg

            SD_Tambah_PR.Txt_PR.Text = PR
            SD_Tambah_PR.Txt_Order.Text = Order
            SD_Tambah_PR.Txt_Sisa.Text = sisa
            SD_Tambah_PR.txtJumlah.Text = jmlInput


            Dim satuan As String = ""
            Dim indexTampilDisplay As Integer
            Dim index As Integer = 0

            OpenConn()
            SD_Tambah_PR.CmbPilihBarang_Satuan.Items.Clear()
            SQL = "select Satuan,Flag_Tampil_Display from barang_Detail_Satuan where Kode_Barang= '" & sendKdBrg & "' "
            SQL = SQL & "and flag_tampil_display = 'Y' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    SD_Tambah_PR.CmbPilihBarang_Satuan.Items.Add(dr("satuan"))

                    If General_Class.CekNULL(dr("Flag_Tampil_Display")) = "Y" Then
                        indexTampilDisplay = index
                    End If

                    index = index + 1
                Loop
            End Using
            SD_Tambah_PR.CmbPilihBarang_Satuan.SelectedIndex = indexTampilDisplay
            SD_Tambah_PR.CmbPilihBarang_Satuan.Enabled = False

            CloseConn()

            SD_Tambah_PR.ShowDialog()


        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()
    End Sub



    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Dim fSimpan As Boolean = False
        '
        For a As Integer = 0 To DataGridView1.Rows.Count - 1
            Get_Isi_Listview2(a)
            If DataGridView1.Rows.Item(a).Cells(CellJmlInput).Value > 0 Then
                fSimpan = True
                If Lv2Satuan = "" Then
                    MessageBox.Show("Satuan harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                ElseIf Lv2TglDelivery = "" Then
                    MessageBox.Show("Tanggal Delivery harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                    'ElseIf Lv2Keterangan = "" Then
                    '    MessageBox.Show("keterangan harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    '    Exit Sub
                End If

                'Exit For
                'Else
                'fSimpan = False

            End If

        Next
        '
        If Not fSimpan Then
            MessageBox.Show("Pilih dahulu data yang mau disimpan....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If


        If TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Harus diisi....!!", Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            get_no_faktur()
            '
            SQL = "insert into EMI_N_EMI_Transaksi_Purchase_Requisition_Trial( Kode_Perusahaan, No_Faktur, Lokasi, Tanggal, Jam, UserId, Keterangan, Flag_Forecast, No_Fak_Material_Requisition) values("
            SQL = SQL & "'" & KodePerusahaan & "','" & Txt_NoFaktur.Text & "', '" & ComboBox6.Text & "', "
            SQL = SQL & "'" & Format(DtpFormulator_Tanggal.Value, "yyyy-MM-dd") & "', '" & Format(DtpFormulator_Tanggal.Value, "HH:MM:ss") & "',"
            SQL = SQL & "'" & UserID & "', '" & TextBox2.Text.Trim & "', 'Y', '" & TxtNoFak_MR.Text & "')"
            ExecuteTrans(SQL)
            ''
            For i As Integer = 0 To DataGridView1.Rows.Count - 1
                If DataGridView1.Rows.Item(i).Cells(CellJmlInput).Value > 0 Then

                    Get_Isi_Listview2(i)

                    SQL = "insert into emi_N_EMI_Transaksi_Purchase_Requisition_Trial_detail(Kode_Perusahaan,No_Faktur,Kode_Stock_Owner,Kode_Barang,Jumlah,Satuan,Tanggal_Delivery,keterangan ) values("
                    SQL = SQL & "'" & KodePerusahaan & "', '" & Txt_NoFaktur.Text.Trim & "' ,"
                    SQL = SQL & "'" & Lv2Lokasi & "', '" & Lv2KdBrg & "' ,"
                    SQL = SQL & "'" & Lv2JmlInput & "',"
                    SQL = SQL & "'" & Lv2Satuan & "', '" & Lv2TglDelivery & "', "
                    SQL = SQL & "'" & Lv2Keterangan & "' )"
                    ExecuteTrans(SQL)

                End If
            Next

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            kosong()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub DataGridView1_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Get_Isi_Listview2(DataGridView1.CurrentRow.Index)
        If Val(Lv2JmlInput) < 0 Or IsNumeric(Lv2JmlInput) = False Then
            DataGridView1.CurrentRow.Cells(CellJmlInput).Value = 0
        End If


        If Val(HilangkanTanda(Lv2JmlInput)) + Val(HilangkanTanda(Lv2JmlPR)) > Val(HilangkanTanda(Lv2JmlOrder)) Then
            MessageBox.Show("Jumlah Input tidak boleh melebihi jumlah order!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DataGridView1.CurrentRow.Cells(CellJmlInput).Value = 0
        End If

        '''Get_Isi_Listview2(DataGridView1.CurrentRow.Index)
        '''Dim sisa As Decimal = Lv2Sisa
        '''Dim jumlah As Decimal

        '''If Decimal.TryParse(Lv2JmlInput, jumlah) Then
        '''    If jumlah > sisa Then
        '''        MessageBox.Show("Jumlah tidak boleh lebih dari " & sisa.ToString(), "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        '''        DataGridView1.CurrentCell.Value = Lv2Sisa
        '''        DataGridView1.BeginEdit(True)
        '''    End If
        '''End If

    End Sub

    'Private Sub SD_Data_PR_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
    '    N_EMI_Transaksi_Purchase_Requisition_Trial.BtnFormulator_Refresh_Click("", e)
    'End Sub


    Private Sub SD_Data_PR_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        N_EMI_Transaksi_Purchase_Requisition_Trial.kosong()
    End Sub

End Class