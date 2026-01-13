Public Class N_EMI_Display_Data_Pelunasan_Tunai_Per_DO
    Dim Arr1, Arr2, Arr3, ArrUrut, ArrNilai, ArrUrut_Lunas, Arrno_UM As New ArrayList
    Dim T As Color = Color.Blue
    Dim KT As Color = Color.Red
    Dim KY As Color = Color.Green
    Dim Batal As Color = Color.Black
    Dim pertama As Integer = 1

    Private Sub Hitung()
        Dim ttl As Double = 0

        For i As Integer = 0 To ListView2.Items.Count - 1
            ttl = ttl + Val(HilangkanTanda(ListView2.Items(i).SubItems(5).Text))
        Next

        TextBoxa.Text = Format(ttl, "N0")
    End Sub

    Private Sub cetak()
        Try

            OpenConn()

            SQL = "select kode_perusahaan from val_do_tunai where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "no_val = '" & ListView1.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                If Ds.Tables("MyTable").Rows.Count <> 0 Then
                    Dim CrDoc As New Faktur_Pelunasan_DO_Tunai  'Nama file CR
                    CrDoc.SetDataSource(Ds)
                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                    CrDoc.PrintOptions.PrinterName = PrinterName
                    CrDoc.RecordSelectionFormula = "{val_do_tunai.Kode_Perusahaan} = '" & KodePerusahaan & "' and {val_do_tunai.no_val} = '" & ListView1.FocusedItem.Text & "'"

                    Dim doctoprint As New System.Drawing.Printing.PrintDocument()
                    doctoprint.PrinterSettings.PrinterName = PrinterName
                    Dim rawKind As Integer
                    CrDoc.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.DefaultPaperSize
                    For i = doctoprint.PrinterSettings.PaperSizes.Count - 1 To 0 Step -1
                        If doctoprint.PrinterSettings.PaperSizes(i).PaperName = "Faktur" Then
                            rawKind = CInt(doctoprint.PrinterSettings.PaperSizes(i).GetType().GetField("kind", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic).GetValue(doctoprint.PrinterSettings.PaperSizes(i)))
                            CrDoc.PrintOptions.PaperSize = rawKind
                            Exit For
                        End If
                    Next

                    CrDoc.PrintOptions.PaperSize = CType(rawKind, CrystalDecisions.Shared.PaperSize)
                    CrDoc.PrintToPrinter(1, False, 1, 99)
                End If
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Display_Data_Val_Penj_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Display_Data_Pembelian_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        ListView1.Columns.Add("No Pelunasan", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Tgl", 70, HorizontalAlignment.Center)
        ListView1.Columns.Add("Jam", 50, HorizontalAlignment.Center)
        ListView1.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("Cara Bayar", 170, HorizontalAlignment.Left)
        ListView1.Columns.Add("UserID", 300, HorizontalAlignment.Left)
        ListView1.View = View.Details

        ListView2.Columns.Add("No DO", 100, HorizontalAlignment.Left)
        ListView2.Columns.Add("No Faktur", 100, HorizontalAlignment.Left)
        ListView2.Columns.Add("Tgl Transaksi", 80, HorizontalAlignment.Center)
        ListView2.Columns.Add("Kode Customer", 100, HorizontalAlignment.Left)
        ListView2.Columns.Add("Nama Customer", 250, HorizontalAlignment.Left)
        ListView2.Columns.Add("Jumlah", 90, HorizontalAlignment.Right)
        ListView2.Columns.Add("Disc Cash", 90, HorizontalAlignment.Right)
        ListView2.View = View.Details

        CheckBox1.Checked = False : CheckBox2.Checked = False
        ComboBox1.Items.Clear() : ComboBox1.Text = "" : Arr1.Clear()
        ComboBox1.Items.Add("Tgl Pelunasan") : Arr1.Add("c.Tanggal")
        ComboBox1.Items.Add("Tgl Transaksi") : Arr1.Add("a.Tanggal")
        ComboBox1.Items.Add("Tgl Jth Tmpo") : Arr1.Add("a.tgl_jatuh_tempo")

        ComboBox2.Items.Clear() : ComboBox2.Text = "" : Arr2.Clear()
        ComboBox2.Items.Add("No Pelunasan") : Arr2.Add("c.No_val")
        ComboBox2.Items.Add("No DO") : Arr2.Add("e.no_do")
        ComboBox2.Items.Add("No Faktur") : Arr2.Add("e.no_faktur")
        ComboBox2.Items.Add("Keterangan") : Arr2.Add("c.keterangan")
        ComboBox2.Items.Add("Cara Bayar") : Arr2.Add("c.cara_bayar")
        ComboBox2.Items.Add("Kode Customer") : Arr2.Add("a.kode_Customer")
        ComboBox2.Items.Add("Nama Customer") : Arr2.Add("b.Nama")
        ComboBox2.Items.Add("UserID") : Arr2.Add("c.Uservalidasi")

        TextBoxa.Text = "0"
        ComboBox1.Enabled = False : ComboBox2.Enabled = False
        DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
        TextBox1.Enabled = False
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            ComboBox1.Enabled = True : DateTimePicker1.Enabled = True : DateTimePicker2.Enabled = True
            CheckBox3.Checked = False
        Else
            ComboBox1.Enabled = False : DateTimePicker1.Enabled = False : DateTimePicker2.Enabled = False
            ComboBox1.SelectedIndex = -1 : DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked Then
            ComboBox2.Enabled = True : TextBox1.Enabled = True
        Else
            ComboBox2.Enabled = False : TextBox1.Enabled = False
            ComboBox2.SelectedIndex = -1 : TextBox1.Text = ""
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            Button1_Click(CheckBox3, e)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            pertama = 1

            If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
                MessageBox.Show("Pilih terlebih dahulu parameter pencarian data!", Judul)
                CheckBox1.Focus() : Exit Sub
            End If

            If CheckBox1.Checked Then
                If ComboBox1.SelectedIndex = -1 Then
                    MessageBox.Show("Parameter pencarian per tanggal harus diisi!", Judul)
                    ComboBox1.Focus() : Exit Sub
                ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                    MessageBox.Show("Periode I tidak boleh lebih dari periode II!", Judul)
                    DateTimePicker1.Value = Now.Date : DateTimePicker2.Value = Now.Date
                    Exit Sub
                End If
            ElseIf CheckBox2.Checked Then
                If ComboBox2.SelectedIndex = -1 Then
                    MessageBox.Show("Parameter lain harus diisi!", Judul)
                    ComboBox2.Focus() : Exit Sub
                ElseIf TextBox1.Text.Trim.Length = 0 Then
                    MessageBox.Show("Value parameter lain harus diisi!", Judul)
                    TextBox1.Focus() : Exit Sub
                End If
            End If

            SQL = "select c.no_val, c.tanggal, c.jam, c.keterangan, c.cara_bayar, c.uservalidasi, c.status "
            SQL = SQL & "from penjualan a, customers b, val_do_tunai c, detail_val_do_tunai d, do_new e where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
            SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and d.kode_perusahaan = e.kode_perusahaan and a.kode_customer = b.kode_customer and "
            SQL = SQL & "c.no_val = d.no_val and d.no_faktur = e.no_do and a.no_faktur =  e.no_faktur and "
            SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' "

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & " c.tanggal between '"
                SQL = SQL & Format(Now, "yyyy-MM-dd") & "' and '" & Format(Now, "yyyy-MM-dd") & "' "
            End If

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr1.Item(ComboBox1.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & Arr2.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox1.Text) & "%' "
            End If

            SQL = SQL & "group by c.no_val, c.tanggal, c.jam, c.keterangan, c.cara_bayar, c.uservalidasi, c.status "
            SQL = SQL & "Order by c.tanggal + c.jam Desc"

            OpenConn()

            ListView1.Items.Clear() : ListView2.Items.Clear() : TextBoxa.Text = "0"

            Dim Lvw As ListViewItem

            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            Lvw = ListView1.Items.Add(.Rows(i).Item("no_val"))
                            Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))
                            Lvw.SubItems.Add(.Rows(i).Item("jam"))
                            Lvw.SubItems.Add(.Rows(i).Item("keterangan"))
                            Lvw.SubItems.Add(.Rows(i).Item("cara_bayar"))
                            Lvw.SubItems.Add(.Rows(i).Item("uservalidasi"))

                            ListView1.Items(i).ForeColor = T

                            If General_Class.CekNULL(.Rows(i).Item("status")) <> "" Then
                                ListView1.Items(i).ForeColor = Batal
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

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then Button1_Click(TextBox1, e)
    End Sub

    Private Sub CheckBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If ComboBox1.Enabled = True Then
                ComboBox1.Focus()
            Else
                CheckBox2.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker1.Focus()
    End Sub

    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
        If e.KeyChar = Chr(13) Then DateTimePicker2.Focus()
    End Sub

    Private Sub DateTimePicker2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker2.KeyPress
        If e.KeyChar = Chr(13) Then Button1_Click(DateTimePicker2, e)
    End Sub

    Private Sub CheckBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles CheckBox2.KeyPress
        If e.KeyChar = Chr(13) Then
            If ComboBox2.Enabled = True Then
                ComboBox2.Focus()
            Else
                Button1.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        DateTimePicker1.Focus()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        TextBox1.Focus()
    End Sub

    Private Sub BatalkanTransaksiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BatalkanTransaksiToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no pelunasan yang mau dibatalkan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim tanya As String = MessageBox.Show("Yakin akan membatalkan transaksi ini?", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If tanya = vbYes Then
            GetTime()
            Try
                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                If CekButtonRole("batal_pelunasan_do_tunai") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                Dim kd_vcr As String = ""

                SQL = "select status, tanggal, kode_voucher from val_do_tunai where "
                SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & ListView1.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            kd_vcr = .Rows(0).Item("kode_voucher")

                            If General_Class.CekNULL(.Rows(0).Item("status")) = "Y" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("Pelunasan tidak bisa diupdate, karena sudah dibatalkan sebelumnya!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Exit Sub
                            ElseIf Format(.Rows(0).Item("tanggal"), "yyyyMM") <> Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyyMM") Then
                                MessageBox.Show("Pembatalan tidak boleh dibulan mundur!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                DateTimePicker1.Focus()
                                Exit Sub
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                Dim sudah_lunas_ada_data As Boolean = True
                Dim faktur_temp As String = ""

                Dim pesan_lunas As String
                pesan_lunas = "Pembatalan Tidak Bisa Dilakukan. Silahkan Batalkan Faktur Berikut Terlebih Dahulu : " & Chr(13)


                SQL = "select a.no_faktur, c.kode_customer, a.no_do, b.no_val, b.byr, b.disc_cash from "
                SQL = SQL & "do_new a, detail_val_do_tunai b, penjualan c where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan And b.kode_perusahaan = c.kode_perusahaan and "
                SQL = SQL & "a.no_do = b.no_faktur and a.no_faktur = c.no_faktur and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "b.no_val = '" & ListView1.FocusedItem.Text & "' ORDER BY Urut desc"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                SQL = "select um from customers where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "kode_customer = '" & .Rows(i).Item("kode_customer") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dr.Close()

                                        SQL = "update customers set um = um + " & .Rows(i).Item("byr") & " where "
                                        SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_customer = '" & .Rows(i).Item("kode_customer") & "'"
                                        ExecuteTrans(SQL)
                                    Else
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Customer tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    End If
                                End Using

                                Dim Sudah_lunas As Boolean = True

                                Dim UrutDataMasuk As String = ""
                                Dim UrutCustomer As String = ""
                                Dim UrutLog As String = ""
                                Dim PlafonSebelum As String = ""
                                Dim MemberSebelum As String = ""
                                Dim KategoriSebelum As String = ""
                                SQL = "select a.Urut, b.Urut_Customer, b.No_Urut, b.Plafon_Sebelum, b.Member_Sebelum, "
                                SQL = SQL & "b.Kode_Kategori_Sebelum from member_data_masuk a, Member_log_Check_All b where "
                                SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur = b.No_Faktur and a.No_Do_Pelunasan = b.No_DO_Pelunasan and "
                                SQL = SQL & "a.No_Faktur ='" & ListView1.FocusedItem.Text & "' and "
                                SQL = SQL & "a.No_DO_Pelunasan ='" & .Rows(i).Item("no_do") & "'"
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        UrutDataMasuk = Dr("Urut")
                                        UrutCustomer = Dr("Urut_Customer")
                                        UrutLog = Dr("No_Urut")
                                        PlafonSebelum = Dr("Plafon_Sebelum")
                                        MemberSebelum = Dr("Member_Sebelum")
                                        KategoriSebelum = Dr("Kode_Kategori_Sebelum")
                                    Else
                                        'Dr.Close()
                                        'CloseTrans()
                                        'CloseConn()
                                        'MessageBox.Show("Log tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        'Exit Sub
                                        Sudah_lunas = False
                                    End If
                                End Using

                                If Sudah_lunas = True Then
                                    Dim ind As Integer = 0
                                    Dim Pesan As String = ""
                                    Dim cek_data As Boolean = True
                                    Dim data_temp As String = ""
                                    Pesan = "Pembatalan Tidak Bisa Dilakukan. Silahkan Batalkan Faktur Berikut Terlebih Dahulu : " & Chr(13)

                                    SQL = ";with Cte_Data as( "
                                    SQL = SQL & "select c.Kode_customer, a.No_Faktur, '' as No_DO_Pelunasan, a.Urut, a.jenis,"
                                    SQL = SQL & "isnull((select top(1) X.Plafon_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and "
                                    SQL = SQL & "X.Kode_Customer= c.Kode_Customer and status is null order by Urut_Customer desc),null) as Plafon_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.Member_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and "
                                    SQL = SQL & "X.Kode_Customer= c.Kode_Customer and status is null order by Urut_Customer desc),null) as Member_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.Kode_Kategori_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and "
                                    SQL = SQL & "X.Kode_Customer= c.Kode_Customer and status is null order by Urut_Customer desc),null) as Kode_Kategori_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.No_Urut from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and "
                                    SQL = SQL & "X.Kode_Customer= c.Kode_Customer and status is null order by Urut_Customer desc),null)  as Urut_Log "
                                    SQL = SQL & "from "
                                    SQL = SQL & "Member_Data_Masuk a, do_new b, Penjualan C where "
                                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.No_Faktur= b.No_DO and "
                                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan And b.No_Faktur = c.No_Faktur "
                                    SQL = SQL & "and a.Jenis ='DO' and b.Status is null and c.Kode_Customer ='" & .Rows(i).Item("kode_customer") & "' "

                                    SQL = SQL & "union all "

                                    SQL = SQL & "select E.Kode_customer, a.No_Faktur, a.No_DO_Pelunasan, a.Urut, a.jenis, "
                                    SQL = SQL & "isnull((select top(1) X.Plafon_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and X.No_DO_Pelunasan = "
                                    SQL = SQL & "a.No_DO_Pelunasan and X.Kode_Customer= e.Kode_Customer and status is null order by Urut_Customer desc),null) as Plafon_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.Member_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and X.No_DO_Pelunasan = "
                                    SQL = SQL & "a.No_DO_Pelunasan and X.Kode_Customer= e.Kode_Customer and status is null order by Urut_Customer desc),null) as Member_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.Kode_Kategori_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and X.No_DO_Pelunasan = "
                                    SQL = SQL & "a.No_DO_Pelunasan and X.Kode_Customer= e.Kode_Customer and status is null order by Urut_Customer desc),null) as Kode_Kategori_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.No_Urut from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and X.No_DO_Pelunasan = "
                                    SQL = SQL & "a.No_DO_Pelunasan and X.Kode_Customer= e.Kode_Customer and status is null order by Urut_Customer desc),null) as Urut_Log "
                                    SQL = SQL & "from "
                                    SQL = SQL & "Member_Data_Masuk a, Val_DO_Tunai b, Detail_Val_DO_Tunai c, Do_New d, Penjualan E  where "
                                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Val = c.No_Val and "
                                    SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur= c.No_Val and a.No_DO_Pelunasan = c.No_Faktur and "
                                    SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_DO and "
                                    SQL = SQL & "d.Kode_Perusahaan = e.Kode_Perusahaan And d.No_Faktur = e.No_Faktur "
                                    SQL = SQL & "and a.Jenis ='PELUNASAN TUNAI' and b.status is null and e.Kode_Customer ='" & .Rows(i).Item("kode_customer") & "' "

                                    SQL = SQL & "union all "

                                    SQL = SQL & "select E.Kode_customer, a.No_Faktur, a.No_DO_Pelunasan, a.Urut, a.jenis,"
                                    SQL = SQL & "isnull((select top(1) X.Plafon_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and X.No_DO_Pelunasan = "
                                    SQL = SQL & "a.No_DO_Pelunasan and X.Kode_Customer= e.Kode_Customer and status is null order by Urut_Customer desc),null) as Plafon_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.Member_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and X.No_DO_Pelunasan = "
                                    SQL = SQL & "a.No_DO_Pelunasan and X.Kode_Customer= e.Kode_Customer and status is null order by Urut_Customer desc),null) as Member_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.Kode_Kategori_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and X.No_DO_Pelunasan = "
                                    SQL = SQL & "a.No_DO_Pelunasan and X.Kode_Customer= e.Kode_Customer and status is null order by Urut_Customer desc),null) as Kode_Kategori_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.No_Urut from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and X.No_DO_Pelunasan = "
                                    SQL = SQL & "a.No_DO_Pelunasan and X.Kode_Customer= e.Kode_Customer and status is null order by Urut_Customer desc),null) as Urut_Log "
                                    SQL = SQL & "from "
                                    SQL = SQL & "Member_Data_Masuk a, Val_DO b, Detail_Val_DO c, Do_New d, Penjualan E  where "
                                    SQL = SQL & "b.Kode_Perusahaan = c.Kode_Perusahaan and b.No_Val = c.No_Val and "
                                    SQL = SQL & "a.Kode_Perusahaan = c.Kode_Perusahaan and a.No_Faktur= c.No_Val and a.No_DO_Pelunasan = c.No_Faktur and "
                                    SQL = SQL & "c.Kode_Perusahaan = d.Kode_Perusahaan and c.No_Faktur = d.No_DO and "
                                    SQL = SQL & "d.Kode_Perusahaan = e.Kode_Perusahaan And d.No_Faktur = e.No_Faktur "
                                    SQL = SQL & "and a.Jenis ='PELUNASAN KREDIT' and b.status is null and e.Kode_Customer ='" & .Rows(i).Item("kode_customer") & "' "

                                    SQL = SQL & "union all "

                                    SQL = SQL & "select b.Kode_customer, a.No_Faktur, '' as No_DO_Pelunasan, a.Urut, a.jenis, "
                                    SQL = SQL & "isnull((select top(1) X.Plafon_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and "
                                    SQL = SQL & "X.Kode_Customer= b.Kode_Customer and status is null order by Urut_Customer desc),null) as Plafon_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.Member_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and "
                                    SQL = SQL & "X.Kode_Customer= b.Kode_Customer and status is null order by Urut_Customer desc),null) as Member_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.Kode_Kategori_Sebelum from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and "
                                    SQL = SQL & "X.Kode_Customer= b.Kode_Customer and status is null order by Urut_Customer desc),null) as Kode_Kategori_Sebelum, "
                                    SQL = SQL & "isnull((select top(1) X.No_Urut from Member_Log_Check_All X where "
                                    SQL = SQL & "X.Kode_Perusahaan = a.Kode_Perusahaan and X.No_Faktur = a.No_Faktur and "
                                    SQL = SQL & "X.Kode_Customer= b.Kode_Customer and status is null order by Urut_Customer desc),null)  as Urut_Log "
                                    SQL = SQL & "from "
                                    SQL = SQL & "Member_Data_Masuk a, member_Adjustment_plafond b where "
                                    SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan And a.No_Faktur = b.No_Faktur "
                                    SQL = SQL & "and a.Jenis ='ADJUST PLAFON' and b.Status is null and b.Kode_Customer ='" & .Rows(i).Item("kode_customer") & "' "

                                    SQL = SQL & ") "
                                    SQL = SQL & "select * from cte_data where "
                                    SQL = SQL & "((Jenis <>'DO' and Member_Sebelum is not null) or (jenis = 'DO' and Member_Sebelum is null)) and Urut>=" & UrutDataMasuk & " order by Urut desc "
                                    Using Dr = OpenTrans(SQL)
                                        Do While Dr.Read
                                            If ind = 0 Then
                                                If Dr("Urut") <> UrutDataMasuk Then
                                                    cek_data = False
                                                End If
                                            End If

                                            If Dr("Urut") <> UrutDataMasuk Then
                                                If data_temp <> Dr("no_faktur") Then
                                                    Pesan = Pesan & Dr("Jenis") & " : " & Dr("No_Faktur") & Chr(13)
                                                End If
                                            End If

                                            data_temp = Dr("No_Faktur")
                                            ind += 1
                                        Loop
                                    End Using

                                    If cek_data = False Then
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show(Pesan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                                        Exit Sub
                                    Else
                                        SQL = "update customers set  "
                                        SQL = SQL & " Plafon ='" & PlafonSebelum & "', "
                                        SQL = SQL & " Kode_Member ='" & MemberSebelum & "', "
                                        SQL = SQL & " Kode_Kategori_Member ='" & KategoriSebelum & "'  "
                                        SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "kode_customer = '" & .Rows(i).Item("kode_customer") & "'"
                                        ExecuteTrans(SQL)

                                        SQL = "update member_log_check_all set  "
                                        SQL = SQL & " Status ='Y' "
                                        SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                                        SQL = SQL & "No_Urut = '" & UrutLog & "'"
                                        ExecuteTrans(SQL)
                                    End If

                                Else

                                    SQL = "select a.No_DO_Pelunasan,No_faktur, Jenis from "
                                    SQL = SQL & "Member_log_Check_All a where "
                                    SQL = SQL & "a.No_DO_Pelunasan ='" & .Rows(i).Item("no_do") & "' and status is null "
                                    Using Dr = OpenTrans(SQL)
                                        If Dr.Read Then
                                            sudah_lunas_ada_data = False

                                            If faktur_temp <> Dr("No_faktur") Then
                                                pesan_lunas = pesan_lunas & Dr("Jenis") & " : " & Dr("No_faktur") & Chr(13)
                                            End If
                                            faktur_temp = Dr("No_faktur")
                                        End If
                                    End Using

                                End If


                                SQL = "update do_new set flag_lunas_do = NULL, "
                                SQL = SQL & "sudah_dilunasi = sudah_dilunasi - " & (.Rows(i).Item("byr") + .Rows(i).Item("disc_cash")) & " where "
                                SQL = SQL & "no_do = '" & .Rows(i).Item("no_do") & "'  "
                                ExecuteTrans(SQL)

                                SQL = "Update penjualan set flag_lunas_tunai = NULL, "
                                SQL = SQL & "Tgl_lunas_tunai = NULL, "
                                SQL = SQL & "jam_lunas_tunai = NULL, "
                                SQL = SQL & "uservalidasi_tunai = NULL where kode_perusahaan = '" & KodePerusahaan & "' and "
                                SQL = SQL & "no_faktur = '" & .Rows(i).Item("no_faktur") & "'"
                                ExecuteTrans(SQL)




                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Pelunasan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                If sudah_lunas_ada_data = False Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show(pesan_lunas, Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If

                'ini untuk Cek Do yg diturunkan

                Dim no_do_cek As String = ""
                Dim Cek_Batal As Boolean = True
                SQL = "select Kode_Customer, No_Faktur from member_log_check_all where "
                SQL = SQL & "No_Faktur ='" & ListView1.FocusedItem.Text & "' "
                SQL = SQL & "group by Kode_Customer, No_Faktur "
                Using Ds2 = BindingTrans(SQL)
                    If Ds2.Tables("MyTable").Rows.Count <> 0 Then
                        For J As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                            SQL = "select top(1) Urut_Customer from member_log_check_all where "
                            SQL = SQL & "No_Faktur ='" & ListView1.FocusedItem.Text & "' and "
                            SQL = SQL & "Kode_Customer ='" & Ds2.Tables("MyTable").Rows(J).Item("Kode_Customer") & "' "
                            SQL = SQL & "order by Urut_Customer desc "
                            Using Ds3 = BindingTrans(SQL)
                                For K As Integer = 0 To Ds3.Tables("MyTable").Rows.Count - 1

                                    SQL = "select No_Faktur, Jenis, No_Urut from member_log_check_all where "
                                    SQL = SQL & "Kode_Customer ='" & Ds2.Tables("MyTable").Rows(J).Item("Kode_Customer") & "' and "
                                    SQL = SQL & "Urut_Customer >" & Ds3.Tables("MyTable").Rows(K).Item("Urut_Customer") & " "
                                    SQL = SQL & "and Jenis ='DO' and status is null order by Urut_Customer desc "
                                    Using Ds4 = BindingTrans(SQL)
                                        For L As Integer = 0 To Ds4.Tables("MyTable").Rows.Count - 1

                                            Cek_Batal = False
                                            SQL = "update member_log_check_all set  "
                                            SQL = SQL & " Status ='Y' "
                                            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "No_Urut = '" & Ds4.Tables("MyTable").Rows(L).Item("No_Urut") & "'"
                                            ExecuteTrans(SQL)

                                            SQL = "update Do_New set  "
                                            SQL = SQL & " Cek_Member= Cek_Member-1 "
                                            SQL = SQL & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                                            SQL = SQL & "No_DO = '" & Ds4.Tables("MyTable").Rows(L).Item("No_Faktur") & "'"
                                            ExecuteTrans(SQL)

                                            If L <> 0 Or J <> 0 Then
                                                no_do_cek = no_do_cek & ", "

                                            End If
                                            no_do_cek = no_do_cek & "'" & Ds4.Tables("MyTable").Rows(L).Item("No_Faktur") & "'"

                                        Next
                                    End Using
                                Next
                            End Using
                        Next
                        'Else
                        '    CloseTrans()
                        '    CloseConn()
                        '    MessageBox.Show("Pelunasan tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                    End If
                End Using

                If Cek_Batal = False Then
                    'cek member ulang
                    Dim Plafond_Kode As New ArrayList
                    Dim Plafond_Dari As New ArrayList
                    Dim Plafond_Sampai As New ArrayList
                    Dim Plafond_Persen As New ArrayList
                    Dim Plafon_Blacklist As New ArrayList

                    Dim Persen_Penurunan_Nol As Integer = 0

                    SQL = "select Kode_Plafond, Hari_Dari, Hari_Sampai, Persen_Penurunan, flag_blacklist "
                    SQL = SQL & "from member_penurunan_plafond order by Kode_Plafond "
                    Using Dr = OpenTrans(SQL)
                        Do While Dr.Read
                            If Dr("Persen_Penurunan") = 0 Then
                                Persen_Penurunan_Nol = Dr("Hari_Dari")
                            End If
                            Plafon_Blacklist.Add(Dr("flag_blacklist"))
                            Plafond_Kode.Add(Dr("Kode_Plafond"))
                            Plafond_Dari.Add(Dr("Hari_Dari"))
                            Plafond_Sampai.Add(Dr("Hari_Sampai"))
                            Plafond_Persen.Add(Dr("Persen_Penurunan"))
                        Loop
                    End Using

                    SQL = "select a.No_DO, a.tanggal_DO, datediff(day,tanggal_DO,'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "') as hari,"
                    SQL = SQL & " a.Total_baru, a.Kode_Customer, b.cek_Member "
                    SQL = SQL & "from Rekap_Sub_Invoice a, do_new b where "
                    SQL = SQL & "a.kode_perusahaan = b.Kode_Perusahaan And a.no_do = b.No_DO and "
                    SQL = SQL & "(a.total_baru_dikurang_diskon + a.nilai_ppn_baru) - (a.retur_baru_dikurang_diskon + a.nilai_ppn_retur_baru) - "
                    SQL = SQL & "(a.retur_baru_beda_bulan_dikurang_diskon + a.nilai_ppn_retur_baru_beda_bulan) - a.sudah_dilunasi > 0 "
                    SQL = SQL & "and a.flag_lunas_do is null and datediff(day,tanggal_DO,'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "') >= " & Persen_Penurunan_Nol & " and a.no_do in (" & no_do_cek & ")"
                    SQL = SQL & "order by b.id"
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            For index As Integer = 0 To .Rows.Count - 1
                                Dim cek_member As Integer = 0
                                If General_Class.CekNULL(.Rows(index).Item("cek_Member")) = "" Then
                                    cek_member = 0
                                Else
                                    cek_member = .Rows(index).Item("cek_Member")
                                End If

                                If .Rows(index).Item("hari") >= Persen_Penurunan_Nol Then

                                    For index2 As Integer = 0 To Plafond_Kode.Count - 1


                                        If .Rows(index).Item("hari") >= Plafond_Dari.Item(index2) And
                                        .Rows(index).Item("hari") <= Plafond_Sampai.Item(index2) And
                                        cek_member <> Plafond_Kode.Item(index2) Then

                                            Dim plafon As Double = 0
                                            Dim Pengurangan_Plafon As Double = 0
                                            Dim Number_Member As Integer = 0
                                            Dim Plafon_Sebelum As Double = 0
                                            Dim MemberSebelum As String = ""
                                            Dim KategoriMemberSebelum As String = ""

                                            SQL = "select Plafon, Number_Member, Kode_Member, Kode_Kategori_Member  from customers "
                                            SQL = SQL & "where Kode_customer = '" & .Rows(index).Item("Kode_Customer") & "'"
                                            Using Dr = OpenTrans(SQL)
                                                If Dr.Read Then
                                                    plafon = Dr("Plafon") ' + Dr("plafon_awal")
                                                    Plafon_Sebelum = Dr("Plafon")
                                                    Number_Member = Dr("Number_Member")
                                                    MemberSebelum = Dr("Kode_Member")
                                                    KategoriMemberSebelum = Dr("Kode_Kategori_Member")
                                                End If
                                            End Using

                                            Pengurangan_Plafon = HilangkanTanda(Format((plafon * Plafond_Persen.Item(index2)) / 100, "N0"))

                                            plafon = plafon - Pengurangan_Plafon


                                            SQL = "insert into member_log_check_per_do "
                                            SQL = SQL & "(Kode_Perusahaan, Kode_Customer, No_Do, Kode_Check, Tanggal_Check, Jam_Check, Urut_Customer) "
                                            SQL = SQL & " Values('" & KodePerusahaan & "', '" & .Rows(index).Item("Kode_Customer") & "', '" & .Rows(index).Item("No_DO") & "', '" & Plafond_Kode.Item(index2) & "', "
                                            SQL = SQL & "'" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', '" & Number_Member & "') "
                                            ExecuteTrans(SQL)



                                            SQL = "Update Do_New set Cek_Member = '" & Plafond_Kode.Item(index2) & "' "
                                            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and No_DO ='" & .Rows(index).Item("No_DO") & "' "
                                            ExecuteTrans(SQL)

                                            If Plafon_Blacklist.Item(index2) = "Y" Then
                                                SQL = "Update customers set blacklist = 'Y' "
                                                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_customer ='" & .Rows(index).Item("Kode_Customer") & "' "
                                                ExecuteTrans(SQL)
                                            End If

                                            SQL = "select Kode_Member, Kode_Kategori, Plafond_Dari, "
                                            SQL = SQL & "Plafond_Sampai, Hari_Dari, Hari_Sampai, Urutan from Member "
                                            SQL = SQL & "order by Urutan "
                                            Using Ds2 = BindingTrans(SQL)
                                                For index3 As Integer = 0 To Ds2.Tables("MyTable").Rows.Count - 1

                                                    If plafon >= Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Dari") And
                                                    plafon <= Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Sampai") And
                                                    .Rows(index).Item("hari") >= Ds2.Tables("MyTable").Rows(index3).Item("Hari_Dari") And
                                                    .Rows(index).Item("hari") <= Ds2.Tables("MyTable").Rows(index3).Item("Hari_Sampai") Then


                                                        SQL = "insert into member_log_check_all "
                                                        SQL = SQL & "(Kode_Perusahaan, Tanggal, Jam, Plafond_Tambahan, Kode_Customer, Nilai_Plafond, Kode_Member, "
                                                        SQL = SQL & "Kode_Kategori, Plafond_Dari, Plafond_Sampai, Hari_Dari, Hari_Sampai, Urutan_Member, No_Faktur, Jenis, Total_Plafond, Plafon_Sebelum, Member_Sebelum, Kode_Kategori_Sebelum, Urut_Customer) "
                                                        SQL = SQL & "Values('" & KodePerusahaan & "', '" & Format(Tanggal_Sekarang, "yyyy-MM-dd") & "', '" & Format(Tanggal_Sekarang, "HH:mm:ss") & "', "
                                                        SQL = SQL & "'" & (-1 * Pengurangan_Plafon) & "', '" & .Rows(index).Item("Kode_Customer") & "', '" & (-1 * Plafond_Persen.Item(index2)) & "', "
                                                        SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Member") & "', '" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Kategori") & "', "
                                                        SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Dari") & "', '" & Ds2.Tables("MyTable").Rows(index3).Item("Plafond_Sampai") & "', "
                                                        SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Hari_Dari") & "', '" & Ds2.Tables("MyTable").Rows(index3).Item("Hari_Sampai") & "', "
                                                        SQL = SQL & "'" & Ds2.Tables("MyTable").Rows(index3).Item("Urutan") & "', '" & .Rows(index).Item("No_DO") & "', 'DO','" & plafon & "', "
                                                        SQL = SQL & "'" & Plafon_Sebelum & "', '" & MemberSebelum & "', '" & KategoriMemberSebelum & "','" & Number_Member & "') "
                                                        ExecuteTrans(SQL)

                                                        Number_Member += 1

                                                        SQL = "Update Customers set Kode_Member ='" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Member") & "', "
                                                        SQL = SQL & "Kode_Kategori_Member = '" & Ds2.Tables("MyTable").Rows(index3).Item("Kode_Kategori") & "', "
                                                        SQL = SQL & "Plafon = '" & plafon & "', Number_Member = '" & Number_Member & "' "
                                                        SQL = SQL & "where Kode_customer ='" & .Rows(index).Item("Kode_Customer") & "' "
                                                        ExecuteTrans(SQL)
                                                        Exit For
                                                    End If

                                                Next
                                            End Using

                                            Exit For

                                        End If

                                    Next
                                End If


                            Next

                        End With

                    End Using
                End If

                Dim kodeunik As String = ""
                SQL = "select kode_unik from val_do_tunai where kode_perusahaan = '" & KodePerusahaan & "' and no_val = '" & ListView1.FocusedItem.Text & "' "
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            If General_Class.CekNULL(.Rows(0).Item("kode_unik")) = "" Then
                                CloseTrans()
                                CloseConn()
                                MessageBox.Show("val do tidak bisa diupdate!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                Exit Sub
                            Else
                                kodeunik = .Rows(0).Item("kode_unik")
                            End If
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("no val tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "update val_do_tunai set status = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "no_val = '" & ListView1.FocusedItem.Text & "'"
                ExecuteTrans(SQL)

                SQL = "select a.no_val, c.kode_customer, a.jumlah from det_do_um a, do_new b, penjualan c where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
                SQL = SQL & "a.no_do = b.no_do and b.no_faktur = c.no_faktur and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "a.no_pelunasan = '" & ListView1.FocusedItem.Text & "'"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                SQL = "update um set sisa = sisa + " & .Rows(i).Item("jumlah") & " where "
                                SQL = SQL & "no_val = '" & .Rows(i).Item("no_val") & "' and "
                                SQL = SQL & "kode_customer = '" & .Rows(i).Item("kode_customer") & "'"
                                ExecuteTrans(SQL)
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi um tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "select no_val,urut_um,no_do,urut,nilai_yang_diInput from Perlunasan_Per_Step_sementara "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_unik = '" & kodeunik & "' order by urut desc"
                Using Ds = BindingTrans(SQL)
                    With Ds.Tables("MyTable")
                        If .Rows.Count <> 0 Then
                            For i As Integer = 0 To .Rows.Count - 1
                                SQL = "Select no_val,sisa,urut from um_global where no_val '" & .Rows(i).Item("no_val") & "' "
                                SQL = SQL & "and urut = '" & .Rows(i).Item("urut_um") & "' "
                                With Ds.Tables("MyTable")
                                    If .Rows.Count <> 0 Then
                                        SQL = "update um_global set sisa = sisa + '" & .Rows(i).Item("nilai_yang_diInput") & "' "
                                        SQL = SQL & "where no_val = '" & .Rows(i).Item("no_val") & "' and "
                                        SQL = SQL & "urut = '" & .Rows(i).Item("urut_um") & "'"
                                        ExecuteTrans(SQL)
                                    Else
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show("Transaksi um_global tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End With
                            Next
                        Else
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show("Transaksi um_global tidak ada!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End With
                End Using

                SQL = "delete from jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_voucher = '" & kd_vcr & "'"
                ExecuteTrans(SQL)

                SQL = "insert into log_val_do_tunai(kode_perusahaan, no_faktur, tanggal, jam, userid, jenis) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & ListView1.FocusedItem.Text & "', "
                SQL = SQL & "'" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "', "
                SQL = SQL & "'" & Format(CDate(FMenuDevFix.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
                SQL = SQL & "'" & UserID & "', 'B')"
                ExecuteTrans(SQL)


                Cmd.Transaction.Commit()

                CloseConn()

                MessageBox.Show("Transaksi berhasil dibatalkan.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
                ListView1.FocusedItem.ForeColor = Batal
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.Items.Count = 0 Then Exit Sub

        Try

            If pertama = 1 Then

                OpenConn()

                Dim Grand As Double = 0
                ListView2.Items.Clear() : TextBoxa.Text = "0"

                SQL = "select d.disc_cash, e.no_do, e.no_faktur, a.tanggal, a.kode_customer, b.nama, d.byr "
                SQL = SQL & "from penjualan a, customers b, val_do_tunai c, detail_val_do_tunai d, do_new e where "
                SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and b.kode_perusahaan = c.kode_perusahaan and "
                SQL = SQL & "c.kode_perusahaan = d.kode_perusahaan and d.kode_perusahaan = e.kode_perusahaan and a.kode_customer = b.kode_customer and "
                SQL = SQL & "c.no_val = d.no_val and a.no_faktur = e.no_faktur and d.no_faktur = e.no_do and "
                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "c.no_val = '" & ListView1.FocusedItem.Text & "' order by d.urut"
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem

                        Lv = ListView2.Items.Add(Dr("no_do"))
                        Lv.SubItems.Add(Dr("no_faktur"))
                        Lv.SubItems.Add(Format(Dr("tanggal"), "dd MMM yyyy"))
                        Lv.SubItems.Add(Dr("kode_customer"))
                        Lv.SubItems.Add(Dr("nama"))
                        Lv.SubItems.Add(Format(Dr("byr"), "N0"))
                        Lv.SubItems.Add(Format(Dr("disc_cash"), "N0"))
                    Loop
                End Using

                CloseConn()

                pertama = 0
            Else
                pertama = 1
            End If
        Catch ex As Exception
            pertama = 1
            CloseConn()
            MessageBox.Show(ex.Message)
            ListView2.Items.Clear()
            Exit Sub
        End Try

        Hitung()
    End Sub

    Private Sub Display_Data_Penjualan_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
        Label1.Size = New Point(Me.Width, 33)
    End Sub

    Private Sub CopyNoFakturToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyNoFakturToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no pelunasan yang mau copy!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Clipboard.SetText(ListView1.FocusedItem.Text)
    End Sub

    Private Sub CetakUlangToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CetakUlangToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show("Pilih dahulu no pelunasan yang mau cetak ulang!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        cetak()
    End Sub

    '==============================================================================================================================
    '= UTILITY
    '==============================================================================================================================

    Protected Overrides Sub WndProc(ByRef m As Message)
        ' WM_NCLBUTTONDBLCLK = 0xA3 (double click di title bar)
        If m.Msg = &HA3 Then
            Return  ' Abaikan pesan, sehingga form tidak maximize
        End If

        MyBase.WndProc(m)
    End Sub
End Class