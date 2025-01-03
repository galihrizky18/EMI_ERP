Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Detail_Account_New2

    Dim Pisah1() As String

    Private Sub Detail_Account_New_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListView1.Columns.Add("Kode M.Acc", 0, HorizontalAlignment.Center)
        'ListView1.Columns.Add("Kode Acc", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode D.Acc", 0, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Account", 120, HorizontalAlignment.Center)
        ListView1.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("Posisi", 70, HorizontalAlignment.Center)
        ListView1.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Letak", 70, HorizontalAlignment.Left)
        ListView1.Columns.Add("Jns", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Aktif", 50, HorizontalAlignment.Center)
        ListView1.Columns.Add("Flag Khusus", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Flag Biaya", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Flag Pbk", 100, HorizontalAlignment.Center)
        ListView1.View = View.Details

        Kosong()
        ComboBox2.Focus()
    End Sub

    Private Sub Kosong()

        Try
            OpenConn()

            ComboBox2.Items.Clear()
            SQL = "select * from Master_Acc where kode_perusahaan = '" & KodePerusahaan & "' Order by kode_master_acc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox2.Items.Add(dr("Kode_Master_Acc") & " - " & dr("Keterangan"))
                Loop
            End Using

            ComboBox5.Items.Clear()
            SQL = "select kode_stock_owner from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using Dr = Open(SQL)
                Do While Dr.Read
                    ComboBox5.Items.Add(Dr("kode_stock_owner"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


        ComboBox6.Items.Clear()
        ComboBox6.Items.Add("LR")
        ComboBox6.Items.Add("Neraca")
        ComboBox6.SelectedIndex = 0

        ComboBox7.Items.Clear()
        ComboBox7.Items.Add("Penjualan")
        ComboBox7.Items.Add("Pembelian")
        ComboBox7.Items.Add("HPP")
        ComboBox7.Items.Add("Biaya")
        ComboBox7.Items.Add("Pendapatan")
        ComboBox7.Items.Add("Biaya Lain")
        ComboBox7.Items.Add("Biaya Pajak")

        ComboBox8.Items.Clear()
        ComboBox8.Items.Add("N_Aktiva_Lancar")
        ComboBox8.Items.Add("N_Aktiva_Tetap")
        ComboBox8.Items.Add("N_Biaya_Dibayar_Dimuka")
        ComboBox8.Items.Add("N_Aktiva_Lain")
        ComboBox8.Items.Add("N_Hutang_Lancar")
        ComboBox8.Items.Add("N_Hutang_Jangka_Panjang")
        ComboBox8.Items.Add("N_Pendapatan_Diterima_Dimuka")
        ComboBox8.Items.Add("N_Modal")

        ComboBox7.Enabled = True
        ComboBox8.Enabled = False
        ComboBox7.SelectedIndex = -1
        ComboBox8.SelectedIndex = -1

        ComboBox9.Items.Clear()
        ComboBox9.Items.Add("Y")
        ComboBox9.Items.Add("T")

        ComboBox11.Items.Clear()
        ComboBox11.Items.Add("Y")
        ComboBox11.Items.Add("T")

        ComboBox12.Items.Clear()
        ComboBox12.Items.Add("Y")
        ComboBox12.Items.Add("T")

        ComboBox13.Items.Clear()
        ComboBox13.Items.Add("Y")
        ComboBox13.Items.Add("T")



        ComboBox1.Items.Clear() : ComboBox1.Text = ""
        ComboBox1.Items.Add("Kode_Master_Acc")
        ComboBox1.Items.Add("Kode_Acc")
        ComboBox1.Items.Add("Kode_Detail_Acc")
        ComboBox1.Items.Add("Keterangan")

        ComboBox4.Items.Clear() : ComboBox4.Text = ""
        ComboBox4.Items.Add("Debit")
        ComboBox4.Items.Add("Kredit")

        ComboBox10.Items.Clear()
        ComboBox10.Items.Add("Y")
        ComboBox10.Items.Add("T")

        TextBox5.Text = "" : TextBox6.Text = ""
        TextBox7.Text = "" : TextBox8.Text = ""

        TextBox5.Enabled = False : TextBox6.Enabled = False
        TextBox7.Enabled = False : TextBox8.Enabled = False

        TextBox1.Text = "" : TextBox2.Text = "" : TextBox3.Text = ""
        Button1.Text = "&Simpan"
        Button2.Enabled = False

        'flag_khusus = "T"
        Try
            OpenConn()

            'If CekButtonRole("akun_khusus") = "Y" Then
            '    flag_khusus = ""
            'Else
            '    flag_khusus = " and flag_khusus = 'T' "
            'End If

            ListView1.Items.Clear()
            SQL = "Select * From Detail_Account where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Master_Acc,Kode_Acc,kode_detail_acc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("Kode_master_acc"))
                    'Lvw.SubItems.Add(dr("kode_detail_acc"))
                    Lvw.SubItems.Add(dr("Kode_acc") & dr("kode_detail_acc"))
                    Lvw.SubItems.Add(dr("Kode_account"))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    Lvw.SubItems.Add(dr("posisi"))
                    Lvw.SubItems.Add(dr("lokasi"))
                    Lvw.SubItems.Add(dr("letak"))
                    Lvw.SubItems.Add(dr("jns"))
                    Lvw.SubItems.Add(dr("aktif"))
                    Lvw.SubItems.Add(dr("flag_khusus"))
                    If General_Class.CekNULL(dr("flag_biaya")) = "" Then
                        Lvw.SubItems.Add("T")
                    Else
                        Lvw.SubItems.Add(General_Class.CekNULL(dr("flag_biaya")))
                    End If
                    Lvw.SubItems.Add(dr("flag_pbk"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Kosong()
        ComboBox2.Focus()
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    'Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If e.KeyChar = Chr(13) Then TextBox1.Focus()
    'End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8) Or e.KeyChar = Chr(Asc("."))) Then e.KeyChar = Chr(0)

    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox4.Focus()
    End Sub

    'Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
    '    OpenConn()

    '    Pisah1 = ComboBox2.Text.Split("-")
    '    ComboBox3.Items.Clear()
    '    Cmd.CommandText = "select * from Account Where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Pisah1(0).Trim & "' Order by kode_acc"
    '    Dr = Cmd.ExecuteReader
    '    Do While Dr.Read
    '        ComboBox3.Items.Add(Dr("Kode_Acc") & " - " & Dr("Keterangan"))
    '    Loop
    '    CloseDr()

    '    CloseConn()
    'End Sub

    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Leave
        If ComboBox2.Text.Trim.Length = 0 Then Exit Sub
        'If ComboBox3.Text.Trim.Length = 0 Then Exit Sub
        If TextBox1.Text.Trim.Length = 0 Then Exit Sub

        OpenConn()

        Pisah1 = ComboBox2.Text.Split("-")
        'Pisah2 = ComboBox3.Text.Split("-")
        'Cmd.CommandText = "Select * From Detail_Account Where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Pisah1(0).Trim & "' and kode_acc = '" & Pisah2(0).Trim & "' and kode_detail_acc = '" & TextBox1.Text & "'"
        Cmd.CommandText = "Select * From Detail_Account Where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Pisah1(0).Trim & "' and kode_acc + kode_detail_acc = '" & TextBox1.Text & "'"
        Dr = Cmd.ExecuteReader
        If Dr.Read Then
            TextBox2.Text = Dr("keterangan")
            If Dr("posisi") = "D" Then ComboBox4.SelectedIndex = 0 Else ComboBox4.SelectedIndex = 1

            ComboBox5.Text = Dr("lokasi")
            ComboBox6.Text = Dr("letak")
            If Dr("letak") = "LR" Then
                ComboBox7.Text = Dr("jns")
            Else
                ComboBox8.Text = Dr("jns")
            End If
            ComboBox9.Text = Dr("aktif")
            ComboBox10.Text = Dr("pakai_budget")
            If Dr("pakai_budget") = "Y" Then
                TextBox5.Text = Dr("lama_budget_harian") : TextBox6.Text = Dr("budget_harian")
                TextBox7.Text = Dr("lama_budget_bulanan") : TextBox8.Text = Dr("budget_bulanan")

                TextBox5.Enabled = True : TextBox6.Enabled = True
                TextBox7.Enabled = True : TextBox8.Enabled = True
            Else
                TextBox5.Text = "" : TextBox6.Text = ""
                TextBox7.Text = "" : TextBox8.Text = ""

                TextBox5.Enabled = False : TextBox6.Enabled = False
                TextBox7.Enabled = False : TextBox8.Enabled = False
            End If
            'If ComboBox10.SelectedIndex = 0 Then 'y
            '    TextBox5.Text = "" : TextBox6.Text = ""
            '    TextBox7.Text = "" : TextBox8.Text = ""

            '    TextBox5.Enabled = True : TextBox6.Enabled = True
            '    TextBox7.Enabled = True : TextBox8.Enabled = True
            'Else
            '    TextBox5.Text = "" : TextBox6.Text = ""
            '    TextBox7.Text = "" : TextBox8.Text = ""

            '    TextBox5.Enabled = False : TextBox6.Enabled = False
            '    TextBox7.Enabled = False : TextBox8.Enabled = False
            'End If

            ComboBox11.Text = Dr("flag_khusus")
            If General_Class.CekNULL(Dr("flag_biaya")) = "" Then
                ComboBox12.Text = "T"
            Else
                ComboBox12.Text = (General_Class.CekNULL(Dr("flag_biaya")))
            End If
            ComboBox13.Text = Dr("flag_pbk")

            Button1.Text = "&Update"
            Button2.Enabled = True
        Else
            TextBox2.Text = ""
            ComboBox4.SelectedIndex = -1
            ComboBox5.SelectedIndex = -1
            ComboBox6.SelectedIndex = 0
            ComboBox7.Enabled = True
            ComboBox7.SelectedIndex = -1
            ComboBox8.SelectedIndex = -1
            ComboBox9.SelectedIndex = -1
            ComboBox10.SelectedIndex = -1
            ComboBox11.SelectedIndex = -1
            ComboBox12.SelectedIndex = -1
            ComboBox13.SelectedIndex = -1
            Button1.Text = "&Simpan"
            Button2.Enabled = False
        End If
        CloseDr()
        Cmd = Nothing

        CloseConn()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus . . ? ?", "Accounting", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            Pisah1 = ComboBox2.Text.Split("-")
            'Pisah2 = ComboBox3.Text.Split("-")

            'OpenConn()
            'If CekButtonRole("update_account") = "T" Then
            '    CloseTrans()
            '    CloseConn()
            '    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    Exit Sub
            'End If
            'CloseConn()

            'Detail Jurnal
            'Using Dr1 As SqlClient.SqlDataReader = General_Class.Open("Select top 1 * from Detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Trim(Pisah1(0)) & "' and kode_acc = '" & Trim(Pisah2(0)) & "' and kode_detail_acc = '" & TextBox1.Text & "'")
            Using Dr1 As SqlClient.SqlDataReader = General_Class.Open("Select top 1 * from Detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Trim(Pisah1(0)) & "' and kode_acc + kode_detail_acc = '" & TextBox1.Text & "'")
                If Dr1.Read Then
                    MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data detail jurnal", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Kosong()
                    ComboBox2.Focus()
                    Exit Sub
                End If
            End Using

            OpenConn()
            If CekButtonRole("update_account") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            'Cmd.CommandText = "Delete From Detail_Account where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Pisah1(0).Trim & "' and kode_acc = '" & Pisah2(0).Trim & "' and kode_detail_acc = '" & TextBox1.Text & "'"
            'Cmd.ExecuteNonQuery()
            'Cmd = Nothing

            SQL = "select kode_master_acc, kode_acc, kode_detail_acc, letak, jns from detail_account "
            SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_master_acc = '" & Pisah1(0).Trim & "' and "
            'SQL = SQL & "kode_acc = '" & Pisah2(0).Trim & "' and "
            SQL = SQL & "kode_acc + kode_detail_acc = '" & TextBox1.Text.Trim & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If dr("letak") = "LR" Then
                        SQL = "delete from lr_lokasi where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_account = '" & dr("kode_master_acc") & dr("kode_acc") & dr("kode_detail_acc") & "' "

                        dr.Close()

                        ExecuteTrans(SQL)
                    Else
                        SQL = "delete from " & dr("jns") & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                        SQL = SQL & "kode_account = '" & dr("kode_master_acc") & dr("kode_acc") & dr("kode_detail_acc") & "' "

                        dr.Close()

                        ExecuteTrans(SQL)
                    End If
                End If
                Cmd.CommandText = "Delete From Detail_Account where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Pisah1(0).Trim & "' and kode_acc = '" & Strings.Left(TextBox1.Text.Trim, 1) & "' and kode_detail_acc = '" & Strings.Mid(TextBox1.Text.Trim, 2) & "'"
                Cmd.ExecuteNonQuery()
                Cmd = Nothing
            End Using
            CloseConn()
        Else
            MessageBox.Show("Penghapusan dibatalkan . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        Kosong()
        ComboBox2.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode master account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
            'ElseIf ComboBox3.Text.Trim.Length = 0 Then
            '    MessageBox.Show("Kode account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    ComboBox2.Focus() : Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode detail Account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
            'ElseIf TextBox1.Text.Trim.Length <> 5 Then
            '    MessageBox.Show("Kode detail Account harus 5 digit . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            '    TextBox1.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        ElseIf ComboBox5.SelectedIndex = -1 Then
            MessageBox.Show("Lokasi harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox5.Focus() : Exit Sub
        ElseIf ComboBox6.SelectedIndex = -1 Then
            MessageBox.Show("Letak harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox6.Focus() : Exit Sub
        ElseIf ComboBox9.SelectedIndex = -1 Then
            MessageBox.Show("Aktif harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox9.Focus() : Exit Sub
        ElseIf ComboBox11.SelectedIndex = -1 Then
            MessageBox.Show("Flag khusus harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox11.Focus() : Exit Sub
        ElseIf ComboBox12.SelectedIndex = -1 Then
            MessageBox.Show("Flag biaya harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox12.Focus() : Exit Sub
        ElseIf ComboBox13.SelectedIndex = -1 Then
            MessageBox.Show("Flag Pbk harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox13.Focus() : Exit Sub
        End If

        If TextBox5.Enabled = True Then
            If Val(TextBox5.Text) = 0 Then
                MessageBox.Show("Lama budget harian harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox5.Focus() : Exit Sub
            ElseIf Val(TextBox6.Text) = 0 Then
                MessageBox.Show("Nilai budget harian harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox6.Focus() : Exit Sub
            ElseIf Val(TextBox7.Text) = 0 Then
                MessageBox.Show("Lama budget bulanan harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox7.Focus() : Exit Sub
            ElseIf Val(TextBox8.Text) = 0 Then
                MessageBox.Show("Nilai budget bulanan harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox6.Focus() : Exit Sub
            ElseIf Val(TextBox5.Text) > Val(TextBox7.Text) Then
                MessageBox.Show("Lama budget harian tidak boleh lebih besar dari lama budget bulanan . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox6.Focus() : Exit Sub
            ElseIf Val(TextBox6.Text) > Val(TextBox8.Text) Then
                MessageBox.Show("Nilai budget harian tidak boleh lebih besar dari nilai budget bulanan . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                TextBox6.Focus() : Exit Sub
            End If
        End If

        If ComboBox6.SelectedIndex = 0 Then 'lr
            If ComboBox7.SelectedIndex = -1 Then
                MessageBox.Show("LR harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox7.Focus() : Exit Sub
            End If
        Else 'neraca
            If ComboBox8.SelectedIndex = -1 Then
                MessageBox.Show("Neraca harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ComboBox8.Focus() : Exit Sub
            End If
        End If

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            Pisah1 = ComboBox2.Text.Split("-")
            'Pisah2 = ComboBox3.Text.Split("-")

            Dim jns As String = ""
            If ComboBox6.SelectedIndex = 0 Then 'lr
                jns = ComboBox7.Text
            Else
                jns = ComboBox8.Text
            End If

            Dim lama_budget_harian As Integer = 0
            Dim nilai_budget_harian As Double = 0
            Dim lama_budget_bulanan As Integer = 0
            Dim nilai_budget_bulanan As Double = 0

            If ComboBox10.SelectedIndex = 0 Then 'ya
                lama_budget_harian = TextBox5.Text
                nilai_budget_harian = TextBox6.Text
                lama_budget_bulanan = TextBox7.Text
                nilai_budget_bulanan = TextBox8.Text
            Else 'tdk
                lama_budget_harian = 0
                nilai_budget_harian = 0
                lama_budget_bulanan = 0
                nilai_budget_bulanan = 0
            End If

            If Button1.Text = "&Simpan" Then
                If ComboBox9.Text = "Y" Then
                    If ComboBox12.Text = "Y" Then
                        SQL = "Insert Into Detail_Account(Kode_Perusahaan, Kode_Master_Acc, Kode_Acc, "
                        SQL = SQL & "Kode_Detail_Acc, Keterangan, Posisi, lokasi, letak, jns, aktif, "
                        SQL = SQL & "pakai_budget, lama_budget_harian, lama_budget_bulanan, "
                        SQL = SQL & "budget_harian, budget_bulanan, flag_khusus, flag_biaya, flag_pbk"
                        SQL = SQL & ",Kode_Account) Values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & Pisah1(0).Trim & "', "
                        SQL = SQL & "'" & Strings.Left(TextBox1.Text.Trim, 1) & "', '" & Strings.Mid(TextBox1.Text.Trim, 2) & "', "
                        SQL = SQL & "'" & TextBox2.Text.ToUpper & "', "
                        SQL = SQL & "'" & Strings.Left(ComboBox4.Text, 1) & "', "
                        SQL = SQL & "'" & ComboBox5.Text & "', '" & ComboBox6.Text & "',  "
                        SQL = SQL & "'" & jns & "', '" & ComboBox9.Text & "', '" & ComboBox10.Text & "', "
                        SQL = SQL & "'" & lama_budget_harian & "', '" & lama_budget_bulanan & "', "
                        SQL = SQL & "'" & nilai_budget_harian & "', '" & nilai_budget_bulanan & "', '" & ComboBox11.Text & "','" & ComboBox12.Text & "','" & ComboBox13.Text & "',"
                        SQL = SQL & "'" & Pisah1(0).Trim & TextBox1.Text.Trim & "')"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "Insert Into Detail_Account(Kode_Perusahaan, Kode_Master_Acc, Kode_Acc, "
                        SQL = SQL & "Kode_Detail_Acc, Keterangan, Posisi, lokasi, letak, jns, aktif, "
                        SQL = SQL & "pakai_budget, lama_budget_harian, lama_budget_bulanan, "
                        SQL = SQL & "budget_harian, budget_bulanan, flag_khusus, flag_biaya, flag_pbk"
                        SQL = SQL & ",Kode_Account) Values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & Pisah1(0).Trim & "', "
                        SQL = SQL & "'" & Strings.Left(TextBox1.Text.Trim, 1) & "', '" & Strings.Mid(TextBox1.Text.Trim, 2) & "', "
                        SQL = SQL & "'" & TextBox2.Text.ToUpper & "', "
                        SQL = SQL & "'" & Strings.Left(ComboBox4.Text, 1) & "', "
                        SQL = SQL & "'" & ComboBox5.Text & "', '" & ComboBox6.Text & "',  "
                        SQL = SQL & "'" & jns & "', '" & ComboBox9.Text & "', '" & ComboBox10.Text & "', "
                        SQL = SQL & "'" & lama_budget_harian & "', '" & lama_budget_bulanan & "', "
                        SQL = SQL & "'" & nilai_budget_harian & "', '" & nilai_budget_bulanan & "', '" & ComboBox11.Text & "',NULL,'" & ComboBox13.Text & "',"
                        SQL = SQL & "'" & Pisah1(0).Trim & TextBox1.Text.Trim & "')"
                        ExecuteTrans(SQL)
                    End If
                Else
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Untuk Proses Simpan Aktif = Y ", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
                'SQL = "Insert Into Detail_Account(Kode_Perusahaan, Kode_Master_Acc, Kode_Acc, "
                'SQL = SQL & "Kode_Detail_Acc, Keterangan, Posisi, saldo, lokasi, letak, jns, aktif, "
                'SQL = SQL & "pakai_budget, lama_budget_harian, lama_budget_bulanan, "
                'SQL = SQL & "budget_harian, budget_bulanan, flag_khusus, flag_biaya"
                'SQL = SQL & ") Values("
                'SQL = SQL & "'" & KodePerusahaan & "', '" & Pisah1(0).Trim & "', "
                'SQL = SQL & "'" & Pisah2(0).Trim & "', '" & TextBox1.Text.Trim & "', "
                'SQL = SQL & "'" & TextBox2.Text.ToUpper & "', "
                'SQL = SQL & "'" & Strings.Left(ComboBox4.Text, 1) & "', '" & TextBox4.Text & "', "
                'SQL = SQL & "'" & ComboBox5.Text & "', '" & ComboBox6.Text & "',  "
                'SQL = SQL & "'" & jns & "', '" & ComboBox9.Text & "', '" & ComboBox10.Text & "', "
                'SQL = SQL & "'" & lama_budget_harian & "', '" & lama_budget_bulanan & "', "
                'SQL = SQL & "'" & nilai_budget_harian & "', '" & nilai_budget_bulanan & "', '" & ComboBox11.Text & "','" & ComboBox12.Text & "')"
                'ExecuteTrans(SQL)
                'If ComboBox12.Text = "Y" Then
                '    SQL = "Insert Into Detail_Account(Kode_Perusahaan, Kode_Master_Acc, Kode_Acc, "
                '    SQL = SQL & "Kode_Detail_Acc, Keterangan, Posisi, saldo, lokasi, letak, jns, aktif, "
                '    SQL = SQL & "pakai_budget, lama_budget_harian, lama_budget_bulanan, "
                '    SQL = SQL & "budget_harian, budget_bulanan, flag_khusus, flag_biaya"
                '    SQL = SQL & ") Values("
                '    SQL = SQL & "'" & KodePerusahaan & "', '" & Pisah1(0).Trim & "', "
                '    SQL = SQL & "'" & Pisah2(0).Trim & "', '" & TextBox1.Text.Trim & "', "
                '    SQL = SQL & "'" & TextBox2.Text.ToUpper & "', "
                '    SQL = SQL & "'" & Strings.Left(ComboBox4.Text, 1) & "', '" & TextBox4.Text & "', "
                '    SQL = SQL & "'" & ComboBox5.Text & "', '" & ComboBox6.Text & "',  "
                '    SQL = SQL & "'" & jns & "', '" & ComboBox9.Text & "', '" & ComboBox10.Text & "', "
                '    SQL = SQL & "'" & lama_budget_harian & "', '" & lama_budget_bulanan & "', "
                '    SQL = SQL & "'" & nilai_budget_harian & "', '" & nilai_budget_bulanan & "', '" & ComboBox11.Text & "','" & ComboBox12.Text & "')"
                '    ExecuteTrans(SQL)
                'Else
                '    SQL = "Insert Into Detail_Account(Kode_Perusahaan, Kode_Master_Acc, Kode_Acc, "
                '    SQL = SQL & "Kode_Detail_Acc, Keterangan, Posisi, saldo, lokasi, letak, jns, aktif, "
                '    SQL = SQL & "pakai_budget, lama_budget_harian, lama_budget_bulanan, "
                '    SQL = SQL & "budget_harian, budget_bulanan, flag_khusus, flag_biaya"
                '    SQL = SQL & ") Values("
                '    SQL = SQL & "'" & KodePerusahaan & "', '" & Pisah1(0).Trim & "', "
                '    SQL = SQL & "'" & Pisah2(0).Trim & "', '" & TextBox1.Text.Trim & "', "
                '    SQL = SQL & "'" & TextBox2.Text.ToUpper & "', "
                '    SQL = SQL & "'" & Strings.Left(ComboBox4.Text, 1) & "', '" & TextBox4.Text & "', "
                '    SQL = SQL & "'" & ComboBox5.Text & "', '" & ComboBox6.Text & "',  "
                '    SQL = SQL & "'" & jns & "', '" & ComboBox9.Text & "', '" & ComboBox10.Text & "', "
                '    SQL = SQL & "'" & lama_budget_harian & "', '" & lama_budget_bulanan & "', "
                '    SQL = SQL & "'" & nilai_budget_harian & "', '" & nilai_budget_bulanan & "', '" & ComboBox11.Text & "',NULL)"
                '    ExecuteTrans(SQL)
                'End If

                If ComboBox9.Text = "Y" Then
                    If ComboBox6.SelectedIndex = 0 Then 'lr
                        SQL = "Insert Into LR_lokasi(kode_perusahaan, lokasi, kode_account, ket) values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & ComboBox5.Text & "', "
                        SQL = SQL & "'" & Pisah1(0).Trim & TextBox1.Text.Trim & "', "
                        SQL = SQL & "'" & ComboBox7.Text & "')"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "Insert Into " & ComboBox8.Text & "(kode_perusahaan,kode_account) values("
                        SQL = SQL & "'" & KodePerusahaan & "', "
                        SQL = SQL & "'" & Pisah1(0).Trim & TextBox1.Text.Trim & "')"
                        ExecuteTrans(SQL)
                    End If
                End If

            Else
                If CekButtonRole("update_account") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "select kode_master_acc, kode_acc, kode_detail_acc, letak, jns from detail_account "
                SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_master_acc = '" & Pisah1(0).Trim & "' and "
                'SQL = SQL & "kode_acc = '" & Pisah2(0).Trim & "' and "
                SQL = SQL & "kode_acc + kode_detail_acc = '" & TextBox1.Text.Trim & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        If dr("letak") = "LR" Then
                            SQL = "delete from lr_lokasi where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_account = '" & dr("kode_master_acc") & dr("kode_acc") & dr("kode_detail_acc") & "' "

                            dr.Close()

                            ExecuteTrans(SQL)
                        Else
                            SQL = "delete from " & dr("jns") & " where kode_perusahaan = '" & KodePerusahaan & "' and "
                            SQL = SQL & "kode_account = '" & dr("kode_master_acc") & dr("kode_acc") & dr("kode_detail_acc") & "' "

                            dr.Close()

                            ExecuteTrans(SQL)
                        End If
                    End If
                End Using

                If ComboBox12.Text = "Y" Then
                    SQL = "Update Detail_Account Set Keterangan = '" & TextBox2.Text.ToUpper & "', "
                    SQL = SQL & "posisi = '" & Strings.Left(ComboBox4.Text, 1) & "', "
                    SQL = SQL & "lokasi = '" & ComboBox5.Text & "', "
                    SQL = SQL & "letak = '" & ComboBox6.Text & "', jns = '" & jns & "', "
                    SQL = SQL & "aktif = '" & ComboBox9.Text & "', "
                    SQL = SQL & "pakai_budget = '" & ComboBox10.Text & "', "
                    SQL = SQL & "lama_budget_harian = '" & lama_budget_harian & "', "
                    SQL = SQL & "lama_budget_bulanan = '" & lama_budget_bulanan & "', "
                    SQL = SQL & "budget_harian = '" & nilai_budget_harian & "', "
                    SQL = SQL & "budget_bulanan = '" & nilai_budget_bulanan & "', flag_khusus = '" & ComboBox11.Text & "', flag_biaya = '" & ComboBox12.Text & "',flag_pbk = '" & ComboBox13.Text & "' "
                    SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_master_acc = '" & Pisah1(0).Trim & "' and "
                    SQL = SQL & "kode_acc = '" & Strings.Left(TextBox1.Text.Trim, 1) & "' and "
                    SQL = SQL & "kode_detail_acc = '" & Strings.Mid(TextBox1.Text.Trim, 2) & "'"
                    ExecuteTrans(SQL)
                Else
                    SQL = "Update Detail_Account Set Keterangan = '" & TextBox2.Text.ToUpper & "', "
                    SQL = SQL & "posisi = '" & Strings.Left(ComboBox4.Text, 1) & "', "
                    SQL = SQL & "lokasi = '" & ComboBox5.Text & "', "
                    SQL = SQL & "letak = '" & ComboBox6.Text & "', jns = '" & jns & "', "
                    SQL = SQL & "aktif = '" & ComboBox9.Text & "', "
                    SQL = SQL & "pakai_budget = '" & ComboBox10.Text & "', "
                    SQL = SQL & "lama_budget_harian = '" & lama_budget_harian & "', "
                    SQL = SQL & "lama_budget_bulanan = '" & lama_budget_bulanan & "', "
                    SQL = SQL & "budget_harian = '" & nilai_budget_harian & "', "
                    SQL = SQL & "budget_bulanan = '" & nilai_budget_bulanan & "', flag_khusus = '" & ComboBox11.Text & "', flag_biaya = NULL,flag_pbk = '" & ComboBox13.Text & "' "
                    SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_master_acc = '" & Pisah1(0).Trim & "' and "
                    SQL = SQL & "kode_acc = '" & Strings.Left(TextBox1.Text.Trim, 1) & "' and "
                    SQL = SQL & "kode_detail_acc = '" & Strings.Mid(TextBox1.Text.Trim, 2) & "'"
                    ExecuteTrans(SQL)
                End If
                'SQL = "Update Detail_Account Set Keterangan = '" & TextBox2.Text.ToUpper & "', "
                'SQL = SQL & "posisi = '" & Strings.Left(ComboBox4.Text, 1) & "', "
                'SQL = SQL & "Saldo = " & TextBox4.Text & ", lokasi = '" & ComboBox5.Text & "', "
                'SQL = SQL & "letak = '" & ComboBox6.Text & "', jns = '" & jns & "', "
                'SQL = SQL & "aktif = '" & ComboBox9.Text & "', "
                'SQL = SQL & "pakai_budget = '" & ComboBox10.Text & "', "
                'SQL = SQL & "lama_budget_harian = '" & lama_budget_harian & "', "
                'SQL = SQL & "lama_budget_bulanan = '" & lama_budget_bulanan & "', "
                'SQL = SQL & "budget_harian = '" & nilai_budget_harian & "', "
                'SQL = SQL & "budget_bulanan = '" & nilai_budget_bulanan & "', flag_khusus = '" & ComboBox11.Text & "', flag_biaya = '" & ComboBox12.Text & "' "
                'SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                'SQL = SQL & "kode_master_acc = '" & Pisah1(0).Trim & "' and "
                'SQL = SQL & "kode_acc = '" & Pisah2(0).Trim & "' and "
                'SQL = SQL & "kode_detail_acc = '" & TextBox1.Text.Trim & "'"
                'ExecuteTrans(SQL)

                If ComboBox9.Text = "Y" Then
                    If ComboBox6.SelectedIndex = 0 Then 'lr
                        SQL = "Insert Into LR_lokasi(kode_perusahaan, lokasi, kode_account, ket) values("
                        SQL = SQL & "'" & KodePerusahaan & "', '" & ComboBox5.Text & "', "
                        SQL = SQL & "'" & Pisah1(0).Trim & TextBox1.Text.Trim & "', "
                        SQL = SQL & "'" & ComboBox7.Text & "')"
                        ExecuteTrans(SQL)
                    Else
                        SQL = "Insert Into " & ComboBox8.Text & "(kode_perusahaan,kode_account) values("
                        SQL = SQL & "'" & KodePerusahaan & "', "
                        SQL = SQL & "'" & Pisah1(0).Trim & TextBox1.Text.Trim & "')"
                        ExecuteTrans(SQL)
                    End If
                End If
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
        ComboBox2.Focus()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        ListView1.Items.Clear()

        Try
            OpenConn()

            SQL = "Select * From Detail_Account where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "" & ComboBox1.Text & " Like '%" & TextBox3.Text & "%' order by keterangan"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(Dr("Kode_master_acc"))
                    'Lvw.SubItems.Add(dr("Kode_acc"))
                    Lvw.SubItems.Add(Dr("Kode_acc") & Dr("kode_detail_acc"))
                    Lvw.SubItems.Add(Dr("Kode_account"))
                    Lvw.SubItems.Add(Dr("Keterangan"))
                    Lvw.SubItems.Add(Dr("posisi"))
                    Lvw.SubItems.Add(Dr("lokasi"))
                    Lvw.SubItems.Add(Dr("letak"))
                    Lvw.SubItems.Add(Dr("jns"))
                    Lvw.SubItems.Add(Dr("aktif"))
                    Lvw.SubItems.Add(Dr("flag_khusus"))
                    If General_Class.CekNULL(Dr("flag_biaya")) = "" Then
                        Lvw.SubItems.Add("T")
                    Else
                        Lvw.SubItems.Add(General_Class.CekNULL(Dr("flag_biaya")))
                    End If
                    Lvw.SubItems.Add(Dr("flag_pbk"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Button5_Click(TextBox3, e)
    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub

        For i As Integer = 0 To ComboBox2.Items.Count - 1
            Pisah1 = ComboBox2.Items(i).Split("-")
            If Trim(Pisah1(0)) = ListView1.Items(ListView1.FocusedItem.Index).Text Then
                ComboBox2.SelectedIndex = i
                Exit For
            End If
        Next

        'For i As Integer = 0 To ComboBox3.Items.Count - 1
        '    Pisah1 = ComboBox3.Items(i).Split("-")
        '    If Trim(Pisah1(0)) = ListView1.Items(ListView1.FocusedItem.Index).SubItems(1).Text Then
        '        ComboBox3.SelectedIndex = i
        '        Exit For
        '    End If
        'Next

        TextBox1.Text = ListView1.FocusedItem.SubItems(1).Text ' Replace(ListView1.Items(ListView1.FocusedItem.Index).SubItems(1).Text, ".", "")
        If ListView1.Items(ListView1.FocusedItem.Index).SubItems(2).Text = "D" Then
            ComboBox4.SelectedIndex = 0
        Else
            ComboBox4.SelectedIndex = 1
        End If
        TextBox1_Leave(ListView1, e)
    End Sub

    Private Sub ComboBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox5.Focus()
    End Sub

    Private Sub ComboBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox6.KeyPress
        If e.KeyChar = Chr(13) Then
            If ComboBox7.Enabled = True Then
                ComboBox7.Focus()
            Else
                ComboBox8.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
        If ComboBox6.SelectedIndex = 0 Then 'LR
            ComboBox7.Enabled = True
            ComboBox8.Enabled = False
            ComboBox7.SelectedIndex = -1
            ComboBox8.SelectedIndex = -1
        Else
            ComboBox7.Enabled = False
            ComboBox8.Enabled = True
            ComboBox7.SelectedIndex = -1
            ComboBox8.SelectedIndex = -1
        End If
    End Sub
    Private Sub ComboBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox6.Focus()
    End Sub

    Private Sub ComboBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox7.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox9.Focus()
    End Sub

    Private Sub ComboBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox8.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox9.Focus()
    End Sub

    Private Sub ComboBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox9.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox10.Focus()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub ComboBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox10.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox5.Enabled = True Then
                TextBox5.Focus()
            Else
                ComboBox11.Focus()
            End If
        End If
    End Sub

    Private Sub ComboBox10_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox10.SelectedIndexChanged
        If ComboBox10.SelectedIndex = 0 Then 'y
            TextBox5.Text = "" : TextBox6.Text = ""
            TextBox7.Text = "" : TextBox8.Text = ""

            TextBox5.Enabled = True : TextBox6.Enabled = True
            TextBox7.Enabled = True : TextBox8.Enabled = True
        Else
            TextBox5.Text = "" : TextBox6.Text = ""
            TextBox7.Text = "" : TextBox8.Text = ""

            TextBox5.Enabled = False : TextBox6.Enabled = False
            TextBox7.Enabled = False : TextBox8.Enabled = False
        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then TextBox6.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged

    End Sub

    Private Sub TextBox6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Chr(13) Then TextBox7.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Chr(13) Then TextBox8.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox8.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox11.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox11.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox12.Focus()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub ComboBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox12.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox13.Focus()
    End Sub

    Private Sub ComboBox13_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox13.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

End Class