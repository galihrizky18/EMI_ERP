Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button


Public Class Master_Dept_Produksi
    Dim arrcari As New ArrayList
    Dim Jenis = "Master_Jenis_Hewan"
    Private Sub kosong()
        TextBox1.Text = ""
        TextBox2.Text = ""



        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add(Base_Language.Lang_Jenis_Hewan_Kode) : arrcari.Add("kode_jenis_hewan")
        ComboBox1.Items.Add(Base_Language.Lang_Jenis_Hewan_Keterangan) : arrcari.Add("keterangan")
        TextBox3.Text = ""

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
        Btn_Cari.Text = Base_Language.Lang_Global_Cari
        Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False

    End Sub

    Private Sub Cari(ByVal semua As String)
        Try

            OpenConn()

            ListView1.Items.Clear()
            SQL = "Select kode_jenis_hewan, keterangan From emi_jenis_hewan where kode_perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & "order by nama"
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("kode_jenis_hewan"))
                    Lvw.SubItems.Add(dr("keterangan"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub
    Private Sub Master_Jenis_Hewan_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Jenis_Hewan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages_Global(Bahasa_Pilihan)

            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Label1.Text = Base_Language.Lang_Jenis_Hewan_Judul
            Label2.Text = Base_Language.Lang_Jenis_Hewan_Kode
            Label3.Text = Base_Language.Lang_Jenis_Hewan_Keterangan
            Label4.Text = Base_Language.Lang_Jenis_Hewan_Kolom

            ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Kode, 150, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Jenis_Hewan_Keterangan, 725, HorizontalAlignment.Left)
            ListView1.View = View.Details

            kosong()

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try


    End Sub

    Private Sub TextBox1_Leave(sender As Object, e As EventArgs) Handles TextBox1.Leave
        If TextBox1.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select kode_jenis_hewan, keterangan From emi_jenis_hewan Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TextBox1.Text = Dr("kode_jenis_hewan")
                    TextBox2.Text = Dr("keterangan")

                    Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
                    Btn_Simpan.Tag = "&Update"
                Else
                    TextBox2.Text = ""

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

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If TextBox1.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Jenis_Hewan_Error_Kode, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox1.Focus() : Exit Sub
        ElseIf TextBox2.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Jenis_Hewan_Error_Nama, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TextBox2.Focus() : Exit Sub
        End If

        Try

            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag = "&Simpan" Then
                SQL = "Insert Into emi_jenis_hewan(Kode_Perusahaan, kode_jenis_hewan, keterangan) "
                SQL = SQL & "Values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TextBox1.Text.Trim & "', '" & TextBox2.Text.Trim & "')"
                ExecuteTrans(SQL)
            Else
                SQL = "Update emi_jenis_hewan Set keterangan = '" & TextBox2.Text.Trim & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
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
        TextBox1.Focus()
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then

            Try

                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Delete From emi_jenis_hewan where Kode_Perusahaan = '" & KodePerusahaan & "' and kode_jenis_hewan = '" & TextBox1.Text.Trim & "'"
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()

                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong()
        TextBox1.Focus()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari_Click(TextBox3, e)
    End Sub


End Class