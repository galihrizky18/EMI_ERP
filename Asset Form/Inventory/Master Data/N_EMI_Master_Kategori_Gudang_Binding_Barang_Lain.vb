Public Class N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain


    Dim Lv_Kd_Kategori_Gudang, Lv_Kd_Sub_Kategori_Gudang_Jenis, Lv_Keterangan, Lv_Urut As String

    Dim item_Kd_Kategori_Gudang As Integer = 0
    Dim item_Kd_Sub_Kategori_Jenis As Integer = 1
    Dim item_Keterangan As Integer = 2
    Dim item_Urut As Integer = 3



    Dim SelectedUrut As String = ""

    Dim Swith_AutoComplete As Boolean

    Private Sub N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Kategori_Gudang.Columns.Clear()
        Lv_Kategori_Gudang.Columns.Add("Kode Kategori Gudang", 150, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.Columns.Add("Jenis Gudang", 150, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.Columns.Add("ID", 0, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.View = View.Details

        Lv_Sub_Kategori_Jenis.Columns.Clear()
        Lv_Sub_Kategori_Jenis.Columns.Add("Kode Sub Kategori Jenis", 150, HorizontalAlignment.Left)
        Lv_Sub_Kategori_Jenis.Columns.Add("Kode Kategori Jenis", 150, HorizontalAlignment.Left)
        Lv_Sub_Kategori_Jenis.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Sub_Kategori_Jenis.Columns.Add("iD", 0, HorizontalAlignment.Left)
        Lv_Sub_Kategori_Jenis.View = View.Details

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Kode Kategori Gudang", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Kode Sub Kategori Jenis", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        'hide
        Lv_Data.Columns.Add("Urut", 0, HorizontalAlignment.Left)
        Lv_Data.View = View.Details



        Kosong()

    End Sub

    Private Sub Get_Data_Lv(ByVal index As Integer)
        Lv_Kd_Kategori_Gudang = Lv_Data.Items(index).SubItems(item_Kd_Kategori_Gudang).Text
        Lv_Kd_Sub_Kategori_Gudang_Jenis = Lv_Data.Items(index).SubItems(item_Kd_Sub_Kategori_Jenis).Text
        Lv_Keterangan = Lv_Data.Items(index).SubItems(item_Keterangan).Text
        Lv_Urut = Lv_Data.Items(index).SubItems(item_Urut).Text
    End Sub


    Private Sub Kosong()


        Swith_AutoComplete = True

        Txt_Kd_Kategori.Text = String.Empty
        Txt_Kategori_Keterangan.Text = String.Empty
        Txt_Sub_Kategori_Jenis.Text = String.Empty
        Txt_Kategori_Jenis.Text = String.Empty
        Txt_Keterangan_Kategori_Jenis.Text = String.Empty
        Txt_Keterangan.Text = String.Empty
        Txt_Jenis_Kategori_Gudang.Text = String.Empty

        SelectedUrut = String.Empty

        Txt_ID_Kategori.Text = String.Empty
        Txt_ID_Sub_Kategori.Text = String.Empty

        Txt_Kd_Kategori.Enabled = True
        Txt_Kd_Kategori.BackColor = Color.White

        Txt_Sub_Kategori_Jenis.Enabled = True
        Txt_Sub_Kategori_Jenis.BackColor = Color.White

        Swith_AutoComplete = False

        Btn_Simpan.Tag = "SIMPAN"
        Btn_Simpan.Text = "&Simpan"



        LoadData()

    End Sub

    Private Sub LoadData()
        Try
            OpenConn()

            Lv_Data.Items.Clear()
            SQL = $"
                select Kode_Kategori_Gudang, Kode_Sub_Kategori_Jenis, Keterangan, urut_oto
                from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and status is null
                order by tanggal, Jam
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Kode_Kategori_Gudang"))
                    Lv.SubItems.Add(Dr("Kode_Sub_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("urut_oto"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Kategori_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd_Kategori.TextChanged
        If Swith_AutoComplete Then Exit Sub

        If Txt_Kd_Kategori.Text.Trim.Length = 0 Then
            Lv_Kategori_Gudang.Visible = False
            Lv_Kategori_Gudang.Location = New Point(1190, 108)
            Txt_Kd_Kategori.Text = ""
            Txt_Jenis_Kategori_Gudang.Text = ""
            Txt_Kategori_Keterangan.Text = ""
            Exit Sub
        Else
            Lv_Kategori_Gudang.Location = New Point(158, 108)
            Lv_Kategori_Gudang.Visible = True
        End If

        Try
            OpenConn()

            Lv_Kategori_Gudang.Items.Clear()


            SQL = $"
                select Kode_Kategori_Gudang, Keterangan, Jenis_Gudang, Urut_Oto
                from N_EMI_Master_Kategori_Gudang_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and status is null
                and Kode_Kategori_Gudang like '%{Txt_Kd_Kategori.Text.Trim}%'
                order by Kode_Kategori_Gudang
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Kategori_Gudang.Items.Add(Dr("Kode_Kategori_Gudang"))
                    Lv.SubItems.Add(Dr("Jenis_Gudang"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Urut_Oto"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_Kd_Kategori_Leave(sender As Object, e As EventArgs) Handles Txt_Kd_Kategori.Leave
        If Txt_Kd_Kategori.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Kategori_Gudang.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Kd_Kategori.Text.Trim.Length = 0 Then

                SQL = $"
                    select Kode_Kategori_Gudang, Keterangan, Jenis_Gudang, Urut_Oto 
                    from N_EMI_Master_Kategori_Gudang_Barang_Lain
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                    order by Kode_Kategori_Gudang
                "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Swith_AutoComplete = True
                        Txt_Kd_Kategori.Text = Dr("Kode_Kategori_Gudang")
                        Txt_Jenis_Kategori_Gudang.Text = Dr("Jenis_Gudang")
                        Txt_Kategori_Keterangan.Text = Dr("Keterangan")
                        Txt_ID_Kategori.Text = Dr("Urut_Oto")
                        Swith_AutoComplete = False
                        Txt_Sub_Kategori_Jenis.Focus()
                    Else
                        MessageBox.Show("Kategori Gudang tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Kd_Kategori.Text = ""
                        Txt_Jenis_Kategori_Gudang.Text = ""
                        Txt_Kategori_Keterangan.Text = ""
                        Txt_Kd_Kategori.Focus()
                    End If


                    Lv_Kategori_Gudang.Visible = False
                    Lv_Kategori_Gudang.Location = New Point(1190, 108)
                End Using
            Else
                Txt_Sub_Kategori_Jenis.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Kd_Kategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd_Kategori.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Kd_Kategori.Text.Trim.Length = 0 Then Txt_Kd_Kategori.Focus()
            Txt_Kd_Kategori_Leave(Txt_Kd_Kategori, e)

            Lv_Kategori_Gudang.Visible = False
            Lv_Kategori_Gudang.Location = New Point(1190, 108)

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_Kd_Kategori_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Kd_Kategori.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Kategori_Gudang.Focus()
    End Sub

    Private Sub Lv_Kategori_Gudang_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Kategori_Gudang.DoubleClick
        If Lv_Kategori_Gudang.Items.Count = 0 Or Lv_Kategori_Gudang.FocusedItem.Index = -1 Then Exit Sub


        Dim Kd_Kategori As String = Lv_Kategori_Gudang.FocusedItem.SubItems(0).Text
        Dim Jenis_Gudang As String = Lv_Kategori_Gudang.FocusedItem.SubItems(1).Text
        Dim Keterangan As String = Lv_Kategori_Gudang.FocusedItem.SubItems(2).Text
        Dim ID As String = Lv_Kategori_Gudang.FocusedItem.SubItems(3).Text

        Swith_AutoComplete = True
        Txt_Kd_Kategori.Text = Kd_Kategori
        Txt_Jenis_Kategori_Gudang.Text = Jenis_Gudang
        Txt_Kategori_Keterangan.Text = Keterangan
        Txt_ID_Kategori.Text = ID
        Swith_AutoComplete = False

        Lv_Kategori_Gudang.Visible = False
        Lv_Kategori_Gudang.Location = New Point(1190, 108)

        Txt_Sub_Kategori_Jenis.Focus()
    End Sub

    Private Sub Lv_Kategori_Gudang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Kategori_Gudang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Kategori_Gudang_DoubleClick(Lv_Kategori_Gudang, e)
        End If
    End Sub

    Private Sub Txt_Sub_Kategori_Jenis_TextChanged(sender As Object, e As EventArgs) Handles Txt_Sub_Kategori_Jenis.TextChanged
        If Swith_AutoComplete Then Exit Sub

        If Txt_Sub_Kategori_Jenis.Text.Trim.Length = 0 Then
            Lv_Sub_Kategori_Jenis.Visible = False
            Lv_Sub_Kategori_Jenis.Location = New Point(1190, 237)
            Txt_Sub_Kategori_Jenis.Text = ""
            Txt_Kategori_Jenis.Text = ""
            Txt_Keterangan_Kategori_Jenis.Text = ""
            Exit Sub
        Else
            Lv_Sub_Kategori_Jenis.Location = New Point(158, 237)
            Lv_Sub_Kategori_Jenis.Visible = True
        End If

        Try
            OpenConn()

            Lv_Sub_Kategori_Jenis.Items.Clear()
            SQL = $"
                select a.Kode_Sub_Kategori_Jenis, b.Kode_Kategori_Jenis, a.Keterangan, a.Id_Sub_Kategori_Jenis
                from N_EMI_Master_Sub_Kategori_Jenis a
	                inner join N_EMI_Master_Kategori_Jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Kode_Sub_Kategori_Jenis like '%{Txt_Sub_Kategori_Jenis.Text}%'
                order by a.Kode_Sub_Kategori_Jenis, b.Kode_Kategori_Jenis
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Sub_Kategori_Jenis.Items.Add(Dr("Kode_Sub_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Kode_Kategori_Jenis"))
                    Lv.SubItems.Add(Dr("Keterangan"))
                    Lv.SubItems.Add(Dr("Id_Sub_Kategori_Jenis"))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_Sub_Kategori_Jenis_Leave(sender As Object, e As EventArgs) Handles Txt_Sub_Kategori_Jenis.Leave
        If Txt_Sub_Kategori_Jenis.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Sub_Kategori_Jenis.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Sub_Kategori_Jenis.Text.Trim.Length = 0 Then

                SQL = $"
                select a.Kode_Sub_Kategori_Jenis, b.Kode_Kategori_Jenis, a.Keterangan, a.id_sub_kategori_jenis
                from N_EMI_Master_Sub_Kategori_Jenis a
	                inner join N_EMI_Master_Kategori_Jenis b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Jenis = b.Id_Kategori_Jenis
                where a.Kode_Perusahaan = '{KodePerusahaan}'
                and a.Kode_Sub_Kategori_Jenis = '{Txt_Sub_Kategori_Jenis.Text}'
                order by a.Kode_Sub_Kategori_Jenis, b.Kode_Kategori_Jenis
            "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Swith_AutoComplete = True
                        Txt_Sub_Kategori_Jenis.Text = Dr("Kode_Sub_Kategori_Jenis")
                        Txt_Kategori_Jenis.Text = Dr("Kode_Kategori_Jenis")
                        Txt_Keterangan_Kategori_Jenis.Text = Dr("Keterangan")
                        Txt_ID_Sub_Kategori.Text = Dr("id_sub_kategori_jenis")
                        Swith_AutoComplete = False
                        Txt_Keterangan.Focus()
                    Else
                        MessageBox.Show("Sub Kategori Jenis tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Txt_Sub_Kategori_Jenis.Text = ""
                        Txt_Kategori_Jenis.Text = ""
                        Txt_Keterangan_Kategori_Jenis.Text = ""
                        Txt_Sub_Kategori_Jenis.Focus()
                    End If


                    Lv_Sub_Kategori_Jenis.Visible = False
                    Lv_Sub_Kategori_Jenis.Location = New Point(1190, 237)
                End Using
            Else
                Txt_Keterangan.Focus()
            End If

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_Sub_Kategori_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Sub_Kategori_Jenis.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Sub_Kategori_Jenis.Text.Trim.Length = 0 Then Txt_Sub_Kategori_Jenis.Focus()
            Txt_Sub_Kategori_Jenis_Leave(Txt_Sub_Kategori_Jenis, e)

            Lv_Sub_Kategori_Jenis.Visible = False
            Lv_Sub_Kategori_Jenis.Location = New Point(1190, 237)

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_Sub_Kategori_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Sub_Kategori_Jenis.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Sub_Kategori_Jenis.Focus()
    End Sub

    Private Sub Lv_Sub_Kategori_Jenis_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Sub_Kategori_Jenis.DoubleClick
        If Lv_Sub_Kategori_Jenis.Items.Count = 0 Or Lv_Sub_Kategori_Jenis.FocusedItem.Index = -1 Then Exit Sub


        Dim Kd_Sub_Kategori As String = Lv_Sub_Kategori_Jenis.FocusedItem.SubItems(0).Text
        Dim Kd_Kategori_Jenis As String = Lv_Sub_Kategori_Jenis.FocusedItem.SubItems(1).Text
        Dim Keterangan As String = Lv_Sub_Kategori_Jenis.FocusedItem.SubItems(2).Text
        Dim ID As String = Lv_Sub_Kategori_Jenis.FocusedItem.SubItems(3).Text

        Swith_AutoComplete = True
        Txt_Sub_Kategori_Jenis.Text = Kd_Sub_Kategori
        Txt_Kategori_Jenis.Text = Kd_Kategori_Jenis
        Txt_Keterangan_Kategori_Jenis.Text = Keterangan
        Txt_ID_Sub_Kategori.Text = ID
        Swith_AutoComplete = False

        Lv_Sub_Kategori_Jenis.Visible = False
        Lv_Sub_Kategori_Jenis.Location = New Point(1190, 237)

        Txt_Keterangan.Focus()
    End Sub

    Private Sub Lv_Sub_Kategori_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Sub_Kategori_Jenis.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Sub_Kategori_Jenis_DoubleClick(Lv_Sub_Kategori_Jenis, e)
        End If
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        Kosong()
    End Sub

    Private Sub Lv_Data_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data.DoubleClick
        If Lv_Data.Items.Count = 0 Then Exit Sub

        Try
            OpenConn()

            Get_Data_Lv(Lv_Data.FocusedItem.Index)

            SQL = $"
                select Kode_Kategori_Gudang, Kode_Sub_Kategori_Jenis, Keterangan, Urut_Oto 
                from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and Kode_Kategori_Gudang = '{Lv_Kd_Kategori_Gudang}'
                and Kode_Sub_Kategori_Jenis = '{Lv_Kd_Sub_Kategori_Gudang_Jenis}'
                and Urut_Oto = '{Lv_Urut}'
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Swith_AutoComplete = True
                            Txt_Kd_Kategori.Text = .Rows(i).Item("Kode_Kategori_Gudang")
                            Swith_AutoComplete = False
                            Txt_Kd_Kategori_Leave(sender, New EventArgs)

                            Swith_AutoComplete = True
                            Txt_Sub_Kategori_Jenis.Text = .Rows(i).Item("Kode_Sub_Kategori_Jenis")
                            Swith_AutoComplete = False
                            Txt_Sub_Kategori_Jenis_Leave(sender, New EventArgs)


                            Txt_Keterangan.Text = .Rows(i).Item("Keterangan")

                            SelectedUrut = .Rows(i).Item("Urut_Oto")

                            Txt_Kd_Kategori.Enabled = False
                            Txt_Kd_Kategori.BackColor = Color.FromArgb(235, 235, 235)

                            Txt_Sub_Kategori_Jenis.Enabled = False
                            Txt_Sub_Kategori_Jenis.BackColor = Color.FromArgb(235, 235, 235)

                            Btn_Simpan.Tag = "UPDATE"
                            Btn_Simpan.Text = "&Simpan"

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


    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Kd_Kategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Kategori Gudang Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Kd_Kategori.Focus()
            Exit Sub
        ElseIf Txt_Sub_Kategori_Jenis.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Sub Kategori Jenis Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Sub_Kategori_Jenis.Focus()
            Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show("Keterangan Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Keterangan.Focus()
            Exit Sub
        End If

        Dim Action As String = ""

        get_jam()
        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag.ToString.ToUpper = "SIMPAN" Then

                '======================================
                '=      CEK APAKAH DATA SUDAH ADA     =
                '======================================
                SQL = $"
                    select Kode_Perusahaan
                    from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                    and Kode_Sub_Kategori_Jenis = '{Txt_Sub_Kategori_Jenis.Text.Trim}'
                "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Kode Kategori Gudang dan Sub Kategori Jenis Sudah Di Binding", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = $"
                    insert into N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain (Kode_Perusahaan, Kode_Kategori_Gudang, Kode_Sub_Kategori_Jenis, Keterangan, Tanggal, Jam, id_Kategori_Gudang, id_Sub_Kategori_Jenis)
                    values ('{KodePerusahaan}', '{Txt_Kd_Kategori.Text.Trim}', '{Txt_Sub_Kategori_Jenis.Text.Trim}', '{Txt_Keterangan.Text.Trim}', 
                    '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{Txt_ID_Kategori.Text.Trim}', '{Txt_ID_Sub_Kategori.Text.Trim}')
                "
                ExecuteTrans(SQL)


                Action = "Simpan"

            ElseIf Btn_Simpan.Tag.ToString.ToUpper = "UPDATE" Then

                '======================================
                '=      CEK APAKAH DATA SUDAH ADA     =
                '======================================
                SQL = $"
                    select Kode_Perusahaan
                    from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                    and Kode_Sub_Kategori_Jenis = '{Txt_Sub_Kategori_Jenis.Text.Trim}'
                    and urut_oto = '{SelectedUrut}'
                "
                Using Dr = OpenTrans(SQL)
                    If Not Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Kode Kategori Gudang dan Sub Kategori Jenis Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using

                SQL = $"
                    update N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                    set Keterangan = '{Txt_Keterangan.Text.Trim}' 
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                    and Kode_Sub_Kategori_Jenis = '{Txt_Sub_Kategori_Jenis.Text.Trim}'
                    and urut_oto = '{SelectedUrut}'
                "
                ExecuteTrans(SQL)


                Action = "Update"

            End If


            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show($"Data Berhasil Di{Action}", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Kosong()

    End Sub

    Private Sub Btn_Delete_Click(sender As Object, e As EventArgs) Handles Btn_Delete.Click
        If SelectedUrut.Trim.Length = 0 Then
            MessageBox.Show("Harap Pilih Dahulu Data Yang Ingin Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Lv_Data.Focus()
            Exit Sub
        End If

        If MessageBox.Show("Yakin Ingin Menghapus Data Binding Ini??", Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = vbNo Then Exit Sub

        Try
            OpenConn()
            Cmd.Transaction = Cn.BeginTransaction

            '===========================
            '=     CEK ROLE BUTTON     =
            '===========================
            If CekButtonRole("Hapus_Binding_Kategori_Gudang_Barang_Lain") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Menghapus Binding Kategori Gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '===============================
            '=     CEK APAKAH DATA ADA     =
            '===============================
            SQL = $"
                select Kode_Kategori_Gudang, Kode_Sub_Kategori_Jenis, Keterangan
                from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                and Kode_Sub_Kategori_Jenis = '{Txt_Sub_Kategori_Jenis.Text.Trim}'
                and urut_oto = '{SelectedUrut}'
            "
            Using Dr = OpenTrans(SQL)
                If Not Dr.Read Then
                    Dr.Close()
                    CloseTrans()
                    CloseConn()
                    MessageBox.Show($"Data Binding Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            End Using


            SQL = $"
                Delete
                from N_EMI_Master_Kategori_Gudang_Binding_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                and Kode_Sub_Kategori_Jenis = '{Txt_Sub_Kategori_Jenis.Text.Trim}'
                and urut_oto = '{SelectedUrut}'
            "
            ExecuteTrans(SQL)




            Cmd.Transaction.Commit()
            CloseTrans()
            CloseConn()
        Catch ex As Exception
            CloseTrans()
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        MessageBox.Show("Data Binding Berhasil Dihapus", Judul, MessageBoxButtons.OK, MessageBoxIcon.Information)
        Kosong()


    End Sub






    '==========================================================================================================================================================================
    '=     CEGAH MAXIMIZED DOUBLE KLIK TITLE BAR
    '==========================================================================================================================================================================

    Protected Overrides Sub WndProc(ByRef m As Message)

        If m.Msg = &HA3 Then
            Return
        End If

        MyBase.WndProc(m)
    End Sub


    Private Sub Lv_Data_MouseMove(sender As Object, e As MouseEventArgs) Handles Lv_Data.MouseMove
        Dim info As ListViewHitTestInfo = Lv_Data.HitTest(e.Location)

        If info.Item IsNot Nothing Then
            Lv_Data.Cursor = Cursors.Hand
        Else
            Lv_Data.Cursor = Cursors.Default
        End If
    End Sub

    Private Sub Lv_Data_MouseLeave(sender As Object, e As EventArgs) Handles Lv_Data.MouseLeave
        Lv_Data.Cursor = Cursors.Default
    End Sub


End Class