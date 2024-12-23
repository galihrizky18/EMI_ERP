Public Class Master_Work_Center

    Dim arrCari, arrKd_biaya, arrKeterangan As New ArrayList
    Dim Jenis = "Master_Work_Center"

    Dim LvID, LvKd, LvKeterangan As String

    Dim itemID As Integer = 0
    Dim itemKode As Integer = 1
    Dim itemKeterangan As Integer = 2

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvID = Lv_WorkCenter.Items(NoIndex).Text
        LvKd = Lv_WorkCenter.Items(NoIndex).SubItems(itemKode).Text
        LvKeterangan = Lv_WorkCenter.Items(NoIndex).SubItems(itemKeterangan).Text
    End Sub

    Private Sub Master_Biaya_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Biaya_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()
            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Lbl_Judul.Text = Base_Language.Lang_Master_Work_Center_Judul

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Btn_Simpan.Tag = "&Simpan"
            Btn_Hapus.Enabled = False

            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom

            Lv_WorkCenter.Columns.Clear()
            Lv_WorkCenter.Columns.Add("ID", 0, HorizontalAlignment.Left)
            Lv_WorkCenter.Columns.Add(Base_Language.Lang_Global_Kode, 280, HorizontalAlignment.Left)
            Lv_WorkCenter.Columns.Add(Base_Language.lang_global_keterangan, 350, HorizontalAlignment.Left)
            Lv_WorkCenter.View = View.Details

            kosong()
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub kosong()
        Lbl_IDWorkCenter.Text = ""
        Txt_Kd.Text = ""
        Txt_Keterangan.Text = ""
        Cmb_Kolom.SelectedIndex = -1
        Txt_Value.Text = ""

        Cmb_Kolom.Items.Clear() : arrCari.Clear() : Cmb_Kolom.SelectedIndex = -1
        Cmb_Kolom.Items.Add(Base_Language.Lang_Global_Kode) : arrCari.Add("Kode_Work_Center")
        Cmb_Kolom.Items.Add(Base_Language.lang_global_keterangan) : arrCari.Add("Keterangan")

        Txt_Kd.Enabled = True
        Btn_Simpan.Tag = "&Simpan"
        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Hapus.Enabled = False

        Try
            OpenConn()

            Lv_WorkCenter.Items.Clear()
            SQL = "Select Id_Work_Center, Kode_Work_Center, Keterangan "
            SQL = SQL & "From emi_master_work_center where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "Order by Id_Work_Center "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_WorkCenter.Items.Add(Dr("Id_Work_Center"))
                    lvw.SubItems.Add(Dr("Kode_Work_Center"))
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
                SQL = "Insert Into emi_master_work_center(Kode_Perusahaan, Kode_Work_Center, Keterangan) "
                SQL = SQL & "Values('" & KodePerusahaan & "', '" & Txt_Kd.Text.Trim & "',"
                SQL = SQL & " '" & Txt_Keterangan.Text.Trim & "' )"
                ExecuteTrans(SQL)
            Else
                SQL = "Update emi_master_work_center Set Keterangan =  '" & Txt_Keterangan.Text.Trim & "'"
                SQL = SQL & "where Kode_Perusahaan = '" & KodePerusahaan & "' and Id_Work_Center = '" & Lbl_IDWorkCenter.Text & "' "
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

    Private Sub Lv_MasterBiaya_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_WorkCenter.SelectedIndexChanged

    End Sub

    Private Sub Lv_MasterBiaya_DoubleClick(sender As Object, e As EventArgs) Handles Lv_WorkCenter.DoubleClick
        If Lv_WorkCenter.Items.Count = 0 Then Exit Sub

        Lbl_IDWorkCenter.Text = Lv_WorkCenter.FocusedItem.Text
        Txt_Kd.Text = Lv_WorkCenter.FocusedItem.SubItems(itemKode).Text
        Txt_Keterangan.Text = Lv_WorkCenter.FocusedItem.SubItems(itemKeterangan).Text

        Txt_Kd_Leave(Lv_WorkCenter, e)
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click

        If Cmb_Kolom.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Kolom & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_Kolom.Focus() : Exit Sub
        ElseIf Txt_Value.Text.Trim.Length = 0 Then
            MessageBox.Show("Value" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Value.Focus() : Exit Sub
        End If

        Cari("T")
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try

            OpenConn()

            Lv_WorkCenter.Items.Clear()
            SQL = "select Id_Work_Center, Kode_Work_Center, Keterangan "
            SQL = SQL & "From emi_master_work_center where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " like '%" & Txt_Value.Text & "%' "
                SQL = SQL & "order by " & arrCari.Item(Cmb_Kolom.SelectedIndex) & " "
            Else
                SQL = SQL & "order by Id_Work_Center"
            End If
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_WorkCenter.Items.Add(Dr("Id_Work_Center"))
                    lvw.SubItems.Add(Dr("Kode_Work_Center"))
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

    Private Sub Txt_Kd_TextChanged(sender As Object, e As EventArgs) Handles Txt_Kd.TextChanged

    End Sub

    Private Sub Txt_Kd_Leave(sender As Object, e As EventArgs) Handles Txt_Kd.Leave
        If Txt_Kd.Text.Trim.Length = 0 Then Exit Sub
        Lbl_IDWorkCenter.Enabled = False
        Txt_Kd.Enabled = False

        Try
            OpenConn()

            SQL = "Select Id_Work_Center, Kode_Work_Center, Keterangan "
            SQL = SQL & "From emi_master_work_center Where "
            SQL = SQL & "Kode_Work_Center = '" & Txt_Kd.Text.Trim & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    Lbl_IDWorkCenter.Text = Dr("Id_Work_Center")
                    Txt_Kd.Text = Dr("Kode_Work_Center")
                    Txt_Keterangan.Text = Dr("Keterangan")
                    Btn_Simpan.Text = Base_Language.Lang_Global_Update : Btn_Hapus.Enabled = True
                    Btn_Simpan.Tag = "&Update"
                Else
                    Lbl_IDWorkCenter.Text = ""
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

    Private Sub Txt_Keterangan_TextChanged(sender As Object, e As EventArgs) Handles Txt_Keterangan.TextChanged

    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus1 = vbYes Then

            Try

                OpenConn()
                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Delete From emi_master_work_center where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Work_Center = '" & Lbl_IDWorkCenter.Text.Trim & "' "
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

    Private Sub Txt_Value_TextChanged(sender As Object, e As EventArgs) Handles Txt_Value.TextChanged

    End Sub

    Private Sub Txt_Kd_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kd.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Txt_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Value.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari.Focus()
    End Sub

End Class