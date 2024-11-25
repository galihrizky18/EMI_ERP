Public Class EMI_Display_Data_Terima_Transfer
    Dim Arr1, Arr2, Arr3 As New ArrayList
    Dim Batal As Color = Color.Black
    Private Sub EMI_Display_Data_Terima_Transfer_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        kosong()
    End Sub

    Private Sub EMI_Display_Data_Terima_Transfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            ListView1.Columns.Add("Kode Transfer", 110, HorizontalAlignment.Center)
            ListView1.Columns.Add("Tanggal", 70, HorizontalAlignment.Center)
            ListView1.Columns.Add("Kode SO Awal", 90, HorizontalAlignment.Center)
            ListView1.Columns.Add("Kode SO Tujuan", 90, HorizontalAlignment.Center)
            ListView1.Columns.Add("Keterangan", 215, HorizontalAlignment.Left)
            ListView1.Columns.Add("User ID", 100, HorizontalAlignment.Center)
            ListView1.Columns.Add("No Terima", 100, HorizontalAlignment.Left)
            ListView1.Columns.Add("Tanggal Terima", 70, HorizontalAlignment.Center)
            ListView1.Columns.Add("Jam Terima", 70, HorizontalAlignment.Center)
            ListView1.Columns.Add("User Terima", 100, HorizontalAlignment.Center)
            ListView1.Columns.Add("No. ", 30, HorizontalAlignment.Right).DisplayIndex = 0
            ListView1.View = View.Details

            ListView2.Columns.Add("Kode Barang", 200, HorizontalAlignment.Center)
            ListView2.Columns.Add("Nama Barang", 400, HorizontalAlignment.Left)
            ListView2.Columns.Add("Jumlah", 90, HorizontalAlignment.Right)
            ListView2.Columns.Add("Satuan", 180, HorizontalAlignment.Left)
            ListView2.View = View.Details

            Try
                OpenConn()

                ComboBox6.Items.Clear()
                ComboBox6.Items.Add("-- Seluruh --")

                SQL = "Select kode_stock_owner From "
                SQL = SQL & "stock_owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
                Using dr = OpenTrans(SQL)
                    Do While dr.Read
                        ComboBox6.Items.Add(dr("kode_stock_owner"))
                    Loop
                End Using

                ComboBox6.SelectedIndex = 0

                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

            kosong()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TerimaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TerimaToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu kode transfer yang mau diterima!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan terima transaksi transfer stock ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbNo Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("terima_transfer_stock") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            Dim so_tujuan As String = ""
            Dim total_hpp As Double = 0
            Dim tgl_ts As String = ""

            SQL = "select grand, kode_so_tujuan, status, no_terima, tanggal + jam as tgl from Emi_Transfer_Stock where kode_perusahaan = '" & KodePerusahaan & "' and kode_transfer = '" & ListView1.FocusedItem.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    so_tujuan = Dr("kode_so_tujuan")
                    total_hpp = Dr("grand")
                    tgl_ts = Format(Dr("tgl"), "yyyy-MM-dd HH:mm:ss")

                    If General_Class.CekNULL(Dr("status")) = "Y" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    ElseIf General_Class.CekNULL(Dr("no_terima")) <> "" Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Proses tidak dapat dilanjutkan karena transaksi ini sudah diterima sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kode transfer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            End Using

            Dim inisial_faktur As String = ""
            Dim coa_pending As String = ""
            Dim coa_persediaan As String = ""

            SQL = "select inisial_faktur, persediaan, pending_persediaan from stock_owner_gudang where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_stock_owner = '" & so_tujuan & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    inisial_faktur = Dr("inisial_faktur")
                    coa_persediaan = Dr("persediaan")
                    coa_pending = Dr("pending_persediaan")
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Lokasi tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using

            Dim no_ts As String = FTS_IN & inisial_faktur & "-" & Format(CDate(tgl_ts), "MM/yy") & "-" &
                                 General_Class.Get_Last_Number2("Emi_Transfer_IN", "kode_transfer", JumlahDigit,
                                 "Kode_perusahaan", KodePerusahaan,
                                 "And", "substring(kode_transfer,1," & Len(FTS_IN) + Len(inisial_faktur) + 6 & ")", FTS_IN & inisial_faktur & "-" & Format(CDate(tgl_ts), "MM/yy"))

            Dim Kode_Voucher As String = GetLastNumberJurnal(Format(CDate(tgl_ts), "yyyyMM"), fJU & inisial_faktur, KodePerusahaan)

            SQL = "insert into Emi_Transfer_IN(kode_perusahaan, kode_transfer, tanggal, jam, kode_so_tujuan, "
            SQL = SQL & "userid, kode_transfer_lama, kode_voucher) values('" & KodePerusahaan & "', "
            SQL = SQL & "'" & no_ts & "', '" & Format(CDate(tgl_ts), "yyyy-MM-dd") & "', "
            SQL = SQL & "'" & Format(CDate(tgl_ts), "HH:mm:ss") & "', "
            SQL = SQL & "'" & so_tujuan & "', '" & UserID & "', '" & ListView1.FocusedItem.Text & "', '" & Kode_Voucher & "')"
            ExecuteTrans(SQL)

            Dim so_awal As String = ""

            SQL = "select kode_so_awal, kode_barang, jumlah from Emi_Transfer_Stock_Detail where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_transfer = '" & ListView1.FocusedItem.Text.Trim & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        so_awal = .Rows(0).Item("kode_so_awal")

                        For i As Integer = 0 To .Rows.Count - 1
                            Dim nama As String = ""

                            SQL = "Select good_stock, nama From barang Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & so_tujuan & "' and "
                            SQL = SQL & "kode_barang = '" & .Rows(i).Item("kode_barang") & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    nama = Dr("nama")
                                    Dr.Close()

                                    SQL = "update barang set good_stock = good_stock +" & .Rows(i).Item("jumlah") & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_stock_owner = '" & so_tujuan & "' and "
                                    SQL = SQL & "kode_barang = '" & .Rows(i).Item("kode_barang") & "'"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()
                                    CloseTrans()
                                    CloseConn()
                                    MessageBox.Show("Barang" & nama & " tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                    Exit Sub
                                End If
                            End Using


                            SQL = "insert into Emi_Transfer_IN_Detail(kode_perusahaan, kode_transfer, kode_so_awal, kode_barang, jumlah) values("
                            SQL = SQL & "'" & KodePerusahaan & "', '" & no_ts & "', '" & so_awal & "', "
                            SQL = SQL & "'" & .Rows(i).Item("kode_barang") & "', '" & .Rows(i).Item("jumlah") & "')"
                            ExecuteTrans(SQL)
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Detail transfer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End With
            End Using

            SQL = "select kode_stock_owner, kode_barang, jumlah, serial_number from Emi_Det_TS where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_transfer = '" & ListView1.FocusedItem.Text.Trim & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            SQL = "Select kode_perusahaan From barang_sn Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & so_tujuan & "' and "
                            SQL = SQL & "kode_barang = '" & .Rows(i).Item("kode_barang") & "' and "
                            SQL = SQL & "serial_number = '" & .Rows(i).Item("serial_number") & "'"
                            Using Dr = OpenTrans(SQL)
                                If Dr.Read Then
                                    Dr.Close()

                                    SQL = "update barang_sn set jumlah = jumlah + " & .Rows(i).Item("jumlah") & " where "
                                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                    SQL = SQL & "kode_stock_owner = '" & so_tujuan & "' and "
                                    SQL = SQL & "kode_barang = '" & .Rows(i).Item("kode_barang") & "' and "
                                    SQL = SQL & "serial_number = '" & .Rows(i).Item("serial_number") & "'"
                                    ExecuteTrans(SQL)
                                Else
                                    Dr.Close()

                                    SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, serial_number, jumlah) values("
                                    SQL = SQL & "'" & KodePerusahaan & "', '" & so_tujuan & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("kode_barang") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("serial_number") & "', "
                                    SQL = SQL & "'" & .Rows(i).Item("jumlah") & "')"
                                    ExecuteTrans(SQL)
                                End If
                            End Using

                            SQL = "insert into Emi_Det_TS_IN(kode_perusahaan, kode_transfer, "
                            SQL = SQL & "kode_stock_owner, kode_barang, serial_number, no_urut, "
                            SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
                            SQL = SQL & "'" & no_ts & "', "
                            SQL = SQL & "'" & .Rows(i).Item("kode_stock_owner") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("kode_barang") & "', "
                            SQL = SQL & "'" & .Rows(i).Item("serial_number") & "', "
                            SQL = SQL & "IDENT_CURRENT('Emi_Transfer_IN_Detail'), '" & .Rows(i).Item("jumlah") & "')"
                            ExecuteTrans(SQL)
                        Next
                    Else
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Detail transfer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                End With
            End Using

            '------------------------

            Dim pagenumber As Integer = 1

            'SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
            'SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
            'SQL = SQL & "'" & Kode_Voucher & "', "
            'SQL = SQL & "'" & Format(CDate(tgl_ts), "yyyy-MM-dd") & "', "
            'SQL = SQL & "'" & Format(CDate(tgl_ts), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
            'SQL = SQL & "'" & KodeProyek & "', 'Transfer Stock Masuk " & no_ts & "', '', "
            'SQL = SQL & "'-', '" & UserID & "', '" & so_tujuan & "')"
            'ExecuteTrans(SQL)

            'SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_persediaan, 1),
            '               Strings.Mid(coa_persediaan, 2, 1),
            '               Strings.Mid(Ganti(coa_persediaan), 3),
            '               KodePerusahaan, KodeProyek, "Persediaan " & no_ts, total_hpp, "0", pagenumber, Lokasi)
            'ExecuteTrans(SQL)
            'pagenumber = pagenumber + 1

            'SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(coa_pending, 1),
            '              Strings.Mid(coa_pending, 2, 1),
            '              Strings.Mid(Ganti(coa_pending), 3),
            '              KodePerusahaan, KodeProyek, "Pending Persediaan " & no_ts, "0", total_hpp, pagenumber, Lokasi)
            'ExecuteTrans(SQL)
            'pagenumber = pagenumber + 1

            '==================
            SQL = "update Emi_Transfer_Stock set no_terima = '" & no_ts & "', "
            SQL = SQL & "tgl_terima = '" & Format(CDate(tgl_ts), "yyyy-MM-dd") & "', "
            SQL = SQL & "jam_terima = '" & Format(CDate(tgl_ts), "HH:mm:sss") & "', "
            SQL = SQL & "user_terima = '" & UserID & "' where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_transfer = '" & ListView1.FocusedItem.Text & "'"
            ExecuteTrans(SQL)

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show("Transaksi berhasil diterima.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
            ListView1.FocusedItem.ForeColor = Color.Red
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong()
        ComboBox6.SelectedIndex = 0

        SQL = "select a.status, a.keterangan, a.kode_transfer, a.tanggal, a.jam, a.kode_so_tujuan, b.kode_so_awal, "
        SQL = SQL & "a.Userid, a.no_terima, a.tgl_terima, a.jam_terima, a.user_terima from "
        SQL = SQL & "Emi_Transfer_Stock a, Emi_Transfer_Stock_Detail b, barang c where "
        SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
        SQL = SQL & "a.kode_transfer = b.kode_transfer and b.kode_so_awal = c.kode_stock_owner and "
        SQL = SQL & "b.kode_barang = c.kode_barang and a.no_terima is null and a.status is null and c.Kode_Perusahaan = '" & KodePerusahaan & "' "

        If ComboBox6.SelectedIndex <> 0 Then
            SQL = SQL & " and a.kode_so_tujuan = '" & ComboBox6.Text & "'  "
        End If


        SQL = SQL & "group by a.status, a.keterangan, a.kode_transfer, a.tanggal, a.jam, a.kode_so_tujuan, b.kode_so_awal, "
        SQL = SQL & "a.Userid, a.no_terima, a.tgl_terima, a.jam_terima, a.user_terima "
        SQL = SQL & "Order by a.kode_so_tujuan, a.tanggal + a.jam Desc "

        Try

            OpenConn()

            ListView1.Items.Clear() : ListView2.Items.Clear()

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView1.Items.Add(.Rows(i).Item("kode_transfer"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd-MMM-yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_so_awal"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_so_tujuan"))
                        If IsDBNull(.Rows(i).Item("Keterangan")) Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Keterangan"))
                        End If
                        Lvw.SubItems.Add(.Rows(i).Item("Userid"))

                        If General_Class.CekNULL(.Rows(i).Item("no_terima")) = "" Then
                            Lvw.SubItems.Add("-")
                            Lvw.SubItems.Add("-")
                            Lvw.SubItems.Add("-")
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("no_terima"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_terima"), "dd-MMM-yyyy"))
                            Lvw.SubItems.Add(.Rows(i).Item("jam_terima"))
                            Lvw.SubItems.Add(.Rows(i).Item("user_terima"))
                        End If

                        Lvw.SubItems.Add(i + 1)

                        If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then
                            ListView1.Items(i).ForeColor = Batal
                        Else
                            ListView1.Items(i).ForeColor = Color.Blue
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


    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.Items.Count = 0 Then Exit Sub
        Try
            OpenConn()

            ListView2.Items.Clear()
            SQL = "select b.kode_barang, c.nama, b.jumlah, c.satuan from Emi_Transfer_Stock_Detail b, barang c where "
            SQL = SQL & "b.kode_perusahaan = c.kode_perusahaan and b.kode_so_awal = c.kode_stock_owner and "
            SQL = SQL & "b.kode_barang = c.kode_barang and c.Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "b.kode_transfer = '" & ListView1.FocusedItem.Text & "'"
            Using Dr = Open(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView2.Items.Add(Dr("kode_barang"))
                    Lvw.SubItems.Add(Dr("nama"))
                    Lvw.SubItems.Add(Format(Dr("jumlah"), "N0"))
                    Lvw.SubItems.Add(Dr("satuan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox6.SelectedIndexChanged


        SQL = "select a.status, a.keterangan, a.kode_transfer, a.tanggal, a.jam, a.kode_so_tujuan, b.kode_so_awal, "
        SQL = SQL & "a.Userid, a.no_terima, a.tgl_terima, a.jam_terima, a.user_terima from "
        SQL = SQL & "Emi_Transfer_Stock a, Emi_Transfer_Stock_Detail b, barang c where "
        SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
        SQL = SQL & "a.kode_transfer = b.kode_transfer and b.kode_so_awal = c.kode_stock_owner and "
        SQL = SQL & "b.kode_barang = c.kode_barang and a.no_terima is null and a.status is null and c.Kode_Perusahaan = '" & KodePerusahaan & "' "

        If ComboBox6.SelectedIndex <> 0 Then
            SQL = SQL & " and a.kode_so_tujuan = '" & ComboBox6.Text & "'  "
        End If


        SQL = SQL & "group by a.status, a.keterangan, a.kode_transfer, a.tanggal, a.jam, a.kode_so_tujuan, b.kode_so_awal, "
        SQL = SQL & "a.Userid, a.no_terima, a.tgl_terima, a.jam_terima, a.user_terima "
        SQL = SQL & "Order by a.kode_so_tujuan, a.tanggal + a.jam Desc "

        Try

            OpenConn()

            ListView1.Items.Clear() : ListView2.Items.Clear()

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView1.Items.Add(.Rows(i).Item("kode_transfer"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd-MMM-yyyy"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_so_awal"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_so_tujuan"))
                        If IsDBNull(.Rows(i).Item("Keterangan")) Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("Keterangan"))
                        End If
                        Lvw.SubItems.Add(.Rows(i).Item("Userid"))

                        If General_Class.CekNULL(.Rows(i).Item("no_terima")) = "" Then
                            Lvw.SubItems.Add("-")
                            Lvw.SubItems.Add("-")
                            Lvw.SubItems.Add("-")
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("no_terima"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_terima"), "dd-MMM-yyyy"))
                            Lvw.SubItems.Add(.Rows(i).Item("jam_terima"))
                            Lvw.SubItems.Add(.Rows(i).Item("user_terima"))
                        End If

                        Lvw.SubItems.Add(i + 1)

                        If General_Class.CekNULL(.Rows(i).Item("status")) = "Y" Then
                            ListView1.Items(i).ForeColor = Batal
                        Else
                            ListView1.Items(i).ForeColor = Color.Blue
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
End Class