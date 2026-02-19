Imports System.Reflection
Public Class Form_Login



    Private Sub Form_Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.Lokasi.Trim.Length = 0 Then
            My.Settings.Lokasi = "HEAD OFFICE"
            My.Settings.Save()

            MessageBox.Show("Lokasi belum di setting!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End
        End If


        Dim url_updater As String = ""

        If My.Settings.Prt_Name = "XXX" Then
            My.Settings.Upgrade()
        End If

        Try
            OpenConn()

            Using Dr = OpenTrans("select url_updater from perusahaan order by kode_perusahaan")
                Do While Dr.Read
                    url_updater = Dr("url_updater")
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        'AutoUpdater.Mandatory = True
        'AutoUpdater.Start(url_updater)

        Try
            OpenConn()

            ComboBox1.Items.Clear()
            Using Dr = OpenTrans("select Kode_perusahaan, nama from perusahaan order by kode_perusahaan")
                Do While Dr.Read
                    ComboBox1.Items.Add(Dr("Kode_perusahaan") & " - " & Dr("nama"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        If ComboBox1.Items.Count = 0 Then
            ComboBox1.SelectedIndex = -1
        Else
            ComboBox1.SelectedIndex = 0
        End If

        TextBox1.Text = "" : TextBox2.Text = ""
        TextBox2.PasswordChar = ""
        TextBox1.Focus()

    End Sub


    Private Sub ComboBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox1.Focus()
    End Sub

    Private Sub TextBox1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.GotFocus
        If TextBox1.Text.ToUpper = "" Then
            TextBox1.Text = ""
        End If
    End Sub

    Private Sub TextBox1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.LostFocus
        If TextBox1.Text.Trim.Length = 0 Then
            TextBox1.Text = ""
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub

    Private Sub TextBox2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.GotFocus
        If TextBox2.Text.ToUpper = "" Then
            TextBox2.Text = ""
            TextBox2.PasswordChar = "*"
        End If
    End Sub

    Private Sub TextBox2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox2.LostFocus
        If TextBox2.Text.Trim.Length = 0 Then
            'TextBox2.Text = ""
            TextBox2.PasswordChar = ""
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then Button1_Click(TextBox2, e)
        If e.KeyChar = Chr(Asc("'")) Then e.KeyChar = Chr(0)
    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLogin.Click
        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show("Perusahaan harus diisi terlebih dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            ComboBox1.Focus() : Exit Sub
        ElseIf TextBox1.Text.Trim.Length = 0 Or TextBox1.Text.Trim.ToUpper = "<USERNAME>" Then
            MessageBox.Show("User ID harus diisi terlebih dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Or TextBox2.Text.Trim.ToUpper = "<PASSWORD>" Then
            MessageBox.Show("Password harus diisi terlebih dahulu!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        End If

        Dim versionNumber As Version
        versionNumber = Assembly.GetExecutingAssembly().GetName().Version

        Try
            OpenConn()

            'SQL = "select * from vrs"
            'Using Dr = OpenTrans(SQL)
            '    If Dr.Read Then
            '        If General_Class.CekNULL(Dr("bypass")) = "" Then
            '            If Dr("versi") <> versionNumber.ToString Then
            '                Dr.Close()
            '                CloseConn()
            '                MessageBox.Show("Versi aplikasi Anda berbeda dengan versi sekarang! Download dahulu ke versi yang lebih baru.", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '                Me.Close()
            '                Exit Sub
            '            End If
            '        End If
            '    End If
            'End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        Try
            OpenConn()

            xSplit = ComboBox1.Text.Split("-")

            SQL = "select a.jatuh_tempo_pemb, a.jatuh_tempo_penj, b.kode_perusahaan, b.userid, b.username, "
            SQL = SQL & "b.pass, b.userlevel from perusahaan a, users b where "
            SQL = SQL & "a.kode_perusahaan = b.kode_perusahaan and a.kode_perusahaan = '" & xSplit(0).Trim & "' and "
            SQL = SQL & "b.userid = '" & TextBox1.Text.Trim & "' and b.pass = hashbytes('MD5', '" & TextBox2.Text & "')"
            Using Ds = BindingTrans(SQL)
                With Ds.Tables("MyTable")
                    If .Rows.Count <> 0 Then
                        UserName = .Rows(0).Item("username").ToString.ToUpper
                        UserLevel = .Rows(0).Item("Userlevel").ToString.ToUpper

                        PrinterName = My.Settings.Prt_Name
                        PrinterNameTS = My.Settings.Prt_Name_TS
                        PrinterNameSPB = My.Settings.Prt_Name_SPB
                        PrinterNameBPB = My.Settings.Prt_Name_BPB
                        PrinterNameBuktiTimbang = My.Settings.Prt_Name_Bukti_Timbang
                        PrinterName2 = My.Settings.Prt_Name_2
                        PrinterBarcode = My.Settings.Prt_Barcode
                        PrinterQC = My.Settings.Prt_QC
                        PrinterBarcodeQC = My.Settings.Prt_Barcode_QC

                        KodePerusahaan = xSplit(0).Trim
                        NamaPerusahaan = xSplit(1).Trim
                        UserID = TextBox1.Text.Trim.ToUpper

                        Main_Menu.Show()
                        Me.Hide()
                    Else
                        MessageBox.Show("User ID atau Password tidak ditemukan!", Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        TextBox1.Text = "" : TextBox2.Text = "" : TextBox1.Focus() : Exit Sub
                    End If


                End With
            End Using



            CloseConn()

            My.Application.ChangeCulture("en-us")
            My.Application.ChangeUICulture("en-us")
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try


    End Sub

End Class