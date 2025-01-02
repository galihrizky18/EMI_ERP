Public Class Detail_Account_New2
    Dim Pisah1(), Pisah2() As String
    Dim no_urut As Integer

    Private Sub Detail_Account_Sementara_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ListView1.Columns.Add("Kode M.Acc", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Acc", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode D.Acc", 80, HorizontalAlignment.Center)
        ListView1.Columns.Add("Nama Account", 200, HorizontalAlignment.Left)
        ListView1.Columns.Add("Posisi", 70, HorizontalAlignment.Center)
        ListView1.Columns.Add("Saldo", 80, HorizontalAlignment.Right)
        ListView1.Columns.Add("Lokasi", 100, HorizontalAlignment.Left)
        ListView1.Columns.Add("Letak", 70, HorizontalAlignment.Left)
        ListView1.Columns.Add("Jns", 120, HorizontalAlignment.Left)
        ListView1.Columns.Add("Pakai Bukti Potong", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Kategori Account", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Grouping Account", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Detail Grouping Account 1", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Kode Detail Grouping Account 2", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Flag Biaya", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Flag PBK", 100, HorizontalAlignment.Center)
        ListView1.Columns.Add("Flag Khusus", 100, HorizontalAlignment.Center)
        ListView1.View = View.Details

        ListView2.Columns.Add("", 30, HorizontalAlignment.Center)
        ListView2.Columns.Add("Divisi", 100, HorizontalAlignment.Left)
        ListView2.View = View.Details

        'Wet Production, Dry Pruduction, Warehouse, Enginering, hse, logistik, marketing, "FAT-HC-GA", "Qa & Qc"

        Dim items() As String = {"Wet Production", "Dry Production", "Warehouse", "Enginering", "HSE", "Logistik", "Marketing", "FAT-HC-GA", "Qa & Qc"}

        For Each itemText As String In items
            Dim item As New ListViewItem

            item = ListView2.Items.Add("")
            item.SubItems.Add(itemText)

        Next


        Kosong()
        ComboBox2.Focus()
    End Sub

    Private Sub Kosong()
        Try
            OpenConn()

            ComboBox2.Items.Clear()
            Cmd.CommandText = "select * from Master_Acc where kode_perusahaan = '" & KodePerusahaan & "' Order by kode_master_acc"
            Dr = Cmd.ExecuteReader
            Do While Dr.Read
                ComboBox2.Items.Add(Dr("Kode_Master_Acc"))
            Loop
            CloseDr()
            ComboBox2.Enabled = False

            ComboBox3.Items.Clear()
            ComboBox3.Enabled = False

            TextBox1.Enabled = False

            ComboBox5.Items.Clear()
            SQL = "select kode_stock_owner from stock_owner where kode_perusahaan = '" & KodePerusahaan & "' order by kode_stock_owner"
            Using Dr = Open(SQL)
                Do While Dr.Read
                    ComboBox5.Items.Add(Dr("kode_stock_owner"))
                Loop
            End Using

            ComboBox11.Items.Clear()
            SQL = "select Kode_kategori from kategori_account where kode_perusahaan = '" & KodePerusahaan & "' order by kode_kategori"
            Using Dr = Open(SQL)
                Do While Dr.Read
                    ComboBox11.Items.Add(Dr("kode_kategori"))
                Loop
            End Using

            ComboBox12.Items.Clear()
            SQL = "select Kode_Grouping from Grouping_account where kode_perusahaan= '" & KodePerusahaan & "' order by kode_Grouping"
            Using Dr = Open(SQL)
                Do While Dr.Read
                    ComboBox12.Items.Add(Dr("kode_Grouping"))
                Loop
            End Using

            ComboBox13.Items.Clear()
            ComboBox14.Items.Clear()

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

            ListView1.Items.Clear()
            SQL = "Select * From Detail_Account where kode_perusahaan = '" & KodePerusahaan & "' order by Kode_Master_Acc,Kode_Acc,kode_detail_acc"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("kode_master_acc"))
                    Lvw.SubItems.Add(dr("Kode_acc"))
                    Lvw.SubItems.Add(dr("Kode_detail_acc"))
                    'Lvw.SubItems.Add(Strings.Left(Dr("Kode_detail_acc"), 1) & "." & _
                    '                 Strings.Mid(Dr("kode_detail_acc"), 2))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    Lvw.SubItems.Add(dr("posisi"))
                    Lvw.SubItems.Add(dr("saldo"))
                    Lvw.SubItems.Add(dr("lokasi"))
                    Lvw.SubItems.Add(dr("letak"))
                    Lvw.SubItems.Add(dr("jns"))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Pakai_Bukti_Potong")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Kode_Kategori")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Kode_grouping")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Kode_detail_grouping_1")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Kode_detail_grouping_2")))
                    If General_Class.CekNULL(dr("Flag_Biaya")) <> "" Then
                        Lvw.SubItems.Add(General_Class.CekNULL(dr("Flag_Biaya")))
                    Else
                        Lvw.SubItems.Add("T")
                    End If
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("Flag_PBK")))
                    Lvw.SubItems.Add(General_Class.CekNULL(dr("flag_Khusus")))
                Loop
            End Using

            CloseDr()

            CloseConn()

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
            ComboBox10.SelectedIndex = -1

            ComboBox9.Items.Clear()
            ComboBox9.Items.Add("Y")
            ComboBox9.Items.Add("T")
            ComboBox9.SelectedIndex = -1

            ComboBox16.Items.Clear()
            ComboBox16.Items.Add("Y")
            ComboBox16.Items.Add("T")
            ComboBox16.SelectedIndex = -1

            ComboBox17.Items.Clear()
            ComboBox17.Items.Add("Y")
            ComboBox17.Items.Add("T")
            ComboBox17.SelectedIndex = -1

            ComboBox18.Items.Clear()
            ComboBox18.Items.Add("Y")
            ComboBox18.Items.Add("T")
            ComboBox18.SelectedIndex = -1

            TextBox5.Text = "" : TextBox6.Text = ""
            TextBox7.Text = "" : TextBox8.Text = ""

            TextBox5.Enabled = False : TextBox6.Enabled = False
            TextBox7.Enabled = False : TextBox8.Enabled = False

            TextBox2.Text = "" : TextBox3.Text = ""
            Button1.Text = "&Update"
            Button2.Enabled = False

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try


        'flag_khusus = "T"
        'Try
        '    OpenConn()

        '    If CekButtonRole("akun_khusus") = "Y" Then
        '        flag_khusus = ""
        '    Else
        '        flag_khusus = " and flag_khusus = 'T' "
        '    End If

        '   
        'End Try
    End Sub

    Private Sub ComboBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox3.Focus()
    End Sub

    Private Sub ComboBox3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub ComboBox15_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox4.Focus()
    End Sub

    Private Sub ComboBox4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox5.Focus()
    End Sub

    Private Sub ComboBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox6.Focus()
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

    Private Sub ComboBox7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox7.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox10.Focus()
    End Sub

    Private Sub ComboBox8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox8.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox10.Focus()
    End Sub

    Private Sub ComboBox10_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox10.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox5.Enabled = True Then
                TextBox5.Focus()
            Else
                ComboBox9.Focus()
            End If
        End If
    End Sub

    Private Sub TextBox5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox5.KeyPress
        If e.KeyChar = Chr(13) Then TextBox6.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
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
        If e.KeyChar = Chr(13) Then ComboBox9.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox9.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox11.Focus()
    End Sub

    Private Sub ComboBox11_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox11.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox12.Focus()
    End Sub

    Private Sub ComboBox12_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox12.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox13.Focus()
    End Sub

    Private Sub ComboBox13_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox13.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox14.Focus()
    End Sub

    Private Sub ComboBox14_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox14.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox16.Focus()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        OpenConn()

        Pisah1 = ComboBox2.Text.Split("-")
        ComboBox3.Items.Clear()
        Cmd.CommandText = "select * from Account Where kode_perusahaan = '" & KodePerusahaan & "' and kode_master_acc = '" & Pisah1(0).Trim & "' Order by kode_acc"
        Dr = Cmd.ExecuteReader
        Do While Dr.Read
            ComboBox3.Items.Add(Dr("Kode_Acc"))
        Loop
        CloseDr()

        CloseConn()
    End Sub

    Private Sub ComboBox15_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox1.Leave
        If ComboBox2.Text.Trim.Length = 0 Then Exit Sub
        If ComboBox3.Text.Trim.Length = 0 Then Exit Sub
        If TextBox1.Text.Trim.Length = 0 Then Exit Sub
        If ListView1.Items.Count = 0 Then Exit Sub

        'Try
        '    OpenConn()

        '    SQL = "Select * From Detail_Account_sementara Where kode_perusahaan = '" & KodePerusahaan & "' "
        '    SQL = SQL & "and kode_master_acc = '" & ComboBox2.Text & "' and kode_acc = '" & ComboBox3.Text & "' "
        '    SQL = SQL & "and kode_detail_acc = '" & ComboBox15.Text & "' and urut ='" & ListView1.FocusedItem.SubItems(12).Text & "'"
        '    Using Dr = Open(SQL)
        '        If Dr.Read Then
        '            TextBox2.Text = Dr("keterangan")
        '            If Dr("posisi") = "D" Then ComboBox4.SelectedIndex = 0 Else ComboBox4.SelectedIndex = 1
        '            ComboBox5.Text = Dr("lokasi")
        '            ComboBox6.Text = Dr("letak")
        '            If Dr("letak") = "LR" Then
        '                ComboBox7.Text = Dr("jns")
        '            Else
        '                ComboBox8.Text = Dr("jns")
        '            End If
        '            ComboBox10.Text = Dr("pakai_budget")
        '            If Dr("pakai_budget") = "Y" Then
        '                TextBox5.Text = Dr("lama_budget_harian") : TextBox6.Text = Dr("budget_harian")
        '                TextBox7.Text = Dr("lama_budget_bulanan") : TextBox8.Text = Dr("budget_bulanan")

        '                TextBox5.Enabled = True : TextBox6.Enabled = True
        '                TextBox7.Enabled = True : TextBox8.Enabled = True
        '            Else
        '                TextBox5.Text = "" : TextBox6.Text = ""
        '                TextBox7.Text = "" : TextBox8.Text = ""

        '                TextBox5.Enabled = False : TextBox6.Enabled = False
        '                TextBox7.Enabled = False : TextBox8.Enabled = False
        '            End If

        '            ComboBox9.Text = Dr("pakai_bukti_potong")
        '            ComboBox11.Text = Dr("kode_kategori")
        '            ComboBox12.Text = Dr("kode_grouping")
        '            ComboBox13.Text = Dr("kode_detail_grouping_1")
        '            ComboBox14.Text = Dr("kode_detail_grouping_2")
        '            no_urut = Dr("urut")

        '            Button1.Text = "&Update"
        '            Button2.Enabled = True
        '        Else
        '            TextBox2.Text = ""
        '            ComboBox4.SelectedIndex = -1
        '            ComboBox5.SelectedIndex = -1
        '            ComboBox6.SelectedIndex = 0
        '            ComboBox7.Enabled = True
        '            ComboBox7.SelectedIndex = -1
        '            ComboBox8.SelectedIndex = -1
        '            ComboBox9.SelectedIndex = -1
        '            ComboBox11.SelectedIndex = -1
        '            ComboBox12.SelectedIndex = -1
        '            ComboBox13.SelectedIndex = -1
        '            ComboBox14.SelectedIndex = -1
        '            Button1.Text = "&Simpan"
        '            Button2.Enabled = False
        '        End If
        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseTrans()
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        OpenConn()

        ListView1.Items.Clear()
        Cmd.CommandText = "Select * From Detail_Account where kode_perusahaan = '" & KodePerusahaan & "' and " & ComboBox1.Text & " like '%" & TextBox3.Text & "%' order by Kode_Master_Acc,kode_acc,kode_detail_acc"
        Dr = Cmd.ExecuteReader
        Do While Dr.Read
            Dim Lvw As ListViewItem
            Lvw = ListView1.Items.Add(Dr("kode_master_acc"))
            Lvw.SubItems.Add(Dr("Kode_acc"))
            Lvw.SubItems.Add(Dr("Kode_detail_acc"))
            'Lvw.SubItems.Add(Strings.Left(Dr("Kode_detail_acc"), 1) & "." & _
            '                 Strings.Mid(Dr("kode_detail_acc"), 2))
            Lvw.SubItems.Add(Dr("Keterangan"))
            Lvw.SubItems.Add(Dr("posisi"))
            Lvw.SubItems.Add(Dr("saldo"))
            Lvw.SubItems.Add(Dr("lokasi"))
            Lvw.SubItems.Add(Dr("letak"))
            Lvw.SubItems.Add(Dr("jns"))
            Lvw.SubItems.Add(General_Class.CekNULL(Dr("Pakai_Bukti_Potong")))
            Lvw.SubItems.Add(General_Class.CekNULL(Dr("Kode_Kategori")))
            Lvw.SubItems.Add(General_Class.CekNULL(Dr("Kode_grouping")))
            Lvw.SubItems.Add(General_Class.CekNULL(Dr("Kode_detail_grouping_1")))
            Lvw.SubItems.Add(General_Class.CekNULL(Dr("Kode_detail_grouping_2")))
            If General_Class.CekNULL(Dr("Flag_Biaya")) <> "" Then
                Lvw.SubItems.Add(General_Class.CekNULL(Dr("Flag_Biaya")))
            Else
                Lvw.SubItems.Add("T")
            End If
            Lvw.SubItems.Add(Dr("Flag_PBK"))
            Lvw.SubItems.Add(Dr("flag_Khusus"))
        Loop
        CloseDr()
        Cmd = Nothing

        CloseConn()
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

        For i As Integer = 0 To ComboBox3.Items.Count - 1
            Pisah1 = ComboBox3.Items(i).Split("-")
            If Trim(Pisah1(0)) = ListView1.Items(ListView1.FocusedItem.Index).SubItems(1).Text Then
                ComboBox3.SelectedIndex = i
                Exit For
            End If
        Next

        TextBox1.Text = Replace(ListView1.Items(ListView1.FocusedItem.Index).SubItems(2).Text, ".", "")
        'ComboBox2.Text = ListView1.FocusedItem.Text
        'ComboBox3.Text = ListView1.FocusedItem.SubItems(1).Text
        'ComboBox15.Text = ListView1.FocusedItem.SubItems(2).Text
        'If ListView1.Items(ListView1.FocusedItem.Index).SubItems(3).Text = "D" Then
        '    ComboBox4.SelectedIndex = 0
        'Else
        '    ComboBox4.SelectedIndex = 1
        'End If
        'ComboBox15_Leave(ListView1, e)
        Try
            OpenConn()

            SQL = "Select * From Detail_Account Where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and kode_master_acc = '" & ListView1.FocusedItem.Text & "' and kode_acc = '" & ListView1.FocusedItem.SubItems(1).Text & "' "
            SQL = SQL & "and kode_detail_acc = '" & ListView1.FocusedItem.SubItems(2).Text & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    ComboBox2.Text = Dr("kode_master_acc")
                    ComboBox3.Text = Dr("kode_acc")
                    TextBox1.Text = Dr("kode_detail_acc")
                    TextBox2.Text = Dr("keterangan")
                    If Dr("posisi") = "D" Then ComboBox4.SelectedIndex = 0 Else ComboBox4.SelectedIndex = 1
                    ComboBox5.Text = Dr("lokasi")
                    ComboBox6.Text = Dr("letak")
                    If Dr("letak") = "LR" Then
                        ComboBox7.Text = Dr("jns")
                    Else
                        ComboBox8.Text = Dr("jns")
                    End If
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

                    ComboBox9.Text = General_Class.CekNULL(Dr("pakai_bukti_potong"))
                    ComboBox11.Text = General_Class.CekNULL(Dr("kode_kategori"))
                    ComboBox12.Text = General_Class.CekNULL(Dr("kode_grouping"))
                    ComboBox13.Text = General_Class.CekNULL(Dr("kode_detail_grouping_1"))
                    ComboBox14.Text = General_Class.CekNULL(Dr("kode_detail_grouping_2"))
                    ComboBox16.Text = Dr("Flag_Khusus")
                    ComboBox17.Text = Dr("Flag_PBK")
                    If General_Class.CekNULL(Dr("Flag_Biaya")) <> "" Then
                        ComboBox18.Text = (General_Class.CekNULL(Dr("Flag_Biaya")))
                    Else
                        ComboBox18.Text = ("T")
                    End If

                    Button1.Text = "&Update"
                    Button2.Enabled = True
                    ComboBox2.Enabled = False
                    ComboBox3.Enabled = False
                    TextBox1.Enabled = False
                Else
                    TextBox2.Text = ""
                    ComboBox4.SelectedIndex = -1
                    ComboBox5.SelectedIndex = -1
                    ComboBox6.SelectedIndex = 0
                    ComboBox7.Enabled = True
                    ComboBox7.SelectedIndex = -1
                    ComboBox8.SelectedIndex = -1
                    ComboBox9.SelectedIndex = -1
                    ComboBox11.SelectedIndex = -1
                    ComboBox12.SelectedIndex = -1
                    ComboBox13.SelectedIndex = -1
                    ComboBox14.SelectedIndex = -1
                    ComboBox16.SelectedIndex = -1
                    ComboBox17.SelectedIndex = -1
                    ComboBox18.SelectedIndex = -1

                    Button2.Enabled = False
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox6_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox6.SelectedIndexChanged
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

    Private Sub ComboBox10_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox10.SelectedIndexChanged
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

    Private Sub ComboBox12_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox12.SelectedIndexChanged
        Try
            OpenConn()

            ComboBox13.Items.Clear()
            SQL = "select Kode_Detail_Grouping_1 from detail_Grouping_account_1 where kode_perusahaan = '" & KodePerusahaan & "' and kode_grouping = '" & ComboBox12.Text & "' order by kode_detail_Grouping_1"
            Using Dr = Open(SQL)
                Do While Dr.Read
                    ComboBox13.Items.Add(Dr("kode_detail_Grouping_1"))
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

    Private Sub ComboBox13_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBox13.SelectedIndexChanged
        Try
            OpenConn()

            ComboBox14.Items.Clear()
            SQL = "select Kode_Detail_Grouping_2 from detail_Grouping_account_2 where kode_perusahaan = '" & KodePerusahaan & "' and kode_grouping = '" & ComboBox12.Text & "' and Kode_Detail_Grouping_1 = '" & ComboBox13.Text & "' order by kode_detail_Grouping_2"
            Using Dr = Open(SQL)
                Do While Dr.Read
                    ComboBox14.Items.Add(Dr("kode_detail_Grouping_2"))
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

    Private Sub ComboBox16_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox16.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox17.Focus()
    End Sub

    Private Sub ComboBox17_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox17.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox18.Focus()
    End Sub

    Private Sub ComboBox18_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox18.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode master account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf ComboBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode detail Account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
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
            MessageBox.Show("Pakai Bukti Potong harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox9.Focus() : Exit Sub
        ElseIf ComboBox11.SelectedIndex = -1 Then
            MessageBox.Show("Kategori Account harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox11.Focus() : Exit Sub
        ElseIf ComboBox12.SelectedIndex = -1 Then
            MessageBox.Show("Kategori I harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox12.Focus() : Exit Sub
        ElseIf ComboBox13.SelectedIndex = -1 Then
            MessageBox.Show("Kategori II harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox13.Focus() : Exit Sub
        ElseIf ComboBox14.SelectedIndex = -1 Then
            MessageBox.Show("Kategori III harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox14.Focus() : Exit Sub
        ElseIf ComboBox18.SelectedIndex = -1 Then
            MessageBox.Show("flag Biaya harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox18.Focus() : Exit Sub
        ElseIf ComboBox16.SelectedIndex = -1 Then
            MessageBox.Show("Flag Khusus harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox16.Focus() : Exit Sub
        ElseIf ComboBox17.SelectedIndex = -1 Then
            MessageBox.Show("Flag PBK harus diisi . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox17.Focus() : Exit Sub
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
            Pisah2 = ComboBox3.Text.Split("-")

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

            If CekButtonRole("update_account") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            SQL = "select kode_master_acc, kode_acc, kode_detail_acc, letak, jns from detail_account "
            SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_master_acc = '" & ComboBox2.Text.Trim & "' and "
            SQL = SQL & "kode_acc = '" & ComboBox3.Text.Trim & "' and "
            SQL = SQL & "kode_detail_acc = '" & TextBox1.Text.Trim & "'"
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

            If ComboBox18.Text = "Y" Then

                SQL = "UPDATE Detail_Account SET Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & ",Kode_Master_Acc = '" & ComboBox2.Text.Trim & "' "
                SQL = SQL & ",Kode_Acc = '" & ComboBox3.Text.Trim & "'"
                SQL = SQL & ",Kode_Detail_Acc = '" & TextBox1.Text.Trim & "'"
                SQL = SQL & ",Keterangan = '" & TextBox2.Text.ToUpper & "'"
                SQL = SQL & ",Posisi = '" & Strings.Left(ComboBox4.Text.Trim, 1) & "'"
                SQL = SQL & ",Saldo = '0'"
                SQL = SQL & ",Lokasi = '" & ComboBox5.Text.Trim & "'"
                SQL = SQL & ",Flag_Biaya = '" & ComboBox18.Text.Trim & "'"
                SQL = SQL & ",Letak = '" & ComboBox6.Text.Trim & "'"
                SQL = SQL & ",Jns = '" & jns & "'"
                SQL = SQL & ",Aktif = 'Y'"
                SQL = SQL & ",Pakai_Budget = '" & ComboBox10.Text.Trim & "'"
                SQL = SQL & ",Lama_Budget_Harian = '" & lama_budget_harian & "'"
                SQL = SQL & ",Lama_Budget_Bulanan = '" & lama_budget_bulanan & "'"
                SQL = SQL & ",Budget_Harian = '" & nilai_budget_harian & "'"
                SQL = SQL & ",Budget_Bulanan = '" & nilai_budget_bulanan & "'"
                SQL = SQL & ",Flag_Khusus = '" & ComboBox16.Text.Trim & "'"
                SQL = SQL & ",Flag_Pbk = '" & ComboBox16.Text.Trim & "'"
                SQL = SQL & ",Pakai_Bukti_Potong = '" & ComboBox9.Text.Trim & "'"
                SQL = SQL & ",Kode_Kategori =  '" & ComboBox11.Text.Trim & "'"
                SQL = SQL & ",Kode_Grouping = '" & ComboBox12.Text.Trim & "'"
                SQL = SQL & ",Kode_Detail_Grouping_1 = '" & ComboBox13.Text.Trim & "'"
                SQL = SQL & ",Kode_Detail_Grouping_2 = '" & ComboBox14.Text.Trim & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Master_Acc = '" & ComboBox2.Text.Trim & "' "
                SQL = SQL & "and Kode_Acc = '" & ComboBox3.Text.Trim & "'"
                SQL = SQL & "and Kode_Detail_Acc = '" & TextBox1.Text.Trim & "'"
                ExecuteTrans(SQL)
            Else
                SQL = "UPDATE Detail_Account SET Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & ",Kode_Master_Acc = '" & ComboBox2.Text.Trim & "' "
                SQL = SQL & ",Kode_Acc = '" & ComboBox3.Text.Trim & "'"
                SQL = SQL & ",Kode_Detail_Acc = '" & TextBox1.Text.Trim & "'"
                SQL = SQL & ",Keterangan = '" & TextBox2.Text.ToUpper & "'"
                SQL = SQL & ",Posisi = '" & Strings.Left(ComboBox4.Text.Trim, 1) & "'"
                SQL = SQL & ",Saldo = '0'"
                SQL = SQL & ",Lokasi = '" & ComboBox5.Text.Trim & "'"
                SQL = SQL & ",Flag_Biaya = NULL "
                SQL = SQL & ",Letak = '" & ComboBox6.Text.Trim & "'"
                SQL = SQL & ",Jns = '" & jns & "'"
                SQL = SQL & ",Aktif = 'Y'"
                SQL = SQL & ",Pakai_Budget = '" & ComboBox10.Text.Trim & "'"
                SQL = SQL & ",Lama_Budget_Harian = '" & lama_budget_harian & "'"
                SQL = SQL & ",Lama_Budget_Bulanan = '" & lama_budget_bulanan & "'"
                SQL = SQL & ",Budget_Harian = '" & nilai_budget_harian & "'"
                SQL = SQL & ",Budget_Bulanan = '" & nilai_budget_bulanan & "'"
                SQL = SQL & ",Flag_Khusus = '" & ComboBox16.Text.Trim & "'"
                SQL = SQL & ",Flag_Pbk = '" & ComboBox16.Text.Trim & "'"
                SQL = SQL & ",Pakai_Bukti_Potong = '" & ComboBox9.Text.Trim & "'"
                SQL = SQL & ",Kode_Kategori =  '" & ComboBox11.Text.Trim & "'"
                SQL = SQL & ",Kode_Grouping = '" & ComboBox12.Text.Trim & "'"
                SQL = SQL & ",Kode_Detail_Grouping_1 = '" & ComboBox13.Text.Trim & "'"
                SQL = SQL & ",Kode_Detail_Grouping_2 = '" & ComboBox14.Text.Trim & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Master_Acc = '" & ComboBox2.Text.Trim & "' "
                SQL = SQL & "and Kode_Acc = '" & ComboBox3.Text.Trim & "'"
                SQL = SQL & "and Kode_Detail_Acc = '" & TextBox1.Text.Trim & "'"
                ExecuteTrans(SQL)
            End If

            'If ComboBox9.Text = "Y" Then
            If ComboBox6.SelectedIndex = 0 Then 'lr
                SQL = "Insert Into LR_lokasi(kode_perusahaan, lokasi, kode_account, ket) values("
                SQL = SQL & "'" & KodePerusahaan & "', '" & ComboBox5.Text & "', "
                SQL = SQL & "'" & Pisah1(0).Trim & Pisah2(0).Trim & TextBox1.Text.Trim & "', "
                SQL = SQL & "'" & ComboBox7.Text & "')"
                ExecuteTrans(SQL)
            Else
                SQL = "Insert Into " & ComboBox8.Text & "(kode_perusahaan,kode_account) values("
                SQL = SQL & "'" & KodePerusahaan & "', "
                SQL = SQL & "'" & Pisah1(0).Trim & Pisah2(0).Trim & TextBox1.Text.Trim & "')"
                ExecuteTrans(SQL)
            End If
            'End If

            MessageBox.Show("Kode account berhasil diupdate", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

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

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Kosong()
        ComboBox2.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs)

        'Try
        '    OpenConn()

        '    SQL = "select * from Emi_Divisi "

        '    Using Dr = OpenTrans(SQL)
        '        Do While Dr.Read



        '        Loop

        '    End Using

        '    CloseConn()
        'Catch ex As Exception
        '    CloseConn()
        '    MessageBox.Show(ex.Message)
        '    Exit Sub
        'End Try

    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim Hapus1 As String = MessageBox.Show("Anda yakin data ini akan dihapus . . ? ?", "Accounting", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Select top 1 * from Detail_jurnal where kode_perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and kode_master_acc = '" & ComboBox2.Text.Trim & "' "
                SQL = SQL & "and kode_acc = '" & ComboBox3.Text.Trim & "' "
                SQL = SQL & "and kode_detail_acc = '" & TextBox1.Text.Trim & "'"
                Using dr = OpenTrans(SQL)
                    If dr.Read Then
                        MessageBox.Show("Penghapusan tidak dapat dilakukan, karena masih dipakai di data detail jurnal", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Kosong()
                        ComboBox2.Focus()
                        Exit Sub
                    End If
                End Using

                If CekButtonRole("update_account") = "T" Then
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show("Anda tidak memiliki akses untuk memproses transaksi ini!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If

                SQL = "select kode_master_acc, kode_acc, kode_detail_acc, letak, jns from detail_account "
                SQL = SQL & "Where kode_perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "kode_master_acc = '" & ComboBox2.Text.Trim & "' and "
                SQL = SQL & "kode_acc = '" & ComboBox3.Text.Trim & "' and "
                SQL = SQL & "kode_detail_acc = '" & TextBox1.Text.Trim & "'"
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

                    SQL = "Delete From Detail_Account where kode_perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "kode_master_acc = '" & ComboBox2.Text.Trim & "' and "
                    SQL = SQL & "kode_acc = '" & ComboBox3.Text.Trim & "' and "
                    SQL = SQL & "kode_detail_acc = '" & TextBox1.Text.Trim & "'"
                    ExecuteTrans(SQL)
                End Using

                Cmd.Transaction.Commit()
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show("Penghapusan dibatalkan . . ! !", "Accounting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        Kosong()
        ComboBox2.Focus()
    End Sub
End Class