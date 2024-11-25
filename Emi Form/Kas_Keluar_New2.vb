'Public Class Kas_Keluar_New2
'    Dim KJ, KdSetting, Ket, SQL As String
'    Dim TampungVoucher As String
'    Dim Debit, Kredit As Double
'    Dim Ada, ByPass As Boolean
'    Dim Pisah() As String
'    Dim PisahProyek() As String
'    Dim Page As Integer
'    Dim Pembeda As String
'    Dim arrInisialFaktur As New ArrayList

'    Private Sub Kas_Keluar_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
'        My.Application.ChangeCulture("id-id")
'        My.Application.ChangeUICulture("id-id")
'    End Sub

'    Private Sub Kas_Keluar_New_Deactivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate

'    End Sub

'    Private Sub Kas_Masuk_New_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
'        My.Application.ChangeCulture("id-id")
'        My.Application.ChangeUICulture("id-id")

'        Dim x As New Point(10, 188)
'        GroupBox5.Location = x

'        ListView1.Columns.Add("Kode Acc", 80, HorizontalAlignment.Center)
'        ListView1.Columns.Add("Nama Perkiraan", 200) '1
'        ListView1.Columns.Add("Keterangan", 200) '2
'        ListView1.Columns.Add("Debit", 120, HorizontalAlignment.Right) '3
'        ListView1.Columns.Add("Kredit", 120, HorizontalAlignment.Right) '4
'        ListView1.Columns.Add("Pakai Budget", 0, HorizontalAlignment.Right) '5
'        ListView1.Columns.Add("Lama Budget Harian", 0, HorizontalAlignment.Right) '6
'        ListView1.Columns.Add("Lama Budget Bulanan", 0, HorizontalAlignment.Right) '7
'        ListView1.Columns.Add("Budget Harian", 0, HorizontalAlignment.Right) '8
'        ListView1.Columns.Add("Budget Bulanan", 0, HorizontalAlignment.Right) '9
'        ListView1.Columns.Add("Kena Harian", 0, HorizontalAlignment.Right) '10
'        ListView1.Columns.Add("Kena Bulanan", 0, HorizontalAlignment.Right) '11
'        ListView1.Columns.Add("Terpakai Harian", 0, HorizontalAlignment.Right) '12
'        ListView1.Columns.Add("Terpakai Bulanan", 0, HorizontalAlignment.Right) '13
'        ListView1.View = View.Details

'        ListView2.Columns.Add("Kode Account", 150, HorizontalAlignment.Center)
'        ListView2.Columns.Add("Keterangan", 400)
'        ListView2.Columns.Add("Posisi", 130, HorizontalAlignment.Center)
'        ListView2.View = View.Details

'        ComboBox2.Items.Clear()
'        ComboBox2.Items.Add("Kas Masuk")
'        ComboBox2.Items.Add("Kas Keluar")
'        ComboBox2.Items.Add("Bukti Bank Masuk")
'        ComboBox2.Items.Add("Bukti Bank Keluar")
'        ComboBox2.Items.Add("Jurnal")
'        ComboBox2.SelectedIndex = 1

'        ComboBox1.Items.Clear()
'        ComboBox1.Items.Add("Debit")
'        ComboBox1.Items.Add("Kredit")
'        ComboBox1.SelectedIndex = 0

'        ComboBox3.Items.Clear()
'        ComboBox3.Items.Add("Kode Account")
'        ComboBox3.Items.Add("Keterangan")

'        ' If LevelUser = "1" Then DateTimePicker1.Enabled = True Else DateTimePicker1.Enabled = False

'        Kosong()
'    End Sub

'    Private Sub Kosong()
'        GroupBox5.Visible = False : DateTimePicker1.Value = CDate(FMenu.ToolStripStatusLabel3.Text)

'        Button1.Text = "&Simpan"
'        ComboBox2.SelectedIndex = 1 : ComboBox1.SelectedIndex = -1

'        ByPass = False : TampungVoucher = ""

'        TextBox2.Text = ""
'        TextBox1.Text = "" : TextBox3.Text = "" : TextBox4.Text = ""
'        TextBox5.Text = "" : TextBox6.Text = "" : TextBox7.Text = ""
'        ListView1.Items.Clear()

'        Debit = 0 : Kredit = 0
'        TextBox3.Enabled = True

'        TextBox11.Text = ""
'        TextBox11.Enabled = True

'        Try
'            OpenConn()

'            ComboBox6.Items.Clear() : arrInisialFaktur.Clear()

'            SQL = "Select kode_stock_owner, flag_default, flag_reseller, kategori_pengganti_reseller, inisial_faktur From "
'            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
'            Using dr = OpenTrans(SQL)
'                Do While dr.Read
'                    ComboBox6.Items.Add(dr("kode_stock_owner"))
'                    arrInisialFaktur.Add(dr("inisial_faktur"))
'                Loop
'            End Using

'            ComboBox6.Text = Lokasi

'            TextBox10.Text = ""

'            SQL = "Select kas_biaya From "
'            SQL = SQL & "stock_owner where kode_perusahaan = '" & KodePerusahaan & "' and "
'            SQL = SQL & "kode_stock_owner = '" & ComboBox6.Text & "'"
'            Using dr = OpenTrans(SQL)
'                If dr.Read Then
'                    TextBox10.Text = dr("kas_biaya")
'                Else
'                    TextBox10.Text = ""
'                End If
'            End Using

'            CloseConn()
'        Catch ex As Exception
'            CloseConn()
'            MessageBox.Show(ex.Message)
'            Exit Sub
'        End Try

'        ComboBox5.Items.Clear() : ComboBox5.SelectedIndex = -1 : ComboBox5.Text = ""
'        Using Dr As SqlClient.SqlDataReader = General_Class.Open("select * from proyek where kode_perusahaan = '" & KodePerusahaan & "' order by kode_perusahaan,kode_proyek")
'            Do While Dr.Read
'                ComboBox5.Items.Add(Dr("kode_Proyek") & " - " & Dr("keterangan"))
'            Loop
'        End Using

'        'If ComboBox2.SelectedIndex = 4 Or ComboBox2.SelectedIndex = -1 Then 'Jurnal Umum
'        '    SQL = "select (kode_master_acc + kode_acc + kode_detail_acc) as KdAcc,* from detail_account where kode_perusahaan = '" & KodePerusahaan & "' and right(kode_detail_acc,3) <> '000' order by kode_master_acc,kode_acc,kode_detail_acc"
'        'ElseIf ComboBox2.SelectedIndex = 0 Or ComboBox2.SelectedIndex = 2 Then 'Kas/Bank Masuk
'        '    SQL = "select (kode_master_acc + kode_acc + kode_detail_acc) as KdAcc,* from detail_account where kode_perusahaan = '" & KodePerusahaan & "' and right(kode_detail_acc,3) <> '000' and posisi = 'K' order by kode_master_acc,kode_acc,kode_detail_acc"
'        'ElseIf ComboBox2.SelectedIndex = 1 Or ComboBox2.SelectedIndex = 3 Then 'Kas/Bank Keluar
'        '    SQL = "select (kode_master_acc + kode_acc + kode_detail_acc) as KdAcc,* from detail_account where kode_perusahaan = '" & KodePerusahaan & "' and right(kode_detail_acc,3) <> '000' and posisi = 'D' order by kode_master_acc,kode_acc,kode_detail_acc"
'        'End If

'        'ListView2.Items.Clear()
'        'Using Dr As SqlClient.SqlDataReader = General_Class.Open(SQL)
'        '    Do While Dr.Read
'        '        Dim Lvw As ListViewItem
'        '        Lvw = ListView2.Items.Add(Strings.Left(Dr("kdacc"), 3) & "." & Strings.Mid(Dr("kdacc"), 4))
'        '        Lvw.SubItems.Add(Dr("Keterangan"))
'        '        Lvw.SubItems.Add(Dr("posisi"))
'        '    Loop
'        'End Using

'        ComboBox5.SelectedIndex = 0
'    End Sub

'    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
'        If e.KeyChar = Chr(13) Then
'            'If LevelUser = "1" Then
'            DateTimePicker1.Focus()
'            'Else
'            '   ComboBox5.Focus()
'            'End If
'        End If
'    End Sub

'    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
'        If e.KeyChar = Chr(13) Then
'            Select Case ComboBox2.SelectedIndex
'                Case 0 To 3
'                    TextBox11.Focus()
'                Case Else
'                    TextBox3.Focus()
'            End Select
'        End If
'        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
'    End Sub

'    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
'        If e.KeyChar = Chr(13) Then
'            TextBox5.Focus()
'        End If
'        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
'    End Sub

'    Private Sub TextBox3_Invalidated(ByVal sender As Object, ByVal e As System.Windows.Forms.InvalidateEventArgs) Handles TextBox3.Invalidated

'    End Sub

'    Private Sub TextBox3_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox3.KeyDown
'        If e.KeyCode = Keys.F1 Then
'            GroupBox5.Visible = True
'            ComboBox3.SelectedIndex = 1 : TextBox8.Focus()
'            TextBox8.Focus()
'        End If
'    End Sub

'    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
'        If e.KeyChar = Chr(13) Then TextBox1.Focus()
'        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
'    End Sub

'    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
'        If e.KeyChar = Chr(13) Then TextBox5.Focus()
'    End Sub

'    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
'        If Button1.Text = "&Update" Then Exit Sub 'ByPass = True
'        If ByPass Then Exit Sub

'        'ComboBox1.Enabled = False
'        Select Case ComboBox2.SelectedIndex
'            Case 0 'Kas Masuk
'                KJ = "KM" : KdSetting = "Kas_Masuk" : Ket = "Kas masuk"
'            Case 1 'Kas Keluar
'                KJ = "KK" : KdSetting = "Kas_Keluar" : Ket = "Kas keluar"
'            Case 2 'Bukti Bank Masuk
'                KJ = "BM" : KdSetting = "Bank_Masuk" : Ket = "Bank masuk"
'            Case 3 'Bukti Bank Keluar
'                KJ = "BK" : KdSetting = "Bank_Keluar" : Ket = "Bank keluar"
'            Case 4 'Jurnal
'                KJ = "JE" : KdSetting = "" : Ket = "Jurnal"
'                'ComboBox1.Enabled = True
'        End Select

'        Ada = False
'        TextBox2.Text = ""
'        ComboBox5.SelectedIndex = -1 : ComboBox5.Text = ""
'    End Sub

'    Private Sub TextBox3_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.Leave
'        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

'        Try

'            OpenConn()

'            Using Dr = OpenTrans("select * from detail_account where kode_perusahaan = '" & KodePerusahaan & "' and kode_account = '" & TextBox3.Text & "'")
'                If Dr.Read Then
'                    TextBox4.Text = Dr("keterangan")
'                    If Dr("posisi") = "D" Then ComboBox1.SelectedIndex = 0 Else ComboBox1.SelectedIndex = 1

'                    If Strings.Right(Dr("kode_account"), 3) = "000" Then
'                        MessageBox.Show("Account induk tidak dapat dientry jurnal . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'                        ComboBox1.SelectedIndex = -1
'                        TextBox3.Text = "" : TextBox4.Text = "" : TextBox3.Focus()
'                        Exit Sub
'                    End If

'                    If ComboBox2.SelectedIndex = 0 Or ComboBox2.SelectedIndex = 2 Then 'Kas/Bank Masuk
'                        If Dr("posisi") = "D" Then
'                            MessageBox.Show("Account ini tidak dapat dientry di jurnal kas/bank masuk . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'                            ComboBox1.SelectedIndex = -1
'                            TextBox3.Text = "" : TextBox4.Text = "" : TextBox3.Focus()
'                            Exit Sub
'                        End If
'                    ElseIf ComboBox2.SelectedIndex = 1 Or ComboBox2.SelectedIndex = 3 Then 'Kas/Bank Keluar
'                        If Dr("posisi") = "K" Then
'                            MessageBox.Show("Account ini tidak dapat dientry di jurnal kas/bank keluar . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'                            ComboBox1.SelectedIndex = -1
'                            TextBox3.Text = "" : TextBox4.Text = "" : TextBox3.Focus()
'                            Exit Sub
'                        End If
'                    End If

'                Else
'                    Dr.Close()
'                    CloseConn()
'                    MessageBox.Show("Kode account tidak ditemukan . . ", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'                    TextBox3.Text = "" : TextBox4.Text = ""
'                    TextBox3.Focus()
'                    Exit Sub
'                End If
'            End Using


'            'For i As Integer = 0 To ListView1.Items.Count - 1
'            '    If TextBox3.Text = Trim(Str(ganti(ListView1.Items(i).Text))) Then
'            '        MessageBox.Show("Kode account sudah anda masukkan . . ", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'            '        TextBox3.Text = "" : TextBox4.Text = "" : TextBox3.Focus()
'            '        Exit Sub
'            '    End If
'            'Next


'        Catch ex As Exception

'        End Try
'    End Sub

'    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
'        Kosong()
'        ComboBox2.Focus()
'    End Sub

'    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
'        Me.Close()
'    End Sub

'    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
'        If TextBox3.Text.Trim.Length = 0 Then
'            MessageBox.Show("Kode account harus diisi . . ", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'            TextBox3.Focus() : Exit Sub
'        ElseIf ComboBox1.Text.Trim.Length = 0 Then
'            MessageBox.Show("Debit / kredit harus diisi . . ", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'            ComboBox1.Focus() : Exit Sub
'        End If

'        If e.KeyChar = Chr(13) Then
'            Dim Lvw As ListViewItem
'            Lvw = ListView1.Items.Add(TextBox3.Text.Trim)
'            Lvw.SubItems.Add(TextBox4.Text)
'            Lvw.SubItems.Add(TextBox1.Text)
'            If ComboBox1.SelectedIndex = 0 Then 'Debit
'                Lvw.SubItems.Add(Format(Val(TextBox5.Text), "N0"))
'                Lvw.SubItems.Add("0")
'                'Debit = Debit + ganti(TextBox5.Text)
'            Else 'Kredit
'                Lvw.SubItems.Add("0")
'                Lvw.SubItems.Add(Format(Val(TextBox5.Text), "N0"))
'                'Kredit = Kredit + ganti(TextBox5.Text)
'            End If
'            Lvw.SubItems.Add("T")
'            Lvw.SubItems.Add("0")
'            Lvw.SubItems.Add("0")
'            Lvw.SubItems.Add("0")
'            Lvw.SubItems.Add("0")
'            Lvw.SubItems.Add("T")
'            Lvw.SubItems.Add("T")
'            Lvw.SubItems.Add("0")
'            Lvw.SubItems.Add("0")

'            TextBox3.Text = "" : TextBox4.Text = "" : ComboBox1.SelectedIndex = -1 : TextBox5.Text = ""
'            TextBox1.Text = ""

'            Debit = 0 : Kredit = 0

'            For i As Integer = 0 To ListView1.Items.Count - 1
'                Debit = Debit + ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text)
'                Kredit = Kredit + ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text)
'            Next

'            If ComboBox2.SelectedIndex = 0 Or ComboBox2.SelectedIndex = 2 Then 'Kas Masuk dan Bank Masuk
'                'TextBox6.Text = Format(Debit, "N0")
'                TextBox7.Text = Format(Kredit - Debit, "N0")
'            ElseIf ComboBox2.SelectedIndex = 1 Or ComboBox2.SelectedIndex = 3 Then 'Kas Keluar dan Bank Keluar
'                'TextBox6.Text = Format(Debit, "N0")
'                TextBox7.Text = Format(Debit - Kredit, "N0")
'            Else
'                TextBox6.Text = Format(Debit, "N0")
'                TextBox7.Text = Format(Kredit, "N0")
'            End If

'            Dim Tanya As String = MessageBox.Show("Input kode account lain . . ? ?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
'            If Tanya = vbYes Then
'                TextBox3.Enabled = True
'                TextBox3.Focus()
'            Else
'                Button1.Focus()
'            End If
'            'End If
'        End If
'        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
'    End Sub

'    Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox3.KeyPress
'        If e.KeyChar = Chr(13) Then TextBox8.Focus()
'    End Sub

'    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
'        If ComboBox3.SelectedIndex = -1 Then Exit Sub
'        If TextBox8.Text.Trim.Length = 0 Then Exit Sub

'        Dim KetPosisi As String = ""
'        If ComboBox2.SelectedIndex = 4 Or ComboBox2.SelectedIndex = -1 Then 'Jurnal Umum
'            KetPosisi = ""
'        ElseIf ComboBox2.SelectedIndex = 0 Or ComboBox2.SelectedIndex = 2 Then 'Kas/Bank Masuk
'            KetPosisi = "" '"and posisi = 'K' "
'        ElseIf ComboBox2.SelectedIndex = 1 Or ComboBox2.SelectedIndex = 3 Then 'Kas/Bank Keluar
'            KetPosisi = "" '"and posisi = 'D' "
'        End If

'        Try
'            OpenConn()

'            If ComboBox3.SelectedIndex = 0 Then 'Kode Account
'                SQL = "select (a.kode_account) as KdAcc,* from detail_account a where "
'                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and right(a.kode_account,3) <> '000' and "
'                SQL = SQL & "a.flag_biaya = 'Y' and "
'                SQL = SQL & "a.kode_account like '%" & TextBox8.Text & "%'" & KetPosisi & " "
'                SQL = SQL & "order by a.kode_account"
'                'and lokasi = '" & Lokasi & "' 
'            Else
'                SQL = "select (a.kode_account) as KdAcc,* from detail_account a where "
'                SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and right(a.kode_account,3) <> '000' and "
'                SQL = SQL & "a.flag_biaya = 'Y' and "
'                SQL = SQL & "a.keterangan like '%" & TextBox8.Text & "%'" & KetPosisi & " "
'                SQL = SQL & "order by a.keterangan"
'                'and lokasi = '" & Lokasi & "' 
'            End If

'            ListView2.Items.Clear()
'            Using Dr = OpenTrans(SQL)
'                Do While Dr.Read
'                    Dim Lvw As ListViewItem
'                    Lvw = ListView2.Items.Add(Dr("kdacc"))
'                    Lvw.SubItems.Add(Dr("Keterangan"))
'                    Lvw.SubItems.Add(Dr("posisi"))
'                Loop
'            End Using

'            CloseConn()
'        Catch ex As Exception
'            CloseConn()
'            MessageBox.Show(ex.Message)
'            Exit Sub
'        End Try

'    End Sub

'    Private Sub TextBox8_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox8.KeyDown
'        If e.KeyCode = Keys.Up Then
'            ListView2.Focus()
'        End If
'    End Sub

'    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
'        If e.KeyChar = Chr(13) Then Button5_Click(TextBox8, e)
'    End Sub

'    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
'        GroupBox5.Visible = False : TextBox3.Focus()
'    End Sub

'    Private Sub ListView2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView2.DoubleClick
'        If ListView2.Items.Count = 0 Then Exit Sub

'        TextBox3.Text = Ganti(ListView2.FocusedItem.Text)
'        TextBox4.Text = ListView2.FocusedItem.SubItems(1).Text
'        If ListView2.FocusedItem.SubItems(2).Text = "D" Then
'            ComboBox1.SelectedIndex = 0
'        Else
'            ComboBox1.SelectedIndex = 1
'        End If
'        TextBox1.Focus()
'        GroupBox5.Visible = False
'    End Sub

'    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
'        Dim Except As Boolean = False
'        Dim ErrMessage As String = ""


'        If ComboBox2.SelectedIndex = -1 Then
'            MessageBox.Show("Jenis transaksi harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'            ComboBox2.Focus() : Exit Sub
'        ElseIf ComboBox5.SelectedIndex = -1 Then
'            MessageBox.Show("Kode proyek harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'            ComboBox5.Focus() : Exit Sub
'        ElseIf TextBox2.Text.Trim.Length = 0 Then
'            MessageBox.Show("Nomor voucher harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'            TextBox2.Focus() : Exit Sub
'        ElseIf ComboBox2.SelectedIndex = 4 Then
'            If TextBox6.Text <> TextBox7.Text Then
'                MessageBox.Show("Transaksi tidak balance . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'                TextBox3.Focus() : Exit Sub
'            End If
'        End If


'        If ListView1.Items.Count = 0 Then
'            MessageBox.Show("Jurnal belum anda input . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'            TextBox3.Focus() : Exit Sub
'        ElseIf TextBox11.Text.Trim.Length = 0 Then
'            MessageBox.Show("Keterangan harus diisi . . ! !", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'            TextBox11.Focus() : Exit Sub
'        End If

'        Dim a, b As Double
'        Dim Selisih As Double

'        Pisah = TextBox10.Text.Split("-")
'        PisahProyek = ComboBox5.Text.Split("-")
'        Dim sementara As Integer = 0

'        If Button1.Text = "&Simpan" Then
'            Try
'                OpenConn()

'                Cmd.Transaction = Cn.BeginTransaction

'                Dim validasi_khusus As Integer = 0
'                Dim validasi_biasa As Integer = 0

'                For i As Integer = 0 To ListView1.Items.Count - 1

'                    Dim pakai_budget As String = ""
'                    Dim budget_harian As Double = 0
'                    Dim budget_bulanan As Double = 0

'                    Dim lama_budget_harian As Integer = 0
'                    Dim lama_budget_bulanan As Integer = 0

'                    SQL = "select pakai_budget, lama_budget_harian, lama_budget_bulanan, budget_harian, "
'                    SQL = SQL & "budget_bulanan from detail_account where "
'                    SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
'                    SQL = SQL & "kode_account = '" & Ganti(ListView1.Items(i).Text) & "' "
'                    'SQL = SQL & "kode_master_acc = '" & Strings.Left(ListView1.Items(i).Text, 1) & "' and "
'                    'SQL = SQL & "kode_acc = '" & Strings.Mid(ListView1.Items(i).Text, 2, 1) & "' and "
'                    'SQL = SQL & "kode_detail_acc = '" & Strings.Mid(ganti(ListView1.Items(i).Text), 3) & "'"
'                    Using Dr = OpenTrans(SQL)
'                        If Dr.Read Then
'                            pakai_budget = Dr("pakai_budget")
'                            If Dr("pakai_budget") = "Y" Then
'                                budget_harian = Dr("budget_harian")
'                                budget_bulanan = Dr("budget_bulanan")
'                                lama_budget_harian = Dr("lama_budget_harian")
'                                lama_budget_bulanan = Dr("lama_budget_bulanan")
'                            End If
'                        Else
'                            Dr.Close()
'                            CloseTrans()
'                            CloseConn()
'                            MessageBox.Show("Data account tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'                            Exit Sub
'                        End If
'                    End Using

'                    If pakai_budget = "Y" Then
'                        SQL = "select isnull(sum(debit + kredit), 0) as total from jurnal a, detail_jurnal b where "
'                        SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_voucher = b.kode_voucher and "
'                        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
'                        SQL = SQL & "a.tanggal between '" & Format(DateAdd(DateInterval.Day, lama_budget_harian, CDate(FMenu.ToolStripStatusLabel3.Text)), "yyyy-MM-dd") & "' and "
'                        SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' and "
'                        SQL = SQL & "b.kode_account = '" & ListView1.Items(i).Text & "' "
'                        'SQL = SQL & "b.kode_master_acc = '" & Strings.Left(ListView1.Items(i).Text, 1) & "' and "
'                        'SQL = SQL & "b.kode_acc = '" & Strings.Mid(ListView1.Items(i).Text, 2, 1) & "' and "
'                        'SQL = SQL & "b.kode_detail_acc = '" & Strings.Mid(ganti(ListView1.Items(i).Text), 3) & "'"
'                        Using Dr = OpenTrans(SQL)
'                            If Dr.Read Then
'                                If Dr("total") + Val(ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text)) + Val(ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text)) > budget_harian Then
'                                    ListView1.Items(i).SubItems(5).Text = "Y"
'                                    ListView1.Items(i).SubItems(6).Text = lama_budget_harian
'                                    ListView1.Items(i).SubItems(7).Text = lama_budget_bulanan
'                                    ListView1.Items(i).SubItems(8).Text = budget_harian
'                                    ListView1.Items(i).SubItems(9).Text = budget_bulanan
'                                    ListView1.Items(i).SubItems(10).Text = "Y"
'                                    ListView1.Items(i).SubItems(12).Text = Dr("total")
'                                    sementara = sementara + 1

'                                    If (Dr("total") + Val(ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text)) + Val(ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text)) - budget_harian) / budget_harian * 100 > 30 Then
'                                        validasi_khusus = validasi_khusus + 1
'                                    Else
'                                        validasi_biasa = validasi_biasa + 1
'                                    End If

'                                Else
'                                    ListView1.Items(i).SubItems(5).Text = "Y"
'                                    ListView1.Items(i).SubItems(6).Text = lama_budget_harian
'                                    ListView1.Items(i).SubItems(7).Text = lama_budget_bulanan
'                                    ListView1.Items(i).SubItems(8).Text = budget_harian
'                                    ListView1.Items(i).SubItems(9).Text = budget_bulanan
'                                    ListView1.Items(i).SubItems(10).Text = "T"
'                                    ListView1.Items(i).SubItems(12).Text = Dr("total")
'                                End If
'                            End If
'                        End Using

'                        'ListView1.Columns.Add("Kode Acc", 80, HorizontalAlignment.Center)
'                        'ListView1.Columns.Add("Nama Perkiraan", 200) '1
'                        'ListView1.Columns.Add("Keterangan", 200) '2
'                        'ListView1.Columns.Add("Debit", 120, HorizontalAlignment.Right) '3
'                        'ListView1.Columns.Add("Kredit", 120, HorizontalAlignment.Right) '4

'                        'ListView1.Columns.Add("Pakai Budget", 0, HorizontalAlignment.Right) '5
'                        'ListView1.Columns.Add("Lama Budget Harian", 0, HorizontalAlignment.Right) '6
'                        'ListView1.Columns.Add("Lama Budget Bulanan", 0, HorizontalAlignment.Right) '7
'                        'ListView1.Columns.Add("Budget Harian", 0, HorizontalAlignment.Right) '8
'                        'ListView1.Columns.Add("Budget Bulanan", 0, HorizontalAlignment.Right) '9
'                        'ListView1.Columns.Add("Kena Harian", 0, HorizontalAlignment.Right) '10
'                        'ListView1.Columns.Add("Kena Bulanan", 0, HorizontalAlignment.Right) '11
'                        'ListView1.Columns.Add("Terpakai Harian", 0, HorizontalAlignment.Right) '12
'                        'ListView1.Columns.Add("Terpakai Bulanan", 0, HorizontalAlignment.Right) '13

'                        SQL = "select isnull(sum(debit + kredit), 0) as total from jurnal a, detail_jurnal b where "
'                        SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_voucher = b.kode_voucher and "
'                        SQL = SQL & "a.kode_perusahaan = '" & KodePerusahaan & "' and "
'                        SQL = SQL & "a.tanggal between '" & Format(DateAdd(DateInterval.Day, lama_budget_bulanan, CDate(FMenu.ToolStripStatusLabel3.Text)), "yyyy-MM-dd") & "' and "
'                        SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' and "
'                        SQL = SQL & "b.kode_account = '" & ListView1.Items(i).Text & "' "
'                        'SQL = SQL & "b.kode_master_acc = '" & Strings.Left(ListView1.Items(i).Text, 1) & "' and "
'                        'SQL = SQL & "b.kode_acc = '" & Strings.Mid(ListView1.Items(i).Text, 2, 1) & "' and "
'                        'SQL = SQL & "b.kode_detail_acc = '" & Strings.Mid(ganti(ListView1.Items(i).Text), 3) & "'"
'                        Using Dr = OpenTrans(SQL)
'                            If Dr.Read Then
'                                If Dr("total") + Val(ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text)) + Val(ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text)) > budget_bulanan Then
'                                    ListView1.Items(i).SubItems(11).Text = "Y"
'                                    ListView1.Items(i).SubItems(13).Text = Dr("total")

'                                    sementara = sementara + 1

'                                    If (Dr("total") + Val(ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text)) + Val(ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text)) - budget_bulanan) / budget_bulanan * 100 > 30 Then
'                                        validasi_khusus = validasi_khusus + 1
'                                    Else
'                                        validasi_biasa = validasi_biasa + 1
'                                    End If
'                                Else
'                                    ListView1.Items(i).SubItems(11).Text = "T"
'                                    ListView1.Items(i).SubItems(13).Text = Dr("total")
'                                End If
'                            End If
'                        End Using
'                    Else
'                        ListView1.Items(i).SubItems(5).Text = "T"
'                        ListView1.Items(i).SubItems(6).Text = lama_budget_harian
'                        ListView1.Items(i).SubItems(7).Text = lama_budget_bulanan
'                        ListView1.Items(i).SubItems(8).Text = budget_harian
'                        ListView1.Items(i).SubItems(9).Text = budget_bulanan
'                        ListView1.Items(i).SubItems(10).Text = "T"
'                        ListView1.Items(i).SubItems(11).Text = "T"
'                        ListView1.Items(i).SubItems(12).Text = "0"
'                        ListView1.Items(i).SubItems(13).Text = "0"
'                    End If
'                Next

'                If sementara > 0 Then
'                    Dim tnylanjut As String = MessageBox.Show("Biaya ini sudah melebihi budget, harus divalidasi pusat dahulu!!", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

'                    If tnylanjut = vbNo Then
'                        CloseTrans()
'                        CloseConn()
'                        Exit Sub
'                    End If
'                End If

'                If sementara > 0 Then
'                    TextBox2.Text = GetLastNumberJurnalSementara(Format(DateTimePicker1.Value, "yyyyMM"), "SK" & arrInisialFaktur(ComboBox6.SelectedIndex), KodePerusahaan)

'                    Dim isi_val_khusus As String = ""
'                    If validasi_khusus <> 0 Then
'                        isi_val_khusus = "Y"
'                    Else
'                        isi_val_khusus = "T"
'                    End If

'                    Dim pagenumber As Integer = 0
'                    SQL = "Insert Into Jurnal_sementara(Kode_Voucher,Tanggal,Jam,Kode_Perusahaan,Kode_Proyek,Keterangan,"
'                    SQL = SQL & "JudulBank,KetDK,jabatan,userid, lokasi, jns, otomatis, validasi_khusus) values("
'                    SQL = SQL & "'" & TextBox2.Text.ToUpper & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
'                    SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
'                    SQL = SQL & "'" & KodePerusahaan.ToUpper & "', '" & PisahProyek(0).Trim & "', "
'                    SQL = SQL & "'" & TextBox11.Text.ToUpper & "','-', "
'                    SQL = SQL & "'-','-',"
'                    SQL = SQL & "'" & UserID & "', '" & ComboBox6.Text & "', 'By', 'T', '" & isi_val_khusus & "')"
'                    ExecuteTrans(SQL)

'                    a = 0 : b = 0 : Selisih = 0
'                    For i As Integer = 0 To ListView1.Items.Count - 1
'                        a = a + ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text) 'Sisi Debit
'                        b = b + ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text) 'Sisi Kredit
'                    Next

'                    'SQL = Get_Detail_Jurnal_Atas(TextBox2.Text, Strings.Left(Pisah(0).Trim, 1), _
'                    '           Strings.Mid(Pisah(0).Trim, 2, 1), _
'                    '           Strings.Mid(ganti(Pisah(0).Trim), 3), _
'                    '           KodePerusahaan, arrProyek.Item(CmbProyek.SelectedIndex), TextBox11.Text, HilangkanTanda(TextBox7.Text), "0", pagenumber + 1, "A", TextBox9.Text)
'                    'ExecuteTrans(SQL)

'                    Selisih = a - b
'                    SQL = Get_Detail_Jurnal_Sementara_Atas(TextBox2.Text, Strings.Left(Pisah(0).Trim, 1),
'                               Strings.Mid(Pisah(0).Trim, 2, 1),
'                               Strings.Mid(Ganti(Pisah(0).Trim), 3),
'                               KodePerusahaan.ToUpper, PisahProyek(0).Trim, TextBox11.Text.ToUpper, 0, Selisih, pagenumber + 1,
'                               "T", 0, 0, 0, 0, "T", "T", 0, 0, "A", Lokasi)

'                    'SQL = "Insert Into Detail_Jurnal_sementara(kode_voucher,kode_master_acc,kode_acc,kode_detail_acc,"
'                    'SQL = SQL & "Kode_perusahaan,Kode_Proyek,Keterangan,debit,kredit,"
'                    'SQL = SQL & "pagenumber, x_pakai_budget, x_lama_budget_harian, x_lama_budget_bulanan, "
'                    'SQL = SQL & "x_budget_harian, x_budget_bulanan, "
'                    'SQL = SQL & "x_kena_budget_harian, x_kena_budget_bulanan, X_Terpakai_Harian, X_Terpakai_Bulanan) Values("
'                    'SQL = SQL & "'" & TextBox2.Text & "','" & Strings.Left(Pisah(0).Trim, 1) & "',"
'                    'SQL = SQL & "'" & Strings.Mid(Pisah(0).Trim, 2, 1) & "',"
'                    'SQL = SQL & "'" & Strings.Mid(ganti(Pisah(0).Trim), 3) & "','" & KodePerusahaan.ToUpper & "',"
'                    'SQL = SQL & "'" & KodeProyek & "','" & TextBox11.Text.ToUpper & "',0," & Selisih & ",1, "
'                    'SQL = SQL & "'T', 0, 0, 0, 0, 'T', 'T', 0, 0)"
'                    ExecuteTrans(SQL)
'                    Pembeda = "K"

'                    If Pembeda = "M" Or Pembeda = "K" Then
'                        Page = 1
'                    ElseIf Pembeda = "J" Then
'                        Page = 0
'                    Else
'                        Page = 0
'                    End If

'                    Dim Masuk As Integer = 1
'                    For i As Integer = 0 To ListView1.Items.Count - 1
'                        SQL = Get_Detail_Jurnal_Sementara(TextBox2.Text, Strings.Left(ListView1.Items(i).Text, 1),
'                               Strings.Mid(ListView1.Items(i).Text, 2, 1),
'                               Strings.Mid(Ganti(ListView1.Items(i).Text), 3),
'                               KodePerusahaan.ToUpper, PisahProyek(0).Trim, ListView1.Items(i).SubItems(2).Text.ToUpper,
'                               ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text), ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text), Masuk,
'                               ListView1.Items(i).SubItems(5).Text, ChangeDotToNothing(ListView1.Items(i).SubItems(6).Text),
'                               ChangeDotToNothing(ListView1.Items(i).SubItems(7).Text), ChangeDotToNothing(ListView1.Items(i).SubItems(8).Text),
'                               ChangeDotToNothing(ListView1.Items(i).SubItems(9).Text), ListView1.Items(i).SubItems(10).Text,
'                               ListView1.Items(i).SubItems(11).Text, ChangeDotToNothing(ListView1.Items(i).SubItems(12).Text),
'                               ChangeDotToNothing(ListView1.Items(i).SubItems(13).Text), Lokasi)
'                        'SQL = "Insert Into Detail_Jurnal_sementara(kode_voucher,kode_master_acc,kode_acc,"
'                        'SQL = SQL & "kode_detail_acc,Kode_perusahaan,Kode_Proyek,Keterangan,debit,kredit,"
'                        'SQL = SQL & "pagenumber, x_pakai_budget, x_lama_budget_harian, x_lama_budget_bulanan, "
'                        'SQL = SQL & "x_budget_harian, x_budget_bulanan, "
'                        'SQL = SQL & "x_kena_budget_harian, x_kena_budget_bulanan, "
'                        'SQL = SQL & "X_Terpakai_Harian, X_Terpakai_Bulanan"
'                        'SQL = SQL & ") Values("
'                        'SQL = SQL & "'" & TextBox2.Text.ToUpper & "',"
'                        'SQL = SQL & "'" & Strings.Left(ListView1.Items(i).Text, 1) & "',"
'                        'SQL = SQL & "'" & Strings.Mid(ListView1.Items(i).Text, 2, 1) & "',"
'                        'SQL = SQL & "'" & Strings.Mid(ganti(ListView1.Items(i).Text), 3) & "',"
'                        'SQL = SQL & "'" & KodePerusahaan.ToUpper & "','" & PisahProyek(0).Trim & "',"
'                        'SQL = SQL & "'" & ListView1.Items(i).SubItems(2).Text.ToUpper & "',"
'                        'SQL = SQL & "'" & ganti(ListView1.Items(i).SubItems(3).Text) & "',"
'                        'SQL = SQL & "'" & ganti(ListView1.Items(i).SubItems(4).Text) & "'," & Masuk & ", "
'                        'SQL = SQL & "'" & ListView1.Items(i).SubItems(5).Text & "', "
'                        'SQL = SQL & "'" & ganti(ListView1.Items(i).SubItems(6).Text) & "', "
'                        'SQL = SQL & "'" & ganti(ListView1.Items(i).SubItems(7).Text) & "', "
'                        'SQL = SQL & "'" & ganti(ListView1.Items(i).SubItems(8).Text) & "', "
'                        'SQL = SQL & "'" & ganti(ListView1.Items(i).SubItems(9).Text) & "', "
'                        'SQL = SQL & "'" & ListView1.Items(i).SubItems(10).Text & "', "
'                        'SQL = SQL & "'" & ListView1.Items(i).SubItems(11).Text & "', "
'                        'SQL = SQL & "'" & ListView1.Items(i).SubItems(12).Text & "', "
'                        'SQL = SQL & "'" & ListView1.Items(i).SubItems(13).Text & "')"
'                        ExecuteTrans(SQL)

'                        Page = Page + 1
'                        If Page = 11 Then
'                            Page = 0
'                            Masuk = Masuk + 1
'                        End If
'                    Next

'                Else
'                    Dim pagenumber As Integer = 0
'                    TextBox2.Text = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), "KK" & arrInisialFaktur(ComboBox6.SelectedIndex), KodePerusahaan)

'                    SQL = "Insert Into Jurnal(Kode_Voucher,Tanggal,Jam,Kode_Perusahaan,Kode_Proyek,Keterangan,"
'                    SQL = SQL & "JudulBank,KetDK,jabatan,userid, lokasi, jns, otomatis) values("
'                    SQL = SQL & "'" & TextBox2.Text.ToUpper & "','" & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "',"
'                    SQL = SQL & "'" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "HH:mm:ss") & "', "
'                    SQL = SQL & "'" & KodePerusahaan.ToUpper & "', '" & PisahProyek(0).Trim & "', "
'                    SQL = SQL & "'" & TextBox11.Text.ToUpper & "','-', "
'                    SQL = SQL & "'-','-',"
'                    SQL = SQL & "'" & UserID & "', '" & ComboBox6.Text & "', 'By', 'T')"
'                    ExecuteTrans(SQL)

'                    a = 0 : b = 0 : Selisih = 0
'                    For i As Integer = 0 To ListView1.Items.Count - 1
'                        a = a + ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text) 'Sisi Debit
'                        b = b + ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text) 'Sisi Kredit
'                    Next

'                    Selisih = a - b
'                    SQL = Get_Detail_Jurnal_Atas(TextBox2.Text, Strings.Left(Pisah(0).Trim, 1),
'                              Strings.Mid(Pisah(0).Trim, 2, 1),
'                              Strings.Mid(Ganti(Pisah(0).Trim), 3),
'                              KodePerusahaan.ToUpper, KodeProyek, TextBox11.Text.ToUpper, 0, Selisih, pagenumber + 1, "A", Lokasi, "id")
'                    'SQL = "Insert Into Detail_Jurnal(kode_voucher,kode_master_acc,kode_acc,kode_detail_acc,"
'                    'SQL = SQL & "Kode_perusahaan,Kode_Proyek,Keterangan,debit,kredit,pagenumber) Values("
'                    'SQL = SQL & "'" & TextBox2.Text & "','" & Strings.Left(Pisah(0).Trim, 1) & "',"
'                    'SQL = SQL & "'" & Strings.Mid(Pisah(0).Trim, 2, 1) & "',"
'                    'SQL = SQL & "'" & Strings.Mid(ganti(Pisah(0).Trim), 3) & "','" & KodePerusahaan.ToUpper & "',"
'                    'SQL = SQL & "'" & KodeProyek & "','" & TextBox11.Text.ToUpper & "',0," & Selisih & ",1)"
'                    ExecuteTrans(SQL)
'                    Pembeda = "K"

'                    If Pembeda = "M" Or Pembeda = "K" Then
'                        Page = 1
'                    ElseIf Pembeda = "J" Then
'                        Page = 0
'                    Else
'                        Page = 0
'                    End If

'                    Dim Masuk As Integer = 1
'                    For i As Integer = 0 To ListView1.Items.Count - 1
'                        SQL = Get_Detail_Jurnal(TextBox2.Text, Strings.Left(ListView1.Items(i).Text, 1),
'                               Strings.Mid(ListView1.Items(i).Text, 2, 1),
'                               Strings.Mid(Ganti(ListView1.Items(i).Text), 3),
'                               KodePerusahaan.ToUpper, PisahProyek(0).Trim, ListView1.Items(i).SubItems(2).Text.ToUpper, ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text), ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text), Masuk, Lokasi, "id")
'                        'SQL = "Insert Into Detail_Jurnal(kode_voucher,kode_master_acc,kode_acc,"
'                        'SQL = SQL & "kode_detail_acc,Kode_perusahaan,Kode_Proyek,Keterangan,debit,kredit,"
'                        'SQL = SQL & "pagenumber) Values('" & TextBox2.Text.ToUpper & "',"
'                        'SQL = SQL & "'" & Strings.Left(ListView1.Items(i).Text, 1) & "',"
'                        'SQL = SQL & "'" & Strings.Mid(ListView1.Items(i).Text, 2, 1) & "',"
'                        'SQL = SQL & "'" & Strings.Mid(ganti(ListView1.Items(i).Text), 3) & "',"
'                        'SQL = SQL & "'" & KodePerusahaan.ToUpper & "','" & PisahProyek(0).Trim & "',"
'                        'SQL = SQL & "'" & ListView1.Items(i).SubItems(2).Text.ToUpper & "',"
'                        'SQL = SQL & "'" & ganti(ListView1.Items(i).SubItems(3).Text) & "',"
'                        'SQL = SQL & "'" & ganti(ListView1.Items(i).SubItems(4).Text) & "'," & Masuk & ")"
'                        ExecuteTrans(SQL)
'                        Page = Page + 1
'                        If Page = 11 Then
'                            Page = 0
'                            Masuk = Masuk + 1
'                        End If
'                    Next

'                End If


'                Cmd.Transaction.Commit()

'                CloseConn()
'            Catch ex As Exception
'                CloseTrans()
'                CloseConn()
'                MessageBox.Show(ex.Message)
'                Exit Sub
'            End Try

'        Else

'        End If



'        Dim Nanya As String = MessageBox.Show("Anda akan lakukan pencetakan voucher . . ? ?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
'        If Nanya = vbYes Then
'            PisahProyek = ComboBox5.Text.Split("-")
'            Try

'                OpenConn()

'                If sementara > 0 Then
'                    SQL = "Select * from Detail_Jurnal_sementara Where kode_perusahaan = '" & KodePerusahaan & "' and kode_proyek = '" & PisahProyek(0).Trim & "' and Kode_Voucher = '" & TextBox2.Text & "' order by nomor"
'                    CRQuery = SQL
'                    Using DS As DataSet = BindingTrans(SQL)
'                        Dim JlhDebit As Double = 0
'                        Dim JlhKredit As Double = 0
'                        For i As Integer = 0 To ListView1.Items.Count - 1
'                            JlhDebit = JlhDebit + ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text)
'                            JlhKredit = JlhKredit + ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text)
'                        Next

'                        Dim CrDoc As New Voucher_Jurnal_Keluar_Sementara    'Nama file CR
'                        CRName = "Voucher_Jurnal_Keluar_Sementara"

'                        Setting_Paper_Size(CrDoc)

'                        With Tempat_Cetak
'                            .Button1.Visible = True
'                            CrDoc.SetDataSource(DS)
'                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
'                            Dim Filtering As String = ""

'                            .Text = "Bukti Kas Keluar"
'                            Filtering = "And not({Detail_Jurnal_sementara.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal_sementara.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal_sementara.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"

'                            CrDoc.RecordSelectionFormula = "{Jurnal_sementara.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Jurnal_sementara.Kode_Proyek} = '" & PisahProyek(0).Trim & "' and {Detail_Jurnal_sementara.Kode_Voucher} = '" & TextBox2.Text & "' " & Filtering
'                            CRSF = "{Jurnal_sementara.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Jurnal_sementara.Kode_Proyek} = '" & PisahProyek(0).Trim & "' and {Detail_Jurnal_sementara.Kode_Voucher} = '" & TextBox2.Text & "' " & Filtering

'                            CrDoc.SummaryInfo.ReportTitle = General_Class.SayRupiah(ChangeDotToNothing(JlhDebit - JlhKredit))
'                            CRRT = General_Class.SayRupiah(ChangeDotToNothing(JlhDebit - JlhKredit))

'                            .CrystalReportViewer1.ReportSource = CrDoc
'                            .CrystalReportViewer1.DisplayGroupTree = False
'                            .Refresh()
'                            .Show()
'                        End With
'                    End Using

'                Else

'                    SQL = "Select * from Detail_Jurnal Where kode_perusahaan = '" & KodePerusahaan & "' and kode_proyek = '" & PisahProyek(0).Trim & "' and Kode_Voucher = '" & TextBox2.Text & "' order by nomor"
'                    CRQuery = SQL
'                    Using DS As DataSet = BindingTrans(SQL)
'                        Dim JlhDebit As Double = 0
'                        Dim JlhKredit As Double = 0
'                        For i As Integer = 0 To ListView1.Items.Count - 1
'                            JlhDebit = JlhDebit + ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text)
'                            JlhKredit = JlhKredit + ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text)
'                        Next

'                        Dim CrDoc As New Voucher_Jurnal_Keluar    'Nama file CR
'                        CRName = "Voucher_Jurnal_Keluar"

'                        Setting_Paper_Size(CrDoc)

'                        With Tempat_Cetak
'                            .Button1.Visible = True
'                            CrDoc.SetDataSource(DS)
'                            CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
'                            Dim Filtering As String = ""

'                            .Text = "Bukti Kas Keluar"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"

'                            CrDoc.RecordSelectionFormula = "{Detail_Jurnal.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Detail_Jurnal.Kode_Proyek} = '" & PisahProyek(0).Trim & "' and {Detail_Jurnal.Kode_Voucher} = '" & TextBox2.Text & "' " & Filtering
'                            CRSF = "{Detail_Jurnal.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Detail_Jurnal.Kode_Proyek} = '" & PisahProyek(0).Trim & "' and {Detail_Jurnal.Kode_Voucher} = '" & TextBox2.Text & "' " & Filtering

'                            CrDoc.SummaryInfo.ReportTitle = General_Class.SayRupiah(ChangeDotToNothing(JlhDebit - JlhKredit))
'                            CRRT = General_Class.SayRupiah(ChangeDotToNothing(JlhDebit - JlhKredit))

'                            .CrystalReportViewer1.ReportSource = CrDoc
'                            .CrystalReportViewer1.DisplayGroupTree = False
'                            .Refresh()
'                            .Show()
'                        End With
'                    End Using

'                End If

'                CloseConn()

'            Catch ex As Exception
'                CloseConn()
'                MessageBox.Show(ex.Message)
'                Exit Sub
'            End Try
'        End If

'        If sementara > 0 Then

'        Else

'        End If

'        Kosong()
'        ComboBox2.Focus()
'        'Tempat_Cetak.Focus()
'    End Sub

'    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
'        If ListView1.Items.Count = 0 Then Exit Sub

'        TextBox3.Text = Trim(Str(ChangeDotToNothing(ListView1.FocusedItem.Text)))
'        TextBox4.Text = Trim(ListView1.FocusedItem.SubItems(1).Text)
'        TextBox1.Text = Trim(ListView1.FocusedItem.SubItems(2).Text)
'        If ListView1.FocusedItem.SubItems(3).Text = 0 Then
'            ComboBox1.SelectedIndex = 1
'            TextBox5.Text = Trim(Str(ChangeDotToNothing(ListView1.FocusedItem.SubItems(4).Text)))
'        Else
'            ComboBox1.SelectedIndex = 0
'            TextBox5.Text = Trim(Str(ChangeDotToNothing(ListView1.FocusedItem.SubItems(3).Text)))
'        End If
'        ListView1.FocusedItem.Remove()
'        HitungTotal()
'    End Sub

'    Private Sub HitungTotal()
'        Debit = 0 : Kredit = 0

'        For i As Integer = 0 To ListView1.Items.Count - 1
'            Debit = Debit + ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text)
'            Kredit = Kredit + ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text)
'        Next

'        If ComboBox2.SelectedIndex = 0 Or ComboBox2.SelectedIndex = 2 Then 'Kas Masuk dan Bank Masuk
'            'TextBox6.Text = Format(Debit, "N0")
'            TextBox7.Text = Format(Kredit - Debit, "N0")
'        ElseIf ComboBox2.SelectedIndex = 1 Or ComboBox2.SelectedIndex = 3 Then 'Kas Keluar dan Bank Keluar
'            'TextBox6.Text = Format(Debit, "N0")
'            TextBox7.Text = Format(Debit - Kredit, "N0")
'        Else
'            TextBox6.Text = Format(Debit, "N0")
'            TextBox7.Text = Format(Kredit, "N0")
'        End If
'    End Sub

'    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'        Dim Tanya As String = MessageBox.Show("Anda yakin data ini akan dihapus . . ? ?", "Perhatian", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
'        If Tanya = vbYes Then
'            General_Class.Execute("Delete from detail_jurnal where kode_voucher = '" & TextBox2.Text & "' and kode_perusahaan = '" & KodePerusahaan & "' and kode_proyek = '" & PisahProyek(0).Trim & "'")
'            General_Class.Execute("Delete from jurnal where kode_voucher = '" & TextBox2.Text & "' and kode_perusahaan = '" & KodePerusahaan & "' and kode_proyek = '" & PisahProyek(0).Trim & "'")
'        End If
'        Kosong()
'        ComboBox2.Focus()
'    End Sub

'    Private Sub DateTimePicker1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DateTimePicker1.KeyPress
'        ComboBox5.Focus()
'    End Sub

'    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
'        Pisah = TextBox10.Text.Split("-")
'        PisahProyek = ComboBox5.Text.Split("-")

'        'SQL = "Select * from Detail_Jurnal Where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Voucher = '" & TextBox2.Text & "' order by nomor"
'        SQL = "Select * from Detail_Jurnal Where kode_perusahaan = '" & KodePerusahaan & "' and kode_proyek = '" & PisahProyek(0).Trim & "' and Kode_Voucher = '" & TextBox2.Text & "' order by nomor"
'        CRQuery = SQL
'        Using DS As DataSet = General_Class.Binding(SQL)
'            '===================
'            'KHUSUS JURNAL ENTRY
'            '===================
'            PisahProyek = ComboBox5.Text.Split("-")
'            If Strings.Left(TextBox2.Text, 2) = "JE" Then
'                Dim CrDoc As New Voucher_Jurnal  'Nama file CR
'                CRName = "Voucher_Jurnal"

'                Setting_Paper_Size(CrDoc)

'                With Tempat_Cetak
'                    .Button1.Visible = True
'                    CrDoc.SetDataSource(DS)
'                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
'                    Dim Filtering As String = ""
'                    Select Case Strings.Left(TextBox2.Text, 2)
'                        Case "KM"
'                            .Text = "Bukti Kas Masuk"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case "KK"
'                            .Text = "Bukti Kas Keluar"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case "BM"
'                            .Text = "Bukti Bank Masuk"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case "BK"
'                            .Text = "Bukti Bank Keluar"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case Else
'                            .Text = "Journal Entry"
'                    End Select

'                    CrDoc.RecordSelectionFormula = "{Detail_Jurnal.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Detail_Jurnal.Kode_Proyek} = '" & PisahProyek(0).Trim & "' and {Detail_Jurnal.Kode_Voucher} = '" & TextBox2.Text & "' " & Filtering
'                    CRSF = "{Detail_Jurnal.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Detail_Jurnal.Kode_Proyek} = '" & PisahProyek(0).Trim & "' and {Detail_Jurnal.Kode_Voucher} = '" & TextBox2.Text & "' " & Filtering

'                    CrDoc.SummaryInfo.ReportTitle = General_Class.SayRupiah(ChangeDotToNothing(TextBox6.Text))
'                    CRRT = General_Class.SayRupiah(ChangeDotToNothing(TextBox6.Text))

'                    .CrystalReportViewer1.ReportSource = CrDoc
'                    .CrystalReportViewer1.DisplayGroupTree = False
'                    .Refresh()
'                    .Show()
'                End With

'            ElseIf Strings.Left(TextBox2.Text, 2) = "KM" Or Strings.Left(TextBox2.Text, 2) = "BM" Then 'KHUSUS JURNAL MASUK

'                Dim JlhDebit As Double = 0
'                Dim JlhKredit As Double = 0
'                For i As Integer = 0 To ListView1.Items.Count - 1
'                    JlhDebit = JlhDebit + ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text)
'                    JlhKredit = JlhKredit + ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text)
'                Next

'                Dim CrDoc As New Voucher_Jurnal_Masuk   'Nama file CR
'                CRName = "Voucher_Jurnal_Masuk"

'                Setting_Paper_Size(CrDoc)

'                With Tempat_Cetak
'                    .Button1.Visible = True
'                    CrDoc.SetDataSource(DS)
'                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
'                    Dim Filtering As String = ""
'                    Select Case Strings.Left(TextBox2.Text, 2)
'                        Case "KM"
'                            .Text = "Bukti Kas Masuk"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case "KK"
'                            .Text = "Bukti Kas Keluar"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case "BM"
'                            .Text = "Bukti Bank Masuk"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case "BK"
'                            .Text = "Bukti Bank Keluar"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case Else
'                            .Text = "Journal Entry"
'                    End Select

'                    CrDoc.RecordSelectionFormula = "{Detail_Jurnal.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Detail_Jurnal.Kode_Proyek} = '" & PisahProyek(0).Trim & "' and {Detail_Jurnal.Kode_Voucher} = '" & TextBox2.Text & "' " & Filtering
'                    CRSF = "{Detail_Jurnal.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Detail_Jurnal.Kode_Proyek} = '" & PisahProyek(0).Trim & "' and {Detail_Jurnal.Kode_Voucher} = '" & TextBox2.Text & "' " & Filtering

'                    CrDoc.SummaryInfo.ReportTitle = General_Class.SayRupiah(ChangeDotToNothing(JlhKredit - JlhDebit))
'                    CRRT = General_Class.SayRupiah(ChangeDotToNothing(JlhKredit - JlhDebit))

'                    .CrystalReportViewer1.ReportSource = CrDoc
'                    .CrystalReportViewer1.DisplayGroupTree = False
'                    .Refresh()
'                    .Show()
'                End With

'            ElseIf Strings.Left(TextBox2.Text, 2) = "KK" Or Strings.Left(TextBox2.Text, 2) = "BK" Then 'KHUSUS JURNAL KELUAR

'                Dim JlhDebit As Double = 0
'                Dim JlhKredit As Double = 0
'                For i As Integer = 0 To ListView1.Items.Count - 1
'                    JlhDebit = JlhDebit + ChangeDotToNothing(ListView1.Items(i).SubItems(3).Text)
'                    JlhKredit = JlhKredit + ChangeDotToNothing(ListView1.Items(i).SubItems(4).Text)
'                Next

'                Dim CrDoc As New Voucher_Jurnal_Keluar    'Nama file CR
'                CRName = "Voucher_Jurnal_Keluar"

'                Setting_Paper_Size(CrDoc)

'                With Tempat_Cetak
'                    .Button1.Visible = True
'                    CrDoc.SetDataSource(DS)
'                    CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
'                    Dim Filtering As String = ""
'                    Select Case Strings.Left(TextBox2.Text, 2)
'                        Case "KM"
'                            .Text = "Bukti Kas Masuk"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case "KK"
'                            .Text = "Bukti Kas Keluar"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case "BM"
'                            .Text = "Bukti Bank Masuk"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case "BK"
'                            .Text = "Bukti Bank Keluar"
'                            Filtering = "And not({Detail_Jurnal.Kode_Master_Acc} = '" & Strings.Left(Pisah(0).Trim, 1) & "' and {Detail_Jurnal.Kode_Acc} = '" & Strings.Mid(Pisah(0).Trim, 2, 1) & "' and {Detail_Jurnal.Kode_Detail_Acc} = '" & Strings.Mid(Ganti(Pisah(0).Trim), 3) & "')"
'                        Case Else
'                            .Text = "Journal Entry"
'                    End Select

'                    CrDoc.RecordSelectionFormula = "{Detail_Jurnal.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Detail_Jurnal.Kode_Proyek} = '" & PisahProyek(0).Trim & "' and {Detail_Jurnal.Kode_Voucher} = '" & TextBox2.Text & "' " & Filtering
'                    CRSF = "{Detail_Jurnal.Kode_Perusahaan} = '" & KodePerusahaan & "' and {Detail_Jurnal.Kode_Proyek} = '" & PisahProyek(0).Trim & "' and {Detail_Jurnal.Kode_Voucher} = '" & TextBox2.Text & "' " & Filtering

'                    CrDoc.SummaryInfo.ReportTitle = General_Class.SayRupiah(ChangeDotToNothing(JlhDebit - JlhKredit))
'                    CRRT = General_Class.SayRupiah(ChangeDotToNothing(JlhDebit - JlhKredit))

'                    .CrystalReportViewer1.ReportSource = CrDoc
'                    .CrystalReportViewer1.DisplayGroupTree = False
'                    .Refresh()
'                    .Show()
'                End With
'            End If
'        End Using
'        Kosong()
'        ComboBox2.Focus()
'        Tempat_Cetak.Focus()
'    End Sub

'    Private Sub TextBox11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox11.KeyPress
'        If e.KeyChar = Chr(13) Then TextBox3.Focus()
'        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
'    End Sub


'    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
'        If ComboBox2.SelectedIndex = 4 Or ComboBox2.SelectedIndex = -1 Then 'Jurnal Umum
'            SQL = "select (a.kode_account) as KdAcc,a.* from detail_account a "
'            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
'            SQL = SQL & "and right(a.kode_account,3) <> '000' and a.flag_biaya = 'Y' order by a.kode_account"
'            'SQL = SQL & "and a.lokasi = '" & ComboBox6.Text & "' "
'        ElseIf ComboBox2.SelectedIndex = 0 Or ComboBox2.SelectedIndex = 2 Then 'Kas/Bank Masuk
'            SQL = "select (a.kode_account) as KdAcc,a.* from a.detail_account a "
'            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.kode_perusahaan = '" & KodePerusahaan & "' "
'            SQL = SQL & "and right(a.kode_account,3) <> '000' and a.flag_biaya = 'Y' order by a.kode_account"
'            'SQL = SQL & "and a.lokasi = '" & ComboBox6.Text & "' order by a.kode_account"
'        ElseIf ComboBox2.SelectedIndex = 1 Or ComboBox2.SelectedIndex = 3 Then 'Kas/Bank Keluar
'            SQL = "select (a.kode_account) as KdAcc,a.* from detail_account a "
'            SQL = SQL & "where a.kode_perusahaan = '" & KodePerusahaan & "' "
'            SQL = SQL & "and right(a.kode_account,3) <> '000' and a.flag_biaya = 'Y' order by a.kode_account"
'            'SQL = SQL & "and a.lokasi = '" & ComboBox6.Text & "' order by a.kode_account"
'        End If

'        ListView2.Items.Clear()
'        Using Dr As SqlClient.SqlDataReader = General_Class.Open(SQL)
'            Do While Dr.Read
'                Dim Lvw As ListViewItem
'                Lvw = ListView2.Items.Add(Dr("kdacc"))
'                Lvw.SubItems.Add(Dr("Keterangan"))
'                Lvw.SubItems.Add(Dr("posisi"))
'            Loop
'        End Using
'    End Sub

'    Private Sub ComboBox5_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox5.SelectedIndexChanged
'        If ComboBox2.SelectedIndex = -1 Then Exit Sub
'        If ComboBox5.SelectedIndex = -1 Then Exit Sub

'        Try
'            OpenConn()

'            TextBox2.Text = GetLastNumberJurnal(Format(DateTimePicker1.Value, "yyyyMM"), "KK" & arrInisialFaktur(ComboBox6.SelectedIndex), KodePerusahaan)

'            CloseConn()
'        Catch ex As Exception
'            CloseConn()
'            MessageBox.Show(ex.Message)
'            Exit Sub
'        End Try
'    End Sub

'    Private Sub ListView2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ListView2.KeyDown
'        If e.KeyCode = Keys.Enter Then
'            TextBox3.Text = Ganti(ListView2.FocusedItem.Text)
'            TextBox4.Text = ListView2.FocusedItem.SubItems(1).Text
'            If ListView2.FocusedItem.SubItems(2).Text = "D" Then
'                ComboBox1.SelectedIndex = 0
'            Else
'                ComboBox1.SelectedIndex = 1
'            End If
'            TextBox1.Focus()
'            GroupBox5.Visible = False
'        End If
'    End Sub

'    Private Sub TextBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
'        If e.KeyChar = Chr(13) Then TextBox3.Focus()
'        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
'    End Sub

'    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged

'    End Sub

'    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged

'    End Sub

'    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

'    End Sub

'    Private Sub Kas_Keluar_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SizeChanged
'        Label1.Size = New Point(Me.Width, 33)
'    End Sub

'    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

'    End Sub
'End Class