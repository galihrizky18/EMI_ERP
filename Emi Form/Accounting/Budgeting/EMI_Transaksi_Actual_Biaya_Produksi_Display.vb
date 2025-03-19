Imports System.Diagnostics.Eventing.Reader
Imports System.Web.Util
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class EMI_Transaksi_Actual_Biaya_Produksi_Display
    Dim arrcari, arr_tgl, arr_Lain As New ArrayList
    Dim Jenis = "Display_Transaksi_Formula"
    Private Sub Display_Trx_Formula_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            CheckBox3.Text = Base_Language.Lang_Global_Hari_ini
            CheckBox1.Text = Base_Language.Lang_Global_Para_Tbl
            CheckBox2.Text = Base_Language.Lang_Global_Para_lain
            Label1.Text = Base_Language.Lang_Display_Transaksi_Formula

            ListView1.Columns.Add(Base_Language.Lang_Display_Transaksi_Formula_No_Faktur, 120, HorizontalAlignment.Left)
            ListView1.Columns.Add("Jenis Biaya", 150, HorizontalAlignment.Center)
            ListView1.Columns.Add("Meteran", 150, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Jumlah, 110, HorizontalAlignment.Right)
            ListView1.Columns.Add(Base_Language.Lang_Global_Satuan, 130, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Tanggal, 110, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 110, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 130, HorizontalAlignment.Center)
            ListView1.Columns.Add("Tanggal Awal", 110, HorizontalAlignment.Center)
            ListView1.Columns.Add("Tanggal Akhir", 110, HorizontalAlignment.Center)
            ListView1.Columns.Add("Id User", 120, HorizontalAlignment.Center)

            ListView1.View = View.Details
            ListView1.Items.Clear()

            arr_tgl.Clear() : ComboBox3.Items.Clear()
            ComboBox3.Items.Add(Base_Language.Lang_Display_Transaksi_Formula_Tgl) : arr_tgl.Add("a.tanggal")


            arr_Lain.Clear() : ComboBox2.Items.Clear()
            ComboBox2.Items.Add(Base_Language.Lang_Display_Transaksi_Formula_No_Faktur) : arr_Lain.Add("a.no_faktur")
            ComboBox2.Items.Add("Jenis Biaya") : arr_Lain.Add("c.keterangan")
            ComboBox2.Items.Add("Meteran") : arr_Lain.Add("b.keterangan")



            ComboBox6.Items.Clear()
            ComboBox6.Items.Add("-- Seluruh --")
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
                    ComboBox6.Items.Add(dr("kode_stock_owner"))
                Loop
            End Using
            ComboBox6.Text = Lokasi

            CheckBox3.Checked = False
            CheckBox1.Checked = False
            CheckBox2.Checked = False
            ComboBox3.Enabled = False
            ComboBox3.SelectedIndex = -1
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            ComboBox2.Enabled = False
            TextBox4.Enabled = False
            TextBox4.Text = ""
            DateTimePicker1.Value = Now
            DateTimePicker2.Value = Now

            TabPage1.Text = Base_Language.Lang_Display_Transaksi_Formula_Detail_Step
            TabPage2.Text = Base_Language.Lang_Display_Transaksi_Formula_Komposisi



            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox1.Checked = False
            ComboBox3.SelectedIndex = -1
            ComboBox3.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            DateTimePicker1.Value = Now
            DateTimePicker2.Value = Now

            Button1_Click(CheckBox3, e)
        End If
    End Sub


    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox3.Checked = False
            ComboBox3.Enabled = True
            DateTimePicker1.Enabled = True
            DateTimePicker2.Enabled = True
        Else
            ComboBox3.SelectedIndex = -1
            ComboBox3.Enabled = False
            DateTimePicker1.Enabled = False
            DateTimePicker2.Enabled = False
            DateTimePicker1.Value = Now
            DateTimePicker2.Value = Now
        End If
    End Sub

    Private Sub BatalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalToolStripMenuItem.Click
        If ListView1.Items.Count = 0 Or ListView1.SelectedItems.Count = 0 Then
            MessageBox.Show(Base_Language.Lang_Display_Transaksi_Formula_Error_Batal, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Try
            OpenConn()
            SQL = "select status from EMI_Actual_Biaya_Produksi where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and no_faktur = '" & ListView1.FocusedItem.Text & "'"
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("status")) <> "" Then
                        dr.Close()
                        CloseConn()
                        MessageBox.Show(Base_Language.Lang_Display_Transaksi_Formula_Error_Tolak_Batal, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    Else
                        dr.Close()
                        SQL = "update EMI_Actual_Biaya_Produksi set status = 'Y' where kode_perusahaan = '" & KodePerusahaan & "' "
                        SQL = SQL & "and no_faktur = '" & ListView1.FocusedItem.Text & "'"
                        ExecuteTrans(SQL)

                    End If
                End If
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        MessageBox.Show(Base_Language.Lang_Global_Berhasil_Batal, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Button1_Click(BatalToolStripMenuItem, e)
        Exit Sub
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Paramater, Base_Language.Lang_Global_Perhatian)
            CheckBox1.Focus() : Exit Sub
        End If
        If CheckBox1.Checked Then
            If ComboBox3.SelectedIndex = -1 Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl, Base_Language.Lang_Global_Perhatian)
                ComboBox3.Focus() : Exit Sub
            ElseIf DateTimePicker1.Value > DateTimePicker2.Value Then
                MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Tgl2, Base_Language.Lang_Global_Perhatian)
                DateTimePicker1.Value = Now : DateTimePicker2.Value = Now
                Exit Sub
            End If
            If CheckBox2.Checked Then
                If ComboBox2.SelectedIndex = -1 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain, Base_Language.Lang_Global_Perhatian)
                    ComboBox2.Focus() : Exit Sub
                ElseIf TextBox4.Text.Trim.Length = 0 Then
                    MessageBox.Show(Base_Language.Lang_Global_Error_Paramater_Lain2, Base_Language.Lang_Global_Perhatian)
                    TextBox4.Focus() : Exit Sub
                End If
            End If
        End If

        Try
            OpenConn()
            ListView1.Items.Clear()
            DataGridView1.Rows.Clear()

            SQL = "select a.status,no_faktur,tanggal,jam,iduser,c.keterangan as jenis_biaya,b.Keterangan as meteran,a.Jumlah, a.Satuan, "
            SQL = SQL & "a.kode_barang,isnull((select top(1) nama from barang x where a.kode_perusahaan = x.kode_perusahaan and a.kode_barang = x.kode_barang ), null) as Nama, "
            SQL = SQL & "a.Tanggal_Awal, a.Tanggal_Akhir  from EMI_Actual_Biaya_Produksi a, EMI_Master_Meteran b, Emi_Jenis_Biaya_Produksi c "
            SQL = SQL & "where a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Meteran = b.Id_Meteran  "
            SQL = SQL & "and b.Kode_Perusahaan = c.Kode_Perusahaan and b.Id_Jenis_Biaya_Produksi = c.Id_Jenis_Biaya_Produksi "

            If ComboBox6.SelectedIndex = 0 Then
                SQL = SQL & " and a.lokasi in("
                Dim list_kota As String = ""
                For x As Integer = 1 To ComboBox6.Items.Count - 1
                    list_kota = list_kota & "'" & ComboBox6.Items(x).ToString & "', "
                Next

                list_kota = Strings.Left(list_kota, Len(list_kota) - 2)

                SQL = SQL & list_kota & ") "
            Else
                SQL = SQL & " and a.lokasi = '" & ComboBox6.Text & "' "
            End If

            If CheckBox3.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & "a.tanggal between '"
                SQL = SQL & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' and '" & Format(CDate(FMenu.ToolStripStatusLabel3.Text), "yyyy-MM-dd") & "' "
            End If

            If CheckBox1.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arr_tgl.Item(ComboBox3.SelectedIndex) & " between '"
                SQL = SQL & Format(DateTimePicker1.Value, "yyyy-MM-dd") & "' and '" & Format(DateTimePicker2.Value, "yyyy-MM-dd") & "' "
            End If

            If CheckBox2.Checked Then
                'Pasang And
                If Not Strings.Right(UCase(SQL), 6) = "WHERE " Then SQL = SQL & "AND "

                SQL = SQL & arr_Lain.Item(ComboBox2.SelectedIndex) & " like '%" & Trim(TextBox4.Text) & "%' "
            End If
            SQL = SQL & "order by a.tanggal desc"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    For i As Integer = 0 To .Rows.Count - 1
                        Dim Lvw As ListViewItem
                        Lvw = ListView1.Items.Add(.Rows(i).Item("no_faktur"))
                        Lvw.SubItems.Add(.Rows(i).Item("jenis_biaya"))
                        Lvw.SubItems.Add(.Rows(i).Item("meteran"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("jumlah"), "N2"))
                        Lvw.SubItems.Add(.Rows(i).Item("satuan"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal"), "dd MMM yyyy"))

                        If General_Class.CekNULL(.Rows(i).Item("kode_barang")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("kode_barang"))
                        End If

                        If General_Class.CekNULL(.Rows(i).Item("nama")) = "" Then
                            Lvw.SubItems.Add("-")
                        Else
                            Lvw.SubItems.Add(.Rows(i).Item("nama"))
                        End If
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_awal"), "dd MMM yyyy"))
                        Lvw.SubItems.Add(Format(.Rows(i).Item("tanggal_akhir"), "dd MMM yyyy"))

                        Lvw.SubItems.Add(.Rows(i).Item("iduser"))





                        ListView1.Items(i).ForeColor = Color.Blue

                        If General_Class.CekNULL(.Rows(i).Item("status")) <> "" Then
                            ListView1.Items(i).ForeColor = Color.Black
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



    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            ComboBox2.Enabled = True
            TextBox4.Enabled = True
        Else
            ComboBox2.Enabled = False
            TextBox4.Enabled = False
            ComboBox2.SelectedIndex = -1
            TextBox4.Text = ""
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.Items.Count = 0 Then Exit Sub
        Try
            OpenConn()

            DataGridView1.Rows.Clear()
            SQL = "select No_step,Tipe,Kode,Deskripsi,Jumlah,Satuan,Persentase from EMI_Transaksi_Formulator_Detail_Step "
            SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Faktur = '" & ListView1.FocusedItem.Text & "'"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1
                            DataGridView1.Rows.Add(1)
                            DataGridView1.Rows.Item(i).Cells(0).Value = .Rows(i).Item("no_step")
                            DataGridView1.Rows.Item(i).Cells(1).Value = .Rows(i).Item("tipe")
                            DataGridView1.Rows.Item(i).Cells(2).Value = .Rows(i).Item("kode")
                            DataGridView1.Rows.Item(i).Cells(3).Value = .Rows(i).Item("deskripsi")
                            DataGridView1.Rows.Item(i).Cells(4).Value = .Rows(i).Item("jumlah")
                            DataGridView1.Rows.Item(i).Cells(5).Value = .Rows(i).Item("satuan")
                            DataGridView1.Rows.Item(i).Cells(6).Value = .Rows(i).Item("persentase")
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
End Class