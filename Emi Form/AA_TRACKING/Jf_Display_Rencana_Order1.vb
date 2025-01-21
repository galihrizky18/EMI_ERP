Imports Microsoft.VisualBasic.PowerPacks.Printing.Compatibility.VB6
Public Class Jf_Display_Rencana_Order1
    Dim Arr1, Arr2, Arr3, arrbatal As New ArrayList
    Dim Clr_Selesai As Color = Color.LightBlue
    Dim Clr_Batal As Color = Color.Black
    Dim Clr_Default As Color = Color.Blue

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Display_Data_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LvRencanaOrder.Columns.Add("ID Rencana", 100, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Lokasi", 120, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Kode Supplier", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Nama Supplier", 200, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("No. PO", 240, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Tgl PO", 100, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("UserID", 120, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("RV", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Kode Kontainer", 120, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Jumlah Kontainer", 100, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("No. PO Pembelian", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Submit PO", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Loading Barang", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("OTW", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Draft", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Final", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Kirim", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Dok. Diterima", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Kapa Tiba", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Penjaluran", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("SPPB", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Tarik Kontainer", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Bongkar", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Biaya", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Freight", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Lokasi Kontainer", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Biling", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("HPP", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Barang Masuk", 0, HorizontalAlignment.Left)
        LvRencanaOrder.Columns.Add("Pembelian", 0, HorizontalAlignment.Left)

        LvRencanaOrder.View = View.Details

        LvDetailRencanaOrder.Columns.Add("Kode Barang", 200, HorizontalAlignment.Left)
        LvDetailRencanaOrder.Columns.Add("Nama Barang", 450, HorizontalAlignment.Left)
        LvDetailRencanaOrder.Columns.Add("Jumlah PO", 150, HorizontalAlignment.Right)
        LvDetailRencanaOrder.View = View.Details

        ChkTgl.Checked = False : ChkParameterLain.Checked = False
        CmbTgl.Items.Clear() : CmbTgl.Text = "" : Arr1.Clear()
        CmbTgl.Items.Add("Tanggal") : Arr1.Add("AND ro.tanggal_po")

        CmbParameterLain.Items.Clear() : CmbParameterLain.Text = "" : Arr2.Clear()
        CmbParameterLain.Items.Add("ID Rencana") : Arr2.Add("ro.id_rencana")
        CmbParameterLain.Items.Add("Kode Supplier") : Arr2.Add("ro.kode_supplier")
        CmbParameterLain.Items.Add("Nama Supplier") : Arr2.Add("s.nama")
        CmbParameterLain.Items.Add("No. PO") : Arr2.Add("ro.no_po")
        CmbParameterLain.Items.Add("UserID") : Arr2.Add("ro.UserID")
        CmbParameterLain.Items.Add("Kode Kontainer") : Arr2.Add("ro.kode_kontainer")
        CmbParameterLain.Items.Add("No. PO Pembelian") : Arr2.Add("ro.no_po_pembelian")

        DtpTgl1.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text) : DtpTgl2.Value = CDate(FMenuDev.ToolStripStatusLabel3.Text)
        TxtValue.Text = ""

        CmbSelesai.Items.Clear() : arrbatal.Clear()
        CmbSelesai.Items.Add("-- Seluruh --") : arrbatal.Add("")
        CmbSelesai.Items.Add("Batal") : arrbatal.Add("AND ro.status = 'Y' ")
        CmbSelesai.Items.Add("Tdk Batal") : arrbatal.Add("AND ro.status is null ")
        CmbSelesai.SelectedIndex = 2

        CmbTgl.Enabled = False : CmbParameterLain.Enabled = False
        DtpTgl1.Enabled = False : DtpTgl2.Enabled = False
        TxtValue.Enabled = False

        CmbTracking.Items.Clear()
        CmbTracking.Items.Add("-- Seluruh --")
        CmbTracking.Items.Add("Loading Barang")
        CmbTracking.Items.Add("OTW")
        CmbTracking.Items.Add("Tracking Dokumen")
        CmbTracking.Items.Add("Kapal Tiba")
        CmbTracking.Items.Add("Penjaluran")
        CmbTracking.Items.Add("SPPB")
        CmbTracking.Items.Add("Tarik Kontainer")
        CmbTracking.Items.Add("Bongkar")

        DataGridView1.Rows.Clear()


        Try
            OpenConn()

            CmbLokasi.Items.Clear()
            CmbLokasi.Items.Add("-- Seluruh --")

            xSplit = CekKotaRole().Split(",")
            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and kode_kota in("
            For i As Integer = 0 To xSplit.Count - 1
                SQL = SQL & "'" & xSplit(i).Trim & "', "
            Next
            SQL = Strings.Left(SQL, Len(SQL) - 2)
            SQL = SQL & ") "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    CmbLokasi.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using


            'CmbLokasi.Text = "Dist semarang"
            CmbLokasi.Text = Lokasi
            OpenConn()
            If CekButtonRole("Ganti_Lokasi_Display_Rencana_Order") = "T" Then
                CmbLokasi.Enabled = False
            Else
                CmbLokasi.Enabled = True
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        CmbLokasi.SelectedIndex = 0
        CmbTracking.SelectedIndex = 2
        'ChkTransaksiHariIni.Checked = True
        'Button1_Click(BtCari, e)

        '''''''''''''''''''''''''''''''''
        'CODING DAYAT/REZA
        '''''''''''''''''''''''''''''''''
        'list view kapal tiba
        'LvKapalTiba.Columns.Add("ID Rencana", 100, HorizontalAlignment.Center)
        'LvKapalTiba.Columns.Add("Tanggal Tiba", 200, HorizontalAlignment.Left)
        'LvKapalTiba.Columns.Add("Pelabuhan", 200, HorizontalAlignment.Left)
        'LvKapalTiba.Columns.Add("Tanggal Input", 150, HorizontalAlignment.Left)
        'LvKapalTiba.Columns.Add("User Input", 150, HorizontalAlignment.Left)
        'LvKapalTiba.View = View.Details

        ''list view tarik kontainer
        'LvTarikKontainer.Columns.Add("ID Rencana", 80, HorizontalAlignment.Center)
        'LvTarikKontainer.Columns.Add("Nomor Container", 200, HorizontalAlignment.Left)
        'LvTarikKontainer.Columns.Add("Tanggal Tarik", 100, HorizontalAlignment.Center)
        'LvTarikKontainer.Columns.Add("Tanggal Tarik Real", 120, HorizontalAlignment.Center)
        'LvTarikKontainer.Columns.Add("Berat Timbangan Bea Cukai", 290, HorizontalAlignment.Center)
        'LvTarikKontainer.Columns.Add("No Urut", 0, HorizontalAlignment.Center)
        'LvTarikKontainer.View = View.Details

        ''list view bongkar
        'LvBongkar.Columns.Add("ID Rencana", 150, HorizontalAlignment.Center)
        'LvBongkar.Columns.Add("Tanggal Bongkar", 200, HorizontalAlignment.Left)
        'LvBongkar.Columns.Add("Tanggal Input", 220, HorizontalAlignment.Left)
        'LvBongkar.Columns.Add("User Input", 220, HorizontalAlignment.Left)
        'LvBongkar.View = View.Details

        ''Listview Status OTW
        'LvTrackingPengiriman.Columns.Add("ETA", 200, HorizontalAlignment.Left)
        'LvTrackingPengiriman.Columns.Add("ETD", 200, HorizontalAlignment.Left)
        'LvTrackingPengiriman.Columns.Add("FREE TIME", 100, HorizontalAlignment.Center)
        'LvTrackingPengiriman.Columns.Add("NO BL", 300, HorizontalAlignment.Left)
        'LvTrackingPengiriman.View = View.Details

        ''Listview SPPB
        'LvSPPB.Columns.Add("Kode Gudang", 100, HorizontalAlignment.Left)
        'LvSPPB.Columns.Add("Ok", 70, HorizontalAlignment.Center)
        'LvSPPB.Columns.Add("Tanggal OK", 100, HorizontalAlignment.Left)
        'LvSPPB.Columns.Add("NHI", 50, HorizontalAlignment.Center)
        'LvSPPB.Columns.Add("Tanggal NHI", 100, HorizontalAlignment.Left)
        'LvSPPB.Columns.Add("Biaya NHI", 100, HorizontalAlignment.Right)
        'LvSPPB.Columns.Add("HICO", 70, HorizontalAlignment.Center)
        'LvSPPB.Columns.Add("Tanggal HICO", 100, HorizontalAlignment.Left)
        'LvSPPB.Columns.Add("Biaya HICO", 100, HorizontalAlignment.Right)
        'LvSPPB.View = View.Details

        ''Listview Penjaluran
        'LvPenjaluran.Columns.Add("No PIB", 300, HorizontalAlignment.Left)
        'LvPenjaluran.Columns.Add("Warna", 250, HorizontalAlignment.Left)
        'LvPenjaluran.Columns.Add("Jumlah Kontainer", 240, HorizontalAlignment.Left)
        'LvPenjaluran.View = View.Details

        ''Listview Tracking dokumen  
        'LvTrackingDokumen.Columns.Add("BL", 50, HorizontalAlignment.Center)
        'LvTrackingDokumen.Columns.Add("HC", 50, HorizontalAlignment.Center)
        'LvTrackingDokumen.Columns.Add("Form E", 50, HorizontalAlignment.Center)
        'LvTrackingDokumen.Columns.Add("Draft Tanggal", 100, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Draft Jam", 80, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Draft User", 80, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Flag Final", 80, HorizontalAlignment.Center)
        'LvTrackingDokumen.Columns.Add("Final Keterangan", 100, HorizontalAlignment.Center)
        'LvTrackingDokumen.Columns.Add("Final Tanggal", 80, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Final Jam", 80, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Final User", 80, HorizontalAlignment.Left)

        'LvTrackingDokumen.Columns.Add("No Resi", 100, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Scan Telex", 100, HorizontalAlignment.Center)
        'LvTrackingDokumen.Columns.Add("ETA Dokumen", 100, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Kirim Tanggal", 100, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Kirim Jam", 100, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Kirim User", 100, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Penerima", 100, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Finish Tanggal", 100, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Finish Jam", 100, HorizontalAlignment.Left)
        'LvTrackingDokumen.Columns.Add("Finish User", 100, HorizontalAlignment.Left)
        'LvTrackingDokumen.View = View.Details

        ''Log Listview SPPB
        'LvLogSppb.Columns.Add("Kode Gudang", 100, HorizontalAlignment.Left)
        'LvLogSppb.Columns.Add("Ok", 50, HorizontalAlignment.Center)
        'LvLogSppb.Columns.Add("Tanggal OK", 100, HorizontalAlignment.Left)
        'LvLogSppb.Columns.Add("NHI", 50, HorizontalAlignment.Center)
        'LvLogSppb.Columns.Add("Tanggal NHI", 100, HorizontalAlignment.Left)
        'LvLogSppb.Columns.Add("Biaya NHI", 100, HorizontalAlignment.Right)
        'LvLogSppb.Columns.Add("HICO", 50, HorizontalAlignment.Center)
        'LvLogSppb.Columns.Add("Tanggal HICO", 100, HorizontalAlignment.Left)
        'LvLogSppb.Columns.Add("Biaya HICO", 100, HorizontalAlignment.Right)
        'LvLogSppb.Columns.Add("Tanggal", 100, HorizontalAlignment.Left)
        'LvLogSppb.Columns.Add("Jam", 100, HorizontalAlignment.Left)
        'LvLogSppb.Columns.Add("User Id", 100, HorizontalAlignment.Left)
        'LvLogSppb.View = View.Details

        ''Log Listview Penjaluran
        'LvLogPenjaluran.Columns.Add("No PIB", 100, HorizontalAlignment.Left)
        'LvLogPenjaluran.Columns.Add("Warna", 100, HorizontalAlignment.Left)
        'LvLogPenjaluran.Columns.Add("Jumlah Kontainer", 100, HorizontalAlignment.Left)
        'LvLogPenjaluran.Columns.Add("Tanggal", 100, HorizontalAlignment.Left)
        'LvLogPenjaluran.Columns.Add("Jam", 100, HorizontalAlignment.Left)
        'LvLogPenjaluran.Columns.Add("User Id", 100, HorizontalAlignment.Left)
        'LvLogPenjaluran.View = View.Details

        'rencana order gabungan
        LvGabungan.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
        LvGabungan.Columns.Add("ID Rencana", 100, HorizontalAlignment.Left)
        LvGabungan.Columns.Add("Tanggal PO", 100, HorizontalAlignment.Left)
        LvGabungan.View = View.Details

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTgl.CheckedChanged
        If ChkTgl.Checked Then
            CmbTgl.Enabled = True : DtpTgl1.Enabled = True : DtpTgl2.Enabled = True
            ChkTransaksiHariIni.Checked = False
        Else
            CmbTgl.Enabled = False : DtpTgl1.Enabled = False : DtpTgl2.Enabled = False
            CmbTgl.SelectedIndex = -1 : DtpTgl1.Value = CDate(FMenu.ToolStripStatusLabel3.Text) : DtpTgl2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkParameterLain.CheckedChanged
        If ChkParameterLain.Checked Then
            CmbParameterLain.Enabled = True : TxtValue.Enabled = True
        Else
            CmbParameterLain.Enabled = False : TxtValue.Enabled = False
            CmbParameterLain.SelectedIndex = -1 : TxtValue.Text = ""
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkTransaksiHariIni.CheckedChanged
        If ChkTransaksiHariIni.Checked = True Then
            ChkTgl.Checked = False
            Button1_Click(ChkTransaksiHariIni, e)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtCari.Click
        'If ChkTgl.Checked = False And ChkParameterLain.Checked = False And ChkTransaksiHariIni.Checked = False Then
        '    MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
        '    ChkTgl.Focus() : Exit Sub
        'End If

        'If CmbTracking.SelectedIndex = -1 Then
        '    MessageBox.Show("Parameter pencarian tracking pengiriman harus diisi!", Judul)
        '    CmbTgl.Focus() : Exit Sub
        'End If

        If ChkTgl.Checked Then
            If CmbTgl.SelectedIndex = -1 Then
                MessageBox.Show("Parameter pencarian per tanggal harus diisi!", Judul)
                CmbTgl.Focus() : Exit Sub
            ElseIf DtpTgl1.Value > DtpTgl2.Value Then
                MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                DtpTgl1.Value = CDate(FMenu.ToolStripStatusLabel3.Text) : DtpTgl2.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
                Exit Sub
            End If
        End If
        If ChkParameterLain.Checked Then
            If CmbParameterLain.SelectedIndex = -1 Then
                MessageBox.Show("Parameter lain harus diisi!", Judul)
                CmbParameterLain.Focus() : Exit Sub
            ElseIf TxtValue.Text.Trim.Length = 0 Then
                MessageBox.Show("Value parameter lain harus diisi!", Judul)
                TxtValue.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            SQL = "SELECT ro.id_rencana, ro.lokasi, ro.kode_supplier, s.nama nama_supplier, "
            SQL = SQL & "ro.no_po, ro.tanggal_po, ro.userid, CAST(ro.rv AS BIGINT) rv, "
            SQL = SQL & "ro.kode_kontainer, ro.total_persen, ro.no_po_pembelian, ro.status, "
            SQL = SQL & "ro.selesai,ro.Flag_Submit_PO,ro.Flag_Loading_Barang,ro.Flag_OTW,"
            SQL = SQL & "ro.Flag_Draft,ro.Flag_Final,ro.Flag_Kirim,ro.Flag_Finish,"
            SQL = SQL & "ro.Flag_SPPB,ro.Flag_Penjaluran,ro.Flag_Kapal_Tiba,ro.Flag_Tarik_Kontainer,"
            SQL = SQL & "ro.Flag_Bongkar,ro.Flag_Sudah_Transaksi,ro.Flag_Sudah_Transaksi3,"
            SQL = SQL & "ro.Flag_Lokasi_Tujuan,ro.Flag_Billing,ro.Flag_HPP,"
            SQL = SQL & "ro.Flag_Barang_Masuk,ro.Flag_Pembelian "
            SQL = SQL & "FROM rencana_order ro, suppliers s "
            SQL = SQL & "WHERE ro.kode_perusahaan = s.kode_perusahaan AND "
            SQL = SQL & "ro.kode_supplier = s.kode_supplier AND ro.kode_perusahaan = '" & KodePerusahaan & "' and ro.status is null and ro.selesai is null  "

            'If CmbLokasi.SelectedIndex <> 0 Then
            '    SQL = SQL & " AND ro.lokasi = '" & CmbLokasi.Text & "' "
            'End If


            If CmbTracking.SelectedIndex = 0 Then
                SQL = SQL & " and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_hpp is null "
            ElseIf CmbTracking.Text = "Loading Barang" Then
                SQL = SQL & " and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw is null "

            ElseIf CmbTracking.Text = "OTW" Then
                SQL = SQL & " and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw ='Y' and ro.flag_kapal_tiba is null "

            ElseIf CmbTracking.Text = "Tracking Dokumen" Then
                SQL = SQL & " and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_finish is null and ro.flag_kapal_tiba is null  "

            ElseIf CmbTracking.Text = "Kapal Tiba" Then
                SQL = SQL & " and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw ='Y' and ro.flag_finish = 'Y' "
                SQL = SQL & " and ro.flag_kapal_tiba ='Y' and ro.flag_penjaluran is null "

            ElseIf CmbTracking.Text = "Penjaluran" Then
                SQL = SQL & " and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw ='Y' and ro.flag_finish = 'Y' "
                SQL = SQL & " and ro.flag_kapal_tiba ='Y' and ro.flag_penjaluran  = 'Y' and ro.flag_sppb  is null "

            ElseIf CmbTracking.Text = "SPPB" Then
                SQL = SQL & " and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw ='Y' and ro.flag_finish = 'Y' "
                SQL = SQL & " and ro.flag_kapal_tiba ='Y' and ro.flag_penjaluran = 'Y' and ro.flag_sppb = 'Y' and ro.flag_tarik_kontainer is null "

            ElseIf CmbTracking.Text = "Tarik Kontainer" Then
                SQL = SQL & " and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw ='Y' and ro.flag_finish = 'Y' "
                SQL = SQL & " and ro.flag_kapal_tiba ='Y' and ro.flag_penjaluran = 'Y' and ro.flag_sppb = 'Y' and ro.flag_tarik_kontainer ='Y' "
                SQL = SQL & " and ro.flag_bongkar is null "

            ElseIf CmbTracking.Text = "Bongkar" Then
                SQL = SQL & " and ro.flag_submit_po = 'Y' and ro.flag_loading_barang = 'Y' and ro.flag_otw ='Y' and ro.flag_finish = 'Y' "
                SQL = SQL & " and ro.flag_kapal_tiba ='Y' and ro.flag_penjaluran = 'Y' and ro.flag_sppb = 'Y' and ro.flag_tarik_kontainer ='Y' "
                SQL = SQL & " and ro.flag_bongkar ='Y' and ro.flag_hpp is null "
            End If


            SQL = SQL & arrbatal.Item(CmbSelesai.SelectedIndex)

            If ChkTransaksiHariIni.Checked Then
                SQL = SQL & "AND ro.tanggal_po BETWEEN '"
                SQL = SQL & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' AND '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            End If

            If ChkTgl.Checked Then
                SQL = SQL & Arr1.Item(CmbTgl.SelectedIndex) & " BETWEEN '"
                SQL = SQL & Format(DtpTgl1.Value, "yyyy-MM-dd") & "' AND '" & Format(DtpTgl2.Value, "yyyy-MM-dd") & "' "
            End If

            If ChkParameterLain.Checked Then
                SQL = SQL & "AND " & Arr2.Item(CmbParameterLain.SelectedIndex) & " LIKE '%" & Trim(TxtValue.Text) & "%' "
            End If

            If CmbLokasi.SelectedIndex = 0 Then
                SQL = SQL & "AND ro.Lokasi IN("
                Dim list_kota As String = ""
                For x As Integer = 1 To CmbLokasi.Items.Count - 1
                    list_kota = list_kota & "'" & CmbLokasi.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & "AND ro.Lokasi = '" & CmbLokasi.Text & "'"
            End If

            SQL = SQL & "Order BY ro.lokasi, ro.tanggal_po Desc "

            LvRencanaOrder.Items.Clear() : LvDetailRencanaOrder.Items.Clear()
            Dim Lvw As ListViewItem
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Lvw = LvRencanaOrder.Items.Add(.Rows(i).Item("id_rencana"))
                        Lvw.SubItems.Add(.Rows(i).Item("lokasi"))
                        Lvw.SubItems.Add("")
                        Lvw.SubItems.Add(.Rows(i).Item("nama_supplier"))
                        Lvw.SubItems.Add(.Rows(i).Item("no_po"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_po"), "dd-MMM-yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("userid"))
                        Lvw.SubItems.Add(.Rows(i).Item("rv"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_kontainer"))


                        Dim kontainer As Integer = (.Rows(i).Item("total_persen")) / 100
                        Dim jumlah As Integer = kontainer * 100
                        Dim selisih As Integer = (.Rows(i).Item("total_persen")) - jumlah


                        If selisih = 0 Or selisih <= 99 Then
                            Lvw.SubItems.Add(kontainer)
                        Else
                            Lvw.SubItems.Add(kontainer + 1)
                        End If



                        Lvw.SubItems.Add(General_Class.CekNULL(.Rows(i).Item("no_po_pembelian")))

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Submit_PO")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Submit_PO"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Loading_Barang")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Loading_Barang"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_OTW")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_OTW"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Draft")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Draft"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Final")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Final"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Kirim")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Kirim"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Finish")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Finish"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Kapal_Tiba")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Kapal_Tiba"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Penjaluran")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Penjaluran"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_SPPB")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_SPPB"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Tarik_Kontainer")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Tarik_Kontainer"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Bongkar")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Bongkar"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Sudah_Transaksi")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Sudah_Transaksi"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Sudah_Transaksi3")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Sudah_Transaksi3"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Lokasi_Tujuan")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Lokasi_Tujuan"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Billing")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Billing"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_HPP")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_HPP"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Barang_Masuk")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Barang_Masuk"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("Flag_Pembelian")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Flag_Pembelian"))
                        End If




                        LvRencanaOrder.Items(i).ForeColor = Clr_Default

                        If General_Class.CekNULL(.Rows(i).Item("selesai")) = "Y" Then
                            LvRencanaOrder.Items(i).BackColor = Clr_Selesai
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then
                            LvRencanaOrder.Items(i).ForeColor = Clr_Batal
                        End If
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

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtValue.KeyPress
        If e.KeyChar = Chr(13) Then Button1_Click(TxtValue, e)
    End Sub

    Private Sub CheckBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ChkTgl.KeyPress
        If e.KeyChar = Chr(13) Then
            If CmbTgl.Enabled = True Then
                CmbTgl.Focus()
            Else
                ChkParameterLain.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbTgl.KeyPress
        If e.KeyChar = Chr(13) Then DtpTgl1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DtpTgl1.KeyPress
        If e.KeyChar = Chr(13) Then DtpTgl2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DtpTgl2.KeyPress
        If e.KeyChar = Chr(13) Then Button1_Click(DtpTgl2, e)
    End Sub

    Private Sub CheckBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ChkParameterLain.KeyPress
        If e.KeyChar = Chr(13) Then
            If CmbParameterLain.Enabled = True Then
                CmbParameterLain.Focus()
            Else
                BtCari.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CmbParameterLain.KeyPress
        If e.KeyChar = Chr(13) Then TxtValue.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbTgl.SelectedIndexChanged
        DtpTgl1.Focus()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbParameterLain.SelectedIndexChanged
        TxtValue.Focus()
    End Sub



    Public Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LvRencanaOrder.SelectedIndexChanged
        Try
            If LvRencanaOrder.Items.Count = 0 Then Exit Sub

            OpenConn()

            LvDetailRencanaOrder.Items.Clear()
            SQL = "SELECT dro.kode_barang, b.nama nama_barang, dro.jumlah_po / dro.isi_satuan_besar jumlah_po "
            SQL = SQL & "FROM detail_rencana_order dro, barang b WHERE "
            SQL = SQL & "dro.kode_perusahaan = b.kode_Perusahaan AND dro.kode_stock_owner = b.kode_stock_owner AND dro.kode_barang = b.kode_barang AND "
            SQL = SQL & "dro.kode_perusahaan = '" & KodePerusahaan & "' AND dro.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' and dro.jumlah_po <> 0 "
            SQL = SQL & "ORDER BY dro.no_urut"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvDetailRencanaOrder.Items.Add(Dr("kode_barang"))
                    Lvw.SubItems.Add(Dr("nama_barang"))
                    Lvw.SubItems.Add(Format(Dr("jumlah_po"), "N0"))
                Loop
            End Using

            '''''''''''''''''''''''''''''''''
            'CODING DAYAT/REZA
            '''''''''''''''''''''''''''''''''
            'kapal tiba
            'LvKapalTiba.Items.Clear()
            'SQL = "SELECT ro.id_rencana, kt.tanggal_tiba, p.lokasi , kt.tanggal_input, kt.user_input "
            'SQL = SQL & "FROM kapal_tiba_import kt, rencana_order ro , pelabuhan p WHERE "
            'SQL = SQL & "kt.id_rencana = ro.id_rencana and kt.kode_pelabuhan = p.kode_pelabuhan and "
            'SQL = SQL & "ro.kode_perusahaan = '" & KodePerusahaan & "' AND ro.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
            'SQL = SQL & "ORDER BY ro.id_rencana"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = LvKapalTiba.Items.Add(General_Class.CekNULL(Dr("ID_Rencana")))
            '        If General_Class.CekNULL(Dr("tanggal_tiba")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal_tiba"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("lokasi")))
            '        If General_Class.CekNULL(Dr("tanggal_input")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal_tiba"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("user_input")))
            '    Loop
            'End Using

            ''Tarik Kontainer
            'LvTarikKontainer.Items.Clear()
            'SQL = "SELECT ro.id_rencana, tk.no_container, tk.tgl_tarik, tk.tgl_tarik_real,tk.no_urut, tk.berat_timbangan_bea_cukai "
            'SQL = SQL & "FROM tarik_kontainer tk, rencana_order ro WHERE "
            'SQL = SQL & "tk.id_rencana = ro.id_rencana and "
            'SQL = SQL & "ro.kode_perusahaan = '" & KodePerusahaan & "' AND ro.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
            'SQL = SQL & "ORDER BY tk.no_urut"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = LvTarikKontainer.Items.Add(General_Class.CekNULL(Dr("ID_Rencana")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("no_container")))
            '        If General_Class.CekNULL(Dr("tgl_tarik")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tgl_tarik"), "dd-MMM-yyyy"))
            '        End If
            '        If General_Class.CekNULL(Dr("tgl_tarik_real")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tgl_tarik_real"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("berat_timbangan_bea_cukai")))
            '        Lvw.SubItems.Add(Dr("no_urut"))
            '    Loop
            'End Using

            ''Bongkar
            'LvBongkar.Items.Clear()
            'SQL = "SELECT ro.id_rencana, bi.tanggal_bongkar, bi.tanggal_input, bi.user_input "
            'SQL = SQL & "FROM bongkar_import bi, rencana_order ro WHERE "
            'SQL = SQL & "bi.id_rencana = ro.id_rencana and "
            'SQL = SQL & "ro.kode_perusahaan = '" & KodePerusahaan & "' AND ro.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
            'SQL = SQL & "ORDER BY ro.id_rencana"
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = LvBongkar.Items.Add(General_Class.CekNULL(Dr("ID_Rencana")))
            '        If General_Class.CekNULL(Dr("tanggal_bongkar")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal_bongkar"), "dd-MMM-yyyy"))
            '        End If
            '        If General_Class.CekNULL(Dr("tanggal_Input")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal_Input"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("user_input")))
            '    Loop
            'End Using
            Dim otw, draft, kapal_tiba, penjaluran, sppb, bongkar, tarik_kontainer As String

            SQL = " select flag_otw, flag_draft,flag_kapal_tiba,flag_penjaluran,flag_sppb,flag_bongkar,flag_tarik_kontainer,total_persen"
            SQL = SQL & " from rencana_order  "
            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana ='" & LvRencanaOrder.FocusedItem.Text & "'"

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    otw = General_Class.CekNULL(Dr("flag_otw"))
                    draft = General_Class.CekNULL(Dr("flag_draft"))
                    kapal_tiba = General_Class.CekNULL(Dr("flag_kapal_tiba"))

                    penjaluran = General_Class.CekNULL(Dr("flag_penjaluran"))

                    sppb = General_Class.CekNULL(Dr("flag_sppb"))
                    bongkar = General_Class.CekNULL(Dr("flag_bongkar"))
                    tarik_kontainer = General_Class.CekNULL(Dr("flag_tarik_kontainer"))
                End If
                Dr.Close()
            End Using

            'cek penjaluran
            SQL = "select count(kode_perusahaan) as count from penjaluran_import where id_rencana ='" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan = '" & KodePerusahaan & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then

                    If Dr("count") > 0 Then
                        penjaluran = "Y"
                    Else
                        penjaluran = ""
                    End If
                End If
                Dr.Close()
            End Using

            SQL = ""

            If bongkar = "Y" Then
                SQL = SQL & " SELECT ('Tgl Bongkar : '+ format(bi.tanggal_bongkar,'dd MMM yyyy') ) as keterangan, 'Bongkar' as status"
                SQL = SQL & " FROM bongkar_import bi, rencana_order ro WHERE bi.id_rencana = ro.id_rencana and  "
                SQL = SQL & " ro.kode_perusahaan = '" & KodePerusahaan & "' AND ro.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'  "
                SQL = SQL & " union all "
            End If

            If tarik_kontainer = "Y" Then
                SQL = SQL & " SELECT top 1 ('Tgl Tarik Terakhir : '+ format(ISNULL((select top 1 tgl_tarik from tarik_kontainer where id_rencana ='" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan ='" & KodePerusahaan & "'),"
                SQL = SQL & " (select top 1 tgl_tarik from tarik_kontainer where id_rencana ='" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan ='" & KodePerusahaan & "')),'dd MMM yyyy')"
                SQL = SQL & " )as keterangan, 'Tarik Kontainer' as status "
                SQL = SQL & " FROM tarik_kontainer tk, rencana_order ro WHERE tk.id_rencana = ro.id_rencana and "
                SQL = SQL & " ro.kode_perusahaan = '" & KodePerusahaan & "' AND ro.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
                SQL = SQL & " union all "
            End If


            If sppb = "Y" Then
                SQL = SQL & " SELECT ('Status : ( '+ case when b.ok <> 'T' then 'OK | Tanggal : ' + format(b.Tanggal_Ok,'dd MMM yyyy' ) Else '' End +"
                SQL = SQL & " case when b.nhi <> '' then '| NHI | Tanggal : ' + format(b.Tanggal_NHI,'dd MMM yyyy' )  Else '' End +"
                SQL = SQL & " case when b.hico <> '' then ' | HICO - Tanggal : ' + format(b.Tanggal_HICO,'dd MMM yyyy' )  Else '' End +"
                SQL = SQL & " ' )'  "
                SQL = SQL & " ) as keterangan , 'SPPB' as status from  rencana_order a ,sppb_import b "
                SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.id_rencana = b.id_rencana and "
                SQL = SQL & " a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
                SQL = SQL & " union all "
            End If

            If penjaluran = "Y" Then
                SQL = SQL & "  select ('Status Warna : '+ b.warna) as keterangan"
                SQL = SQL & " , 'Penjaluran' as status from Rencana_Order a,penjaluran_import b  "
                SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.id_rencana = b.id_rencana and "
                SQL = SQL & " a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'  "
                SQL = SQL & " union all "

            End If

            If kapal_tiba = "Y" Then
                SQL = SQL & " SELECT ('Tgl Tiba : '+ format(kt.tanggal_tiba,'dd MMM yyyy') )as keterangan, 'Kapal Tiba' as status"
                SQL = SQL & " FROM kapal_tiba_import kt, rencana_order ro , pelabuhan p "
                SQL = SQL & " WHERE kt.id_rencana = ro.id_rencana and kt.kode_pelabuhan = p.kode_pelabuhan and ro.kode_perusahaan = '" & KodePerusahaan & "'"
                SQL = SQL & " AND ro.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
                SQL = SQL & " union all "
            End If

            If draft = "Y" Then
                SQL = SQL & " SELECT ('Status : ( '+ case when b.BL = 'Y' and b.Flag_Final is null and b.Kirim_Jam is null and b.finish_jam is null  then 'Draft' Else '' End +"
                SQL = SQL & " case when b.BL = 'Y' and b.Flag_Final is not null and b.Kirim_Jam is null and b.finish_jam is null  then 'Final' Else '' End +"
                SQL = SQL & " case when b.BL = 'Y' and b.Flag_Final is not null and b.Kirim_Jam is not null and b.finish_jam is null  then ' Kirim' Else '' End +"
                SQL = SQL & " case when  b.BL = 'Y' and b.Flag_Final is not null and b.Kirim_Jam is not null and b.finish_jam is not null  then ' Terkirim' Else '' End +'  )') AS keterangan,"
                SQL = SQL & " 'Tracking Dokumen' AS status"
                SQL = SQL & " from Rencana_Order a, Tracking_Dokumen b  "
                SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.id_rencana = b.id_rencana and a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'  "
                SQL = SQL & " union all "
            End If

            If otw = "Y" Then
                SQL = SQL & " select ('ETD : '+format(b.etd,'dd MMM yyyy')+' | ETA :'+format(b.eta,'dd MMM yyyy') ) as keterangan,  "
                SQL = SQL & " 'OTW' as status "
                SQL = SQL & " from rencana_order a, ubah_status_otw b  "
                SQL = SQL & " where a.kode_perusahaan = b.kode_perusahaan and a.id_rencana = b.id_rencana and a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'  "

                DataGridView1.Rows.Clear()



                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        For index As Integer = 0 To .Rows.Count - 1

                            DataGridView1.Rows.Add(.Rows(index).Item("status") & Environment.NewLine & .Rows(index).Item("keterangan"))
                            DataGridView1.Rows.Item(index).Cells(1).Value = .Rows(index).Item("status")

                        Next
                    End With
                End Using
            Else
                DataGridView1.Rows.Clear()
                DataGridView1.Rows.Add("ID Rencana ini belum memiliki history pengiriman")
                'DataGridView1.Rows.Item(index).Cells(1).Value = .Rows(index).Item("status")

            End If

            'ubah status otw

            'LvTrackingPengiriman.Items.Clear()
            'SQL = "select b.eta,b.etd, b.free_time, b.no_bl from rencana_order a, ubah_status_otw b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and "
            'SQL = SQL & "a.id_rencana = b.id_rencana and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = LvTrackingPengiriman.Items.Add(Format(Dr("eta"), "dd-MMM-yyyy"))
            '        Lvw.SubItems.Add(Format(Dr("etd"), "dd-MMM-yyyy"))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("free_time")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("no_bl")))
            '    Loop
            'End Using

            'sppb import
            'LvSPPB.Items.Clear()
            'SQL = "select b.kode_gudang,b.ok,b.tanggal_ok,b.nhi,b.tanggal_nhi,b.biaya_nhi,b.hico,b.tanggal_hico,b.biaya_hico from  rencana_order a ,sppb_import b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and "
            'SQL = SQL & "a.id_rencana = b.id_rencana and "
            'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = LvSPPB.Items.Add(General_Class.CekNULL(Dr("kode_gudang")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("ok")))
            '        If General_Class.CekNULL(Dr("tanggal_ok")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal_ok"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("nhi")))
            '        If General_Class.CekNULL(Dr("tanggal_nhi")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal_nhi"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("biaya_nhi")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("hico")))
            '        If General_Class.CekNULL(Dr("tanggal_hico")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal_hico"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("biaya_hico")))
            '    Loop
            'End Using

            ''penjaluran import
            'LvPenjaluran.Items.Clear()
            'SQL = "select b.no_pib, b.warna, b.jml_kontainer from Rencana_Order a,penjaluran_import b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and "
            'SQL = SQL & "a.id_rencana = b.id_rencana and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = LvPenjaluran.Items.Add(General_Class.CekNULL(Dr("no_pib")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("warna")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("jml_kontainer")))
            '    Loop
            'End Using

            ''tracking dokumen
            'LvTrackingDokumen.Items.Clear()
            'SQL = "select b.bl, b.hc, b.form_e,b.draft_tanggal, b.draft_jam, b.draft_user, "
            'SQL = SQL & "b.flag_final, b.final_keterangan, b.final_tanggal, b.final_jam, b.final_user,"
            'SQL = SQL & "b.no_resi, b.scan_telex, b.eta_dokumen, b.kirim_tanggal,b.kirim_jam, b.kirim_user, "
            'SQL = SQL & "b.penerima, b.finish_tanggal, b.finish_jam, b.finish_user "
            'SQL = SQL & "from Rencana_Order a, Tracking_Dokumen b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and "
            'SQL = SQL & "a.id_rencana = b.id_rencana and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = LvTrackingDokumen.Items.Add(General_Class.CekNULL(Dr("bl")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("hc")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("form_e")))
            '        If General_Class.CekNULL(Dr("draft_tanggal")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("draft_tanggal"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("draft_jam")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("draft_user")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("flag_final")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("final_keterangan")))

            '        If General_Class.CekNULL(Dr("final_tanggal")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("final_tanggal"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("final_jam")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("final_user")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("no_resi")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("scan_telex")))

            '        If General_Class.CekNULL(Dr("eta_dokumen")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("eta_dokumen"), "dd-MMM-yyyy"))
            '        End If
            '        If General_Class.CekNULL(Dr("kirim_tanggal")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("kirim_tanggal"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("kirim_jam")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("kirim_user")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("penerima")))
            '        If General_Class.CekNULL(Dr("finish_tanggal")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("finish_tanggal"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("finish_jam")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("finish_user")))
            '    Loop
            'End Using

            ''log sppb import
            'LvLogSppb.Items.Clear()
            'SQL = "select b.kode_gudang,b.ok,b.tanggal_ok,b.nhi,b.tanggal_nhi,b.biaya_nhi,b.hico,b.tanggal_hico,b.biaya_hico,b.tanggal,b.jam,b.userId from  rencana_order a ,log_sppb_import b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and "
            'SQL = SQL & "a.id_rencana = b.id_rencana and "
            'SQL = SQL & "a.Kode_Perusahaan = '" & KodePerusahaan & "' and a.ID_Rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = LvLogSppb.Items.Add(General_Class.CekNULL(Dr("kode_gudang")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("ok")))
            '        If General_Class.CekNULL(Dr("tanggal_ok")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal_ok"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("nhi")))
            '        If General_Class.CekNULL(Dr("tanggal_nhi")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal_nhi"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("biaya_nhi")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("hico")))
            '        If General_Class.CekNULL(Dr("tanggal_hico")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal_hico"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("biaya_hico")))
            '        If General_Class.CekNULL(Dr("tanggal")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("jam")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("userId")))
            '    Loop
            'End Using

            ''log penjaluran import
            'LvLogPenjaluran.Items.Clear()
            'SQL = "select b.no_pib, b.warna, b.jml_kontainer, b.tanggal,b.jam,b.userId from Rencana_Order a,log_penjaluran_import b "
            'SQL = SQL & "where a.kode_perusahaan = b.kode_perusahaan and "
            'SQL = SQL & "a.id_rencana = b.id_rencana and "
            'SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
            'Using Dr = OpenTrans(SQL)
            '    Do While Dr.Read
            '        Dim Lvw As ListViewItem
            '        Lvw = LvLogPenjaluran.Items.Add(General_Class.CekNULL(Dr("no_pib")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("warna")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("jml_kontainer")))
            '        If General_Class.CekNULL(Dr("tanggal")) = "" Then
            '            Lvw.SubItems.Add("-")
            '        Else
            '            Lvw.SubItems.Add(Format(Dr("tanggal"), "dd-MMM-yyyy"))
            '        End If
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("jam")))
            '        Lvw.SubItems.Add(General_Class.CekNULL(Dr("userId")))
            '    Loop
            'End Using

            'rencana order gabungan
            LvGabungan.Items.Clear()
            SQL = "select a.lokasi,b.id_rencana,a.tanggal_po from rencana_order a, Rencana_Order_Gabungan b where a.ID_Rencana=b.ID_Rencana "
            SQL = SQL & "and a.kode_perusahaan = b.kode_perusahaan and "
            SQL = SQL & "a.id_rencana = b.id_rencana and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvGabungan.Items.Add(Dr("lokasi"))
                    Lvw.SubItems.Add(General_Class.CekNULL(Dr("id_rencana")))
                    If General_Class.CekNULL(Dr("tanggal_po")) = "" Then
                        Lvw.SubItems.Add("-")
                    Else
                        Lvw.SubItems.Add(Format(Dr("tanggal_po"), "dd-MMM-yyyy"))
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



    Private Sub Kosong()
        LvRencanaOrder.Items.Clear()
        LvDetailRencanaOrder.Items.Clear()
        'LvTrackingPengiriman.Items.Clear()
        'LvTrackingDokumen.Items.Clear()
        'LvKapalTiba.Items.Clear()
        'LvPenjaluran.Items.Clear()
        'LvSPPB.Items.Clear()
        'LvTarikKontainer.Items.Clear()
        'LvBongkar.Items.Clear()
        LvGabungan.Items.Clear()
    End Sub

    Private Sub UpdateTanggalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub
    Private Sub LvTarikKontainer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub LvStatusOtw_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'Display_Edit_Tanggal_ETA.ShowDialog()
    End Sub

    Private Sub LvStatusOtw_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub UpdateTanggalTarikToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'Display_Edit_Tanggal_Tarik.ShowDialog()
    End Sub

    Private Sub UpdateBeratTimbanganToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        'Display_Edit_Berat_Timbangan.ShowDialog()
    End Sub

    'Private Sub UpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    
    'End Sub

    Private Sub UpdateToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RencanaOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RencanaOrderToolStripMenuItem.Click
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("batal_rencana_order") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv, flag_submit_po FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("flag_submit_po")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah disubmit PO!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("ID Rencana Order tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "UPDATE rencana_order SET status = 'Y' WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_rencana_order(kode_perusahaan, id_rencana, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "', "
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Rencana Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                LvRencanaOrder.FocusedItem.ForeColor = Clr_Batal
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub StatusOTWToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StatusOTWToolStripMenuItem.Click
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan Status Otw rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft is null and a.flag_final is null and a.flag_kirim is null "
        filter_flag = filter_flag & " and a.flag_finish is null and a.flag_kapal_tiba is null and a.flag_penjaluran is null and a.flag_sppb is null and a.flag_bongkar is null "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer is null  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Status_Otw") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_flag
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("ID Rencana Order tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "UPDATE rencana_order SET flag_otw = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal Otw' ,"
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "Delete ubah_status_otw WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Status Otw Rencana Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub KapalTibaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KapalTibaToolStripMenuItem.Click
        'batal kapal tiba
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan Kapal Tiba rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft = 'Y' and a.flag_final = 'Y' and a.flag_kirim = 'Y' "
        filter_flag = filter_flag & " and a.flag_finish = 'Y' and a.flag_kapal_tiba = 'Y' and a.flag_penjaluran is null and a.flag_sppb is null and a.flag_bongkar is null "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer is null  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Kapal_Tiba") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv, a.flag_penjaluran FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("flag_penjaluran")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("ID Rencana ini telah di tahapan penjaluran", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini telah di tahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                'cek log_penjaluran_import kalo ada delete
                SQL = "SELECT count(id_rencana) as count from log_penjaluran_import a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'  "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekZERO(Dr("count")) <> 0 Then
                            SQL = "Delete log_penjaluran_import WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                            ExecuteTrans(SQL)
                        End If
                    End If
                End Using

                SQL = "UPDATE rencana_order SET flag_kapal_tiba = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal Kapal Tiba' ,"
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "Delete kapal_tiba_import WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Kapal Tiba Rencana Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub PenjaluranToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PenjaluranToolStripMenuItem.Click
        'batal penjaluran
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan Penjaluran rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft = 'Y' and a.flag_final = 'Y' and a.flag_kirim = 'Y' "
        filter_flag = filter_flag & " and a.flag_finish = 'Y' and a.flag_kapal_tiba = 'Y' and a.flag_sppb is null and a.flag_bongkar is null "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer is null  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Penjaluran") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "SELECT warna FROM penjaluran_import a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("warna")) = "Hijau" Then
                            filter_flag = filter_flag & " and a.flag_penjaluran = 'Y' "
                        ElseIf General_Class.CekNULL(Dr("warna")) <> "Hijau" Then
                            filter_flag = filter_flag & " and a.flag_penjaluran is null "
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan, karena id rencana ini belum ditahapan penjaluran!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena id rencana ini belum ditahapan penjaluran!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv, a.flag_sppb FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_flag
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("flag_sppb")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order telah ditahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini telah di tahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "UPDATE rencana_order SET flag_penjaluran = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal Penjaluran', "
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "Delete Penjaluran_Import WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "Delete log_penjaluran_import WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Penjaluran Rencana Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub SPPBToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SPPBToolStripMenuItem.Click
        'batal sppb
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan SPPB rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft = 'Y' and a.flag_final = 'Y' and a.flag_kirim = 'Y' "
        filter_flag = filter_flag & " and a.flag_finish = 'Y' and a.flag_kapal_tiba = 'Y' and a.flag_penjaluran = 'Y' and a.flag_bongkar is null "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer is null  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Sppb") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                ' cek sppb_import kalo ada data bisa lanjut kalo gada ditolak
                SQL = "SELECT ok FROM sppb_import a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("ok")) = "Y" Then
                            filter_flag = filter_flag & " and a.flag_sppb = 'Y' "
                        Else
                            filter_flag = filter_flag & " and a.flag_sppb is null "
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena id rencana ini belum ditahapan sppb!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv, a.flag_tarik_kontainer FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_flag
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("flag_tarik_kontainer")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order telah ditahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini telah di tahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "UPDATE rencana_order SET flag_sppb = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal SPPB' ,"
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "Delete sppb_import WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "Delete log_sppb_import WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("SPPB Rencana Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub TarikKontainerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TarikKontainerToolStripMenuItem.Click
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu tarik kontainer yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan tarik kontainer rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft = 'Y' and a.flag_final = 'Y' and a.flag_kirim = 'Y' "
        filter_flag = filter_flag & " and a.flag_finish = 'Y' and a.flag_kapal_tiba = 'Y' and a.flag_penjaluran = 'Y' and a.flag_sppb = 'Y' and a.flag_bongkar is null "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer = 'Y'  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Tarik_Kontainer") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_flag
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini telah di tahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "UPDATE rencana_order SET flag_tarik_kontainer = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal Tarik Kontainer', "
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "Delete tarik_kontainer WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Status Tarik Kontainer berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()

            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BongkarToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BongkarToolStripMenuItem.Click
        'batal bongkar
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu bongkar import yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan bongkar kontainer rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft = 'Y' and a.flag_final = 'Y' and a.flag_kirim = 'Y' "
        filter_flag = filter_flag & " and a.flag_finish = 'Y' and a.flag_kapal_tiba = 'Y' and a.flag_penjaluran = 'Y' and a.flag_sppb = 'Y' and a.flag_bongkar = 'Y' "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer = 'Y'  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Bongkar") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv, a.flag_gabungan FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_flag
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("flag_gabungan")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("ID Rencana ini tidak telah ditahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini telah di tahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "UPDATE rencana_order SET flag_bongkar = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal Bongkar Import', "
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "Delete bongkar_import WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Status Bongkar Import berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub RencanaOrderGabunganToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RencanaOrderGabunganToolStripMenuItem.Click
        'batal rencana order gabungan
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order gabungan yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan gabungan rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft = 'Y' and a.flag_final = 'Y' and a.flag_kirim = 'Y' "
        filter_flag = filter_flag & " and a.flag_finish = 'Y' and a.flag_kapal_tiba = 'Y' and a.flag_penjaluran = 'Y' and a.flag_sppb = 'Y' and a.flag_bongkar = 'Y' and a.flag_gabungan = 'Y' "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer = 'Y'  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Rencana_Order_Gabungan") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_flag
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini telah di tahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "UPDATE rencana_order SET flag_gabungan = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal Rencana Order Gabungan' ,"
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "Delete rencana_order_gabungan WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Status Rencana Order Gabungan berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub DraftToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DraftToolStripMenuItem.Click
        'tracking dokumen draft
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan tracking dokument draft rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft is null and a.flag_final is null and a.flag_kirim is null "
        filter_flag = filter_flag & " and a.flag_finish is null and a.flag_kapal_tiba is null and a.flag_penjaluran is null and a.flag_sppb is null and a.flag_bongkar is null "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer is null  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Tracking_Dokumen") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_flag
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini telah di tahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal Tracking Dokument Draft', "
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "delete tracking_dokumen WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Tracking Dokument Draft Rencana Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub FinalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FinalToolStripMenuItem.Click
        'tracking dokumen final
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan tracking dokument final rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft = 'Y' and a.flag_final = 'Y' and a.flag_kirim is null "
        filter_flag = filter_flag & " and a.flag_finish is null and a.flag_kapal_tiba is null and a.flag_penjaluran is null and a.flag_sppb is null and a.flag_bongkar is null "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer is null  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Tracking_Dokumen") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_flag
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini telah di tahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "UPDATE rencana_order SET flag_draft = null, flag_final = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal Tracking Dokument Final', "
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "update tracking_dokumen set Flag_Final = null , Final_keterangan = null, final_tanggal = null, final_user = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Tracking Dokument Final Rencana Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub KirimToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KirimToolStripMenuItem.Click
        'tracking dokumen batal kirim
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan tracking dokument kirim rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft = 'Y' and a.flag_final = 'Y' "
        filter_flag = filter_flag & " and a.flag_kapal_tiba is null and a.flag_penjaluran is null and a.flag_sppb is null and a.flag_bongkar is null "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer is null  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "


        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Tracking_Dokumen") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                SQL = "SELECT a.scan_telex  FROM tracking_dokumen a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("scan_telex")) = "T" Then
                            filter_flag = filter_flag & " and a.flag_kirim = 'Y' and a.flag_finish is null "
                        ElseIf General_Class.CekNULL(Dr("scan_telex")) = "Y" Then
                            filter_flag = filter_flag & " and a.flag_kirim = 'Y' and a.flag_finish = 'Y'"
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana belum ditahapan ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If

                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "SELECT a.selesai, a.status, a.no_po_pembelian, CAST(a.rv AS BIGINT) rv  FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_flag
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini telah di tahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "UPDATE rencana_order SET flag_kirim = null , flag_finish = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal Tracking Dokument Kirim' ,"
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "update tracking_dokumen set no_resi = null, scan_telex = null, eta_dokumen = null, kirim_tanggal = null, kirim_jam = null, kirim_user = null "
                SQL = SQL & " WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Tracking Dokument Kirim Rencana Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub DokDiterimaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DokDiterimaToolStripMenuItem.Click
        'tracking dokumen batal finish
        If LvRencanaOrder.Items.Count = 0 Or LvRencanaOrder.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu rencana order yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan tracking dokument diterima rencana order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        Dim filter_flag As String = " and a.Flag_submit_PO = 'Y' and a.flag_loading_barang = 'Y' and a.flag_otw = 'Y' and a.flag_draft = 'Y' and a.flag_final = 'Y'  "
        filter_flag = filter_flag & " and a.flag_kapal_tiba is null and a.flag_penjaluran is null and a.flag_sppb is null and a.flag_bongkar is null "
        filter_flag = filter_flag & " and a.flag_tarik_kontainer is null  and a.flag_sudah_transaksi is null and a.flag_sudah_transaksi3 is null and a.flag_lokasi_tujuan is null and a.flag_billing is null "
        filter_flag = filter_flag & " and a.flag_hpp is null and a.flag_barang_masuk is null and a.flag_selisih_barang_masuk is null and a.flag_pembelian is null "
        If tanya = vbYes Then
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("Batal_Tracking_Dokumen") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "SELECT a.scan_telex  FROM tracking_dokumen a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("scan_telex")) = "T" Then
                            filter_flag = filter_flag & " and a.flag_kirim = 'Y' and a.flag_finish = 'Y' "
                        ElseIf General_Class.CekNULL(Dr("scan_telex")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pembatalan gagal id rencana ini menggunakan pengiriman telex, silakan membatalkan dari menu batalkan kirim!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        Else
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini belum di tahapan ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "SELECT a.selesai, a.status, no_po_pembelian, CAST(a.rv AS BIGINT) rv FROM rencana_order a WHERE "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' " & filter_flag
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If General_Class.CekNULL(Dr("selesai")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("status")) = "Y" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf General_Class.CekNULL(Dr("no_po_pembelian")) <> "" Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena rencana order ini sudah dibuat po pembelian!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        ElseIf LvRencanaOrder.FocusedItem.SubItems(7).Text <> Dr("rv") Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Proses tidak dapat dilanjutkan karena terdapat perubahan pada rencana order ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan, karena ID Rencana ini telah di tahapan selanjutnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End Using

                SQL = "UPDATE rencana_order SET flag_finish = null  WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "INSERT INTO log_batal_rencana_order(kode_perusahaan, id_rencana,keterangan, tanggal, jam, userid) VALUES("
                SQL = SQL & "'" & KodePerusahaan & "', '" & LvRencanaOrder.FocusedItem.Text & "','Batal Tracking Dokument Diterima',"
                SQL = SQL & "'" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', '" & Format(CDate(FMenuDev.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "')"
                ExecuteTrans(SQL)

                SQL = "update tracking_dokumen set penerima = null, finish_tanggal =  null, finish_jam = null, finish_user = null WHERE kode_perusahaan = '" & KodePerusahaan & "' AND id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Tracking Dokument Diterima Rencana Order berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)

                Kosong()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

#Region "PERHATIKAN"

    'Private Sub UpdateTrackingPengirimanToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UpdateTrackingPengirimanToolStripMenuItem.Click
    '    Try
    '        OpenConn()
    '        If CekButtonRole("import_update_tracking_pengiriman") = "T" Then
    '            CloseTrans()
    '            CloseConn()
    '            MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            Exit Sub
    '        End If


    '        Dim otw, draft, final, kirim, finish, kapal_tiba, penjaluran, sppb, bongkar, tarik_kontainer As String
    '        Dim kontainer, jumlah, selisih As Integer
    '        Dim lokasi_order As String = ""
    '        SQL = " select Lokasi, flag_otw, flag_draft,flag_final,flag_kirim,flag_finish,flag_kapal_tiba,flag_penjaluran,flag_sppb,flag_bongkar,flag_tarik_kontainer,total_persen"
    '        SQL = SQL & " from rencana_order "
    '        SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and id_rencana ='" & LvRencanaOrder.FocusedItem.Text & "'"

    '        Using Dr = OpenTrans(SQL)
    '            If Dr.Read Then
    '                otw = General_Class.CekNULL(Dr("flag_otw"))
    '                draft = General_Class.CekNULL(Dr("flag_draft"))
    '                final = General_Class.CekNULL(Dr("flag_final"))
    '                kirim = General_Class.CekNULL(Dr("flag_kirim"))
    '                finish = General_Class.CekNULL(Dr("flag_finish"))
    '                kapal_tiba = General_Class.CekNULL(Dr("flag_kapal_tiba"))

    '                penjaluran = General_Class.CekNULL(Dr("flag_penjaluran"))
    '                kontainer = (Dr("total_persen")) / 100
    '                jumlah = kontainer * 100
    '                selisih = (Dr("total_persen")) - jumlah

    '                lokasi_order = Dr("Lokasi")

    '                sppb = General_Class.CekNULL(Dr("flag_sppb"))
    '                bongkar = General_Class.CekNULL(Dr("flag_bongkar"))
    '                tarik_kontainer = General_Class.CekNULL(Dr("flag_tarik_kontainer"))
    '            Else
    '                MessageBox.Show("ID Rencana tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If
    '            Dr.Close()
    '        End Using


    '        If otw = "" Then
    '            Ubah_Status_OTW.TxtId_Rencana.Text = ""
    '            Ubah_Status_OTW.TxtId_Rencana.Text = LvRencanaOrder.FocusedItem.Text
    '            Ubah_Status_OTW.ShowDialog()
    '            Ubah_Status_OTW.Focus()
    '        ElseIf finish = "" Then
    '            If draft = "" Then
    '                Ubah_Status_Tracking_Dokumen.ComboBox3.Text = "Draft"

    '                Ubah_Status_Tracking_Dokumen.TabControl1.SelectedIndex = 0
    '                Ubah_Status_Tracking_Dokumen.Button2.Enabled = True
    '                Ubah_Status_Tracking_Dokumen.TxtId_Rencana.Text = ""
    '                Ubah_Status_Tracking_Dokumen.TxtId_Rencana.Text = LvRencanaOrder.FocusedItem.Text
    '                Ubah_Status_Tracking_Dokumen.ShowDialog()
    '                Ubah_Status_Tracking_Dokumen.Focus()
    '            ElseIf final = "" Then
    '                Ubah_Status_Tracking_Dokumen.ComboBox3.Text = "Final"
    '                Ubah_Status_Tracking_Dokumen.TabControl1.SelectedIndex = 1
    '                Ubah_Status_Tracking_Dokumen.TxtId_Rencana.Text = ""
    '                Ubah_Status_Tracking_Dokumen.TxtId_Rencana.Text = LvRencanaOrder.FocusedItem.Text
    '                Ubah_Status_Tracking_Dokumen.ShowDialog()
    '                Ubah_Status_Tracking_Dokumen.Focus()
    '            ElseIf kirim = "" Then
    '                Ubah_Status_Tracking_Dokumen.ComboBox3.Text = "Kirim"
    '                Ubah_Status_Tracking_Dokumen.TabControl1.SelectedIndex = 2
    '                Ubah_Status_Tracking_Dokumen.TxtId_Rencana.Text = ""
    '                Ubah_Status_Tracking_Dokumen.TxtId_Rencana.Text = LvRencanaOrder.FocusedItem.Text
    '                Ubah_Status_Tracking_Dokumen.ShowDialog()
    '                Ubah_Status_Tracking_Dokumen.Focus()
    '            ElseIf finish = "" Then
    '                Ubah_Status_Tracking_Dokumen.ComboBox3.Text = "Finish"
    '                Ubah_Status_Tracking_Dokumen.TabControl1.SelectedIndex = 3
    '                Ubah_Status_Tracking_Dokumen.TxtId_Rencana.Text = ""
    '                Ubah_Status_Tracking_Dokumen.TxtId_Rencana.Text = LvRencanaOrder.FocusedItem.Text
    '                Ubah_Status_Tracking_Dokumen.ShowDialog()
    '                Ubah_Status_Tracking_Dokumen.Focus()
    '            End If
    '        ElseIf kapal_tiba = "" Then
    '            Try
    '                OpenConn()
    '                Ubah_Keterangan_Kapal_Tiba.ComboBox1.Items.Clear()
    '                SQL = "Select kode_pelabuhan From pelabuhan where kode_perusahaan = '" & KodePerusahaan & "' and "
    '                SQL = SQL & "lokasi = '" & lokasi_order & "' order by kode_pelabuhan asc"
    '                Using dr = OpenTrans(SQL)
    '                    Do While dr.Read
    '                        Ubah_Keterangan_Kapal_Tiba.ComboBox1.Items.Add(dr("kode_pelabuhan"))
    '                    Loop
    '                    dr.Close()
    '                End Using
    '                CloseConn()
    '            Catch ex As Exception

    '                CloseConn()
    '                MessageBox.Show(ex.Message)
    '                Exit Sub
    '            End Try

    '            Ubah_Keterangan_Kapal_Tiba.TxtId_Rencana.Text = ""
    '            Ubah_Keterangan_Kapal_Tiba.TxtId_Rencana.Text = LvRencanaOrder.FocusedItem.Text
    '            Ubah_Keterangan_Kapal_Tiba.ShowDialog()
    '            Ubah_Keterangan_Kapal_Tiba.Focus()

    '            'cek di penjaluran
    '        ElseIf kapal_tiba = "Y" And penjaluran = "" Then

    '            Ubah_Keterangan_Penjaluran.TxtJumlah_conte.Text = "0"
    '            Ubah_Keterangan_Penjaluran.ComboBox2.Items.Clear()
    '            Ubah_Keterangan_Penjaluran.ComboBox3.Items.Clear()
    '            If selisih = 0 Or selisih <= 99 Then

    '                Ubah_Keterangan_Penjaluran.TxtJumlah_conte.Text = kontainer
    '            Else

    '                Ubah_Keterangan_Penjaluran.TxtJumlah_conte.Text = kontainer + 1
    '            End If

    '            Ubah_Keterangan_Penjaluran.ComboBox1.Items.Clear()
    '            Ubah_Keterangan_Penjaluran.ComboBox1.Items.Add("Merah")
    '            Ubah_Keterangan_Penjaluran.ComboBox1.Items.Add("Kuning")
    '            Ubah_Keterangan_Penjaluran.ComboBox1.Items.Add("Hijau")
    '            SQL = "select count(kode_perusahaan) as count from penjaluran_import where id_rencana ='" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan = '" & KodePerusahaan & "'"
    '            Using Dr = OpenTrans(SQL)
    '                If Dr.Read Then

    '                    If Dr("count") > 0 Then
    '                        Dr.Close()

    '                        SQL = "SELECT warna,jml_kontainer,no_pib,jml_kontainer_karantina "
    '                        SQL = SQL & "from penjaluran_import where id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "'"
    '                        Using Dt = BindingTrans(SQL)
    '                            With Dt.Tables("MyTable")
    '                                For a As Integer = 0 To .Rows.Count - 1
    '                                    Dim ValWarna As String = General_Class.CekNULL((.Rows(0).Item("warna")))
    '                                    Dim ValJml_Konte As String = General_Class.CekNULL((.Rows(0).Item("jml_kontainer")))
    '                                    Dim ValNo_PIB As String = General_Class.CekNULL((.Rows(0).Item("no_pib")))
    '                                    Dim ValJmlAsuransi As String = General_Class.CekNULL((.Rows(0).Item("jml_kontainer_karantina")))

    '                                    Ubah_Keterangan_Penjaluran.ComboBox1.Text = ValWarna
    '                                    Ubah_Keterangan_Penjaluran.ComboBox2.Text = ValJml_Konte
    '                                    Ubah_Keterangan_Penjaluran.TextBox2.Text = ValNo_PIB
    '                                    Ubah_Keterangan_Penjaluran.ComboBox3.Text = ValJmlAsuransi
    '                                    Ubah_Keterangan_Penjaluran.CheckBox1.Checked = True


    '                                Next
    '                            End With
    '                        End Using
    '                        Ubah_Keterangan_Penjaluran.BtnSimpan.Text = "&Update"
    '                    Else
    '                        Ubah_Keterangan_Penjaluran.BtnSimpan.Text = "&Simpan"
    '                    End If
    '                Else

    '                    Ubah_Keterangan_Penjaluran.BtnSimpan.Text = "&Simpan"
    '                End If
    '                Dr.Close()
    '            End Using

    '            Ubah_Keterangan_Penjaluran.TxtId_Rencana.Text = ""
    '            Ubah_Keterangan_Penjaluran.TxtId_Rencana.Text = LvRencanaOrder.FocusedItem.Text
    '            Ubah_Keterangan_Penjaluran.ShowDialog()
    '            Ubah_Keterangan_Penjaluran.Focus()
    '        ElseIf sppb = "" Then

    '            SQL = "select count(kode_perusahaan) as count from sppb_import where id_rencana ='" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan = '" & KodePerusahaan & "'"
    '            Using Dr = OpenTrans(SQL)
    '                If Dr.Read Then

    '                    If Dr("count") > 0 Then
    '                        Dr.Close()
    '                        Ubah_Keterangan_Sppb.ComboBox1.Items.Add("NHI")
    '                        Ubah_Keterangan_Sppb.ComboBox1.Items.Add("HICO")
    '                        Ubah_Keterangan_Sppb.ComboBox1.Items.Add("OK")

    '                        SQL = "SELECT kode_gudang,ok,tanggal_ok,nhi,tanggal_nhi,biaya_nhi,hico,tanggal_hico,biaya_hico "
    '                        SQL = SQL & "from sppb_import where id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan ='" & KodePerusahaan & "'"
    '                        Using Dt = BindingTrans(SQL)
    '                            With Dt.Tables("MyTable")
    '                                For a As Integer = 0 To .Rows.Count - 1
    '                                    Dim KdGdg As String = General_Class.CekNULL((.Rows(0).Item("kode_gudang")))
    '                                    Dim ok As String = General_Class.CekNULL((.Rows(0).Item("ok")))
    '                                    Dim tgl_ok As String = General_Class.CekNULL((.Rows(0).Item("tanggal_ok")))

    '                                    Dim nhi As String = General_Class.CekNULL((.Rows(0).Item("nhi")))
    '                                    Dim tgl_nhi As String = General_Class.CekNULL((.Rows(0).Item("tanggal_nhi")))
    '                                    Dim biaya_nhi As String = General_Class.CekNULL((.Rows(0).Item("biaya_nhi")))

    '                                    Dim hico As String = General_Class.CekNULL((.Rows(0).Item("hico")))
    '                                    Dim tgl_hico As String = General_Class.CekNULL((.Rows(0).Item("tanggal_hico")))
    '                                    Dim biaya_hico As String = General_Class.CekNULL((.Rows(0).Item("biaya_hico")))

    '                                    If ok = "Y" Then
    '                                        Ubah_Keterangan_Sppb.ComboBox1.Text = "OK"
    '                                        Ubah_Keterangan_Sppb.ComboBox2.Text = KdGdg
    '                                        Ubah_Keterangan_Sppb.TextBox2.Text = ""
    '                                        Ubah_Keterangan_Sppb.DateTimePicker1.Value = Format(tgl_ok, "dd MM yyyy")
    '                                    ElseIf nhi = "Y" Then
    '                                        Ubah_Keterangan_Sppb.ComboBox1.Text = "NHI"
    '                                        Ubah_Keterangan_Sppb.ComboBox2.Text = KdGdg
    '                                        Ubah_Keterangan_Sppb.TextBox2.Text = biaya_nhi
    '                                        Ubah_Keterangan_Sppb.DateTimePicker1.Value = Format(tgl_nhi, "dd MM yyyy")
    '                                    ElseIf hico = "Y" Then
    '                                        Ubah_Keterangan_Sppb.ComboBox1.Text = "HICO"
    '                                        Ubah_Keterangan_Sppb.ComboBox2.Text = KdGdg
    '                                        Ubah_Keterangan_Sppb.TextBox2.Text = biaya_hico
    '                                        Ubah_Keterangan_Sppb.DateTimePicker1.Value = Format(tgl_hico, "dd MM yyyy")
    '                                    End If


    '                                Next
    '                            End With
    '                        End Using
    '                        Ubah_Keterangan_Penjaluran.BtnSimpan.Text = "&Update"
    '                    Else

    '                        Ubah_Keterangan_Sppb.ComboBox1.Items.Add("NHI")
    '                        Ubah_Keterangan_Sppb.ComboBox1.Items.Add("HICO")
    '                        Ubah_Keterangan_Sppb.ComboBox1.Items.Add("OK")

    '                        Ubah_Keterangan_Sppb.BtnSimpan.Text = "&Simpan"
    '                    End If
    '                End If
    '                Dr.Close()
    '            End Using



    '            Ubah_Keterangan_Sppb.TxtId_Rencana.Text = ""
    '            Ubah_Keterangan_Sppb.TxtId_Rencana.Text = LvRencanaOrder.FocusedItem.Text
    '            Ubah_Keterangan_Sppb.ShowDialog()
    '            Ubah_Keterangan_Sppb.Focus()

    '        ElseIf tarik_kontainer = "" Then
    '            Ubah_Keterangan_Tarik_Kontainer.TxtLokasi.Text = CmbLokasi.Text
    '            Ubah_Keterangan_Tarik_Kontainer.TxtId_Rencana.Text = ""
    '            Ubah_Keterangan_Tarik_Kontainer.TxtId_Rencana.Text = LvRencanaOrder.FocusedItem.Text
    '            Ubah_Keterangan_Tarik_Kontainer.ShowDialog()
    '            Ubah_Keterangan_Tarik_Kontainer.Focus()
    '        ElseIf bongkar = "" Then
    '            OpenConn()

    '            SQL = "select count(ID_Rencana) as count, eta from Ubah_Status_OTW where id_rencana ='" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan ='" & KodePerusahaan & "' group by id_rencana,eta"
    '            Using Dr = OpenTrans(SQL)
    '                If Dr.Read Then
    '                    If Dr("count") <> 0 Then
    '                        Dr.Close()

    '                        SQL = "SELECT eta "
    '                        SQL = SQL & "from ubah_status_otw where id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan ='" & KodePerusahaan & "'"
    '                        Using Dt = BindingTrans(SQL)
    '                            With Dt.Tables("MyTable")
    '                                For a As Integer = 0 To .Rows.Count - 1
    '                                    Dim ValETA As String = General_Class.CekNULL((.Rows(0).Item("eta")))

    '                                    Ubah_Keterangan_Bongkar.DateTimePicker1.Value = ValETA


    '                                Next
    '                            End With
    '                        End Using


    '                        SQL = "SELECT Tanggal_Tiba "
    '                        SQL = SQL & "from kapal_tiba_import where id_rencana = '" & LvRencanaOrder.FocusedItem.Text & "' and kode_perusahaan ='" & KodePerusahaan & "'"
    '                        Using Dt = BindingTrans(SQL)
    '                            With Dt.Tables("MyTable")
    '                                For a As Integer = 0 To .Rows.Count - 1
    '                                    Dim ValTanggal As String = General_Class.CekNULL((.Rows(0).Item("tanggal_tiba")))

    '                                    Ubah_Keterangan_Bongkar.DateTimePicker3.Value = ValTanggal

    '                                Next
    '                            End With
    '                        End Using
    '                        'Dim ValETA_OTW As String = Dr("eta")

    '                        Ubah_Keterangan_Bongkar.BtnSimpan.Text = "&Simpan"
    '                        'Ubah_Keterangan_Bongkar.DateTimePicker1.Value = ValETA_OTW

    '                    End If

    '                Else
    '                    Dr.Close()

    '                End If
    '            End Using
    '            Ubah_Keterangan_Bongkar.TxtId_Rencana.Text = ""
    '            Ubah_Keterangan_Bongkar.TxtId_Rencana.Text = LvRencanaOrder.FocusedItem.Text

    '            Ubah_Keterangan_Bongkar.ShowDialog()
    '            Ubah_Keterangan_Bongkar.Focus()
    '        Else
    '            MessageBox.Show("Tracking Pengiriman Rencana Order ini telah selesai!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '            Exit Sub
    '        End If

    '        CloseConn()

    '    Catch ex As Exception
    '        CloseConn()
    '        MessageBox.Show(ex.Message)
    '        Exit Sub
    '    End Try
    'End Sub

    'Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick

    '    Dim row As DataGridViewRow = DataGridView1.CurrentRow
    '    Dim cell As DataGridViewCell = row.Cells(1)
    '    Dim value As String = cell.Value.ToString()

    '    If value = "OTW" Then
    '        Try
    '            OpenConn()
    '            If CekButtonRole("import_edit_otw") = "T" Then
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If
    '            CloseConn()

    '            Display_Edit_Tanggal_ETA.ShowDialog()

    '        Catch ex As Exception

    '            CloseTrans()
    '            CloseConn()
    '            MessageBox.Show(ex.Message)
    '            Exit Sub
    '        End Try

    '    ElseIf value = "Tarik Kontainer" Then
    '        Try
    '            OpenConn()
    '            If CekButtonRole("import_edit_tarik_kontainer") = "T" Then
    '                CloseTrans()
    '                CloseConn()
    '                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '                Exit Sub
    '            End If
    '            CloseConn()
    '            Edit_Keterangan_Tarik_Kontainer.ShowDialog()
    '        Catch ex As Exception

    '            CloseTrans()
    '            CloseConn()
    '            MessageBox.Show(ex.Message)
    '            Exit Sub
    '        End Try
    '    Else
    '        MessageBox.Show("Tidak dapat mengedit bagian ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    End If

    'End Sub

#End Region

    'Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
    '    Dim row As DataGridViewRow = DataGridView1.CurrentRow
    '    Dim cell As DataGridViewCell = row.Cells(1)
    '    Dim value As String = cell.Value.ToString()

    '    If value = "OTW" Then

    '        Display_Edit_Tanggal_ETA.ShowDialog()
    '    ElseIf value = "Tarik Kontainer" Then

    '        Edit_Keterangan_Tarik_Kontainer.ShowDialog()
    '    Else

    '        MessageBox.Show("Tidak dapat mengedit bagian ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    '        Exit Sub
    '    End If
    'End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Label1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label1.Click

    End Sub

    Private Sub CmbTracking_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbTracking.SelectedIndexChanged
        Button1_Click(BtCari, e)
    End Sub

    Private Sub CmbLokasi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbLokasi.SelectedIndexChanged
        Button1_Click(BtCari, e)
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CmbTracking.SelectedIndex = 0
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        CmbTracking.SelectedIndex = 1
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        CmbTracking.SelectedIndex = 2
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        CmbTracking.SelectedIndex = 4
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        CmbTracking.SelectedIndex = 5
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        CmbTracking.SelectedIndex = 6
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        CmbTracking.SelectedIndex = 7
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        CmbTracking.SelectedIndex = 8
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        CmbTracking.SelectedIndex = 3
    End Sub
End Class