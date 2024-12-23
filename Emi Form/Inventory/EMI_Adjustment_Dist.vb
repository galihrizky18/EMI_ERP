Public Class EMI_Adjustment_Dist

    Dim JumlahOld As Double
    Dim arrInisialFaktur, Arr_COA_Persediaan, Arr_COA_Adj_Tambah, Arr_COA_Adj_Kurang As New ArrayList

    Private Sub Adjustment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Kosong()

        ListView3.Columns.Add("Stock Owner", 130, HorizontalAlignment.Center)
        ListView3.Columns.Add("Kode Barang", 130, HorizontalAlignment.Center)
        ListView3.Columns.Add("Nama", 310, HorizontalAlignment.Left)
        ListView3.Columns.Add("Good Stock", 100, HorizontalAlignment.Right)
        ListView3.View = View.Details

        ListView3.Location = New Point(4, 83)
        'DateTimePicker1.Focus()
    End Sub

    Private Sub Kosong()
        DateTimePicker1.Value = CDate(FMenu.ToolStripStatusLabel3.Text)
        urutan.Text = ""

        Try
            OpenConn()

            Cmb_Lokasi.Items.Clear() : arrInisialFaktur.Clear()
            Arr_COA_Persediaan.Clear() : Arr_COA_Adj_Tambah.Clear() : Arr_COA_Adj_Kurang.Clear()

            SQL = "select kode_stock_owner, inisial_faktur, persediaan, "
            'SQL = SQL & "adjustment_stock_tambah, adjustment_stock_kurang from stock_owner where "
            SQL = SQL & "adjustment_stock_tambah, adjustment_stock_kurang from stock_owner_gudang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Lokasi.Items.Add(Dr("kode_stock_owner")) : arrInisialFaktur.Add(Dr("inisial_faktur"))
                    Arr_COA_Persediaan.Add(Dr("persediaan"))
                    Arr_COA_Adj_Tambah.Add(Dr("adjustment_stock_tambah"))
                    Arr_COA_Adj_Kurang.Add(Dr("adjustment_stock_kurang"))
                Loop
            End Using
            'Cmb_Lokasi.Text = Lokasi
            Cmb_Lokasi.Text = "RAW MATERIAL"

            TextBox1.Text = FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                      General_Class.Get_Last_Number2("EMI_Adjustment", "kode_adjustment", JumlahDigit,
                      "Kode_perusahaan", KodePerusahaan,
                      "And", "substring(kode_adjustment,1," & Len(FAdj) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Cmb_Lokasi.Focus()

        DateTimePicker1.Enabled = True
        ListView3.Visible = False
        TextBox2.Text = "" : TextBox3.Text = "" : Txt_SisaStock.Text = "" : TextBox5.Text = "" : TextBox6.Text = ""
        TextBox7.Text = ""

        Cmb_Lokasi.Enabled = True : TextBox2.Enabled = True
        Btn_Simpan.Text = "&Simpan" : Btn_Hapus.Enabled = False
    End Sub

    Private Sub Adjustment_Dist_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub



    ''Private Sub Adjustment_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
    ''    Label13.Size = New Point(Me.Width, 33)
    ''End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Exit.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Cmb_Lokasi.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView3.Items.Count = 0 Then Exit Sub
            ListView3.Focus()
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox5.Focus()
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then TextBox6.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(Asc("-")) Or e.KeyChar = Chr(Asc(".")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Chr(13) Then
            Dtp_TglProd.Focus()
        End If
    End Sub

    Private Sub DateTimePicker1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles DateTimePicker1.Leave
        Try
            OpenConn()

            TextBox1.Text = FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                          General_Class.Get_Last_Number2("EMI_Adjustment", "kode_adjustment", JumlahDigit,
                          "Kode_perusahaan", KodePerusahaan,
                          "And", "substring(kode_adjustment,1," & Len(FAdj) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Simpan.Click
        If Cmb_Lokasi.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode stock owner harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Lokasi.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode barang harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        ElseIf TextBox5.Text.Trim.Length = 0 Then
            MessageBox.Show("Jumlah adjustment harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox5.Focus() : Exit Sub
        ElseIf TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show("Harga beli harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox7.Focus() : Exit Sub
        ElseIf TextBox6.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan harus diisi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox6.Focus() : Exit Sub
        ElseIf Format(DateTimePicker1.Value, "yyyyMM") <> Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyyMM") Then
            MessageBox.Show("Adjustment tidak boleh dibulan mundur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker1.Focus()
            Exit Sub
        ElseIf Dtp_TglProd.Text = Dtp_TglEx.Text Then
            MessageBox.Show("Tanggal Produksi Tidak Boleh Sama Dengan Tanggal Expire", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dtp_TglProd.Focus() : Exit Sub
        ElseIf Format(Dtp_TglProd.Value, "yyyy-MM-dd") > Format(Dtp_TglEx.Value, "yyyy-MM-dd") Then
            MessageBox.Show("Tanggal Produksi Tidak Boleh Lebih Besar dari Tanggal Expired!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dtp_TglProd.Focus() : Exit Sub
        ElseIf Format(Dtp_TglProd.Value, "yyyy-MM-dd") > Format(Tanggal_Sekarang, "yyyy-MM-dd") Then
            MessageBox.Show("Tanggal Produksi Tidak Boleh di Tanggal Maju!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Dtp_TglProd.Focus() : Exit Sub
        End If

        'Dim Tanya As String = MessageBox.Show("Anda yakin akan menyimpan data adjustment ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        'If Tanya = vbNo Then Exit Sub

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Text = "&Simpan" Then
                '==========================================================
                'Cek apakah update stock akan membuat stock menjadi negatif
                '==========================================================

                SQL = "select good_stock from barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi.Text & "' "
                SQL = SQL & "and kode_barang = '" & TextBox2.Text.Trim & "'"
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("good_stock") + HilangkanTanda(Val(TextBox5.Text)) < 0 Then
                            MessageBox.Show("Proses adjustment akan membuat stock menjadi negatif, proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            TextBox2.Focus()
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            Exit Sub
                        End If
                    Else
                        MessageBox.Show("Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        Exit Sub
                    End If
                End Using

                Dim flag_opm As String = ""

                SQL = "select flag_opname, buka_adjustment from Stock_Owner_Gudang "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "'" '
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        If Dr("flag_opname") = "Y" Then
                            If Dr("buka_adjustment") = 0 Then
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show(err_msg_opname, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            ElseIf Dr("buka_adjustment") > 0 Then
                                Dr.Close()
                                SQL = "update Stock_Owner_Gudang set buka_adjustment = buka_adjustment - 1 "
                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "'"
                                ExecuteTrans(SQL)

                                flag_opm = "'Y'"
                            Else
                                Dr.Close()
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Terjadi kesalahan!!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        Else
                            flag_opm = "NULL"
                        End If
                    Else
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                TextBox1.Text = FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                          General_Class.Get_Last_Number2("EMI_Adjustment", "kode_adjustment", JumlahDigit,
                          "Kode_perusahaan", KodePerusahaan,
                          "And", "substring(kode_adjustment,1," & Len(FAdj) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))


                Dim Kode_Voucher As String = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), fJU & arrInisialFaktur(Cmb_Lokasi.SelectedIndex), KodePerusahaan)

                SQL = "Insert Into EMI_Adjustment (kode_perusahaan, kode_adjustment, tanggal, jam, kode_stock_owner, kode_barang, jumlah, "
                SQL = SQL & "keterangan, userid, kode_voucher, harga_beli, flag_opm, Tgl_Produksi, Tgl_Expired) "
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & TextBox1.Text.Trim & "', '" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '"
                SQL = SQL & Cmb_Lokasi.Text & "', '" & TextBox2.Text.Trim & "', " & HilangkanTanda(TextBox5.Text) & ", "
                SQL = SQL & "'" & TextBox6.Text.Trim & "', '" & UserID & "', '" & Kode_Voucher & "', '" & TextBox7.Text & "', " & flag_opm & ", "
                SQL = SQL & "'" & Format(Dtp_TglProd.Value, "yyyy-MM-dd") & "', '" & Format(Dtp_TglEx.Value, "yyyy-MM-dd") & "')"
                ExecuteTrans(SQL)

                Dim total_hpp As Double = 0

                If Val(TextBox5.Text) < 0 Then 'minus 

                    Dim sisa As Double = 0

                    SQL = "select kode_stock_owner, kode_barang, serial_number, jumlah from barang_sn where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "' and "
                    SQL = SQL & "kode_barang = '" & TextBox2.Text.Trim & "' and jumlah <> 0 "
                    SQL = SQL & "order by " & SN_Tanggal("serial_number") & Metode
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then
                                sisa = -Val(HilangkanTanda(TextBox5.Text))

                                For h As Integer = 0 To .Rows.Count - 1
                                    If sisa = 0 Then
                                        Exit For
                                    ElseIf sisa < 0 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Sisa < 0", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    If sisa < .Rows(h).Item("jumlah") Or sisa = .Rows(h).Item("jumlah") Then
                                        SQL = "Update barang_sn set jumlah = jumlah - " & sisa & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                        SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                        SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                        ExecuteTrans(SQL)

                                        SQL = "insert into EMI_Det_Adj(kode_perusahaan, no_faktur, "
                                        SQL = SQL & "kode_stock_owner, kode_barang, serial_number, "
                                        SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
                                        SQL = SQL & "'" & TextBox1.Text.Trim & "', "
                                        SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                        SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                        SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                        SQL = SQL & "'" & sisa & "')"
                                        ExecuteTrans(SQL)

                                        total_hpp = total_hpp + (sisa * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                        sisa = 0
                                    ElseIf sisa > .Rows(h).Item("jumlah") Then
                                        SQL = "insert into EMI_Det_Adj(kode_perusahaan, no_faktur, "
                                        SQL = SQL & "kode_stock_owner, kode_barang, serial_number, "
                                        SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
                                        SQL = SQL & "'" & TextBox1.Text.Trim & "', "
                                        SQL = SQL & "'" & .Rows(h).Item("kode_stock_owner") & "', "
                                        SQL = SQL & "'" & .Rows(h).Item("kode_barang") & "', "
                                        SQL = SQL & "'" & .Rows(h).Item("serial_number") & "', "
                                        SQL = SQL & "'" & .Rows(h).Item("jumlah") & "')"
                                        ExecuteTrans(SQL)

                                        SQL = "Update barang_sn set jumlah = jumlah - jumlah where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_stock_owner = '" & .Rows(h).Item("kode_stock_owner") & "' and "
                                        SQL = SQL & "kode_barang = '" & .Rows(h).Item("kode_barang") & "' and "
                                        SQL = SQL & "serial_number = '" & .Rows(h).Item("serial_number") & "'"
                                        ExecuteTrans(SQL)

                                        total_hpp = total_hpp + (.Rows(h).Item("jumlah") * Get_Harga_SN(.Rows(h).Item("serial_number")))

                                        sisa = sisa - .Rows(h).Item("jumlah")
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Barang SN terjadi kesalahan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If

                                    If sisa <> 0 And h = .Rows.Count - 1 Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Jumlah stock tidak mencukupi!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                Next
                            Else
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("SN untuk barang ini tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            End If
                        End With
                    End Using

                Else ' kalo nambahin stock

                    Dim Rand As New Random
                    Dim str As String = Format(Rand.Next(0, 999), "000") & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HHmmss")
                    Dim Kode_Unik As String = str.Substring(0, 5) & "BB" & Chr(64 + str.Substring(6, 1)) & str.Substring(6, Len(str) - 6)

                    Dim SN As String = Kode_Unik & Tanda_SN & "01" & Tanda_SN & TextBox7.Text & Tanda_SN & "02" & Tanda_SN & Format(DateTimePicker1.Value, "yyyy-MM-dd")

                    SQL = "select kode_barang from barang_sn where "
                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "' and "
                    SQL = SQL & "kode_barang = '" & TextBox2.Text.Trim & "' and serial_number = '" & SN & "'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            SQL = "Update barang_sn set jumlah = jumlah + " & TextBox5.Text & ", rr = 'X' where "
                            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "' and kode_barang = '" & TextBox2.Text.Trim & "' and "
                            SQL = SQL & "serial_number = '" & SN & "'"
                            ExecuteTrans(SQL)
                        Else
                            SQL = "insert into barang_sn(kode_perusahaan, kode_stock_owner, kode_barang, "
                            SQL = SQL & "serial_number, jumlah, rr, Tgl_Produksi, Tgl_Expired) values('" & KodePerusahaan & "', "
                            SQL = SQL & "'" & Cmb_Lokasi.Text & "', '" & TextBox2.Text.Trim & "', "
                            SQL = SQL & "'" & SN & "', " & TextBox5.Text & ", 'X', '" & Format(Dtp_TglProd.Value, "yyyy-MM-dd") & "', '" & Format(Dtp_TglEx.Value, "yyyy-MM-dd") & "')"
                            Dr.Close()
                            ExecuteTrans(SQL)
                        End If
                    End Using


                    SQL = "insert into EMI_Det_Adj(kode_perusahaan, no_faktur, "
                    SQL = SQL & "kode_stock_owner, kode_barang, serial_number, "
                    SQL = SQL & "jumlah) values('" & KodePerusahaan & "', "
                    SQL = SQL & "'" & TextBox1.Text.Trim & "', "
                    SQL = SQL & "'" & Cmb_Lokasi.Text & "', "
                    SQL = SQL & "'" & TextBox2.Text.Trim & "', "
                    SQL = SQL & "'" & SN & "', "
                    SQL = SQL & "'" & TextBox5.Text & "')"
                    ExecuteTrans(SQL)

                End If

                '==========================================================

                SQL = "Update barang set good_stock = good_stock + " & HilangkanTanda(TextBox5.Text) & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_stock_owner = '" & Cmb_Lokasi.Text & "' and kode_Barang = '" & TextBox2.Text.Trim & "'"
                ExecuteTrans(SQL)


                '==========================================================


                Dim pagenumber As Integer = 1

                SQL = "Insert Into Jurnal(Kode_Voucher, Tanggal, Jam, Kode_Perusahaan, Kode_Proyek, "
                SQL = SQL & "Keterangan, JudulBank, KetDK, userid, lokasi) values("
                SQL = SQL & "'" & Kode_Voucher & "', "
                SQL = SQL & "'" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', '" & KodePerusahaan.ToUpper & "', "
                SQL = SQL & "'" & KodeProyek & "', 'Adjustment Stock " & TextBox1.Text.Trim & "', '', "
                SQL = SQL & "'-', '" & UserID & "', '" & Cmb_Lokasi.Text & "')"
                ExecuteTrans(SQL)

                Dim tot_adj As Double = 0

                If Val(TextBox5.Text) < 0 Then 'minus
                    'tot_adj = Val(TextBox5.Text) * Val(TextBox7.Text)
                    tot_adj = total_hpp

                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Arr_COA_Adj_Kurang.Item(Cmb_Lokasi.SelectedIndex), 1),
                              Strings.Mid(Arr_COA_Adj_Kurang.Item(Cmb_Lokasi.SelectedIndex), 2, 1),
                              Strings.Mid(Ganti(Arr_COA_Adj_Kurang.Item(Cmb_Lokasi.SelectedIndex)), 3),
                              KodePerusahaan, KodeProyek, "Biaya Selisih Adjustment " & TextBox1.Text.Trim, tot_adj, "0", pagenumber, Cmb_Lokasi.Text)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Arr_COA_Persediaan.Item(Cmb_Lokasi.SelectedIndex), 1),
                                   Strings.Mid(Arr_COA_Persediaan.Item(Cmb_Lokasi.SelectedIndex), 2, 1),
                                   Strings.Mid(Ganti(Arr_COA_Persediaan.Item(Cmb_Lokasi.SelectedIndex)), 3),
                                   KodePerusahaan, KodeProyek, "Persediaan " & TextBox1.Text.Trim, "0", tot_adj, pagenumber, Cmb_Lokasi.Text)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    '==============================

                    SQL = "update EMI_Adjustment set grand = " & tot_adj & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_adjustment = '" & TextBox1.Text.Trim & "'"
                    ExecuteTrans(SQL)

                    '===============================

                Else 'tambah
                    tot_adj = Val(TextBox5.Text) * Val(TextBox7.Text)

                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Arr_COA_Persediaan.Item(Cmb_Lokasi.SelectedIndex), 1),
                              Strings.Mid(Arr_COA_Persediaan.Item(Cmb_Lokasi.SelectedIndex), 2, 1),
                              Strings.Mid(Ganti(Arr_COA_Persediaan.Item(Cmb_Lokasi.SelectedIndex)), 3),
                              KodePerusahaan, KodeProyek, "Persediaan " & TextBox1.Text.Trim, tot_adj, "0", pagenumber, Cmb_Lokasi.Text)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    SQL = Get_Detail_Jurnal(Kode_Voucher, Strings.Left(Arr_COA_Adj_Tambah.Item(Cmb_Lokasi.SelectedIndex), 1),
                                   Strings.Mid(Arr_COA_Adj_Tambah.Item(Cmb_Lokasi.SelectedIndex), 2, 1),
                                   Strings.Mid(Ganti(Arr_COA_Adj_Tambah.Item(Cmb_Lokasi.SelectedIndex)), 3),
                                   KodePerusahaan, KodeProyek, "Pendapatan Selisih Adjustment " & TextBox1.Text.Trim, "0", tot_adj, pagenumber, Cmb_Lokasi.Text)
                    ExecuteTrans(SQL)
                    pagenumber = pagenumber + 1

                    '==============================

                    SQL = "update EMI_Adjustment set grand = " & tot_adj & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_adjustment = '" & TextBox1.Text.Trim & "'"
                    ExecuteTrans(SQL)

                    '===============================
                End If


                SQL = "update EMI_Brg_Lampung set sudah_dist = 'Y' where urut = '" & urutan.Text.Trim & "'"
                ExecuteTrans(SQL)

                '========= 



            Else 'Proses update

                If CekButtonRole("update_adjustment") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                'SQL = "select jumlah from adjustment where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_adjustment = '" & TextBox1.Text.Trim & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        JumlahOld = Dr("jumlah")
                '    Else
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        MessageBox.Show("Data adjustment tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Exit Sub
                '    End If
                'End Using

                'Dim tanda As String
                'If Strings.Left(JumlahOld, 1) = "-" Then 'JumlahOld = +JumlahOld Else JumlahOld = -JumlahOld
                '    tanda = "-"
                'Else
                '    tanda = "+"
                'End If

                ' ''==========================================================
                ' ''Cek apakah update stock akan membuat stock menjadi negatif
                ' ''==========================================================
                'SQL = "select good_stock from barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox1.Text & "' "
                'SQL = SQL & "and kode_barang = '" & TextBox2.Text.Trim & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        Dim x As Double = Dr("good_stock") - JumlahOld
                '        If x < 0 Then
                '            MessageBox.Show("Proses adjustment stock lama akan membuat stock menjadi negatif, proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            TextBox2.Focus()
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            Exit Sub
                '        End If
                '    Else
                '        MessageBox.Show("Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        Exit Sub
                '    End If
                'End Using
                ' ''==========================================================

                ' ''==========================================================
                ' ''Cek apakah update stock akan membuat stock menjadi negatif
                ' ''==========================================================
                'SQL = "select good_stock from barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox1.Text & "' "
                'SQL = SQL & "and kode_barang = '" & TextBox2.Text.Trim & "'"
                'Using Dr = OpenTrans(SQL)
                '    If Dr.Read Then
                '        Dim x As Double = Dr("good_stock") - JumlahOld
                '        If x + HilangkanTanda(TextBox5.Text) < 0 Then
                '            MessageBox.Show("Proses adjustment akan membuat stock menjadi negatif, proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '            TextBox2.Focus()
                '            Dr.Close()
                '            CloseTrans()
                '            CloseConn()
                '            Exit Sub
                '        End If
                '    Else
                '        MessageBox.Show("Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '        Dr.Close()
                '        CloseTrans()
                '        CloseConn()
                '        Exit Sub
                '    End If
                'End Using
                ' ''==========================================================

                'SQL = "Update Adjustment set "
                'SQL = SQL & "jumlah = " & HilangkanTanda(TextBox5.Text) & ", "
                'SQL = SQL & "harga_beli = '" & TextBox7.Text & "', "
                'SQL = SQL & "keterangan = '" & TextBox6.Text.Trim & "', userid = '" & UserID & "' "
                'SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_adjustment = '" & TextBox1.Text.Trim & "'"
                'ExecuteTrans(SQL)

                'SQL = "Update barang set good_stock = good_stock -(" & JumlahOld & ")" & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_stock_owner = '" & ComboBox1.Text & "' and kode_Barang = '" & TextBox2.Text.Trim & "'"
                'ExecuteTrans(SQL)

                'SQL = "Update barang set good_stock = good_stock + " & HilangkanTanda(TextBox5.Text) & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_stock_owner = '" & ComboBox1.Text & "' and kode_Barang = '" & TextBox2.Text.Trim & "'"
                'ExecuteTrans(SQL)

            End If

            Cmd.Transaction.Commit()

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
        DateTimePicker1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btn_Hapus.Click
        Dim AskFirst As String = MessageBox.Show("Anda yakin akan hapus data ini . . ? ?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If AskFirst = vbNo Then Exit Sub

        If Format(DateTimePicker1.Value, "yyyyMM") <> Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyyMM") Then
            MessageBox.Show("Adjustment tidak boleh dibulan mundur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            DateTimePicker1.Focus()
            Exit Sub
        End If

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            If CekButtonRole("hapus_adjustment") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            'Dim tanda As String
            'If Strings.Left(JumlahOld, 1) = "-" Then 'JumlahOld = +JumlahOld Else JumlahOld = -JumlahOld
            '    tanda = "-"
            'Else
            '    tanda = "+"
            'End If
            ''==========================================================
            ''Cek apakah update stock akan membuat stock menjadi negatif
            ''==========================================================
            'SQL = "select * from barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & ComboBox1.Text & "' "
            'SQL = SQL & "and kode_barang = '" & TextBox2.Text.Trim & "'"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If Dr("good_stock") - JumlahOld < 0 Then
            '            MessageBox.Show("Proses adjustment stock akan membuat stock menjadi negatif, proses tidak dapat dilanjutkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '            TextBox2.Focus()
            '            Dr.Close()
            '            CloseTrans()
            '            CloseConn()
            '            Exit Sub
            '        End If
            '    End If
            'End Using
            ''==========================================================

            'SQL = "Update barang set good_stock = good_stock -(" & JumlahOld & ") where kode_perusahaan = '" & KodePerusahaan & "' and "
            'SQL = SQL & "kode_stock_owner = '" & ComboBox1.Text & "' and kode_Barang = '" & TextBox2.Text.Trim & "'"
            'ExecuteTrans(SQL)

            'ExecuteTrans("Delete from adjustment where kode_perusahaan = '" & KodePerusahaan & "' and kode_adjustment = '" & TextBox1.Text.Trim & "'")

            Cmd.Transaction.Commit()

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Kosong()
        DateTimePicker1.Focus()
    End Sub

    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        If TextBox1.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "select a.*,b.nama,b.good_stock from EMI_Adjustment as a inner join barang as b on a.kode_perusahaan = b.kode_perusahaan "
            SQL = SQL & "and a.kode_stock_owner = b.kode_stock_owner and a.kode_barang = b.kode_barang "
            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' and a.kode_adjustment = '" & TextBox1.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("kode_adjustment")
                    DateTimePicker1.Value = Dr("tanggal")
                    For i As Integer = 0 To Cmb_Lokasi.Items.Count - 1
                        xSplit = Cmb_Lokasi.Items(i).split("-")
                        If Dr("kode_stock_owner") = xSplit(0).Trim Then
                            Cmb_Lokasi.SelectedIndex = i
                            Exit For
                        End If
                    Next
                    TextBox2.Text = Dr("kode_barang")
                    TextBox3.Text = Dr("nama")
                    Txt_SisaStock.Text = Dr("good_stock")
                    TextBox5.Text = Dr("jumlah")
                    TextBox7.Text = Dr("harga_beli")
                    JumlahOld = Dr("jumlah")
                    TextBox6.Text = Dr("keterangan")
                    TextBox2.Enabled = False : Cmb_Lokasi.Enabled = False
                    DateTimePicker1.Enabled = False
                    Btn_Simpan.Text = "&Update" : Btn_Hapus.Enabled = True
                Else
                    TextBox1.Text = FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy") & "-" &
                                      General_Class.Get_Last_Number2("EMI_Adjustment", "kode_adjustment", JumlahDigit,
                                      "Kode_perusahaan", KodePerusahaan,
                                      "And", "substring(kode_adjustment,1," & Len(FAdj) + Len(arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex)) + 6 & ")", FAdj & arrInisialFaktur.Item(Cmb_Lokasi.SelectedIndex) & "-" & Format(DateTimePicker1.Value, "MM/yy"))

                    'ComboBox1.SelectedIndex = -1
                    TextBox2.Text = "" : TextBox3.Text = "" : Txt_SisaStock.Text = "" : TextBox5.Text = "" : TextBox6.Text = ""
                    TextBox2.Enabled = True : Cmb_Lokasi.Enabled = True : DateTimePicker1.Enabled = True
                    TextBox7.Text = ""
                    Btn_Simpan.Text = "&Simpan" : Btn_Hapus.Enabled = False
                End If
            End Using
            ListView3.Visible = False

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox2_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.Leave
        If TextBox2.Text.Trim.Length = 0 Then Exit Sub
        If Btn_Simpan.Text <> "&Simpan" Then Exit Sub
        If ListView3.Focused = True Then Exit Sub

        Try

            OpenConn()

            'iniiiii
            Dim boleh_lihat As Boolean

            SQL = "select flag_hide_stock, "
            SQL = SQL & "ISNULL(("
            SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            SQL = SQL & "), 'T') AS boleh_lihat_stock "
            SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_stock_owner = '" & Cmb_Lokasi.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("flag_hide_stock") = "Y" Then
                        If Dr("boleh_lihat_stock") = "Y" Then
                            boleh_lihat = True
                        Else
                            boleh_lihat = False
                        End If
                    Else
                        boleh_lihat = True
                    End If
                Else
                    boleh_lihat = False
                End If
            End Using

            SQL = "select kode_barang, nama, good_stock, last_hpp from barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi.Text & "' and "
            SQL = SQL & "kode_barang = '" & TextBox2.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox2.Text = Dr("kode_barang")
                    TextBox3.Text = Dr("nama")
                    'iniiiii
                    If boleh_lihat = True Then
                        Txt_SisaStock.Text = Dr("good_stock")
                    Else
                        Txt_SisaStock.Text = ""
                    End If
                    'iniiiii
                    'TextBox4.Text = Dr("good_stock")
                    TextBox7.Text = Dr("last_hpp")
                    urutan.Text = "0"
                Else
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Kode barang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    TextBox2.Text = "" : TextBox3.Text = "" : Txt_SisaStock.Text = "" : urutan.Text = ""
                    MessageBox.Show("Barang tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using
            ListView3.Visible = False

            CloseConn()

        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text.Trim.Length = 0 Then
            ListView3.Visible = False : Exit Sub
        Else
            ListView3.Visible = True
        End If

        Try
            OpenConn()

            Dim boleh_lihat As Boolean

            SQL = "select flag_hide_stock, "
            SQL = SQL & "ISNULL(("
            SQL = SQL & "select top(1) 'Y' from role_button a where a.kode_perusahaan = x.kode_perusahaan and "
            SQL = SQL & "a.userid = '" & UserID & "' and buttonname = 'LIHAT_STOCK'"
            SQL = SQL & "), 'T') AS boleh_lihat_stock "
            'SQL = SQL & " from stock_owner x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & " from Stock_Owner_Gudang x where x.kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "x.kode_stock_owner = '" & Cmb_Lokasi.Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    If Dr("flag_hide_stock") = "Y" Then
                        If Dr("boleh_lihat_stock") = "Y" Then
                            boleh_lihat = True
                        Else
                            boleh_lihat = False
                        End If
                    Else
                        boleh_lihat = True
                    End If
                Else
                    boleh_lihat = False
                End If
            End Using

            ListView3.Items.Clear()

            SQL = "Select kode_stock_owner,kode_barang,nama,good_stock From barang where kode_perusahaan = '" & KodePerusahaan & "' and kode_stock_owner = '" & Cmb_Lokasi.Text & "' and (kode_barang like '" & TextBox2.Text & "%' or nama like '" & TextBox2.Text & "%') order by kode_stock_owner,kode_barang"
            Using ds = Binding(SQL)
                With ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView3.Items.Add(.Rows(i).Item("kode_stock_owner"))
                        Lvw.SubItems.Add(.Rows(i).Item("kode_barang"))
                        Lvw.SubItems.Add(.Rows(i).Item("Nama"))
                        'iniiiii
                        If boleh_lihat = True Then
                            Lvw.SubItems.Add(.Rows(i).Item("good_stock"))
                        Else
                            Lvw.SubItems.Add("")
                        End If
                        'iniiiii
                        'Lvw.SubItems.Add(.Rows(i).Item("good_stock"))
                    Next
                End With
            End Using

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView3_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView3.DoubleClick
        If ListView3.Items.Count = 0 Then Exit Sub

        TextBox2.Text = ListView3.FocusedItem.SubItems(1).Text
        TextBox2_Leave(ListView3, e)
        TextBox2.Focus()
        TextBox5.Focus()
        ListView3.Visible = False
    End Sub

    Private Sub ListView3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView3.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView3_DoubleClick(ListView3, e)
        End If
    End Sub

    Private Sub DateTimePicker1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePicker1.ValueChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick

    End Sub

    Private Sub ComboBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Cmb_Lokasi.Leave
        TextBox2.Text = ""
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cmb_Lokasi.SelectedIndexChanged

    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        For i As Integer = 0 To ListView1.Items.Count - 1

            Cmb_Lokasi.Text = ListView1.Items(i).Text
            TextBox2.Text = ListView1.Items(i).SubItems(1).Text
            TextBox2_Leave(ListView1, e)
            TextBox5.Text = Val(ListView1.Items(i).SubItems(2).Text)
            TextBox6.Text = "ADJ PLUS JAMBI"
            TextBox7.Text = ListView1.Items(i).SubItems(3).Text
            urutan.Text = ListView1.Items(i).SubItems(4).Text
            Button1_Click(ListView1, e)
        Next
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            OpenConn()

            Dim lv As New ListViewItem
            ListView1.Items.Clear()

            SQL = "select  kode_stock_owner, kode_barang,jumlah as jml, hpp, urut from EMI_Brg_Lampung where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "sudah_dist is null"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView1.Items.Add(Dr("kode_stock_owner"))
                    lv.SubItems.Add(Dr("kode_barang"))
                    lv.SubItems.Add(Dr("jml"))
                    lv.SubItems.Add(Dr("hpp"))
                    lv.SubItems.Add(Dr("urut"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub Dtp_TglProd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dtp_TglProd.KeyPress
        If e.KeyChar = Chr(13) Then Dtp_TglEx.Focus()
    End Sub

    Private Sub Dtp_TglEx_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Dtp_TglEx.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

End Class