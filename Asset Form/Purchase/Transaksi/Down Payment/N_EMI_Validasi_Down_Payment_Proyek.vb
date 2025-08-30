Public Class N_EMI_Validasi_Down_Payment_Proyek

    Dim Arr1, Arr2, Arr3, Arr4, arrcari As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

    Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
        If Lv_Pembelian.Items.Count = 0 OrElse Lv_Pembelian.SelectedItems Is Nothing OrElse Lv_Pembelian.SelectedItems.Count = 0 Then Exit Sub


        get_jam()

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction
            Dim tanya As String = MessageBox.Show("Yakin akan Melakukan Validasi Pembelian ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If tanya = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If


            If CekButtonRole("Validasi_Pembelian_Down_Payment_Proyek") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("User Tidak Ada Akses untuk Melakukan Validasi . . !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            SQL = "update Pembelian_Proyek set "
            SQL = SQL & "Validasi_DP = 'Y', Tgl_Validasi_DP = '" & Format(tgl_skg, "yyyy-MM-dd") & "', "
            SQL = SQL & "Jam_Validasi_DP = '" & Format(tgl_skg, "HH:mm:ss") & "', User_Validasi_DP = '" & UserID & "' "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and No_Faktur = '" & Lv_Pembelian.FocusedItem.SubItems(0).Text & "' "
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()

            MessageBox.Show("Data Telah diSimpan", "Validasi Pembelian Dimuka", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Btn_Cari_Click(ValidasiToolStripMenuItem, e)

    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        Try


            OpenConn()

            Lv_Pembelian.Items.Clear()
            Lv_Pembelian_Detail.Items.Clear()




            SQL = "Select a.No_Faktur, a.Lokasi, c.Kode_Supplier, d.nama, a.Tanggal, a.Jam, b.No_PO, a.No_Nota, "
            SQL = SQL & "a.Jenis_Transaksi, a.UserID, a.Nilai_Sblm_PPN As Jumlah_Hutang, a.PPN, a.Nilai_PPN, a.Grand, a.Nilai_DP "
            SQL = SQL & "From Pembelian_Proyek a, barang_masuk_proyek b, PO_Pembelian_Proyek c, Suppliers d  Where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_PO = b.No_Faktur And "
            SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_PO = c.No_Faktur And "
            SQL = SQL & "c.kode_perusahaan = d.Kode_Perusahaan And c.Kode_Supplier = d.Kode_Supplier And "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and a.Status is null and a.Nilai_DP<>0 and Validasi_DP is null "
            SQL = SQL & "order by  " & arrcari.Item(Cmb_Order.SelectedIndex)

            Dim Lvw As ListViewItem

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1


                            Lvw = Lv_Pembelian.Items.Add(.Rows(i).Item("no_faktur"))

                            'Lvw.SubItems.Add(.Rows(i).Item("no_nota"))
                            Lvw.SubItems.Add(.Rows(i).Item("kode_supplier"))
                            Lvw.SubItems.Add(.Rows(i).Item("nama"))

                            'If General_Class.CekNULL(.Rows(i).Item("jenis_pembayaran")) = "N" Then
                            '    Lvw.SubItems.Add("Non Tunai")
                            'Else
                            '    Lvw.SubItems.Add("Tunai")
                            'End If
                            'Lvw.SubItems.Add(.Rows(i).Item("cara_bayar"))

                            'If .Rows(i).Item("jenis_pembayaran") = "N" Then
                            '    Lvw.SubItems.Add(Format(.Rows(i).Item("Tgl_Jatuh_Tempo"), "dd MMM yyyy"))
                            'Else
                            '    Lvw.SubItems.Add("-")
                            'End If

                            Lvw.SubItems.Add(.Rows(i).Item("no_po"))
                            Lvw.SubItems.Add(.Rows(i).Item("no_nota"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Tanggal"), "dd MMM yyyy"))
                            If General_Class.CekNULL(.Rows(i).Item("Jenis_Transaksi")) = "N" Then
                                Lvw.SubItems.Add("Non Tunai")
                            Else
                                Lvw.SubItems.Add("Tunai")
                            End If

                            'Lvw.SubItems.Add(Format(.Rows(i).Item("total_idr"), "N2"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("ppn"), "N2"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("grand"), "N2"))
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("etd_simulasi"), "dd MMM yyyy"))

                            'Lv_PO.Items(i).ForeColor = T

                            'If General_Class.CekNULL(.Rows(i).Item("flag_release")) <> "Y" Then
                            '    Lv_PO.Items(i).ForeColor = Batal
                            'End If

                            'Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            'If General_Class.CekNULL(.Rows(i).Item("tanggal_release")) = "" Then
                            '    Lvw.SubItems.Add("-")
                            'Else
                            '    Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_release"), "dd MMM yyyy"))
                            'End If
                            'Lvw.SubItems.Add(Format(.Rows(i).Item("etd_simulasi"), "dd MMM yyyy"))

                            'If General_Class.CekNULL(.Rows(i).Item("Flag_release")) = "Y" Then
                            '    Lvw.SubItems.Add("SUBMITTED")
                            'Else
                            '    Lvw.SubItems.Add("UNSUBMITTED")
                            'End If
                            Lvw.SubItems.Add(.Rows(i).Item("userid"))

                            Lvw.SubItems.Add(Format(.Rows(i).Item("Jumlah_Hutang"), "N2"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("PPN"), "N0") & " %")
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Nilai_PPN"), "N2"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Grand"), "N2"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("Nilai_DP"), "N2"))
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

    Private Sub Display_Pembelian_Barang_Masuk_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()

        Btn_Cari_Click(Lv_Pembelian, e)
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

        Lv_Pembelian.Items.Clear()

        Lv_Pembelian.Columns.Add(Base_Language.Lang_Global_NoFaktur, 130, HorizontalAlignment.Left) '0
        Lv_Pembelian.Columns.Add(Base_Language.Lang_Global_Supplier, 0, HorizontalAlignment.Left) '1
        Lv_Pembelian.Columns.Add(Base_Language.lang_global_Nama_Supplier, 250, HorizontalAlignment.Left) '2
        Lv_Pembelian.Columns.Add("No PO", 130, HorizontalAlignment.Left) '3
        Lv_Pembelian.Columns.Add("Keterangan", 150, HorizontalAlignment.Left) '4
        Lv_Pembelian.Columns.Add("Tanggal", 110, HorizontalAlignment.Center) '5
        Lv_Pembelian.Columns.Add("Pembayaran", 110, HorizontalAlignment.Center) '6
        Lv_Pembelian.Columns.Add("User ID", 100, HorizontalAlignment.Center) '7
        Lv_Pembelian.Columns.Add("Total Hutang", 150, HorizontalAlignment.Right) '8
        Lv_Pembelian.Columns.Add("PPN", 80, HorizontalAlignment.Center) '9
        Lv_Pembelian.Columns.Add("Total PPN", 150, HorizontalAlignment.Right) '10
        Lv_Pembelian.Columns.Add("Grand Total", 150, HorizontalAlignment.Right) '11
        Lv_Pembelian.Columns.Add("Nilai DP", 150, HorizontalAlignment.Right) '12
        Lv_Pembelian.Columns(8).DisplayIndex = 7
        Lv_Pembelian.Columns(9).DisplayIndex = 8
        Lv_Pembelian.Columns(10).DisplayIndex = 9
        Lv_Pembelian.Columns(11).DisplayIndex = 10

        Lv_Pembelian.View = View.Details

        Lv_Pembelian_Detail.Items.Clear()
        Lv_Pembelian_Detail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 120, HorizontalAlignment.Left) '0
        Lv_Pembelian_Detail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 250, HorizontalAlignment.Left) '1
        Lv_Pembelian_Detail.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Right) '2
        Lv_Pembelian_Detail.Columns.Add("Satuan", 120, HorizontalAlignment.Center) '3
        Lv_Pembelian_Detail.Columns.Add("Harga", 150, HorizontalAlignment.Right) '4
        Lv_Pembelian_Detail.Columns.Add("Total", 250, HorizontalAlignment.Right) '5
        Lv_Pembelian_Detail.View = View.Details

        Cmb_Order.Items.Clear()
        Cmb_Order.Items.Add("Supplier") : arrcari.Add("d.nama")
        Cmb_Order.Items.Add("Tanggal PO") : arrcari.Add("c.tanggal")
        Cmb_Order.Items.Add("Tanggal Pembelian") : arrcari.Add("a.tanggal")

        Cmb_Order.SelectedIndex = 2
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Pembelian.SelectedIndexChanged
        Try
            OpenConn()
            Lv_Pembelian_Detail.Items.Clear()


            SQL = "select a.Kode_Stock_Owner,a.Kode_Barang, b.Nama,a.Harga,a.jumlah,trim(b.Satuan) as satuan, a.Subtotal as  Total_Akhir "
            SQL = SQL & "From Detail_Pembelian_Proyek a, Barang_Proyek b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Stock_Owner = b.Kode_Stock_Owner  and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_Pembelian.FocusedItem.SubItems(0).Text & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Pembelian_Detail.Items.Add(Dr("kode_barang")) '0
                    lvw.SubItems.Add(Dr("nama")) '1
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N0")) '2
                    lvw.SubItems.Add(Dr("satuan")) '3
                    lvw.SubItems.Add(Format(Dr("harga"), "N2")) '4
                    lvw.SubItems.Add(Format(Dr("Total_Akhir"), "N2")) '5


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