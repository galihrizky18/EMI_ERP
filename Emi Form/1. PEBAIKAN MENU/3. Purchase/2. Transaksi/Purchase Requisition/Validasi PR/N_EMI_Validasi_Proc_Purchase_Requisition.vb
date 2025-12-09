Public Class N_EMI_Validasi_Proc_Purchase_Requisition

    Dim Arr1, Arr2, Arr3, Arr4 As New ArrayList
    Dim pertama As Integer = 1
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black

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

        Lv_PR.Items.Clear() : Lv_PR.Columns.Clear()
        Lv_PR.Columns.Add(Base_Language.Lang_Global_NoFaktur, 180, HorizontalAlignment.Left)
        Lv_PR.Columns.Add("PR Released", 150, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("Keterangan", 450, HorizontalAlignment.Center)
        Lv_PR.Columns.Add("User ID", 100, HorizontalAlignment.Left)
        Lv_PR.View = View.Details

        Lv_PRDetail.Items.Clear() : Lv_PRDetail.Columns.Clear()
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_NamaBarang, 200, HorizontalAlignment.Left)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Satuan, 100, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add(Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("Estimasi Delivery", 110, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("Tanggal Delivery", 110, HorizontalAlignment.Center)
        Lv_PRDetail.Columns.Add("Kebutuhan Delivery ", 125, HorizontalAlignment.Center)
        Lv_PRDetail.View = View.Details


        BtnBarangMasuk_Cari_Click(BtnBarangMasuk_Cari, Nothing)
    End Sub



    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_PR.SelectedIndexChanged
        Try
            OpenConn()
            Lv_PRDetail.Items.Clear()
            'If ListView1.FocusedItem.SubItems(8).Text = "Y" Then
            '    SQL = "select a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,a.Nilai_Pengali,a.Satuan_Barang,a.Nilai_Barang, "
            '    SQL = SQL & "a.Tgl_Produksi,a.Tgl_Expired from EMI_Pembelian_Barang_Masuk_Detail a, barang b "
            '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang "
            '    SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "'and a.no_faktur = '" & ListView1.FocusedItem.Text & "' "
            '    SQL = SQL & "group by  a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,a.Nilai_Pengali,a.Satuan_Barang,a.Nilai_Barang,a.Tgl_Produksi,a.Tgl_Expired "

            '    SQL = SQL & "order by kode_barang "
            'Else
            '    SQL = "select a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,a.Nilai_Pengali,a.Satuan_Barang,a.Nilai_Barang, "
            '    SQL = SQL & "a.Tgl_Produksi,a.Tgl_Expired from EMI_Pembelian_Barang_Masuk_Sementara_Det a, barang b "
            '    SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang "
            '    SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "'and a.no_faktur = '" & ListView1.FocusedItem.Text & "' "
            '    SQL = SQL & "group by  a.Kode_Stock_Owner,a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,a.Nilai_Pengali,a.Satuan_Barang,a.Nilai_Barang,a.Tgl_Produksi,a.Tgl_Expired "

            '    SQL = SQL & "order by kode_barang "
            'End If

            'SQL = "select a.kode_stock_owner, a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,a.Harga,a.Satuan,a.Nilai_Barang From EMI_Purchase_Requisition_Detail a, barang b "
            'SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and  "
            'SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner  and a.Kode_Barang = b.Kode_Barang "
            'SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            'SQL = SQL & "and a.no_faktur = '" & Lv_PR.FocusedItem.SubItems(0).Text & "' "
            SQL = "select a.kode_stock_owner, a.Kode_Barang,b.Nama,a.jumlah,a.Satuan,"
            'jumlah masuk

            SQL = SQL & "isnull((select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan And x.No_Faktur = y.No_Faktur "
            SQL = SQL & "And y.Kode_Perusahaan = a.Kode_Perusahaan And y.no_urut_pr = a.No_Urut And x.status Is null And x.No_Faktur_Induk Is null And y.No_FakInduk Is null ), "
            SQL = SQL & "0) as jumlah_masuk, "

            SQL = SQL & "isnull((select sum(y.Jumlah) from EMI_Pembelian_PO_Induk x, EMI_Pembelian_PO_Det_Induk y "
            SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "
            SQL = SQL & "and y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut and x.status is null), "
            SQL = SQL & "0) as jumlah_masuk_new, "

            SQL = SQL & "isnull(a.flag_sudah_po,'T') as flag_selesai_po, "
            SQL = SQL & "isnull(a.estimasi, 0) as estimasi, a.Tanggal_Delivery, "

            SQL = SQL & "DATEDIFF(DAY, ( "
            SQL = SQL & "select z.Tanggal_Release from EMI_Purchase_Requisition z "
            SQL = SQL & "where a.Kode_Perusahaan = z.Kode_Perusahaan "
            SQL = SQL & "and a.No_Faktur = z.No_Faktur "
            SQL = SQL & "and z.Status is null "
            SQL = SQL & "), a.Tanggal_Delivery) as Kebutuhan_Estimasi "

            'sisa
            'SQL = SQL & "(a.jumlah - isnull((select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur and "
            'SQL = SQL & "y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut and x.status is null), 0)) "
            'SQL = SQL & "as sisa, "
            ''percentComplete
            'SQL = SQL & "(isnull((select sum(y.Jumlah) from EMI_Pembelian_PO x, EMI_Pembelian_PO_Det y "
            'SQL = SQL & "where x.Kode_Perusahaan = y.Kode_Perusahaan and x.No_Faktur = y.No_Faktur "
            'SQL = SQL & "and y.Kode_Perusahaan = a.Kode_Perusahaan and y.no_urut_pr = a.No_Urut), "
            'SQL = SQL & "0) / a.jumlah) * 100 as percentComplete "

            SQL = SQL & "From EMI_Purchase_Requisition_Detail a, barang b "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and "
            SQL = SQL & "a.Kode_Stock_Owner = b.Kode_Stock_Owner  and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and a.no_faktur = '" & Lv_PR.FocusedItem.SubItems(0).Text & "' and a.jumlah<>0 "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim total As Double = Dr("jumlah_masuk") + Dr("jumlah_masuk_new")

                    Dim lvw As ListViewItem
                    lvw = Lv_PRDetail.Items.Add(Dr("kode_barang"))
                    lvw.SubItems.Add(Dr("nama"))
                    lvw.SubItems.Add(Dr("satuan"))
                    lvw.SubItems.Add(Format(Dr("jumlah"), "N2"))
                    lvw.SubItems.Add($"{Dr("estimasi")} Hari")
                    lvw.SubItems.Add(Format(Dr("Tanggal_Delivery"), "dd MMM yyyy"))
                    lvw.SubItems.Add($"{Dr("Kebutuhan_Estimasi")} Hari")

                    If Val(HilangkanTanda(Dr("Kebutuhan_Estimasi"))) < Val(HilangkanTanda(Dr("estimasi"))) Then
                        lvw.BackColor = Color.FromArgb(243, 180, 61)
                    Else
                        lvw.BackColor = Color.White
                    End If





                    'lvw.SubItems.Add(Format(total, "N2"))
                    'Dim sisa As Double = Dr("jumlah") - total
                    'Dim persen As Double = total / Dr("jumlah") * 100

                    'If Dr("flag_selesai_po") = "Y" Then
                    '    lvw.BackColor = Color.LightGreen
                    '    lvw.SubItems.Add(Format(0, "N2"))
                    'Else
                    '    lvw.SubItems.Add(Format(sisa, "N2"))
                    'End If

                    'lvw.SubItems.Add(Format(persen, "N2"))

                    'lvw.SubItems.Add(Format(Dr("tgl_produksi"), "dd MMM yyyy"))
                    'lvw.SubItems.Add(Format(Dr("tgl_expired"), "dd MMM yyyy"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub



    Private Sub BtnBarangMasuk_Cari_Click(sender As Object, e As EventArgs) Handles BtnBarangMasuk_Cari.Click
        Try


            OpenConn()

            Lv_PR.Items.Clear()
            Lv_PRDetail.Items.Clear()

            SQL = "select No_Faktur, tanggal, tanggal_release, keterangan, userid, Flag_Release, Status "
            SQL = SQL & "from EMI_Purchase_Requisition   "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and flag_Val_Proc='P' "
            SQL = SQL & "order by No_Faktur, Tanggal, Tanggal_Release, UserId"


            Dim Lvw As ListViewItem

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = Lv_PR.Items.Add(.Rows(i).Item("no_faktur"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            'If General_Class.CekNULL(.Rows(i).Item("tanggal_release")) = "" Then
                            '    Lvw.SubItems.Add("-")
                            'Else
                            '    Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_release"), "dd MMM yyyy"))
                            'End If

                            Lvw.SubItems.Add(.Rows(i).Item("keterangan"))

                            'If General_Class.CekNULL(.Rows(i).Item("Flag_Release")) = "Y" Then
                            '    Lvw.SubItems.Add("SUBMITTED")
                            'Else
                            '    Lvw.SubItems.Add("UNSUBMITTED")
                            'End If

                            Lvw.SubItems.Add(.Rows(i).Item("userid"))

                            If General_Class.CekNULL(.Rows(i).Item("Status")) <> "" Then
                                Lvw.BackColor = Color.FromArgb(139, 0, 0)
                                Lvw.ForeColor = Color.White
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

    Private Sub TolakToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TolakToolStripMenuItem.Click
        If Lv_PR.Items.Count = 0 Or Lv_PR.FocusedItem Is Nothing Then Exit Sub

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Validasi_Purchase_Requisition") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Validasi PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim SelectedFaktur As String = Lv_PR.FocusedItem.Text

            '====================
            '=     CEK DATA     =
            '====================
            SQL = $"
                select Status, Flag_Release from EMI_Purchase_Requisition
                where Kode_Perusahaan = '{KodePerusahaan}'
                and No_Faktur = '{SelectedFaktur}'
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"No Faktur {SelectedFaktur} Sudah Dibatalkan Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Flag_Release")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"No Faktur {SelectedFaktur} Sudah Direlease Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"No Faktur {SelectedFaktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            If MessageBox.Show($"Yakin Ingin Melakukan Validasi No Faktur : {SelectedFaktur} Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            SQL = $"update EMI_Purchase_Requisition set Flag_Release = null, flag_Val_Proc = 'T',
                user_Val_Proc = '{UserID}', 
                tanggal_Val_Proc = '{Format(tgl_skg, "yyyy-MM-dd")}', jam_Val_Proc = '{Format(tgl_skg, "HH:mm:ss")}'
                where Kode_Perusahaan = '{KodePerusahaan}'
                and No_Faktur = '{SelectedFaktur}'
            "
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show("No Faktur Berhasil Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        kosong()
    End Sub

    Private Sub Lv_PR_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_PR.MouseMove
        Dim info = Lv_PR.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            Lv_PR.Cursor = Cursors.Hand
        Else
            Lv_PR.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If Lv_PR.Items.Count = 0 Then
            e.Cancel = True
            Exit Sub
        End If

        '=========================================================
        '=     CEK APAKAH MOUSE BERADA DI ATAS ROWS LISTVIEW     =
        '=========================================================
        Dim mousePos As Point = Lv_PR.PointToClient(Cursor.Position)
        Dim info As ListViewHitTestInfo = Lv_PR.HitTest(mousePos)

        If info.Item Is Nothing Then
            e.Cancel = True
            Exit Sub
        End If

        Lv_PR.FocusedItem = info.Item
        info.Item.Selected = True
    End Sub


    Private Sub ValidasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ValidasiToolStripMenuItem.Click
        If Lv_PR.Items.Count = 0 Or Lv_PR.FocusedItem Is Nothing Then Exit Sub

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '====================
            '=     CEK ROLE     =
            '====================
            If CekButtonRole("Validasi_Purchase_Requisition") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Melakukan Validasi PR", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim SelectedFaktur As String = Lv_PR.FocusedItem.Text

            '====================
            '=     CEK DATA     =
            '====================
            SQL = $"
                select Status, Flag_Release from EMI_Purchase_Requisition
                where Kode_Perusahaan = '{KodePerusahaan}'
                and No_Faktur = '{SelectedFaktur}'
            "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If General_Class.CekNULL(Dr("Status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"No Faktur {SelectedFaktur} Sudah Dibatalkan Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("Flag_Release")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"No Faktur {SelectedFaktur} Sudah Direlease Sebelumnya", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"No Faktur {SelectedFaktur} Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            If MessageBox.Show($"Yakin Ingin Melakukan Validasi No Faktur : {SelectedFaktur} Ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = vbNo Then
                CloseTrans()
                CloseConn()
                Exit Sub
            End If

            SQL = $"update EMI_Purchase_Requisition set Flag_Release = 'Y', flag_Val_Proc = 'Y',
                user_Val_Proc = '{UserID}', 
                tanggal_Val_Proc = '{Format(tgl_skg, "yyyy-MM-dd")}', jam_Val_Proc = '{Format(tgl_skg, "HH:mm:ss")}'
                where Kode_Perusahaan = '{KodePerusahaan}'
                and No_Faktur = '{SelectedFaktur}'
            "
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show("No Faktur Berhasil Divalidasi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        kosong()

    End Sub

End Class