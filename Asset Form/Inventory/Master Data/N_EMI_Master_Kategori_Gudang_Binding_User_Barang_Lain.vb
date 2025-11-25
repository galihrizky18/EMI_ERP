Public Class N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain


    Dim Lv_Kd_Kategori_Gudang, Lv_Kd_Sub_Kategori_Gudang_Jenis, Lv_Keterangan, Lv_Urut As String

    Dim item_Kd_Kategori_Gudang As Integer = 0
    Dim item_Kd_Sub_Kategori_Jenis As Integer = 1
    Dim item_Keterangan As Integer = 2
    Dim item_Urut As Integer = 3



    Dim SelectedUrut As String = ""

    Dim Swith_AutoComplete As Boolean

    Private Sub N_EMI_Master_Binding_Kategori_Gudang_Barang_Lain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Lv_Kategori_Gudang.Columns.Clear()
        Lv_Kategori_Gudang.Columns.Add("Kode Kategori Gudang", 150, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.Columns.Add("Jenis Gudang", 150, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.Columns.Add("Keterangan", 200, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.Columns.Add("ID", 0, HorizontalAlignment.Left)
        Lv_Kategori_Gudang.View = View.Details

        Lv_Users.Columns.Clear()
        Lv_Users.Columns.Add("User ID", 150, HorizontalAlignment.Left)
        Lv_Users.Columns.Add("Username", 150, HorizontalAlignment.Left)
        Lv_Users.View = View.Details

        Lv_Data.Columns.Clear()
        Lv_Data.Columns.Add("Kode Kategori Gudang", 150, HorizontalAlignment.Left)
        Lv_Data.Columns.Add("User ID", 150, HorizontalAlignment.Left)
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
        Txt_UserID.Text = String.Empty
        Txt_Username.Text = String.Empty
        Txt_Keterangan.Text = String.Empty
        Txt_Jenis_Kategori_Gudang.Text = String.Empty

        SelectedUrut = String.Empty

        Txt_ID_Kategori.Text = String.Empty

        Txt_Kd_Kategori.Enabled = True
        Txt_Kd_Kategori.BackColor = Color.White

        Rb_Desktop.Checked = False
        Rb_Android.Checked = False

        Rb_Desktop.Enabled = True
        Rb_Android.Enabled = True

        Txt_UserID.Enabled = False
        Txt_UserID.BackColor = Color.FromArgb(235, 235, 235)

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
                select Kode_Kategori_Gudang, User_ID, Keterangan, Urut_Oto
                from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and status is null
                order by User_ID, tanggal, Jam
            "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim Lv As ListViewItem
                    Lv = Lv_Data.Items.Add(Dr("Kode_Kategori_Gudang"))
                    Lv.SubItems.Add(Dr("User_ID"))
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
                        Txt_UserID.Focus()
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
                Txt_UserID.Focus()
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

        Txt_UserID.Focus()
    End Sub

    Private Sub Lv_Kategori_Gudang_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Kategori_Gudang.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Kategori_Gudang_DoubleClick(Lv_Kategori_Gudang, e)
        End If
    End Sub

    Private Sub Txt_Sub_Kategori_Jenis_TextChanged(sender As Object, e As EventArgs) Handles Txt_UserID.TextChanged
        If Swith_AutoComplete Then Exit Sub

        If Txt_UserID.Text.Trim.Length = 0 Then
            Lv_Users.Visible = False
            Lv_Users.Location = New Point(1190, 256)
            Txt_UserID.Text = ""
            Txt_Username.Text = ""
            Exit Sub
        Else
            Lv_Users.Location = New Point(158, 256)
            Lv_Users.Visible = True
        End If

        Try
            OpenConn()


            If Rb_Desktop.Checked = True Then

                Lv_Users.Items.Clear()
                SQL = $"
                    select UserID, UserName
                    from Users
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and userid like '%{Txt_UserID.Text.Trim}%'
                "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = Lv_Users.Items.Add(Dr("UserID"))
                        Lv.SubItems.Add(If(General_Class.CekNULL(Dr("UserName")) = "", "-", Dr("UserName")))
                    Loop
                End Using

            Else

                Lv_Users.Items.Clear()
                SQL = $"
                    select id, UserName
                    from Emi_Users
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and UserName like '%{Txt_UserID.Text.Trim}%'
                "
                Using Dr = OpenTrans(SQL)
                    Do While Dr.Read
                        Dim Lv As ListViewItem
                        Lv = Lv_Users.Items.Add(Dr("id"))
                        Lv.SubItems.Add(If(General_Class.CekNULL(Dr("UserName")) = "", "-", Dr("UserName")))
                    Loop
                End Using

            End If




            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_Sub_Kategori_Jenis_Leave(sender As Object, e As EventArgs) Handles Txt_UserID.Leave
        If Txt_UserID.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Users.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_UserID.Text.Trim.Length = 0 Then

                If Rb_Desktop.Checked Then
                    SQL = $"
                        select UserID, UserName
                        from Users
                        where Kode_Perusahaan = '{KodePerusahaan}'
                        and userid = '{Txt_UserID.Text.Trim}'
                    "
                    Using Dr = Open(SQL)
                        If Dr.Read Then
                            Swith_AutoComplete = True
                            Txt_UserID.Text = Dr("UserID")
                            Txt_Username.Text = If(General_Class.CekNULL(Dr("UserName")) = "", "-", Dr("UserName"))
                            Swith_AutoComplete = False
                            Txt_Keterangan.Focus()
                        Else
                            MessageBox.Show("User ID tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Txt_UserID.Text = ""
                            Txt_Username.Text = ""
                            Txt_UserID.Focus()
                        End If


                        Lv_Users.Visible = False
                        Lv_Users.Location = New Point(1190, 256)
                    End Using

                Else
                    SQL = $"
                        select Id, UserName
                        from Emi_Users
                        where Kode_Perusahaan = '{KodePerusahaan}'
                        and Id = '{Txt_UserID.Text.Trim}'
                    "
                    Using Dr = Open(SQL)
                        If Dr.Read Then
                            Swith_AutoComplete = True
                            Txt_UserID.Text = Dr("Id")
                            Txt_Username.Text = If(General_Class.CekNULL(Dr("UserName")) = "", "-", Dr("UserName"))
                            Swith_AutoComplete = False
                            Txt_Keterangan.Focus()
                        Else
                            MessageBox.Show("User ID tidak ditemukan . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Txt_UserID.Text = ""
                            Txt_Username.Text = ""
                            Txt_UserID.Focus()
                        End If


                        Lv_Users.Visible = False
                        Lv_Users.Location = New Point(1190, 256)
                    End Using
                End If

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

    Private Sub Txt_Sub_Kategori_Jenis_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_UserID.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_UserID.Text.Trim.Length = 0 Then Txt_UserID.Focus()
            Txt_Sub_Kategori_Jenis_Leave(Txt_UserID, e)

            Lv_Users.Visible = False
            Lv_Users.Location = New Point(1190, 256)

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_Sub_Kategori_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_UserID.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Users.Focus()
    End Sub

    Private Sub Lv_Sub_Kategori_Jenis_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Users.DoubleClick
        If Lv_Users.Items.Count = 0 Or Lv_Users.FocusedItem.Index = -1 Then Exit Sub


        Dim Kd_Sub_Kategori As String = Lv_Users.FocusedItem.SubItems(0).Text
        Dim Kd_Kategori_Jenis As String = Lv_Users.FocusedItem.SubItems(1).Text

        Swith_AutoComplete = True
        Txt_UserID.Text = Kd_Sub_Kategori
        Txt_Username.Text = Kd_Kategori_Jenis
        Swith_AutoComplete = False

        Lv_Users.Visible = False
        Lv_Users.Location = New Point(1190, 256)

        Txt_Keterangan.Focus()
    End Sub

    Private Sub Lv_Sub_Kategori_Jenis_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Users.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Sub_Kategori_Jenis_DoubleClick(Lv_Users, e)
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
                select Kode_Kategori_Gudang, User_ID, Keterangan, Urut_Oto, User_Id_Android
                from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and Kode_Kategori_Gudang = '{Lv_Kd_Kategori_Gudang}'
                and User_ID = '{Lv_Kd_Sub_Kategori_Gudang_Jenis}'
                and urut_oto = '{Lv_Urut}'
            "
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        For i As Integer = 0 To .Rows.Count - 1

                            Swith_AutoComplete = True
                            Txt_Kd_Kategori.Text = .Rows(i).Item("Kode_Kategori_Gudang")
                            Swith_AutoComplete = False
                            Txt_Kd_Kategori_Leave(sender, New EventArgs)

                            If General_Class.CekNULL(.Rows(i).Item("User_ID")) = "-" Then
                                Rb_Android.Checked = True
                                Swith_AutoComplete = True
                                Txt_UserID.Text = .Rows(i).Item("User_Id_Android")
                                Swith_AutoComplete = False
                            Else
                                Rb_Desktop.Checked = True
                                Swith_AutoComplete = True
                                Txt_UserID.Text = .Rows(i).Item("User_ID")
                                Swith_AutoComplete = False
                            End If



                            Txt_Sub_Kategori_Jenis_Leave(sender, New EventArgs)


                            Txt_Keterangan.Text = .Rows(i).Item("Keterangan")

                            SelectedUrut = .Rows(i).Item("Urut_Oto")

                            Txt_Kd_Kategori.Enabled = False
                            Txt_Kd_Kategori.BackColor = Color.FromArgb(235, 235, 235)

                            Txt_UserID.Enabled = False
                            Txt_UserID.BackColor = Color.FromArgb(235, 235, 235)

                            Rb_Desktop.Enabled = False
                            Rb_Android.Enabled = False

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
        ElseIf Txt_UserID.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Sub Kategori Jenis Harus Diisi", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_UserID.Focus()
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



                '===============================================================
                '=      CEK CEK APAKAH KATEGORI WAREHOUSE ATAU DEPARTEMENT     =
                '===============================================================
                SQL = $"
                    select Jenis_Gudang
                    from N_EMI_Master_Kategori_Gudang_Barang_Lain
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Urut_Oto = '{Txt_ID_Kategori.Text.Trim}'
                "
                Using DS = BindingTrans(SQL)
                    With DS.Tables("MYTable")
                        If .Rows.Count <> 0 Then

                            If .Rows(0).Item("Jenis_Gudang") = "Warehouse" Then

                                '========================================================
                                '=      CEK CEK APAKAH USER SUDAH BINDING WAREHOUSE     =
                                '========================================================
                                SQL = $"
                                    select a.Kode_Perusahaan
                                    from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain a
	                                    inner join N_EMI_Master_Kategori_Gudang_Barang_Lain b on a.Kode_Perusahaan = b.Kode_Perusahaan and a.Id_Kategori_Gudang = b.Urut_Oto
                                    where a.Status is null
                                    and a.Kode_Perusahaan = '{KodePerusahaan}'
                                    and a.User_ID = '{Txt_UserID.Text}'
                                    and b.Jenis_Gudang = 'Warehouse'
                                "
                                Using Dr = OpenTrans(SQL)
                                    If Dr.Read Then
                                        Dr.Close()
                                        CloseTrans()
                                        CloseConn()
                                        MessageBox.Show($"User {Txt_UserID.Text.Trim} Telah Memiliki Binding Ke Gudang Warehouse", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                        Exit Sub
                                    End If
                                End Using

                            End If

                        End If
                    End With
                End Using

                '======================================
                '=      CEK APAKAH DATA SUDAH ADA     =
                '======================================
                SQL = $"
                    select Kode_Perusahaan
                    from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                    and User_ID = '{Txt_UserID.Text.Trim}'
                "
                Using Dr = OpenTrans(SQL)
                    If Dr.Read Then
                        Dr.Close()
                        CloseTrans()
                        CloseConn()
                        MessageBox.Show($"Kode Kategori Gudang dan User Sudah Di Binding", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub
                    End If
                End Using




                If Rb_Desktop.Checked Then

                    SQL = $"
                        insert into N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain (Kode_Perusahaan, Kode_Kategori_Gudang, User_ID, Keterangan, Tanggal, Jam, Id_Kategori_Gudang)
                        values ('{KodePerusahaan}', '{Txt_Kd_Kategori.Text.Trim}', '{Txt_UserID.Text.Trim}', '{Txt_Keterangan.Text.Trim}', 
                        '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{Txt_ID_Kategori.Text.Trim}')
                    "
                    ExecuteTrans(SQL)

                Else
                    SQL = $"
                        insert into N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain (Kode_Perusahaan, Kode_Kategori_Gudang, User_ID, User_Id_Android, Keterangan, Tanggal, Jam, Id_Kategori_Gudang)
                        values ('{KodePerusahaan}', '{Txt_Kd_Kategori.Text.Trim}', '-', '{Txt_UserID.Text.Trim}', '{Txt_Keterangan.Text.Trim}', 
                        '{Format(tgl_skg, "yyyy-MM-dd")}', '{Format(tgl_skg, "HH:mm:ss")}', '{Txt_ID_Kategori.Text.Trim}')
                    "
                    ExecuteTrans(SQL)

                End If


                Action = "Simpan"

            ElseIf Btn_Simpan.Tag.ToString.ToUpper = "UPDATE" Then

                '======================================
                '=      CEK APAKAH DATA SUDAH ADA     =
                '======================================
                If Rb_Desktop.Checked Then
                    SQL = $"
                    select Kode_Perusahaan
                    from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain
                    where Kode_Perusahaan = '{KodePerusahaan}'
                    and status is null
                    and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                    and User_ID = '{Txt_UserID.Text.Trim}'
                    and Urut_Oto = '{SelectedUrut}'
                "
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show($"Kode Kategori Gudang dan User Jenis Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = $"
                        update N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain
                        set Keterangan = '{Txt_Keterangan.Text.Trim}' 
                        where Kode_Perusahaan = '{KodePerusahaan}'
                        and status is null
                        and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                        and User_ID = '{Txt_UserID.Text.Trim}'
                        and Urut_Oto = '{SelectedUrut}'
                    "
                    ExecuteTrans(SQL)

                Else
                    SQL = $"
                        select Kode_Perusahaan
                        from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain
                        where Kode_Perusahaan = '{KodePerusahaan}'
                        and status is null
                        and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                        and User_Id_Android = '{Txt_UserID.Text.Trim}'
                        and Urut_Oto = '{SelectedUrut}'
                    "
                    Using Dr = OpenTrans(SQL)
                        If Not Dr.Read Then
                            Dr.Close()
                            CloseTrans()
                            CloseConn()
                            MessageBox.Show($"Kode Kategori Gudang dan User Jenis Tidak Ditemukan", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            Exit Sub
                        End If
                    End Using

                    SQL = $"
                        update N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain
                        set Keterangan = '{Txt_Keterangan.Text.Trim}' 
                        where Kode_Perusahaan = '{KodePerusahaan}'
                        and status is null
                        and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                        and User_Id_Android = '{Txt_UserID.Text.Trim}'
                        and Urut_Oto = '{SelectedUrut}'
                    "
                    ExecuteTrans(SQL)
                End If



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
            If CekButtonRole("Hapus_Binding_User_Kategori_Gudang_Barang_Lain") = "T" Then
                CloseTrans()
                CloseConn()
                MessageBox.Show("Anda Tidak Memiliki Akses Untuk Menghapus Binding User Kategori Gudang", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            '===============================
            '=     CEK APAKAH DATA ADA     =
            '===============================
            SQL = $"
                select Kode_Kategori_Gudang, User_ID, Keterangan
                from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                and User_ID = '{Txt_UserID.Text.Trim}'
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
                from N_EMI_Master_Kategori_Gudang_Binding_User_Barang_Lain
                where Kode_Perusahaan = '{KodePerusahaan}'
                and Status is null
                and Kode_Kategori_Gudang = '{Txt_Kd_Kategori.Text.Trim}'
                and User_ID = '{Txt_UserID.Text.Trim}'
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

    Private Sub Rb_Desktop_CheckedChanged(sender As Object, e As EventArgs) Handles Rb_Desktop.CheckedChanged
        If Rb_Desktop.Checked Then
            Txt_UserID.Enabled = True
            Txt_UserID.BackColor = Color.White
        Else
            Txt_UserID.Enabled = False
            Txt_UserID.BackColor = Color.FromArgb(235, 235, 235)
        End If
        Txt_UserID.Text = ""
    End Sub

    Private Sub Rb_Android_CheckedChanged(sender As Object, e As EventArgs) Handles Rb_Android.CheckedChanged
        If Rb_Android.Checked Then
            Txt_UserID.Enabled = True
            Txt_UserID.BackColor = Color.White
        Else
            Txt_UserID.Enabled = False
            Txt_UserID.BackColor = Color.FromArgb(235, 235, 235)
        End If
        Txt_UserID.Text = ""
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