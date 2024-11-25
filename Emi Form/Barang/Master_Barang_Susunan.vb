Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar


Public Class Master_Barang_Susunan
    Dim arrcari, arrid As New ArrayList
    Dim Jenis = "Master_Barang_Susunan"
    Private Sub kosong()
        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
        Btn_Cari.Text = Base_Language.Lang_Global_Cari
        Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False

        Try

            OpenConn()

            TextBox1.Focus()
            TextBox1.Text = ""
            TextBox4.Text = ""
            ComboBox3.Items.Clear()
            ComboBox3.Items.Add("Tier")
            ComboBox3.Items.Add("Tumpukan")
            ComboBox3.SelectedIndex = -1
            TextBox1.Enabled = True
            TextBox4.Enabled = True
            ComboBox3.Enabled = True
            ComboBox4.Items.Clear() : arrid.Clear()
            SQL = "select Id_WMS_Pallet,Keterangan from EMI_WMS_Pallet order by Keterangan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox4.Items.Add(dr("Keterangan")) : arrid.Add(dr("Id_WMS_Pallet"))
                Loop
            End Using
            ComboBox4.SelectedIndex = -1
            TextBoxP.Text = ""
            TextBox4.Text = ""
            TextBoxL.Text = ""
            TextBoxT.Text = ""
            ComboBox2.Items.Clear() : ComboBox5.Items.Clear()
            SQL = "select Satuan from EMI_Satuan where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by Satuan"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    ComboBox5.Items.Add(dr("Satuan"))
                Loop
            End Using


            ComboBox5.Enabled = True
            SQL = "select Satuan_Panjang_default from init where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                If dr.Read Then
                    If General_Class.CekNULL(dr("Satuan_Panjang_default")) <> "" Then
                        ComboBox5.Text = dr("Satuan_Panjang_default")
                        ComboBox5.Enabled = False
                    End If
                End If
            End Using

            ComboBox2.SelectedIndex = -1 ': ComboBox5.SelectedIndex = -1
            TextBox6.Text = ""
            TextBox7.Text = ""
            TextBox3.Text = ""
            TextBox8.Text = ""
            CheckBox1.Checked = False
            ComboBox1.Items.Clear() : arrcari.Clear()
            ComboBox1.Items.Add(Base_Language.Lang_Global_KodeBarang) : arrcari.Add("a.Kode_Barang")
            ComboBox1.Items.Add(Base_Language.Lang_Global_NamaBarang) : arrcari.Add("b.Nama")
            ComboBox1.Items.Add(Base_Language.Lang_Barang_Susunan_Jenis_Palet) : arrcari.Add("c.Keterangan")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Jumlah) : arrcari.Add("a.Satuan_Jumlah")
            ComboBox1.Items.Add(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Ukuran) : arrcari.Add("a.Satuan_Ukuran")
            ComboBox1.SelectedIndex = -1

            ListView1.Items.Clear()
            SQL = "select a.Kode_Barang,b.Nama,a.Susunan,a.Id_WMS_Pallet,c.Keterangan as Palet,"
            SQL = SQL & "a.Pjumlah,a.Ljumlah, a.TJumlah, a.Satuan_Jumlah,a.Satuan_Ukuran,a.Urut,"
            SQL = SQL & "a.Tinggi_Per_Tumpukan,a.Keterangan,a.Total,a.Flag_Default from "
            SQL = SQL & "Barang_Detail_Susunan a,Barang b,EMI_WMS_Pallet c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_WMS_Pallet = c.Id_WMS_Pallet "
            SQL = SQL & "group by a.Kode_Barang,b.Nama,a.Susunan,a.Id_WMS_Pallet,c.Keterangan,a.Pjumlah,"
            SQL = SQL & "a.Ljumlah,a.Satuan_Jumlah,a.Satuan_Ukuran,a.Urut,a.Tinggi_Per_Tumpukan,"
            SQL = SQL & "a.Keterangan,a.Total,a.Flag_Default, a.TJumlah order by Nama"
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("Kode_Barang"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(dr("Susunan"))
                    Lvw.SubItems.Add(dr("Palet"))
                    Lvw.SubItems.Add(dr("PJumlah"))
                    Lvw.SubItems.Add(dr("LJumlah"))
                    Lvw.SubItems.Add(dr("TJumlah"))
                    Lvw.SubItems.Add(dr("Total"))
                    Lvw.SubItems.Add(dr("Satuan_Jumlah"))
                    Lvw.SubItems.Add(dr("Satuan_Ukuran"))
                    Lvw.SubItems.Add(dr("Tinggi_Per_Tumpukan"))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    If General_Class.CekNULL(dr("Flag_Default")) = "" Then
                        Lvw.SubItems.Add("T")
                    Else
                        Lvw.SubItems.Add(dr("Flag_Default"))
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

    Private Sub Cari(ByVal semua As String)
        Try

            OpenConn()

            ListView1.Items.Clear()
            SQL = "select a.Kode_Barang,b.Nama,a.Susunan,a.Id_WMS_Pallet,c.Keterangan as Palet,"
            SQL = SQL & "a.Pjumlah,a.Ljumlah,a.TJumlah,a.Satuan_Jumlah,a.Satuan_Ukuran,a.Urut,"
            SQL = SQL & "a.Tinggi_Per_Tumpukan,a.Keterangan,a.Total,a.Flag_Default from "
            SQL = SQL & "Barang_Detail_Susunan a,Barang b,EMI_WMS_Pallet c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_WMS_Pallet = c.Id_WMS_Pallet "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                SQL = SQL & "group by a.Kode_Barang,b.Nama,a.Susunan,a.Id_WMS_Pallet,c.Keterangan,a.Pjumlah,"
                SQL = SQL & "a.Ljumlah,a.Satuan_Jumlah,a.Satuan_Ukuran,a.Urut,a.Tinggi_Per_Tumpukan,"
                SQL = SQL & "a.Keterangan,a.Total,a.Flag_Default,a.TJumlah "
                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & "group by a.Kode_Barang,b.Nama,a.Susunan,a.Id_WMS_Pallet,c.Keterangan,a.Pjumlah,"
                SQL = SQL & "a.Ljumlah,a.Satuan_Jumlah,a.Satuan_Ukuran,a.Urut,a.Tinggi_Per_Tumpukan,"
                SQL = SQL & "a.Keterangan,a.Total,a.Flag_Default,a.TJumlah "
                SQL = SQL & "order by nama"
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("Kode_Barang"))
                    Lvw.SubItems.Add(dr("Nama"))
                    Lvw.SubItems.Add(dr("Susunan"))
                    Lvw.SubItems.Add(dr("Palet"))
                    Lvw.SubItems.Add(dr("PJumlah"))
                    Lvw.SubItems.Add(dr("LJumlah"))
                    Lvw.SubItems.Add(dr("TJumlah"))
                    Lvw.SubItems.Add(dr("Total"))
                    Lvw.SubItems.Add(dr("Satuan_Jumlah"))
                    Lvw.SubItems.Add(dr("Satuan_Ukuran"))
                    Lvw.SubItems.Add(dr("Tinggi_Per_Tumpukan"))
                    Lvw.SubItems.Add(dr("Keterangan"))
                    If General_Class.CekNULL(dr("Flag_Default")) = "" Then
                        Lvw.SubItems.Add("T")
                    Else
                        Lvw.SubItems.Add(dr("Flag_Default"))
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
    Private Sub Master_Barang_Susunan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub Master_Barang_Susunan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Barang_Susunan_Judul
            Label8.Text = Base_Language.Lang_Global_Barang
            Label2.Text = Base_Language.Lang_Barang_Susunan_Susunan
            Label7.Text = Base_Language.Lang_Barang_Susunan_Jenis_Palet
            Label3.Text = Base_Language.Lang_Barang_Susunan_Susunan
            Label9.Text = Base_Language.Lang_Barang_Susunan_Panjang
            Label10.Text = Base_Language.Lang_Barang_Susunan_Lebar
            'Label6.Text = Base_Language.Lang_Global_Satuan
            'Label11.Text = Base_Language.Lang_Global_Jumlah
            'Label12.Text = Base_Language.Lang_Global_Ukuran
            Label13.Text = Base_Language.Lang_Barang_Susunan_Tinggi_Per
            Label14.Text = Base_Language.Lang_Global_Keterangan
            Label4.Text = Base_Language.Lang_Barang_Susunan_Kolom

            ListView1.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_NamaBarang, 300, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Barang_Susunan_Susunan, 100, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Barang_Susunan_Jenis_Palet, 100, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Barang_Susunan_Panjang, 50, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Barang_Susunan_Lebar, 50, HorizontalAlignment.Center)
            ListView1.Columns.Add("T", 50, HorizontalAlignment.Center)
            ListView1.Columns.Add("Total", 80, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Jumlah, 100, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Satuan & " " & Base_Language.Lang_Global_Ukuran, 100, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Barang_Susunan_Tinggi_Per, 120, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Global_Keterangan, 200, HorizontalAlignment.Left)
            ListView1.Columns.Add("Default", 80, HorizontalAlignment.Center)
            ListView1.View = View.Details

            ListView2.Columns.Add(Base_Language.Lang_Global_KodeBarang, 150, HorizontalAlignment.Left)
            ListView2.Columns.Add(Base_Language.Lang_Global_NamaBarang, 250, HorizontalAlignment.Left)
            ListView2.Location = New Point(179, 95)
            ListView2.Visible = False

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Down Then
            ListView2.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox3.Text.Trim.Length = 0 Then
                ListView2.Visible = False : TextBox4.Focus() : Exit Sub
            End If
            TextBox1_Leave(TextBox3, e)
        End If
    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If TextBox1.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If
        If ListView2.Focused = True Then Exit Sub

        Try
            OpenConn()

            SQL = "select Kode_Barang,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Kode_Barang = '" & TextBox1.Text & "' "
            SQL = SQL & "group by Kode_Barang,Nama"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("Kode_Barang")
                    TextBox4.Text = Dr("Nama")
                    ComboBox3.Focus()

                    Dr.Close()
                    ComboBox2.Items.Clear()
                    SQL = "select satuan from Barang_Detail_Satuan where kode_perusahaan = '" & KodePerusahaan & "' "
                    SQL = SQL & " and kode_barang = '" & TextBox1.Text & "' order by satuan "
                    Using Ds = BindingTrans(SQL)
                        With Ds.Tables("MyTable")
                            If .Rows.Count <> 0 Then

                                For i As Integer = 0 To .Rows.Count - 1
                                    ComboBox2.Items.Add(.Rows(i).Item("satuan"))
                                Next

                                'Else
                                '    CloseConn()
                                '    MessageBox.Show(Base_Language.Lang_Global_Data_Tdk_Ditemukan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                '    Exit Sub
                            End If
                        End With
                    End Using
                Else
                    ComboBox2.Items.Clear()
                    TextBox1.Text = ""
                    TextBox4.Text = ""
                    TextBox1.Focus()
                End If
                ListView2.Visible = False
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If TextBox1.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If

        ListView2.Items.Clear()
        Dim lv As New ListViewItem

        Try
            OpenConn()

            SQL = "select Kode_Barang,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Barang like '%" & TextBox1.Text & "%' "
            SQL = SQL & "group by Kode_Barang,Nama order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView2.Items.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ListView2_DoubleClick(sender As Object, e As EventArgs) Handles ListView2.DoubleClick
        If ListView2.Items.Count = 0 Then Exit Sub
        Dim kode As String = ListView2.FocusedItem.Text
        Dim nama As String = ListView2.FocusedItem.SubItems(1).Text
        TextBox1.Text = kode
        TextBox4.Text = nama
        ListView2.Visible = False
        ComboBox3.Focus()
        TextBox1_Leave(ListView1, e)
    End Sub

    Private Sub ListView2_KeyDown(sender As Object, e As KeyEventArgs) Handles ListView2.KeyDown
        If e.KeyCode = Keys.Enter Then
            ListView2_DoubleClick(ListView2, e)
        End If
    End Sub

    Private Sub TextBox4_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox4.KeyDown
        If e.KeyCode = Keys.Down Then
            If ListView2.Items.Count = 0 Then Exit Sub
            ListView2.Focus()
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If e.KeyChar = Chr(13) Then
            If TextBox3.Text.Trim.Length = 0 Then TextBox4.Text = "" : ListView2.Visible = False ': Exit Sub
            ComboBox3.Focus()
        End If
    End Sub

    Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        If ListView2.Focused = True Then Exit Sub
        TextBox3.Text = "" : TextBox4.Text = ""
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text.Trim.Length = 0 Then
            ListView2.Visible = False : Exit Sub
        Else
            ListView2.Visible = True
        End If

        ListView2.Items.Clear()
        Dim lv As New ListViewItem
        Try
            OpenConn()

            SQL = "select Kode_Barang,Nama from Barang where "
            SQL = SQL & "kode_perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Nama like '%" & TextBox4.Text & "%' "
            SQL = SQL & "group by Kode_Barang,Nama order by Nama"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    lv = ListView2.Items.Add(Dr("Kode_Barang"))
                    lv.SubItems.Add(Dr("Nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.Text.Trim.Length = 0 Then Exit Sub
        If TextBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox4.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "select a.Kode_Barang,b.Nama,a.Susunan,a.Id_WMS_Pallet,c.Keterangan as Palet,"
            SQL = SQL & "a.Pjumlah,a.Ljumlah, a.TJumlah, a.Satuan_Jumlah,a.Satuan_Ukuran,a.Urut,"
            SQL = SQL & "a.Tinggi_Per_Tumpukan,a.Keterangan,a.Total,a.Flag_Default from "
            SQL = SQL & "Barang_Detail_Susunan a,Barang b,EMI_WMS_Pallet c where "
            SQL = SQL & "a.Kode_Perusahaan = b.Kode_Perusahaan and a.Kode_Barang = b.Kode_Barang "
            SQL = SQL & "and a.Kode_Perusahaan = c.Kode_Perusahaan and a.Id_WMS_Pallet = c.Id_WMS_Pallet "
            SQL = SQL & "and a.Kode_Barang = '" & TextBox1.Text & "' and a.Susunan = '" & ComboBox3.Text & "' "
            SQL = SQL & "group by a.Kode_Barang,b.Nama,a.Susunan,a.Id_WMS_Pallet,c.Keterangan,a.Pjumlah,"
            SQL = SQL & "a.Ljumlah,a.Satuan_Jumlah,a.Satuan_Ukuran,a.Urut,a.Tinggi_Per_Tumpukan,"
            SQL = SQL & "a.Keterangan,a.Total,a.Flag_Default, a.TJumlah order by Nama"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    ComboBox4.Text = Dr("Palet")
                    TextBoxP.Text = Dr("PJumlah")
                    TextBoxL.Text = Dr("LJumlah")
                    TextBoxT.Text = Dr("TJumlah")
                    TextBox8.Text = Dr("Total")
                    ComboBox2.Text = Dr("Satuan_Jumlah")
                    'ComboBox5.Text = Dr("Satuan_Ukuran")
                    TextBox6.Text = Dr("Tinggi_Per_Tumpukan")
                    TextBox7.Text = Dr("Keterangan")
                    If General_Class.CekNULL(Dr("Flag_Default")) = "" Then
                        CheckBox1.Checked = False
                    Else
                        CheckBox1.Checked = True
                    End If

                    TextBox1.Enabled = False
                    TextBox4.Enabled = False
                    ComboBox3.Enabled = False
                    Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
                    Btn_Simpan.Tag = "&Update"
                Else
                    ComboBox4.SelectedIndex = -1
                    TextBoxP.Text = ""
                    TextBoxL.Text = ""
                    ComboBox2.SelectedIndex = -1
                    'ComboBox5.SelectedIndex = -1
                    TextBox6.Text = ""
                    TextBox7.Text = ""
                    TextBox8.Text = ""
                    CheckBox1.Checked = False

                    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = False
                    Btn_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox2_Leave(sender As Object, e As EventArgs) Handles TextBoxP.Leave
        If ComboBox3.Text.Trim.Length = 0 Then Exit Sub
        If TextBoxP.Text.Trim.Length = 0 Then Exit Sub
        If TextBoxL.Text.Trim.Length = 0 Then Exit Sub
        If TextBoxT.Text.Trim.Length = 0 Then Exit Sub

        If ComboBox3.SelectedIndex = 0 Then
            TextBox8.Text = (Val(TextBoxP.Text) + Val(TextBoxL.Text)) * Val(TextBoxT.Text)
        Else
            TextBox8.Text = Val(TextBoxP.Text) * Val(TextBoxL.Text) * Val(TextBoxT.Text)
        End If
    End Sub

    Private Sub TextBox5_Leave(sender As Object, e As EventArgs) Handles TextBoxL.Leave
        If ComboBox3.Text.Trim.Length = 0 Then Exit Sub
        If TextBoxP.Text.Trim.Length = 0 Then Exit Sub
        If TextBoxL.Text.Trim.Length = 0 Then Exit Sub
        If TextBoxT.Text.Trim.Length = 0 Then Exit Sub

        If ComboBox3.SelectedIndex = 0 Then
            TextBox8.Text = (Val(TextBoxP.Text) + Val(TextBoxL.Text)) * Val(TextBoxT.Text)
        Else
            TextBox8.Text = Val(TextBoxP.Text) * Val(TextBoxL.Text) * Val(TextBoxT.Text)
        End If
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Kd_Brg, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf TextBox4.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Nm_Brg, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox4.Focus() : Exit Sub
        ElseIf ComboBox3.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Susunan_Error_Susunan, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox3.Focus() : Exit Sub
        ElseIf ComboBox4.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Susunan_Error_Jenis_Palet, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox4.Focus() : Exit Sub
        ElseIf TextBoxP.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Susunan_Error_Panjang, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxP.Focus() : Exit Sub
        ElseIf TextBoxL.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Susunan_Error_Lebar, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBoxL.Focus() : Exit Sub
        ElseIf TextBox8.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Susunan_Error_Total, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox8.Focus() : Exit Sub
        ElseIf ComboBox2.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Susunan_Error_Jumlah, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox2.Focus() : Exit Sub
        ElseIf ComboBox5.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Susunan_Error_Ukuran, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox5.Focus() : Exit Sub
        ElseIf TextBox6.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Susunan_Error_Tinggi_Per, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox6.Focus() : Exit Sub
        ElseIf TextBox7.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Barang_Susunan_Error_Ket, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox7.Focus() : Exit Sub
        End If

        Dim F_Default As String = ""
        If CheckBox1.Checked = True Then
            F_Default = "'Y'"
        Else
            F_Default = "NULL"
        End If

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction



            If Btn_Simpan.Tag = "&Simpan" Then

                If CheckBox1.Checked = True Then
                    SQL = "Select Kode_Barang,Susunan,Flag_Default from Barang_Detail_Susunan "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_barang ='" & TextBox1.Text & "' and satuan_jumlah='" & ComboBox2.Text & "' "
                    SQL = SQL & "and Flag_Default = 'Y'"
                    Using Dr = OpenTrans(SQL)
                        If Dr.Read Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show(Base_Language.Lang_Barang_Susunan_Error1, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            CheckBox1.Focus() : Exit Sub
                        End If
                    End Using
                End If

                SQL = "Insert Into Barang_Detail_Susunan(Kode_Perusahaan,Kode_Barang,Susunan,"
                SQL = SQL & "Id_WMS_Pallet,Pjumlah,Ljumlah,Satuan_Jumlah,Satuan_Ukuran,"
                SQL = SQL & "Tinggi_Per_Tumpukan,Keterangan,Total,Flag_Default,Tjumlah) Values("
                SQL = SQL & "'" & KodePerusahaan & "','" & TextBox1.Text & "',"
                SQL = SQL & "'" & ComboBox3.Text & "','" & arrid.Item(ComboBox4.SelectedIndex) & "',"
                SQL = SQL & "'" & TextBoxP.Text & "','" & TextBoxL.Text & "',"
                SQL = SQL & "'" & ComboBox2.Text & "','" & ComboBox5.Text & "',"
                SQL = SQL & "'" & TextBox6.Text & "','" & TextBox7.Text & "',"
                SQL = SQL & "'" & TextBox8.Text & "'," & F_Default & ",'" & TextBoxT.Text & "')"
                ExecuteTrans(SQL)
            Else

                If CheckBox1.Checked = True Then
                    SQL = "update Barang_Detail_Susunan set Flag_Default=NULL "
                    SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                    SQL = SQL & "Kode_Barang = '" & TextBox1.Text & "' "
                    ExecuteTrans(SQL)
                End If

                SQL = "update Barang_Detail_Susunan set "
                SQL = SQL & "Id_WMS_Pallet = '" & arrid.Item(ComboBox4.SelectedIndex) & "',"
                SQL = SQL & "Pjumlah = '" & TextBoxP.Text & "',"
                SQL = SQL & "Ljumlah = '" & TextBoxL.Text & "',"
                SQL = SQL & "Tjumlah = '" & TextBoxT.Text & "',"
                SQL = SQL & "Satuan_Jumlah = '" & ComboBox2.Text & "',"
                SQL = SQL & "Satuan_Ukuran = '" & ComboBox5.Text & "',"
                SQL = SQL & "Tinggi_Per_Tumpukan = '" & TextBox6.Text & "',"
                SQL = SQL & "Keterangan = '" & TextBox7.Text & "',"
                SQL = SQL & "Total = '" & TextBox8.Text & "',"
                SQL = SQL & "Flag_Default = " & F_Default & " where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Kode_Barang = '" & TextBox1.Text & "' and "
                SQL = SQL & "Susunan = '" & ComboBox3.Text & "'"
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosong()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        TextBox1.Text = ListView1.FocusedItem.Text
        TextBoxP.Text = ListView1.FocusedItem.SubItems(1).Text
        ComboBox3.Text = ListView1.FocusedItem.SubItems(2).Text
        TextBox1_Leave(ListView1, e)
        ComboBox3_SelectedIndexChanged(ListView1, e)
        TextBox2_Leave(ListView1, e)
    End Sub

    Private Sub ComboBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox3.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox4.Focus()
    End Sub

    Private Sub ComboBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox4.KeyPress
        If e.KeyChar = Chr(13) Then TextBoxP.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxP.KeyPress
        If e.KeyChar = Chr(13) Then TextBoxL.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxL.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox2.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub ComboBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox2.KeyPress
        If e.KeyChar = Chr(13) Then ComboBox5.Focus()
    End Sub

    Private Sub ComboBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox5.KeyPress
        If e.KeyChar = Chr(13) Then TextBox6.Focus()
    End Sub

    Private Sub TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox6.KeyPress
        If e.KeyChar = Chr(13) Then CheckBox1.Focus()
        If Not (e.KeyChar >= Chr(Asc("0")) And e.KeyChar <= Chr(Asc("9")) Or e.KeyChar = Chr(Asc(".")) Or e.KeyChar = Chr(8)) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub CheckBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CheckBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox7.Focus()
    End Sub

    Private Sub TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox7.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Error_Kd_Brg, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        End If

        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Delete From Barang_Detail_Susunan where "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Kode_Barang = '" & TextBox1.Text & "' and "
                SQL = SQL & "Susunan = '" & ComboBox3.Text & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        kosong()
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged

    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBoxP.TextChanged

    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub TextBoxL_TextChanged(sender As Object, e As EventArgs) Handles TextBoxL.TextChanged

    End Sub

    Private Sub TextBox6_Paint(sender As Object, e As PaintEventArgs) Handles TextBox6.Paint

    End Sub

    Private Sub TextBoxT_Leave(sender As Object, e As EventArgs) Handles TextBoxT.Leave
        If ComboBox3.Text.Trim.Length = 0 Then Exit Sub
        If TextBoxP.Text.Trim.Length = 0 Then Exit Sub
        If TextBoxL.Text.Trim.Length = 0 Then Exit Sub
        If TextBoxT.Text.Trim.Length = 0 Then Exit Sub

        If ComboBox3.SelectedIndex = 0 Then
            TextBox8.Text = (Val(TextBoxP.Text) + Val(TextBoxL.Text)) * Val(TextBoxT.Text)
        Else
            TextBox8.Text = Val(TextBoxP.Text) * Val(TextBoxL.Text) * Val(TextBoxT.Text)
        End If
    End Sub
End Class