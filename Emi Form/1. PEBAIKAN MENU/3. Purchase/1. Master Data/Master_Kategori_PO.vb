Public Class Master_Kategori_PO
    Dim arrcariKategoriPO As New ArrayList

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub Master_Kategori_PO_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Kategori_PO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "Master_Kategori_PO")

            Label1.Text = Base_Language.Lang_Kategori_PO_Judul

            'lblKategoriPO_Judul.Text = Base_Language.Lang_Kategori_PO_Judul
            LblKategoriPO_Kode.Text = Base_Language.Lang_Kategori_PO_Kode
            LblKategoriPO_Ket.Text = Base_Language.Lang_Kategori_PO_Keterangan
            LblKategoriPO_Kolom.Text = Base_Language.Lang_Kategori_PO_Kolom

            LvwKategoriPO_Data.Columns.Add("Id Jenis", 0, HorizontalAlignment.Left)
            LvwKategoriPO_Data.Columns.Add(Base_Language.Lang_Kategori_PO_Kode, 150, HorizontalAlignment.Left)
            LvwKategoriPO_Data.Columns.Add(Base_Language.Lang_Kategori_PO_Keterangan, 725, HorizontalAlignment.Left)

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try
        kosongKategoriPO()
    End Sub

    '================================================================================================================
    'Kategori PO
    '================================================================================================================

    Private Sub kosongKategoriPO()
        TxtKategoriPO_Kode.Text = ""
        TxtKategoriPO_Kode.Enabled = True
        TxtKategoriPO_Ket.Text = ""

        CmbKategoriPO_Kolom.Items.Clear() : arrcariKategoriPO.Clear()
        CmbKategoriPO_Kolom.Items.Add(Base_Language.Lang_Kategori_PO_Kode) : arrcariKategoriPO.Add("kode_Kategori_PO")
        CmbKategoriPO_Kolom.Items.Add(Base_Language.Lang_Kategori_PO_Keterangan) : arrcariKategoriPO.Add("keterangan")
        TxtKategoriPO_Value.Text = ""

        Try

            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")

            BtnKategoriPO_Simpan.Text = Base_Language.Lang_Global_Simpan
            BtnKategoriPO_Hapus.Text = Base_Language.Lang_Global_Hapus
            BtnKategoriPO_Cari.Text = Base_Language.Lang_Global_Cari
            BtnKategoriPO_Refresh.Text = Base_Language.Lang_Global_Refresh
            BtnKategoriPO_Simpan.Tag = "&Simpan"
            BtnKategoriPO_Hapus.Enabled = False

            LvwKategoriPO_Data.Items.Clear()
            SQL = "Select id_Kategori_PO,kode_Kategori_PO, keterangan From emi_Kategori_PO where kode_perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "order by keterangan "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvwKategoriPO_Data.Items.Add(dr("id_Kategori_PO"))
                    Lvw.SubItems.Add(dr("kode_Kategori_PO"))
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

    Private Sub CariKategoriPO(ByVal semua As String)
        Try

            OpenConn()

            LvwKategoriPO_Data.Items.Clear()
            SQL = "Select id_Kategori_PO,Kode_Kategori_PO, keterangan From emi_Kategori_PO where kode_perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrcariKategoriPO.Item(CmbKategoriPO_Kolom.SelectedIndex) & " like '%" & TxtKategoriPO_Value.Text & "%' "
                SQL = SQL & "order by " & arrcariKategoriPO.Item(CmbKategoriPO_Kolom.SelectedIndex) & " "
            Else
                SQL = SQL & "order by keterangan "
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = LvwKategoriPO_Data.Items.Add(dr("id_Kategori_PO"))
                    Lvw.SubItems.Add(dr("Kode_Kategori_PO"))
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

    Private Sub TxtKategoriPO_Kode_Leave(sender As Object, e As EventArgs) Handles TxtKategoriPO_Kode.Leave
        If TxtKategoriPO_Kode.Text.Trim.Length = 0 Then Exit Sub

        Try

            OpenConn()

            SQL = "Select Kode_Kategori_PO, keterangan From emi_Kategori_PO Where "
            SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Kode_Kategori_PO = '" & TxtKategoriPO_Kode.Text.Trim & "'"
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    TxtKategoriPO_Ket.Text = Dr("keterangan")

                    TxtKategoriPO_Kode.Enabled = False
                    BtnKategoriPO_Simpan.Text = Base_Language.Lang_Global_Update : BtnKategoriPO_Hapus.Enabled = True
                    BtnKategoriPO_Simpan.Tag = "&Update"
                Else
                    TxtKategoriPO_Ket.Text = ""

                    BtnKategoriPO_Simpan.Text = Base_Language.Lang_Global_Simpan : BtnKategoriPO_Hapus.Enabled = False
                    BtnKategoriPO_Simpan.Tag = "&Simpan"
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnKategoriPO_Simpan_Click(sender As Object, e As EventArgs) Handles BtnKategoriPO_Simpan.Click
        If TxtKategoriPO_Kode.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Kategori_PO_Error_Kode, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKategoriPO_Kode.Focus() : Exit Sub
        ElseIf TxtKategoriPO_Ket.Text.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Kategori_PO_Error_Nama, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            TxtKategoriPO_Ket.Focus() : Exit Sub
        End If

        Try

            OpenConn()

            Cmd.Transaction = Cn.BeginTransaction

            If BtnKategoriPO_Simpan.Tag = "&Simpan" Then
                SQL = "Insert Into emi_Kategori_PO(Kode_Perusahaan, Kode_Kategori_PO, keterangan) "
                SQL = SQL & "Values('" & KodePerusahaan & "', "
                SQL = SQL & "'" & TxtKategoriPO_Kode.Text.Trim & "', '" & TxtKategoriPO_Ket.Text.Trim & "')"
                ExecuteTrans(SQL)
            Else
                SQL = "Update emi_Kategori_PO Set keterangan = '" & TxtKategoriPO_Ket.Text.Trim & "' "
                SQL = SQL & "where kode_perusahaan = '" & KodePerusahaan & "' and Kode_Kategori_PO = '" & TxtKategoriPO_Kode.Text.Trim & "'"
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        kosongKategoriPO()
        TxtKategoriPO_Kode.Focus()
    End Sub

    Private Sub BtnKategoriPO_Hapus_Click(sender As Object, e As EventArgs) Handles BtnKategoriPO_Hapus.Click
        Dim Hapus1 As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If Hapus1 = vbYes Then

            Try

                OpenConn()

                Cmd.Transaction = Cn.BeginTransaction

                SQL = "Delete From emi_Kategori_PO where Kode_Perusahaan = '" & KodePerusahaan & "' and Kode_Kategori_PO = '" & TxtKategoriPO_Kode.Text.Trim & "'"
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
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Base_Language.Lang_Global_Perhatian, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        kosongKategoriPO()
        TxtKategoriPO_Kode.Focus()
    End Sub

    Private Sub BtnKategoriPO_Refresh_Click(sender As Object, e As EventArgs) Handles BtnKategoriPO_Refresh.Click
        kosongKategoriPO()
    End Sub

    Private Sub BtnKategoriPO_Cari_Click(sender As Object, e As EventArgs) Handles BtnKategoriPO_Cari.Click
        If CmbKategoriPO_Kolom.Text.Trim.Length = 0 Then Exit Sub
        If TxtKategoriPO_Value.Text.Trim.Length = 0 Then Exit Sub

        CariKategoriPO("T")
    End Sub

    Private Sub LvwKategoriPO_Data_DoubleClick(sender As Object, e As EventArgs) Handles LvwKategoriPO_Data.DoubleClick

        TxtKategoriPO_Kode.Text = LvwKategoriPO_Data.FocusedItem.SubItems(1).Text
        TxtKategoriPO_Kode_Leave(LvwKategoriPO_Data, e)
    End Sub

    Private Sub TxtKategoriPO_Kode_TextChanged(sender As Object, e As EventArgs) Handles TxtKategoriPO_Kode.TextChanged

    End Sub

    Private Sub TxtKategoriPO_Kode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKategoriPO_Kode.KeyPress
        If e.KeyChar = Chr(13) Then TxtKategoriPO_Ket.Focus()
    End Sub

    Private Sub TxtKategoriPO_Ket_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKategoriPO_Ket.KeyPress
        If e.KeyChar = Chr(13) Then BtnKategoriPO_Simpan.Focus()
    End Sub

    Private Sub CmbKategoriPO_Kolom_KeyPress(sender As Object, e As KeyPressEventArgs) Handles CmbKategoriPO_Kolom.KeyPress
        If e.KeyChar = Chr(13) Then TxtKategoriPO_Value.Focus()
    End Sub

    Private Sub TxtKategoriPO_Value_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtKategoriPO_Value.KeyPress
        If e.KeyChar = Chr(13) Then BtnKategoriPO_Cari_Click(TxtKategoriPO_Value, e)
    End Sub
End Class