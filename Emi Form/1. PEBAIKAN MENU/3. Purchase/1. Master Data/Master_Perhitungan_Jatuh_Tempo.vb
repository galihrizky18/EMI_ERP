Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Master_Perhitungan_Jatuh_Tempo
    Dim arrCari, arrKd_biaya, arrKeterangan As New ArrayList
    Private Sub Master_Perhitungan_Jatuh_Tempo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Lv_MasterBiaya.Columns.Clear()
        Lv_MasterBiaya.Columns.Add("Kode", 280, HorizontalAlignment.Left)
        Lv_MasterBiaya.Columns.Add("Keterangan", 350, HorizontalAlignment.Left)
        Lv_MasterBiaya.View = View.Details

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub Master_Perhitungan_Jatuh_Tempo_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub kosong()
        Txt_Kd.Text = ""
        Txt_Keterangan.Text = ""
        Cmb_Kolom.SelectedIndex = -1
        Txt_Value.Text = ""

        Cmb_Kolom.Items.Clear() : arrCari.Clear() : Cmb_Kolom.SelectedIndex = -1
        Cmb_Kolom.Items.Add("Kode Jenis") : arrCari.Add("Kode_Jenis_Perhitungan_JT")
        Cmb_Kolom.Items.Add("Keterangan") : arrCari.Add("Keterangan")

        Txt_Kd.Enabled = True
        Btn_Simpan.Tag = "&Simpan"
        Btn_Hapus.Enabled = False

        Try
            OpenConn()

            Lv_MasterBiaya.Items.Clear()
            SQL = "Select Kode_Jenis_Perhitungan_JT, Keterangan "
            SQL = SQL & "From Emi_Master_Perhitungan_Jatuh_Tempo Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "Order by Kode_Jenis_Perhitungan_JT "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_MasterBiaya.Items.Add(Dr("Kode_Jenis_Perhitungan_JT"))
                    lvw.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Txt_Kd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Btn_Simpan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Btn_Simpan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Hapus.Focus()
    End Sub

    Private Sub Btn_Hapus_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Btn_Hapus.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Refresh.Focus()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Cmb_Kolom.SelectedIndex = -1 Then
            MessageBox.Show("kolom paramater belum diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_Kolom.Focus() : Exit Sub
        ElseIf Txt_Value.Text.Trim.Length = 0 Then
            MessageBox.Show("Value belum diisi . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Value.Focus() : Exit Sub
        End If

        Cari("T")
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try

            OpenConn()

            Lv_MasterBiaya.Items.Clear()
            SQL = "Select Kode_Jenis_Perhitungan_JT, Keterangan "
            SQL = SQL & "From Emi_Master_Perhitungan_Jatuh_Tempo Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " like '%" & Txt_Value.Text & "%' "
                SQL = SQL & "order by " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " "
            Else
                SQL = SQL & "order by Kode_Jenis_Perhitungan_JT"
            End If
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_MasterBiaya.Items.Add(Dr("Kode_Jenis_Perhitungan_JT"))
                    lvw.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Cmb_Kolom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Kolom.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Value.Focus()
    End Sub

    Private Sub Txt_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        If Txt_Kd.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Kode & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Kd.Focus() : Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Keterangan.Focus() : Exit Sub
        End If

        Try
            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            If Btn_Simpan.Tag = "&Simpan" Then
                SQL = "Insert Into Emi_Master_Perhitungan_Jatuh_Tempo(Kode_Perusahaan, Kode_Jenis_Perhitungan_JT, Keterangan) "
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_Kd.Text.Trim & "',"
                SQL = SQL & " '" & Txt_Keterangan.Text.Trim & "' )"
                ExecuteTrans(SQL)
            Else
                SQL = "Update Emi_Master_Perhitungan_Jatuh_Tempo Set Keterangan =  '" & Txt_Keterangan.Text.Trim & "' "
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Jenis_Perhitungan_JT = '" & Txt_Kd.Text.Trim & "' "
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
        Txt_Kd.Focus()
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        If Txt_Kd.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Global_Kode & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Kd.Focus() : Exit Sub
        ElseIf Txt_Keterangan.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Keterangan.Focus() : Exit Sub
        End If
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus1 = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Delete From Emi_Master_Perhitungan_Jatuh_Tempo where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Kode_Jenis_Perhitungan_JT = '" & Txt_Kd.Text.Trim & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseTrans()
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try

        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong()
        Txt_Kd.Focus()
    End Sub

    Private Sub Txt_Kd_Leave(sender As Object, e As EventArgs) Handles Txt_Kd.Leave
        If Txt_Kd.Text.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            SQL = "Select Kode_Jenis_Perhitungan_JT, Keterangan "
            SQL = SQL & "From Emi_Master_Perhitungan_Jatuh_Tempo Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Jenis_Perhitungan_JT = '" & Txt_Kd.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Txt_Kd.Text = Dr("Kode_Jenis_Perhitungan_JT")
                    Txt_Keterangan.Text = Dr("Keterangan")
                    Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
                    Btn_Simpan.Tag = "&Update"
                Else
                    Txt_Keterangan.Text = ""
                    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = False
                    Txt_Kd.Enabled = True
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

    Private Sub Lv_MasterBiaya_DoubleClick(sender As Object, e As EventArgs) Handles Lv_MasterBiaya.DoubleClick
        Txt_Kd.Text = Lv_MasterBiaya.FocusedItem.Text
        Txt_Kd_Leave(Lv_MasterBiaya, e)
    End Sub
End Class