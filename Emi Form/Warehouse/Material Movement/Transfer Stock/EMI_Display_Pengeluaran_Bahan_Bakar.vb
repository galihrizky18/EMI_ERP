Public Class EMI_Display_Pengeluaran_Bahan_Bakar

    Dim arrCari As New ArrayList
    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList

    Dim Lv_KdTransfer As String

    Dim item_KdTransfer As Integer = 0

    Private Sub Emi_Display_Transfer_Stock_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Emi_Display_Transfer_Stock_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Intial_ListView_Stock()
        kosong()

    End Sub

    Private Sub kosong()

        Lv_Stock.Items.Clear()
        Lv_Stock_Detail.Items.Clear()

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

            CmbSO_Asal.Items.Clear() : CmbSO_Asal.SelectedIndex = -1 : Arr1.Clear()
            CmbSO_Asal.Items.Add("--- SELURUH ---")
            SQL = "Select a.kode_stock_owner, a.inisial_faktur, a.pending_persediaan, a.persediaan, a.Keterangan "
            SQL = SQL & "From Stock_Owner_Gudang a, Binding_Lokasi_Gudang b where "
            SQL = SQL & "a.kode_Perusahaan=b.kode_Perusahaan and a.kode_stock_owner=b.kode_stock_owner_Gudang and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and b.kode_stock_owner='" & Lokasi & "' "
            SQL = SQL & "and a.aktif = 'Y' and (flag_produksi='Y' or Flag_Penyimpanan='Y') "
            SQL = SQL & "order by a.kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbSO_Asal.Items.Add(dr("Keterangan")) : Arr1.Add(dr("kode_stock_owner"))
                Loop
            End Using
            CmbSO_Asal.SelectedIndex = 0

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_FIlterTanggal.Enabled = False : Cmb_Filter_ParamLain.Enabled = False
        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        Txt_ParamLain.Enabled = False

        Cmb_FIlterTanggal.Items.Clear() : Arr2.Clear()
        Cmb_FIlterTanggal.Items.Add("Tanggal") : Arr2.Add("a.Tanggal")

        Cmb_Filter_ParamLain.Items.Clear() : arrCari.Clear()
        Cmb_Filter_ParamLain.Items.Add("No Transaksi") : arrCari.Add("a.no_faktur")
        Cmb_Filter_ParamLain.Items.Add("Lokasi Gudang") : arrCari.Add("a.Kode_stock_Owner")
        Cmb_Filter_ParamLain.Items.Add("Keterangan") : arrCari.Add("a.Keterangan")
        Cmb_Filter_ParamLain.Items.Add("Kode Barang") : arrCari.Add("a.Kode_Barang")
        Cmb_Filter_ParamLain.Items.Add("Nama Barang") : arrCari.Add("b.nama")
        Cmb_Filter_ParamLain.Items.Add("User") : arrCari.Add("a.userid")
        'ComboBox1.Items.Add("Lokasi Awal") : arrCari.Add("a.SO_Awal")
        'Cmb_Filter_ParamLain.Items.Add("Kode Barang") : arrCari.Add("b.Kode_Barang")

        CmbSO_Asal.DroppedDown = True
        CmbSO_Asal.Focus()

    End Sub

    Private Sub Intial_ListView_Stock()

        Lv_Stock.Columns.Clear()
        Lv_Stock.Columns.Add("No Transaksi", 130, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Keterangan", 300, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Tanggal", 100, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Jam", 80, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("User", 100, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Kode Barang", 130, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Lokasi Gudang", 150, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Nama", 200, HorizontalAlignment.Left)
        Lv_Stock.Columns.Add("Total", 130, HorizontalAlignment.Right)
        Lv_Stock.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_Stock.Columns.Add("Total Bags", 130, HorizontalAlignment.Right)
        Lv_Stock.View = View.Details

        Lv_Stock_Detail.Columns.Clear()
        Lv_Stock_Detail.Columns.Add("No Transaksi", 0, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Qr Code", 400, HorizontalAlignment.Left)
        Lv_Stock_Detail.Columns.Add("Jumlah", 130, HorizontalAlignment.Right)
        Lv_Stock_Detail.Columns.Add("Satuan", 90, HorizontalAlignment.Center)
        Lv_Stock_Detail.Columns.Add("Jumlah Bags", 130, HorizontalAlignment.Right)

        Lv_Stock_Detail.View = View.Details

    End Sub

    Private Sub Get_Tf_Stock_Listview(ByVal index As Integer)
        Lv_KdTransfer = Lv_Stock.Items(index).SubItems(item_KdTransfer).Text

    End Sub

    Private Sub Lv_Stock_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Stock.SelectedIndexChanged

        If Lv_Stock.Items.Count = 0 Then Exit Sub

        Get_Tf_Stock_Listview(Lv_Stock.FocusedItem.Index)

        If Lv_KdTransfer.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Lv_Stock_Detail.Items.Clear()
            SQL = "select c.Qr_Code+'-'+Kode_Unik_Berjalan as Qr, b.Jumlah, a.Satuan, b.Jumlah_Bags, a.no_faktur "
            SQL = SQL & "from EMI_Pengeluaran_Bahan_Bakar a, EMI_Pengeluaran_Bahan_Bakar_Det b, barang_sn c where "
            SQL = SQL & "a.kode_perusahaan=b.kode_perusahaan and a.No_Faktur=b.no_faktur "
            'SQL = SQL & "and a.status is null "
            SQL = SQL & "and b.Kode_Perusahaan=c.Kode_Perusahaan and b.Serial_Number=c.Serial_Number and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.No_Faktur = '" & Lv_Stock.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem

                    lv = Lv_Stock_Detail.Items.Add(Dr("no_faktur"))
                    lv.SubItems.Add(Dr("Qr"))
                    lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    lv.SubItems.Add(Dr("satuan"))
                    lv.SubItems.Add(Format(Dr("jumlah_bags"), "N2"))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub CetakUlangFakturToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangFakturToolStripMenuItem.Click
        If Lv_Stock.Items.Count = 0 Or Lv_Stock.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no faktur yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Try

            OpenConn()

            'SQL = "select a.Kode_Transfer "
            'SQL = SQL & "from Pengeluaran_Stock a, barang b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            'SQL = SQL & "and a.Kode_Barang = b.Kode_Barang "
            'SQL = SQL & "and a.so_awal = B.Kode_Stock_Owner "
            'SQL = SQL & "and a.Status is null "
            'SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.Kode_Transfer='" & Lv_Stock.FocusedItem.Text & "' "
            'Using Ds = BindingTrans(SQL)
            '    If Ds.Tables("MyTable").Rows.Count <> 0 Then
            '        Dim CrDoc As New Rpt_Faktur_Transfer_Stock       'Nama file CR
            '        With A_Place_For_Printing2
            '            CrDoc.SetDataSource(Ds)
            '            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
            '            CrDoc.RecordSelectionFormula = "{Tf_Stock.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Tf_Stock.Kode_Transfer} = '" & Lv_Stock.FocusedItem.Text & "'"
            '            .Text = "Faktur Transfer Stock"
            '            .CrystalReportViewer1.ReportSource = CrDoc
            '            .CrystalReportViewer1.DisplayGroupTree = False
            '            .Refresh()
            '            .Show()
            '        End With
            '    Else
            '        MessageBox.Show("Tidak ada data yang dapat dicetak!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    End If
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click
        If Chk_Transaksi_HariIni.Checked = False And Chk_Tanggal.Checked = False And Chk_Param_Lain.Checked = False Then
            MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
            Chk_Transaksi_HariIni.Focus() : Exit Sub
        ElseIf CmbSO_Asal.Text.Trim.Length = 0 Then
            MessageBox.Show("Lokasi Harus harus diisi!", Judul)
            CmbSO_Asal.Focus() : Exit Sub
        End If

        If Chk_Tanggal.Checked = True Then
            If Not Cmb_FIlterTanggal.SelectedIndex = -1 Then
                If DateTimePicker1.Value > DateTimePicker2.Value Then
                    MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                    DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                    Exit Sub
                End If
            Else
                MessageBox.Show("Pilih Dahulu Tanggal yang akan Di Filter!", Judul)
                Cmb_FIlterTanggal.Focus() : Exit Sub
            End If
        End If

        If Chk_Param_Lain.Checked = True Then
            If Cmb_Filter_ParamLain.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                Cmb_Filter_ParamLain.Focus() : Exit Sub
            ElseIf Txt_ParamLain.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                Txt_ParamLain.Focus() : Exit Sub
            End If

        End If

        Try
            OpenConn()

            Lv_Stock.Items.Clear() : Lv_Stock_Detail.Items.Clear()
            SQL = "select a.no_faktur, a.Keterangan, a.Tanggal, a.jam, a.userid, "
            SQL = SQL & "a.Kode_Stock_Owner, a.Kode_Barang, b.nama, a.Jumlah, a.Satuan, a.Jumlah_Bags, a.status "
            SQL = SQL & "from EMI_Pengeluaran_Bahan_Bakar a, barang b where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang and a.Kode_Stock_Owner = b.Kode_Stock_Owner "

            If Chk_Transaksi_HariIni.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal Between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(DateAdd(DateInterval.Day, 1, Now), "yyyy-MM-dd") & "' "

            End If

            If Chk_Tanggal.Checked = True Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.Tanggal between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If Chk_Param_Lain.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrCari.Item(Cmb_Filter_ParamLain.SelectedIndex) & " like '%" & Trim(Txt_ParamLain.Text) & "%' "
            End If

            'If CmbSO_Asal.SelectedIndex <> 0 Then
            '    If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

            '    SQL = SQL & "a.Kode_Stock_Owner = '" & CmbSO_Asal.Text & "' "
            'End If

            SQL = SQL & "order by a.Tanggal asc, a.Jam asc "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lv As New ListViewItem
                    lv = Lv_Stock.Items.Add(Dr("No_Faktur"))
                    lv.SubItems.Add(Dr("Keterangan"))
                    lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    lv.SubItems.Add(Dr("Jam"))
                    lv.SubItems.Add(Dr("UserID"))
                    lv.SubItems.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    lv.SubItems.Add(Dr("nama"))
                    lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    lv.SubItems.Add(Dr("Satuan"))
                    lv.SubItems.Add(Format(Dr("Jumlah_Bags"), "N2"))

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        lv.BackColor = Color.DarkRed
                        lv.ForeColor = Color.White
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

    Private Sub Chk_Transaksi_HariIni_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Transaksi_HariIni.CheckedChanged
        If Chk_Transaksi_HariIni.Checked = True Then
            Chk_Tanggal.Checked = False
            BtnBarangMasuk_Cari_Click(Chk_Tanggal, e)
        End If
    End Sub

    'Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
    '    If CheckBox1.Checked = True Then
    '        CheckBox2.Checked = False
    '        Btn_Cari_Click(CheckBox1, e)
    '    End If
    'End Sub

    Private Sub Chk_Tanggal_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Tanggal.CheckedChanged
        If Chk_Tanggal.Checked Then
            Cmb_FIlterTanggal.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Chk_Transaksi_HariIni.Checked = False
            Cmb_FIlterTanggal.DroppedDown = True
        Else
            Cmb_FIlterTanggal.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_FIlterTanggal.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub Chk_Param_Lain_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Param_Lain.CheckedChanged
        If Chk_Param_Lain.Checked = True Then
            Cmb_Filter_ParamLain.Enabled = True : Txt_ParamLain.Enabled = True
            Cmb_Filter_ParamLain.DroppedDown = True
        Else
            Cmb_Filter_ParamLain.Enabled = False : Txt_ParamLain.Enabled = False
            Cmb_Filter_ParamLain.SelectedIndex = -1 : Txt_ParamLain.Text = ""
        End If
    End Sub

    '=======================================================================================
    '=     MENU STRIP
    '=======================================================================================

    Private Sub SalinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalinToolStripMenuItem.Click
        If Lv_Stock.Items.Count = 0 Or Lv_Stock.SelectedItems.Count = 0 Or Lv_Stock.FocusedItem Is Nothing Then
            MessageBox.Show("Pilih dahulu no faktur yang mau salin!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(Lv_Stock.FocusedItem.Text)
    End Sub

    Private Sub BatalPengeluaranBahanBakarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalPengeluaranBahanBakarToolStripMenuItem.Click
        If Lv_Stock.Items.Count = 0 Or Lv_Stock.FocusedItem Is Nothing Then Exit Sub

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            Dim JudulNotif As String = "Pembatalan Pengeluaran Bahan Bakar"
            Dim NoFaktur As String = Lv_Stock.FocusedItem.Text

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Batal_Bahan_Bakar") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan Bahan Bakar", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Faktur Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            '========================================
            '=     CEK APAKAH FAKTUR DIBATALKAN     =
            '========================================
            SQL = "select Kode_Perusahaan from EMI_Pengeluaran_Bahan_Bakar "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoFaktur & "' and Status = 'Y'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Pengeluaran Bahan Bakar Tidak dapat Dilakukan karena No Faktur ini Sudah Dibatalkan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '========================================
            '=     CEK APAKAH SUDAH TUTUP SALDO     =
            '========================================
            Dim HasData As Boolean = False
            Dim TglTransaksi As DateTime
            SQL = "select Tanggal from EMI_Pengeluaran_Bahan_Bakar where status is null and Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & NoFaktur & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    HasData = True
                    TglTransaksi = Dr("Tanggal")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data RePengeluaran Bahan Bakar Tidak Ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            If HasData Then
                If CekSudahTutupSaldo(TglTransaksi) = "Y" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Pembatalan Pengeluaran Bahan Bakar tidak dapat dilakukan karena No Faktur Sudah Tutup Saldo", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End If

            '========================================
            '=     CEK APAKAH SUDAH LEWAT BULAN     =
            '========================================
            If Not tgl_skg.Month = TglTransaksi.Month Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Pembatalan Pengeluaran Bahan Bakar tidak dapat dilakukan karena Sudah Melewati Bulan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '=========================
            '=     ROLLBACK DATA     =
            '=========================

            Dim KdSo As String = ""
            Dim KdBarang As String = ""
            Dim SN As String = ""
            Dim Jumlah, JumlahBags, JumlahKecil As Double
            Dim Satuan As String = ""
            Dim KdVoucher As String = ""

            ' ROLLBACK BARANG SN
            SQL = "select a.No_Faktur, b.Kode_Stock_Owner, b.Kode_Barang, b.Serial_Number, b.Jumlah, b.Jumlah_Bags, a.satuan, a.Kode_Voucher, "
            SQL = SQL & "(dbo.ubah_satuan(a.kode_perusahaan, 'masa',a.kode_barang, a.satuan, 'Gram', b.Jumlah)) as Jumlah_Kecil, b.Jumlah_Terpakai "
            SQL = SQL & "from EMI_Pengeluaran_Bahan_Bakar a, EMI_Pengeluaran_Bahan_Bakar_Det b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & NoFaktur & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            If General_Class.CekNULL(.Rows(i).Item("Kode_Stock_Owner")) <> 0 Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pembatalan Pengeluaran Bahan Bakar tidak dapat dilakukan karena Kode Barang " & .Rows(i).Item("Kode_Barang") & " Sudah Dipakai", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If

                            KdSo = .Rows(i).Item("Kode_Stock_Owner")
                            KdBarang = .Rows(i).Item("Kode_Barang")
                            SN = .Rows(i).Item("Serial_Number")
                            Jumlah = .Rows(i).Item("Jumlah")
                            JumlahBags = .Rows(i).Item("Jumlah_Bags")
                            JumlahKecil = .Rows(i).Item("Jumlah_Kecil")
                            Satuan = .Rows(i).Item("satuan")
                            KdVoucher = .Rows(i).Item("Kode_Voucher")

                            '=================================
                            '=     CEK KESEIMBANGAN DATA     =
                            '=================================
                            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan, Data Tidak Seimbang . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                            '=     UPDATE BARANG SN     =
                            '============================
                            SQL = "select Jumlah, Jumlah_Bags from Barang_SN where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "update Barang_SN set Jumlah = Jumlah + " & JumlahKecil & ", Jumlah_Bags = Jumlah_Bags + " & JumlahBags & " "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Serial_Number = '" & SN & "' "
                                    ExecuteTrans(SQL)
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Pembatalan Pengeluaran Bahan Bakar Tidak Dapat Dilakukan karena Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '=========================
                            '=     UPDATE BARANG     =
                            '=========================
                            SQL = "select Good_Stock, Jumlah_Bags from barang where Kode_Perusahaan = '" & KodePerusahaan & "' "
                            SQL = SQL & "and Kode_Stock_Owner = '" & KdSo & "' and Kode_Barang = '" & KdBarang & "' "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then

                                    SQL = "update barang set Good_Stock = Good_Stock + " & JumlahKecil & ", Jumlah_Bags = Jumlah_Bags + " & JumlahBags & " "
                                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Stock_Owner = '" & KdSo & "' and Kode_Barang = '" & KdBarang & "' "
                                    ExecuteTrans(SQL)
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Pembatalan Pengeluaran Bahan Bakar Tidak Dapat Dilakukan karena Barang Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                            '================================
                            '=     CEK KESESUAIAN STOCK     =
                            '================================
                            SQL = "SELECT round(SUM(good_stock),2) AS good_stock, isnull((select round(sum(jumlah),2) from Barang_sn x "
                            SQL = SQL & "where a.kode_Barang=x.kode_Barang and a.Kode_Stock_Owner=x.kode_Stock_Owner "
                            SQL = SQL & "and a.kode_Perusahaan=x.kode_Perusahaan ),0) as Jumlah_sn, "
                            SQL = SQL & "isnull(round(SUM(jumlah_bags), 2), 0) AS jumlah_bags_barang, "
                            SQL = SQL & "isnull((select round(sum(Jumlah_Bags), 2) from Barang_sn y "
                            SQL = SQL & "where a.kode_Barang=y.kode_Barang and a.Kode_Stock_Owner=y.kode_Stock_Owner and a.kode_Perusahaan=y.kode_Perusahaan ), 0) as jumlah_bags_sn "
                            SQL = SQL & "FROM barang a WHERE a.Kode_Stock_Owner = '" & KdSo & "' "
                            SQL = SQL & "AND a.Kode_Barang = '" & KdBarang & "' and a.Kode_Perusahaan='" & KodePerusahaan & "' "
                            SQL = SQL & "group by a.kode_Barang, a.Kode_Stock_Owner, a.kode_Perusahaan "
                            Using Ds1 = BindingTrans(SQL)
                                If Ds1.Tables("MyTable").Rows.Count <> 0 Then
                                    If Ds1.Tables("MyTable").Rows(0).Item("good_stock") <> Ds1.Tables("MyTable").Rows(0).Item("Jumlah_sn") Or Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_barang") <> Ds1.Tables("MyTable").Rows(0).Item("jumlah_bags_sn") Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Terjadi Kesalahan, Data Tidak Sesuai . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Else
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Data tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                    Exit Sub
                                End If
                            End Using

                        Next
                    End If
                End With
            End Using

            '=========================
            '=     UPDATE JURNAL     =
            '=========================
            SQL = "SELECT Kode_Perusahaan FROM Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & KdVoucher & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        SQL = "delete Jurnal where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & KdVoucher & "' "
                        ExecuteTrans(SQL)
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Pembatalan Pengeluaran Bahan Bakar Tidak Dapat Dilakukan karena Jurnal Tidak Ditemukan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End With
            End Using

            '===============================
            '=     UPDATE DATA RESTOCK     =
            '===============================
            SQL = "select Kode_Perusahaan from EMI_Pengeluaran_Bahan_Bakar "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null "
            SQL = SQL & "and No_Faktur = '" & NoFaktur & "' "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        SQL = "update EMI_Pengeluaran_Bahan_Bakar set Status = 'Y', UserID_Batal = '" & UserID & "', "
                        SQL = SQL & "Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
                        SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and status is null "
                        SQL = SQL & "and No_Faktur = '" & NoFaktur & "' "
                        ExecuteTrans(SQL)

                    End If
                End With
            End Using

            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("Pengeluaran Bahan Bakar Berhasil Dibatalkan", JudulNotif, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        BtnBarangMasuk_Cari_Click(e, New EventArgs)

    End Sub

    '=======================================================================================
    '=     HANDLE KEYPRESS
    '=======================================================================================
    Private Sub CmbSO_Asal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbSO_Asal.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Transaksi_HariIni.Focus()
    End Sub

    Private Sub Chk_Transaksi_HariIni_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Transaksi_HariIni.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Tanggal.Focus()
    End Sub

    Private Sub Chk_Tanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Tanggal.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_Tanggal.Checked = True Then
                Cmb_FIlterTanggal.DroppedDown = True
                Cmb_FIlterTanggal.Focus()
            Else
                Chk_Param_Lain.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_FIlterTanggal_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_FIlterTanggal.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Chk_Param_Lain.Focus()
    End Sub

    Private Sub Chk_Param_Lain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Chk_Param_Lain.KeyPress
        If e.KeyChar = Chr(13) Then
            If Chk_Param_Lain.Checked Then
                Cmb_Filter_ParamLain.DroppedDown = True
                Cmb_Filter_ParamLain.Focus()
            Else
                BtnBarangMasuk_Cari.Focus()
            End If
        End If
    End Sub

    Private Sub Cmb_Filter_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Filter_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then Txt_ParamLain.Focus()
    End Sub

    Private Sub Txt_ParamLain_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_ParamLain.KeyPress
        If e.KeyChar = Chr(13) Then BtnBarangMasuk_Cari.Focus()
    End Sub

End Class