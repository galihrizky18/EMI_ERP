Public Class EMI_Selisih_Barang_Masuk_Validasi
    Dim Jenis = "Validasi_Barang_Masuk"
    Dim arr_tgl, arr_Lain As New ArrayList
    Dim arrInisialFaktur As String = ""
    Dim faktur As String = ""
    Public Tanda_SN As String = "#"
    Dim LvNo_Faktur As String
    Dim LvTanggal As String
    Dim LvJam As String
    Dim LvNoBM As String

    Private Sub Get_Isi_Listview(ByVal No_Index As Integer)
        LvNo_Faktur = ListView1.Items(No_Index).Text
        LvTanggal = ListView1.Items(No_Index).SubItems(1).Text
        LvJam = ListView1.Items(No_Index).SubItems(2).Text
        LvNoBM = ListView1.Items(No_Index).SubItems(3).Text

    End Sub

    Private Sub Display_Validasi_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Validasi_Barang_Masuk")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Selisih_BM")
            Base_Language.Get_Languages(Bahasa_Pilihan, "Validasi_Selisih_BM")
            BtnBarangMasuk_Cari.Text = Base_Language.Lang_Global_Cari
            'Label1.Text = Base_Language.Lang_Validasi_Selisih_BM_Judul

            ListView1.Columns.Clear()
            DataGridView1.Rows.Clear()
            ListView1.Columns.Add(Base_Language.Lang_Global_NoFaktur, 300, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal, 150, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Jam, 150, HorizontalAlignment.Center)
            ListView1.Columns.Add("Supplier", 150, HorizontalAlignment.Center)
            ListView1.Columns.Add("Total Selisih", 120, HorizontalAlignment.Center)
            ListView1.Columns.Add("Total Harga Selisih", 120, HorizontalAlignment.Right)
            ListView1.Columns.Add(Base_Language.Lang_Global_NO_BM, 0, HorizontalAlignment.Center)
            ListView1.Columns.Add("Kd_Supplier", 0, HorizontalAlignment.Center)

            'DataGridView1.Columns(0).HeaderText = Base_Language.Lang_Global_LokasiGudang
            'DataGridView1.Columns(1).HeaderText = Base_Language.Lang_Global_KodeBarang
            'DataGridView1.Columns(2).HeaderText = Base_Language.Lang_Global_NamaBarang
            'DataGridView1.Columns(3).HeaderText = Base_Language.Lang_Selisih_BM_Jumlah_PL
            'DataGridView1.Columns(4).HeaderText = Base_Language.Lang_Selisih_BM_Jumlah_BM
            'DataGridView1.Columns(5).HeaderText = Base_Language.Lang_Global_Satuan
            'DataGridView1.Columns(6).HeaderText = Base_Language.Lang_Selisih_BM_Selisih
            'DataGridView1.Columns(7).HeaderText = Base_Language.Lang_Selisih_BM_Penyelesaian_Plus
            'DataGridView1.Columns(8).HeaderText = Base_Language.Lang_Selisih_BM_Penyelesaian_Min
            'DataGridView1.Columns(9).HeaderText = Base_Language.lang_global_keterangan
            'DataGridView1.Columns(10).HeaderText = Base_Language.Lang_Selisih_BM_Selisih_Fix
            'DataGridView1.Columns(11).HeaderText = Base_Language.Lang_Selisih_BM_Selisih_Rp
            'DataGridView1.Columns(12).HeaderText = Base_Language.Lang_Selisih_BM_Harga_Per_PCS
            'DataGridView1.Columns(13).HeaderText = Base_Language.Lang_Global_Tanggal_Produksi
            'DataGridView1.Columns(14).HeaderText = Base_Language.Lang_Global_Tanggal_Expired

            ValidasiToolStripMenuItem.Text = Base_Language.Lang_Global_Validasi

            Cmb_Lokasi.Items.Clear()
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
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            Cmb_Lokasi.Text = Lokasi

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(Me, e)
    End Sub

    Private Sub Display_Validasi_Pembelian_Barang_Masuk_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
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
            SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Selisih, B.Jumlah_BM, b.Qty_PenyelesaianPlus, "
            SQL = SQL & "b.Qty_PenyelesaianMin, B.Selisih_Fix, B.Harga, b.Selisih_Rp, a.status, a.flag_validasi, b.Tgl_Produksi, b.Tgl_Expired, b.Serial_Number, b.satuan, No_Faktur_BM as No_PO "
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

                            If Val(HilangkanTanda(.Rows(index).Item("Qty_PenyelesaianPlus"))) <> 0 Or Val(HilangkanTanda(.Rows(index).Item("Qty_Penyelesaianmin"))) <> 0 Then
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

                                        SQL = "Update barang_sn set jumlah = jumlah + " & HilangkanTanda(.Rows(index).Item("Qty_PenyelesaianPlus")) & " where "
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

                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang SN Tidak Ditemukan")
                                        Exit Sub

                                    End If
                                End Using

                                '=========================
                                '=     UPDATE BARANG     =
                                '=========================
                                SQL = "select kode_barang from barang where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and "
                                SQL = SQL & "kode_barang = '" & .Rows(index).Item("Kode_Barang") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        SQL = "Update barang set good_stock = good_stock + " & HilangkanTanda(.Rows(index).Item("Qty_PenyelesaianPlus")) & " where "
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

                                        If (Val(Dr("Jumlah")) - Val(HilangkanTanda(.Rows(index).Item("Qty_PenyelesaianMin")))) < 0 Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Stock " & .Rows(index).Item("Kode_Barang") & " Menjadi Minus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        SQL = "Update barang_sn set jumlah = jumlah - " & HilangkanTanda(.Rows(index).Item("Qty_PenyelesaianMin")) & " where "
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

                                '=========================
                                '=     UPDATE BARANG     =
                                '=========================

                                SQL = "select kode_barang, Good_Stock from barang where "
                                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & .Rows(index).Item("Kode_Stock_Owner") & "' and "
                                SQL = SQL & "kode_barang = '" & .Rows(index).Item("Kode_Barang") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then

                                        If (Val(Dr("Good_Stock")) - Val(HilangkanTanda(.Rows(index).Item("Qty_PenyelesaianMin")))) < 0 Then
                                            Dr.Close()
                                            CloseTrans()
                                            CloseConn()
                                            MessageBox.Show("Stock " & .Rows(index).Item("Kode_Barang") & " Menjadi Minus!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                            Exit Sub
                                        End If

                                        SQL = "Update barang set good_stock = good_stock - " & HilangkanTanda(.Rows(index).Item("Qty_PenyelesaianMin")) & " where "
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

                            'If .Rows(index).Item("Selisih_Fix") = 0 Then
                            '    tdk_ada_selisih_fix = tdk_ada_selisih_fix + 1
                            'End If

                            'If .Rows(index).Item("Qty_PenyelesaianMin") + .Rows(index).Item("Qty_PenyelesaianMin") <> 0 Then
                            '    ada_selisih_plus_min = ada_selisih_plus_min + 1
                            'End If

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
                            'jumlah_masuk = jumlah_masuk + (.Rows(index).Item("Qty_PenyelesaianPlus") * .Rows(index).Item("Harga"))
                            'jumlah_keluar = jumlah_keluar + (.Rows(index).Item("Qty_PenyelesaianMin") * .Rows(index).Item("Harga"))

                            'If .Rows(index).Item("Selisih_Fix") > 0 Then
                            '    selisih_plus = selisih_plus + (.Rows(index).Item("Selisih_RP"))
                            'ElseIf .Rows(index).Item("Selisih_Fix") < 0 Then
                            '    selisih_min = selisih_min + (Math.Abs(.Rows(index).Item("Selisih_RP")))
                            'End If

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

            SQL = "Update EMI_Pembelian_Selisih_Barang_Masuk set flag_validasi = 'Y', "
            SQL = SQL & "Tgl_validasi='" & Format(tgl_skg, "yyyy-MM-dd") & "', jam_validasi='" & Format(tgl_skg, "HH:mm:ss") & "', user_validasi='" & UserID & "' "
            'SQL = SQL & ",kode_voucher = " & kode_voucher_fix & ", "
            'SQL = SQL & "kode_voucher2 = " & kode_voucher_fix2 & ", "
            'SQL = SQL & "kode_voucher3 = " & kode_voucher_fix3 & ", "
            'SQL = SQL & "Tgl_validasi = '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', "
            'SQL = SQL & "jam_validasi = '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
            'SQL = SQL & "user_validasi = '" & userid & "', "
            'SQL = SQL & "Flag_Opm = " & flag_opm & ", "
            'SQL = SQL & "Id_Transaksi = " & id_transaksi & " where "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "No_Faktur = '" & ListView1.FocusedItem.Text & "'"
            ExecuteTrans(SQL)

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

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click
        Try
            OpenConn()

            ListView1.Items.Clear()
            DataGridView1.Rows.Clear()
            SQL = "select a.No_Faktur, a.Tanggal, a.jam, No_Faktur_BM, a.Kode_Supplier, b.Nama as supplier, a.Total_Selisih, a.Total_Harga_Selisih "
            SQL = SQL & "FROM EMI_Pembelian_Selisih_Barang_Masuk a, Suppliers b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.Kode_Supplier = b.Kode_Supplier "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.Flag_Validasi is null "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "order by a.Tanggal asc, a.jam asc"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView1.Items.Add(.Rows(i).Item("no_faktur"))

                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("jam"))
                        Lvw.SubItems.Add(.Rows(i).Item("supplier"))
                        Lvw.SubItems.Add(.Rows(i).Item("Total_Selisih"))
                        Lvw.SubItems.Add(.Rows(i).Item("Total_Harga_Selisih"))

                        'Hide
                        Lvw.SubItems.Add(.Rows(i).Item("no_faktur_bm"))
                        Lvw.SubItems.Add(.Rows(i).Item("Kode_Supplier"))
                    Next
                End With
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
            If ListView1.Items.Count = 0 Then Exit Sub
            DataGridView1.Rows.Clear()
            Dim no As Integer = 0
            SQL = "select a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.Jumlah_PL,a.Jumlah_BM,a.Selisih, "
            SQL = SQL & "a.Qty_PenyelesaianPlus, a.Qty_PenyelesaianMin, a.Keterangan,a.Selisih_Fix,a.Harga, "
            SQL = SQL & "a.Selisih_Rp, a.No_Faktur_Barang_Masuk,a.Tgl_Produksi, a.Tgl_Expired "
            SQL = SQL & "from EMI_Pembelian_Selisih_Barang_Masuk_Det a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner "
            SQL = SQL & "and a.Kode_Barang = b.Kode_Barang and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & ListView1.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    DataGridView1.Rows.Add(1)
                    DataGridView1.Rows.Item(no).Cells(0).Value = Dr("Kode_Stock_Owner")
                    DataGridView1.Rows.Item(no).Cells(1).Value = Dr("kode_barang")
                    DataGridView1.Rows.Item(no).Cells(2).Value = Dr("nama")
                    DataGridView1.Rows.Item(no).Cells(3).Value = Format(Dr("jumlah_pl"), "N0")
                    DataGridView1.Rows.Item(no).Cells(4).Value = Format(Dr("jumlah_bm"), "N0")
                    DataGridView1.Rows.Item(no).Cells(5).Value = Format(Dr("selisih"), "N0")
                    DataGridView1.Rows.Item(no).Cells(6).Value = Format(Dr("Qty_PenyelesaianPlus"), "N0")
                    DataGridView1.Rows.Item(no).Cells(7).Value = Format(Dr("Qty_PenyelesaianMin"), "N0")
                    DataGridView1.Rows.Item(no).Cells(8).Value = General_Class.CekNULL(Dr("keterangan"))
                    DataGridView1.Rows.Item(no).Cells(9).Value = Format(Dr("selisih_fix"), "N0")
                    DataGridView1.Rows.Item(no).Cells(10).Value = Format(Dr("harga"), "N0")

                    DataGridView1.Rows.Item(no).Cells(11).Value = Format(Dr("selisih_rp"), "N0")
                    DataGridView1.Rows.Item(no).Cells(12).Value = Dr("No_Faktur_Barang_Masuk")
                    If General_Class.CekNULL(Dr("Tgl_Produksi")) = "" Then
                        DataGridView1.Rows.Item(no).Cells(13).Value = "-"
                        DataGridView1.Rows.Item(no).Cells(14).Value = "-"
                    Else
                        DataGridView1.Rows.Item(no).Cells(13).Value = Format(Dr("Tgl_Produksi"), "dd MMM yyyy")
                        DataGridView1.Rows.Item(no).Cells(14).Value = Format(Dr("Tgl_Expired"), "dd MMM yyyy")
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

End Class