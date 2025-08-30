

Public Class Display_Summary_POInduk

    Dim arrFilterTanggal, arrFilterLain, arrFilterStatus As New ArrayList

    Dim Lv_NoFak, Lv_Supplier, Lv_PoCreated, Lv_PoReleased, Lv_ETD, Lv_StatusPO, Lv_UserID As String

    Dim item_NoFak As Integer = 0
    Dim item_Supplier As Integer = 1
    Dim item_PoCreated As Integer = 2
    Dim item_PoReleased As Integer = 3
    Dim item_ETD As Integer = 4
    Dim item_Status As Integer = 5
    Dim item_UserID As Integer = 6



    Private Sub Display_Laporan_PurchaseOrder_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Kosong()
    End Sub

    Private Sub CetakUlangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakUlangToolStripMenuItem.Click
        If Lv_PO.Items.Count = 0 Or Lv_PO.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        Try
            OpenConn()

            '==================================
            '=     UPDATE JUMLAH CETAK PO     =
            '==================================
            SQL = "update EMI_Pembelian_PO_Induk set Jumlah_Print = isnull(Jumlah_Print,0) + 1 where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Lv_PO.FocusedItem.Text & "'"
            ExecuteTrans(SQL)


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        Try
            OpenConn()

            SQL = "select Kode_Perusahaan, No_Faktur from View_Laporan_PO2_Induk "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "

            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    With Ds.Tables(0)
                        Dim CrDoc As New Faktur_Purchase_Order2_Induk
                        With A_Place_For_Printing2
                            CrDoc.SetDataSource(Ds)
                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                            CrDoc.SummaryInfo.ReportTitle = "Laporan Faktur Purchase Order"
                            CrDoc.RecordSelectionFormula = " {View_Laporan_PO2_Induk.Kode_Perusahaan} = '" & KodePerusahaan & "' and {View_Laporan_PO2_Induk.No_Faktur} = '" & Ds.Tables("MyTable").Rows(0).Item("No_Faktur") & "'"

                            .Text = "Laporan Faktur Purchase Order"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With
                    End With
                Else
                    MessageBox.Show("Data tidak ditemukan!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If

            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub Kosong()

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

        Lv_PO.Columns.Clear()
        Lv_PO.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Supplier", 230, HorizontalAlignment.Left)
        Lv_PO.Columns.Add("Po Created", 130, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("PO Released", 130, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("ETD", 130, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("Status PO", 140, HorizontalAlignment.Center)
        Lv_PO.Columns.Add("User Id", 150, HorizontalAlignment.Center)
        Lv_PO.View = View.Details


        Lv_PO_Detail.Columns.Clear()
        Lv_PO_Detail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 110, HorizontalAlignment.Left) '0
        Lv_PO_Detail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 250, HorizontalAlignment.Left) '1
        Lv_PO_Detail.Columns.Add(Base_Language.Lang_Global_Jumlah, 130, HorizontalAlignment.Right) '2
        Lv_PO_Detail.Columns.Add("Jumlah PO", 130, HorizontalAlignment.Right) '3
        Lv_PO_Detail.Columns.Add("Sisa", 130, HorizontalAlignment.Right) '4
        Lv_PO_Detail.Columns.Add(Base_Language.Lang_Global_Satuan, 80, HorizontalAlignment.Center) '5
        Lv_PO_Detail.Columns.Add("%Complete", 130, HorizontalAlignment.Center) '6
        Lv_PO_Detail.Columns.Add(Base_Language.Lang_Global_Harga, 130, HorizontalAlignment.Right) '7
        Lv_PO_Detail.Columns.Add("UrutPOInduk", 0, HorizontalAlignment.Right) '8
        Lv_PO_Detail.Columns.Add("FakPOInduk", 0, HorizontalAlignment.Right) '9
        Lv_PO_Detail.Columns.Add("KDSO", 0, HorizontalAlignment.Right) '10
        Lv_PO_Detail.Columns.Add("NoPenawaran", 0, HorizontalAlignment.Right) '11
        Lv_PO_Detail.View = View.Details


        Lv_DetSubPO.Columns.Clear()
        Lv_DetSubPO.Columns.Add("No Sub PO", 140, HorizontalAlignment.Left)
        Lv_DetSubPO.Columns.Add("Tanggal Sub PO", 130, HorizontalAlignment.Center)
        Lv_DetSubPO.Columns.Add("Lokasi", 150, HorizontalAlignment.Left)
        Lv_DetSubPO.Columns.Add("Kd Barang", 130, HorizontalAlignment.Left)
        Lv_DetSubPO.Columns.Add("Jumlah", 180, HorizontalAlignment.Right)
        Lv_DetSubPO.Columns.Add("Satuan", 100, HorizontalAlignment.Center)
        Lv_DetSubPO.View = View.Details


        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear()

            Cmb_Lokasi.Items.Add(Base_Language.Lang_Global_SeluruhCombobox)
            SQL = "Select kode_stock_owner From "
            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by kode_stock_owner"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Cmb_Lokasi.Items.Add(dr("kode_stock_owner"))
                Loop
                Cmb_Lokasi.Text = Lokasi
            End Using

            If CekButtonRole("Ganti_Lokasi_Display_Penjualan") = "T" Then
                Cmb_Lokasi.Enabled = False
            Else
                Cmb_Lokasi.Enabled = True
            End If


            Cmb_FIlter_Tanggal.Items.Clear() : arrFilterTanggal.Clear()
            Cmb_FIlter_Tanggal.Items.Add("Tanggal") : arrFilterTanggal.Add("a.Tanggal")
            Cmb_FIlter_Tanggal.Items.Add("Tanggal Release") : arrFilterTanggal.Add("a.tanggal_release")
            Cmb_FIlter_Tanggal.Items.Add("Tanggal Keberangkatan") : arrFilterTanggal.Add("a.ETD_Simulasi")

            Cmb_FIlter_Tanggal.Enabled = False : Cmb_Filter_Lain.Enabled = False
            DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Txt_Filter_Lain.Enabled = False : Cmb_Filter_Status.Enabled = False

            Cmb_Filter_Lain.Items.Clear() : Cmb_Filter_Lain.Text = "" : arrFilterLain.Clear()
            Cmb_Filter_Lain.Items.Add("No Faktur") : arrFilterLain.Add("a.no_faktur")
            Cmb_Filter_Lain.Items.Add("Supplier") : arrFilterLain.Add("b.Nama")
            Cmb_Filter_Lain.Items.Add("User ID") : arrFilterLain.Add("a.userid")


            Cmb_Filter_Status.Items.Clear() : Cmb_Filter_Status.Text = "" : arrFilterStatus.Clear()
            Cmb_Filter_Status.Items.Add("SUBMITTED") : arrFilterStatus.Add("a.Flag_Release = 'Y'")
            Cmb_Filter_Status.Items.Add("UNSUBMITTED") : arrFilterStatus.Add("a.Flag_Release is null")



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub



    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        Try
            OpenConn()

            Lv_PO.Items.Clear()
            Lv_PO_Detail.Items.Clear()
            Lv_DetSubPO.Items.Clear()

            SQL = "select a.No_Faktur,b.kode_supplier,b.Nama,a.tanggal,a.tanggal_release, a.ETD_Simulasi, a.userid, a.Flag_Release, a.Status, a.Selesai "
            SQL = SQL & "from EMI_Pembelian_PO_Induk a, Suppliers b  "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Supplier = b.Kode_Supplier "

            If Chk_Status.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilterStatus(Cmb_Filter_Status.SelectedIndex) & " "
            End If

            If Chk1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " a.tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If Chk2.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilterTanggal.Item(Cmb_FIlter_Tanggal.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If Chk3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arrFilterLain.Item(Cmb_Filter_Lain.SelectedIndex) & " like '%" & Trim(Txt_Filter_Lain.Text) & "%' "
            End If


            If Cmb_Lokasi.SelectedIndex = 0 Then
                SQL = SQL & " and a.Lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To Cmb_Lokasi.Items.Count - 1
                    list_kota = list_kota & "'" & Cmb_Lokasi.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ")"
            Else
                SQL = SQL & " and a.Lokasi = '" & Cmb_Lokasi.Text & "' "
            End If

            SQL = SQL & "order by a.tanggal, a.jam"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_PO.Items.Add(Dr("No_Faktur"))
                    Lv.SubItems.Add(Dr("Nama"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("tanggal")) = "", "-", Format(Dr("tanggal"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("tanggal_release")) = "", "-", Format(Dr("tanggal_release"), "dd MMM yyyy")))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("ETD_Simulasi")) = "", "-", Format(Dr("ETD_Simulasi"), "dd MMM yyyy")))

                    If General_Class.CekNULL(Dr("Flag_release")) = "Y" Then
                        Lv.SubItems.Add("SUBMITTED")
                    Else
                        Lv.SubItems.Add("UNSUBMITTED")
                    End If

                    Lv.SubItems.Add(Dr("userid"))

                    If General_Class.CekNULL(Dr("Selesai")) = "Y" Then
                        Lv.BackColor = Color.FromArgb(0, 128, 0)
                        Lv.ForeColor = Color.White
                    End If

                    If General_Class.CekNULL(Dr("Status")) <> "" Then
                        Lv.BackColor = Color.FromArgb(139, 0, 0)
                        Lv.ForeColor = Color.White
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



    Private Sub Lv_PO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PO.SelectedIndexChanged

        If Lv_PO.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            GetDataPO(Lv_PO.FocusedItem.Index)


            Lv_PO_Detail.Items.Clear() : Lv_DetSubPO.Items.Clear()
            SQL = "SELECT a.kode_stock_owner, a.Kode_Barang, b.Nama, a.harga, a.jumlah, a.Satuan, "

            SQL = SQL & "ISNULL(( "
            SQL = SQL & "SELECT SUM(x.Jumlah) "
            SQL = SQL & "FROM EMI_Pembelian_PO z, EMI_Pembelian_PO_det x "
            SQL = SQL & "WHERE a.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "AND a.No_Faktur = x.No_FakInduk "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.Kode_Barang = x.Kode_Barang and z.status is null "
            SQL = SQL & "GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
            SQL = SQL & "), 0) AS jumlah_masuk, "

            SQL = SQL & "(a.jumlah - ISNULL(( "
            SQL = SQL & "SELECT SUM(x.Jumlah) "
            SQL = SQL & "FROM EMI_Pembelian_PO z, EMI_Pembelian_PO_det x "
            SQL = SQL & "WHERE a.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "AND a.No_Faktur = x.No_FakInduk  "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.Kode_Barang = x.Kode_Barang and z.status is null "
            SQL = SQL & "GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
            SQL = SQL & "), 0)) AS sisa, "

            SQL = SQL & "CASE "
            SQL = SQL & "WHEN ISNULL(( "
            SQL = SQL & "SELECT SUM(x.Jumlah) "
            SQL = SQL & "FROM EMI_Pembelian_PO z, EMI_Pembelian_PO_det x "
            SQL = SQL & "WHERE a.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "AND a.No_Faktur = x.No_FakInduk  "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.Kode_Barang = x.Kode_Barang "
            SQL = SQL & "GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
            SQL = SQL & "), 0) = 0 THEN 0 "
            SQL = SQL & "ELSE ROUND(( "
            SQL = SQL & "(ISNULL(( "
            SQL = SQL & "SELECT SUM(x.Jumlah) "
            SQL = SQL & "FROM EMI_Pembelian_PO z, EMI_Pembelian_PO_det x "
            SQL = SQL & "WHERE a.Kode_Perusahaan = z.Kode_Perusahaan and z.Kode_Perusahaan = x.Kode_Perusahaan "
            SQL = SQL & "AND a.No_Faktur = x.No_FakInduk  "
            SQL = SQL & "and z.No_Faktur = x.No_Faktur "
            SQL = SQL & "AND a.Kode_Stock_Owner = x.Kode_Stock_Owner AND a.Kode_Barang = x.Kode_Barang "
            SQL = SQL & "GROUP BY z.Kode_Perusahaan, x.Kode_Barang, x.Satuan_Barang "
            SQL = SQL & "), 0))/a.jumlah  * 100 ), 2) END AS percentComplete, "

            SQL = SQL & "a.No_Urut, a.No_Faktur, a.Kode_Stock_Owner, a.No_Penawaran "

            SQL = SQL & "FROM EMI_Pembelian_PO_Detail_Induk a, barang b "
            SQL = SQL & "WHERE a.Kode_Perusahaan = b.Kode_Perusahaan  "
            SQL = SQL & "AND a.Kode_Stock_Owner = b.Kode_Stock_Owner  "
            SQL = SQL & "AND a.Kode_Barang = b.Kode_Barang  "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_NoFak & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_PO_Detail.Items.Add(Dr("kode_barang"))
                    lvw.SubItems.Add(Dr("nama"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    lvw.SubItems.Add(Format(Dr("jumlah_masuk"), "N0"))
                    lvw.SubItems.Add(Format(Dr("sisa"), "N0"))
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(Format(Dr("percentComplete"), "N1") & " %")
                    lvw.SubItems.Add(Format(Dr("harga"), "N2"))
                    lvw.SubItems.Add(Dr("No_Urut"))
                    lvw.SubItems.Add(Dr("No_Faktur"))
                    lvw.SubItems.Add(Dr("Kode_Stock_Owner"))
                    lvw.SubItems.Add(Dr("No_Penawaran"))


                Loop
            End Using



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub


    Private Sub BatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalToolStripMenuItem.Click
        If Lv_PO.Items.Count = 0 Or Lv_PO.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '===========================
            '=     CEK BUTTON ROLE     =
            '===========================
            If CekButtonRole("Pembatalan_POInduk") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Pembatalan PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Membatalkan Purhcase Order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            '==========================================
            '=     CEK APAKAH PO SUDAH DIBATALKAN     =
            '==========================================
            SQL = "select Status from EMI_Pembelian_PO_Induk "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) <> "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("PO Induk Sudah Dibatalkan Sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("PO Induk Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '============================================
            '=     CEK APAKAH SUDAH MASUK KE SUB PO     =
            '============================================
            SQL = "select a.No_Faktur, c.No_Faktur as Faktur_Sub "
            SQL = SQL & "from EMI_Pembelian_PO_Induk a, EMI_Pembelian_PO_det_Induk b, EMI_Pembelian_PO_Det c, EMI_Pembelian_PO d "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur and a.No_Faktur = c.No_FakInduk and b.No_Urut = c.urut_det_induk "
            SQL = SQL & "and c.No_Faktur = d.No_Faktur and a.status is null and d.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("PO Induk Tidak Bisa Dibatalkan, Karena Sudah Masuk Tahap Sub PO!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            '=============================================
            '=     CEK APAKAH ADA DP TERHADAP PO INI     =
            '=============================================
            SQL = "select top 1 a.Kode_Perusahaan "
            SQL = SQL & "from emi_transaksi_pembayaran_dimuka a, emi_transaksi_pembayaran_dimuka_detail b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan "
            SQL = SQL & "and a.No_Transaksi = b.No_Transaksi "
            SQL = SQL & "and a.Status is null "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and b.No_Fak_PO = '" & Lv_PO.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("PO Induk Tidak Bisa Dibatalkan, Karena Sudah Memiliki Nominal Pembayaran Dimuka!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            '=============================
            '=     UPDATE FLAG PO PR     =
            '=============================
            SQL = "Select No_Urut_PR from "
            SQL = SQL & "EMI_Pembelian_PO_det_Induk where "
            SQL = SQL & "Kode_Perusahaan='" & KodePerusahaan & "' and "
            SQL = SQL & "no_faktur='" & Lv_PO.FocusedItem.Text & "' "
            Using ds = BindingTrans(SQL)
                With ds.Tables("MyTable")
                    For index = 0 To .Rows.Count - 1

                        SQL = "Update EMI_Purchase_Requisition_Detail set Flag_Sudah_PO = null where "
                        SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "No_Urut = '" & .Rows(index).Item("No_Urut_PR") & "' "
                        ExecuteTrans(SQL)

                    Next
                End With
            End Using



            '==================================
            '=     UPDATE FLAG PEMBATALAN     =
            '==================================
            SQL = "update EMI_Pembelian_PO_Induk set Status = 'Y', "
            SQL = SQL & "UserID_Batal = '" & UserID & "', "
            SQL = SQL & "Tanggal_Batal = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "Jam_Batal = '" & Format(tgl_skg, "HH:mm:ss") & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("PO Induk Berhasil Dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Btn_Cari_Click(BatalToolStripMenuItem, e)

    End Sub

    Private Sub SelesaiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SelesaiToolStripMenuItem.Click
        If Lv_PO.Items.Count = 0 Or Lv_PO.SelectedItems.Count = 0 Then
            Exit Sub
        End If

        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("Penyelesaian_POInduk") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Penyelesaian PO", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim tanya As String = MessageBox.Show("Yakin Ingin Menyelesaikan Purhcase Order ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If


            '=======================================
            '=     CEK APAKAH PO SUDAH SELESAI     =
            '=======================================
            SQL = "select Selesai from EMI_Pembelian_PO_Induk  "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Lv_PO.FocusedItem.Text & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Selesai")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("PO Sudah Selesai, Tidak Bisa Diselesaikan Lagi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Data PO Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using



            '========================================
            '=     UPDATE FLAG SELESAI PO INDUK     =
            '========================================
            SQL = "update EMI_Pembelian_PO_Induk set Selesai = 'Y', UserID_Selesai = '" & UserID & "', "
            SQL = SQL & "Tanggal_Selesai = '" & Format(tgl_skg, "yyyy-MM-dd") & "', Jam_Selesai = '" & Format(tgl_skg, "HH:mm:ss") & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & Lv_PO.FocusedItem.Text & "' and Selesai is null "
            ExecuteTrans(SQL)





            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
            MessageBox.Show("PO Induk Berhasil di Selesaikan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Btn_Cari_Click(BatalToolStripMenuItem, e)

    End Sub


    Private Sub GetDataPO(ByVal index As Integer)
        Lv_NoFak = Lv_PO.Items(index).SubItems(item_NoFak).Text
        Lv_Supplier = Lv_PO.Items(index).SubItems(item_Supplier).Text
        Lv_PoCreated = Lv_PO.Items(index).SubItems(item_PoCreated).Text
        Lv_PoReleased = Lv_PO.Items(index).SubItems(item_PoReleased).Text
        Lv_ETD = Lv_PO.Items(index).SubItems(item_ETD).Text
        Lv_StatusPO = Lv_PO.Items(index).SubItems(item_Status).Text
        Lv_UserID = Lv_PO.Items(index).SubItems(item_UserID).Text
    End Sub
















    Private Sub Chk1_CheckedChanged(sender As Object, e As EventArgs) Handles Chk1.CheckedChanged
        If Chk1.Checked = True Then
            Chk2.Checked = False
            Btn_Cari_Click(Chk1, e)
        End If
    End Sub

    Private Sub Chk2_CheckedChanged(sender As Object, e As EventArgs) Handles Chk2.CheckedChanged
        If Chk2.Checked Then
            Cmb_FIlter_Tanggal.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            Chk1.Checked = False
        Else
            Cmb_FIlter_Tanggal.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            Cmb_FIlter_Tanggal.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub Chk3_CheckedChanged(sender As Object, e As EventArgs) Handles Chk3.CheckedChanged
        If Chk3.Checked Then
            Cmb_Filter_Lain.Enabled = True : Txt_Filter_Lain.Enabled = True
        Else
            Cmb_Filter_Lain.Enabled = False : Txt_Filter_Lain.Enabled = False
            Cmb_Filter_Lain.SelectedIndex = -1 : Txt_Filter_Lain.Text = ""
        End If
    End Sub

    Private Sub Chk_Status_CheckedChanged(sender As Object, e As EventArgs) Handles Chk_Status.CheckedChanged
        If Chk_Status.Checked Then
            Cmb_Filter_Status.Enabled = True
        Else
            Cmb_Filter_Status.Enabled = False
            Cmb_Filter_Status.SelectedIndex = -1
        End If
    End Sub

    Private Sub Lv_PO_Detail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PO_Detail.SelectedIndexChanged

        If Lv_PO_Detail.Items.Count = 0 Or Lv_PO_Detail.FocusedItem.Index = -1 Then Exit Sub

        Try
            OpenConn()

            Lv_DetSubPO.Items.Clear()
            SQL = "select a.No_Faktur as No_Fak_Induk, d.No_Faktur as No_FakSub, c.Kode_Stock_Owner, c.Kode_Barang, sum(c.Jumlah) as Jumlah, c.Satuan, d.Status, d.Tanggal "
            SQL = SQL & "from EMI_Pembelian_PO_Induk a, EMI_Pembelian_PO_Det_Induk b, EMI_Pembelian_PO_Det c, EMI_Pembelian_PO d "
            SQL = SQL & "where a.kode_perusahaan = b.Kode_Perusahaan and b.Kode_Perusahaan = c.Kode_Perusahaan and c.Kode_Perusahaan = d.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = b.No_Faktur and c.No_Faktur = d.No_Faktur and b.No_Faktur = c.No_FakInduk and b.No_Urut = c.urut_det_induk "
            SQL = SQL & "and a.Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.No_Faktur = '" & Lv_PO_Detail.FocusedItem.SubItems(9).Text & "' "
            SQL = SQL & "and b.Kode_Stock_Owner = '" & Lv_PO_Detail.FocusedItem.SubItems(10).Text & "' "
            SQL = SQL & "and b.Kode_Barang = '" & Lv_PO_Detail.FocusedItem.SubItems(0).Text & "' "
            SQL = SQL & "and b.No_Penawaran = '" & Lv_PO_Detail.FocusedItem.SubItems(11).Text & "' "
            SQL = SQL & "group by  a.No_Faktur , d.No_Faktur , c.Kode_Stock_Owner, c.Kode_Barang, c.Satuan, d.Status, d.Tanggal "

            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_DetSubPO.Items.Add(Dr("No_FakSub"))
                    Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
                    Lv.SubItems.Add(Dr("Kode_Stock_Owner"))
                    Lv.SubItems.Add(Dr("Kode_Barang"))
                    Lv.SubItems.Add(Format(Dr("Jumlah"), "N2"))
                    Lv.SubItems.Add(Dr("Satuan"))

                    If General_Class.CekNULL(Dr("Status")) <> "" Then
                        Lv.BackColor = Color.FromArgb(139, 0, 0)
                        Lv.ForeColor = Color.White
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


End Class