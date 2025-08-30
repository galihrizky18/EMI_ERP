Public Class Emi_Laporan_Pelunasan



    Private Sub Emi_Laporan_Pelunasan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kosong()
    End Sub

    Private Sub kosong()
        Tgl1.Value = Now.Date
        Tgl2.Value = Now.Date

        Txt_Faktur.Text = ""
        Txt_UserValidasi.Text = ""
        Txt_KdPerusahaan.Text = "" : Txt_NmPerusahaan.Text = ""
        Txt_KdKategori.Text = "" : Txt_NmKategori.Text = ""


        Lv_Faktur.Columns.Clear()
        Lv_Faktur.Columns.Add("No Faktur", 150, HorizontalAlignment.Left)
        Lv_Faktur.Columns.Add("Tanggal", 200, HorizontalAlignment.Center)
        Lv_Faktur.View = View.Details

        Lv_User.Columns.Clear()
        Lv_User.Columns.Add("User Id", 150, HorizontalAlignment.Left)
        Lv_User.Columns.Add("Nama", 200, HorizontalAlignment.Center)
        Lv_User.View = View.Details

        Lv_Perusahaan.Columns.Clear()
        Lv_Perusahaan.Columns.Add("Kode Perusahaan Biaya Import", 150, HorizontalAlignment.Left)
        Lv_Perusahaan.Columns.Add("Nama Perusahaan Biaya Import", 200, HorizontalAlignment.Center)
        Lv_Perusahaan.View = View.Details

        Lv_Perusahaan.Columns.Clear()
        Lv_Perusahaan.Columns.Add("Kode Perusahaan Biaya Import", 150, HorizontalAlignment.Left)
        Lv_Perusahaan.Columns.Add("Nama Perusahaan Biaya Import", 200, HorizontalAlignment.Center)
        Lv_Perusahaan.View = View.Details

        Lv_Kategori.Columns.Clear()
        Lv_Kategori.Columns.Add("Kode Kategori Biaya Import", 150, HorizontalAlignment.Left)
        Lv_Kategori.Columns.Add("Nama Kategori Biaya Import", 200, HorizontalAlignment.Center)
        Lv_Kategori.View = View.Details





    End Sub













    '=======================================================================================================================================================================================
    '=     HANDLE TEXT CHANGE
    '=======================================================================================================================================================================================
    Private Sub Txt_Faktur_TextChanged(sender As Object, e As EventArgs) Handles Txt_Faktur.TextChanged
        If Txt_Faktur.Text.Trim.Length = 0 Then
            Me.Size = New Size(613, 325)
            Lv_Faktur.Location = New Point(600, 134)
            Lv_Faktur.Visible = False
            Txt_Faktur.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(613, 382)
            Lv_Faktur.Visible = True
            Lv_Faktur.Location = New Point(190, 134)
        End If

        Try
            OpenConn()

            Lv_Faktur.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Faktur.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select No_Val, Tanggal from EMI_Pelunasan where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val like '%" & Txt_Faktur.Text & "%' and status is null"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Faktur.Items.Add(Dr("No_Val"))

                    If General_Class.CekNULL(Dr("Tanggal")) = "" Then
                        Lv.SubItems.Add("-")
                    Else
                        Lv.SubItems.Add(Format(Dr("Tanggal"), "dd MMM yyyy"))
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
    Private Sub Txt_UserValidasi_TextChanged(sender As Object, e As EventArgs) Handles Txt_UserValidasi.TextChanged
        If Txt_UserValidasi.Text.Trim.Length = 0 Then
            Me.Size = New Size(613, 325)
            Lv_User.Location = New Point(600, 163)
            Lv_User.Visible = False
            Txt_UserValidasi.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(613, 414)
            Lv_User.Visible = True
            Lv_User.Location = New Point(190, 163)
        End If

        Try
            OpenConn()

            Lv_User.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_User.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")
            SQL = "select UserID, UserName from users where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID like '%" & Txt_UserValidasi.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_User.Items.Add(Dr("UserID"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("UserName")) = "", "-", Dr("UserName")))

                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdPerusahaan_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdPerusahaan.TextChanged
        If Txt_KdPerusahaan.Text.Trim.Length = 0 Then
            Me.Size = New Size(613, 325)
            Lv_Perusahaan.Location = New Point(600, 192)
            Lv_Perusahaan.Visible = False
            Txt_KdPerusahaan.Text = ""
            Txt_NmPerusahaan.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(613, 442)
            Lv_Perusahaan.Visible = True
            Lv_Perusahaan.Location = New Point(190, 192)
        End If


        Try
            OpenConn()

            Lv_Perusahaan.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Perusahaan.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select Kode_Perusahaan_Biaya_Import, Nama from Perusahaan_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Perusahaan_Biaya_Import like '%" & Txt_KdPerusahaan.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Perusahaan.Items.Add(Dr("Kode_Perusahaan_Biaya_Import"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Nama")) = "", "-", Dr("Nama")))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_NmPerusahaan_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmPerusahaan.TextChanged
        If Txt_NmPerusahaan.Text.Trim.Length = 0 Then
            Me.Size = New Size(613, 325)
            Lv_Perusahaan.Location = New Point(600, 192)
            Lv_Perusahaan.Visible = False
            Txt_KdPerusahaan.Text = ""
            Txt_NmPerusahaan.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(613, 442)
            Lv_Perusahaan.Visible = True
            Lv_Perusahaan.Location = New Point(190, 192)
        End If


        Try
            OpenConn()

            Lv_Perusahaan.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Perusahaan.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select Kode_Perusahaan_Biaya_Import, Nama from Perusahaan_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Nama like '%" & Txt_NmPerusahaan.Text & "%' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Perusahaan.Items.Add(Dr("Kode_Perusahaan_Biaya_Import"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Nama")) = "", "-", Dr("Nama")))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_KdKategori_TextChanged(sender As Object, e As EventArgs) Handles Txt_KdKategori.TextChanged
        If Txt_KdKategori.Text.Trim.Length = 0 Then
            Me.Size = New Size(613, 325)
            Lv_Kategori.Location = New Point(600, 220)
            Lv_Kategori.Visible = False
            Txt_KdKategori.Text = ""
            Txt_NmKategori.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(613, 470)
            Lv_Kategori.Visible = True
            Lv_Kategori.Location = New Point(190, 220)
        End If


        Try
            OpenConn()

            Lv_Kategori.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Kategori.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select Kode_Master_Kategori_Biaya_Import, Keterangan from Master_Kategori_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Master_Kategori_Biaya_Import like '%" & Txt_KdKategori.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Kategori.Items.Add(Dr("Kode_Master_Kategori_Biaya_Import"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan")) = "", "-", Dr("Keterangan")))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_NmKategori_TextChanged(sender As Object, e As EventArgs) Handles Txt_NmKategori.TextChanged
        If Txt_NmKategori.Text.Trim.Length = 0 Then
            Me.Size = New Size(613, 325)
            Lv_Kategori.Location = New Point(600, 220)
            Lv_Kategori.Visible = False
            Txt_KdKategori.Text = ""
            Txt_NmKategori.Text = ""
            Exit Sub
        Else
            Me.Size = New Size(613, 470)
            Lv_Kategori.Visible = True
            Lv_Kategori.Location = New Point(190, 220)
        End If


        Try
            OpenConn()

            Lv_Kategori.Items.Clear()

            Dim Lv As ListViewItem
            Lv = Lv_Kategori.Items.Add("--- SELURUH ---")
            Lv.SubItems.Add("--- SELURUH ---")

            SQL = "select Kode_Master_Kategori_Biaya_Import, Keterangan from Master_Kategori_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Keterangan like '%" & Txt_NmKategori.Text & "%'"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Lv = Lv_Kategori.Items.Add(Dr("Kode_Master_Kategori_Biaya_Import"))
                    Lv.SubItems.Add(If(General_Class.CekNULL(Dr("Keterangan")) = "", "-", Dr("Keterangan")))
                Loop
            End Using


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub






    '=======================================================================================================================================================================================
    '=     HANDLE LEAVE
    '=======================================================================================================================================================================================
    Private Sub Txt_Faktur_Leave(sender As Object, e As EventArgs) Handles Txt_Faktur.Leave
        If Txt_Faktur.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Faktur.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_Faktur.Text = "--- SELURUH ---" Then

                SQL = "select No_Val, Tanggal from EMI_Pelunasan where Kode_Perusahaan = '" & KodePerusahaan & "' and No_Val = '" & Txt_Faktur.Text & "' and status is null"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_Faktur.Text = Dr("No_Val")
                        Txt_UserValidasi.Focus()
                    Else
                        MessageBox.Show("No Pelunasan tidak ditemukan . . ! !", Judul)
                        Txt_Faktur.Text = ""
                        Txt_Faktur.Focus()
                    End If

                    Me.Size = New Size(613, 325)
                    Lv_Faktur.Location = New Point(600, 134)
                    Lv_Faktur.Visible = False
                End Using

            Else
                Txt_UserValidasi.Focus()
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_UserValidasi_Leave(sender As Object, e As EventArgs) Handles Txt_UserValidasi.Leave
        If Txt_UserValidasi.Text.Trim.Length = 0 Then Exit Sub
        If Lv_User.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_UserValidasi.Text = "--- SELURUH ---" Then

                SQL = "select UserID, UserName from users where Kode_Perusahaan = '" & KodePerusahaan & "' and UserID = '" & Txt_UserValidasi.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_UserValidasi.Text = Dr("UserID")
                        Txt_KdPerusahaan.Focus()
                    Else
                        MessageBox.Show("User tidak ditemukan . . ! !", Judul)
                        Txt_UserValidasi.Text = ""
                        Txt_UserValidasi.Focus()
                    End If

                    Me.Size = New Size(613, 325)
                    Lv_User.Location = New Point(600, 163)
                    Lv_User.Visible = False
                End Using

            Else
                Txt_KdPerusahaan.Focus()
            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Txt_KdPerusahaan_Leave(sender As Object, e As EventArgs) Handles Txt_KdPerusahaan.Leave
        If Txt_KdPerusahaan.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Perusahaan.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdPerusahaan.Text = "--- SELURUH ---" Then

                SQL = "select Kode_Perusahaan_Biaya_Import, Nama from Perusahaan_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Perusahaan_Biaya_Import = '" & Txt_KdPerusahaan.Text & "' "
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdPerusahaan.Text = Dr("Kode_Perusahaan_Biaya_Import")
                        Txt_NmPerusahaan.Text = Dr("Nama")
                        Txt_KdKategori.Focus()
                    Else
                        MessageBox.Show("Perusahaan tidak ditemukan . . ! !", Judul)
                        Txt_KdPerusahaan.Text = ""
                        Txt_NmPerusahaan.Text = ""
                        Txt_KdPerusahaan.Focus()
                    End If

                    Me.Size = New Size(613, 325)
                    Lv_Perusahaan.Location = New Point(600, 192)
                    Lv_Perusahaan.Visible = False
                End Using

            Else
                Txt_KdKategori.Focus()

            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Txt_KdKategori_Leave(sender As Object, e As EventArgs) Handles Txt_KdKategori.Leave
        If Txt_KdKategori.Text.Trim.Length = 0 Then Exit Sub
        If Lv_Kategori.Focused = True Then Exit Sub

        Try
            OpenConn()

            If Not Txt_KdKategori.Text = "--- SELURUH ---" Then

                SQL = "select Kode_Master_Kategori_Biaya_Import, Keterangan from Master_Kategori_Biaya_Import where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Master_Kategori_Biaya_Import = '" & Txt_KdKategori.Text & "'"
                Using Dr = Open(SQL)
                    If Dr.Read Then
                        Txt_KdKategori.Text = Dr("Kode_Master_Kategori_Biaya_Import")
                        Txt_NmKategori.Text = Dr("Keterangan")
                        BtnCetak.Focus()
                    Else
                        MessageBox.Show("Kategori Perusahaan Biaya Import tidak ditemukan . . ! !", Judul)
                        Txt_KdKategori.Text = ""
                        Txt_NmKategori.Text = ""
                        Lv_Kategori.Focus()
                    End If

                    Me.Size = New Size(613, 325)
                    Lv_Kategori.Location = New Point(600, 220)
                    Lv_Kategori.Visible = False
                End Using

            Else
                BtnCetak.Focus()

            End If


            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub







    '=======================================================================================================================================================================================
    '=     HANDLE LISTVIEW
    '=======================================================================================================================================================================================
    Private Sub Lv_Faktur_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Faktur.DoubleClick
        If Lv_Faktur.Items.Count = 0 Or Lv_Faktur.FocusedItem.Index = -1 Then Exit Sub

        Dim Faktur As String = Lv_Faktur.FocusedItem.SubItems(0).Text

        Txt_Faktur.Text = Faktur

        Me.Size = New Size(613, 325)
        Lv_Faktur.Location = New Point(600, 134)
        Lv_Faktur.Visible = False
        Txt_UserValidasi.Focus()
    End Sub
    Private Sub Lv_User_DoubleClick(sender As Object, e As EventArgs) Handles Lv_User.DoubleClick
        If Lv_User.Items.Count = 0 Or Lv_User.FocusedItem.Index = -1 Then Exit Sub

        Dim UserId As String = Lv_User.FocusedItem.SubItems(0).Text

        Txt_UserValidasi.Text = UserId

        Me.Size = New Size(613, 325)
        Lv_User.Location = New Point(600, 163)
        Lv_User.Visible = False
        Txt_KdPerusahaan.Focus()
    End Sub
    Private Sub Lv_Perusahaan_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Perusahaan.DoubleClick
        If Lv_Perusahaan.Items.Count = 0 Or Lv_Perusahaan.FocusedItem.Index = -1 Then Exit Sub

        Dim KdPerusahaan As String = Lv_Perusahaan.FocusedItem.SubItems(0).Text
        Dim NmPerusahaan As String = Lv_Perusahaan.FocusedItem.SubItems(1).Text

        Txt_KdPerusahaan.Text = KdPerusahaan
        Txt_NmPerusahaan.Text = NmPerusahaan

        Me.Size = New Size(613, 325)
        Lv_Perusahaan.Location = New Point(600, 192)
        Lv_Perusahaan.Visible = False
        Txt_KdKategori.Focus()
    End Sub
    Private Sub Lv_Kategori_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Kategori.DoubleClick
        If Lv_Kategori.Items.Count = 0 Or Lv_Kategori.FocusedItem.Index = -1 Then Exit Sub

        Dim KdKategori As String = Lv_Kategori.FocusedItem.SubItems(0).Text
        Dim NmKategori As String = Lv_Kategori.FocusedItem.SubItems(1).Text

        Txt_KdKategori.Text = KdKategori
        Txt_NmKategori.Text = NmKategori

        Me.Size = New Size(613, 325)
        Lv_Kategori.Location = New Point(600, 220)
        Lv_Kategori.Visible = False
        BtnCetak.Focus()
    End Sub

    Private Sub Lv_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Faktur.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Faktur_DoubleClick(Lv_Faktur, e)
        End If
    End Sub
    Private Sub Lv_User_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_User.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_User_DoubleClick(Lv_User, e)
        End If
    End Sub
    Private Sub Lv_Perusahaan_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Perusahaan.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Perusahaan_DoubleClick(Lv_Perusahaan, e)
        End If
    End Sub
    Private Sub Lv_Kategori_KeyDown(sender As Object, e As KeyEventArgs) Handles Lv_Kategori.KeyDown
        If e.KeyCode = Keys.Enter Then
            Lv_Kategori_DoubleClick(Lv_Kategori, e)
        End If
    End Sub






    '=======================================================================================================================================================================================
    '=     HANDLE KEY PRESS
    '=======================================================================================================================================================================================
    Private Sub Tgl1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl1.KeyPress
        If e.KeyChar = Chr(13) Then Tgl2.Focus()
    End Sub

    Private Sub Tgl2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Tgl2.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Faktur.Focus()
    End Sub

    Private Sub Txt_Faktur_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Faktur.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_Faktur.Text.Trim.Length = 0 Then Txt_Faktur.Focus()
            Txt_Faktur_Leave(Txt_Faktur, e)

            Me.Size = New Size(613, 325)
            Lv_Faktur.Location = New Point(600, 134)
            Lv_Faktur.Visible = False

            'Txt_UserValidasi.Focus()
        End If
    End Sub
    Private Sub Txt_UserValidasi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_UserValidasi.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_UserValidasi.Text.Trim.Length = 0 Then Txt_UserValidasi.Focus()
            Txt_UserValidasi_Leave(Txt_UserValidasi, e)

            Me.Size = New Size(613, 325)
            Lv_User.Location = New Point(600, 163)
            Lv_User.Visible = False

            'Txt_UserValidasi.Focus()
        End If
    End Sub
    Private Sub Txt_KdPerusahaan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdPerusahaan.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdPerusahaan.Text.Trim.Length = 0 Then Txt_KdPerusahaan.Focus()
            Txt_KdPerusahaan_Leave(Txt_KdPerusahaan, e)

            Me.Size = New Size(613, 325)
            Lv_Perusahaan.Location = New Point(600, 192)
            Lv_Perusahaan.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_NmPerusahaan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmPerusahaan.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdPerusahaan_Leave(Txt_NmPerusahaan, e)
            Me.Size = New Size(613, 325)
            Lv_Perusahaan.Location = New Point(600, 192)
            Lv_Perusahaan.Visible = False

            'Txt_KdKategori.Focus()
        End If
    End Sub
    Private Sub Txt_KdKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KdKategori.KeyPress
        If e.KeyChar = Chr(13) Then
            If Txt_KdKategori.Text.Trim.Length = 0 Then Txt_KdKategori.Focus()
            Txt_KdKategori_Leave(Txt_KdKategori, e)

            Me.Size = New Size(613, 325)
            Lv_Perusahaan.Location = New Point(600, 220)
            Lv_Perusahaan.Visible = False

            'BtnCetak.Focus()
        End If
    End Sub
    Private Sub Txt_NmKategori_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmKategori.KeyPress
        If e.KeyChar = Chr(13) Then
            Txt_KdKategori_Leave(Txt_NmKategori, e)
            Me.Size = New Size(613, 325)
            Lv_Kategori.Location = New Point(600, 220)
            Lv_Kategori.Visible = False

            'BtnCetak.Focus()
        End If
    End Sub

    Private Sub Txt_Faktur_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_Faktur.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Faktur.Focus()
    End Sub

    Private Sub Txt_UserValidasi_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_UserValidasi.KeyDown
        If e.KeyCode = Keys.Down Then Lv_User.Focus()
    End Sub

    Private Sub Txt_KdPerusahaan_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdPerusahaan.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Perusahaan.Focus()
    End Sub

    Private Sub Txt_NmPerusahaan_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmPerusahaan.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Perusahaan.Focus()
    End Sub
    Private Sub Txt_KdKategori_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_KdKategori.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Kategori.Focus()
    End Sub

    Private Sub Txt_NmKategori_KeyDown(sender As Object, e As KeyEventArgs) Handles Txt_NmKategori.KeyDown
        If e.KeyCode = Keys.Down Then Lv_Kategori.Focus()
    End Sub


    '=======================================================================================================================================================================================
    '=     HANDLE BUTTON
    '=======================================================================================================================================================================================

    Private Sub BtnCetak_Click(sender As Object, e As EventArgs) Handles BtnCetak.Click
        If Tgl1.Value > Tgl2.Value Then
            MessageBox.Show("Periode I tidak boleh lebih dari periode II!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Tgl1.Value = Now.Date : Tgl2.Value = Now.Date
            Tgl1.Focus() : Exit Sub
        ElseIf Txt_Faktur.Text.Trim.Length = 0 Then
            MessageBox.Show("Faktur harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_Faktur.Focus() : Exit Sub
        ElseIf Txt_UserValidasi.Text.Trim.Length = 0 Then
            MessageBox.Show("User Validasi harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_UserValidasi.Focus() : Exit Sub

        ElseIf Txt_KdPerusahaan.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Perusahaan Biaya Import harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdPerusahaan.Focus() : Exit Sub
        ElseIf Txt_KdKategori.Text.Trim.Length = 0 Then
            MessageBox.Show("Kode Kategori Biaya Import harus diisi!", "Perhatian", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KdKategori.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Dim SF As String = ""

            SQL = "select kode_perusahaan from View_Laporan_Pelunasan "
            SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Tanggal between '" & Format(Tgl1.Value, "yyyy-MM-dd") & "' and '" & Format(Tgl2.Value, "yyyy-MM-dd") & "' "

            SF = "{View_Laporan_Pelunasan.kode_perusahaan} = '" & KodePerusahaan & "' "
            SF = SF & "and {View_Laporan_Pelunasan.Tanggal} >= #" & Format(Tgl1.Value, "yyyy-MM-dd") & "# and "
            SF = SF & "{View_Laporan_Pelunasan.Tanggal} <= #" & Format(Tgl2.Value, "yyyy-MM-dd") & "# "

            If Not Txt_Faktur.Text = "--- SELURUH ---" Then
                SQL = SQL & "and No_Val = '" & Txt_Faktur.Text & "' "
                SF = SF & "And {View_Laporan_Pelunasan.No_Val} = '" & Txt_Faktur.Text & "'"
            End If

            If Not Txt_UserValidasi.Text = "--- SELURUH ---" Then
                SQL = SQL & "and UserValidasi = '" & Txt_UserValidasi.Text & "' "
                SF = SF & "And {View_Laporan_Pelunasan.UserValidasi} = '" & Txt_UserValidasi.Text & "'"
            End If

            If Not Txt_KdPerusahaan.Text = "--- SELURUH ---" Then
                SQL = SQL & "and kode_perusahaan_Biaya_Import = '" & Txt_KdPerusahaan.Text & "' "
                SF = SF & "And {View_Laporan_Pelunasan.kode_perusahaan_Biaya_Import} = '" & Txt_KdPerusahaan.Text & "'"
            End If

            If Not Txt_KdKategori.Text = "--- SELURUH ---" Then
                SQL = SQL & "and kode_master_kategori_biaya_import = '" & Txt_KdKategori.Text & "' "
                SF = SF & "And {View_Laporan_Pelunasan.kode_master_kategori_biaya_import} = '" & Txt_KdKategori.Text & "'"
            End If


            Using DS = BindingTrans(SQL)
                With DS.Tables("MyTable")
                    If .Rows.Count <> 0 Then

                        Dim CrDoc As New Rpt_Laporan_Pelunasan

                        CrDoc.SetDataSource(DS)
                        CrDoc.SetDatabaseLogon(CUserId, CPassword, CServer, CDatabase)
                        CrDoc.SummaryInfo.ReportTitle = "Periode : " & Format(Tgl1.Value, "dd/MMM/yyyy") & " s/d " &
                                                                            Format(Tgl2.Value, "dd/MMM/yyyy")
                        CrDoc.RecordSelectionFormula = SF

                        With A_Place_For_Printing2
                            .Text = "Laporan Pelunasan"
                            .CrystalReportViewer1.ReportSource = CrDoc
                            .CrystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
                            .Refresh()
                            .Show()
                        End With
                    Else

                        CloseConn()
                        MessageBox.Show("Data Tidak Ditemukan", "Laporan Pelunasan", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        Exit Sub

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

    Private Sub BtnExit_Click(sender As Object, e As EventArgs) Handles BtnExit.Click
        Me.Close()
    End Sub
End Class