Imports System.Diagnostics.Eventing.Reader
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Master_Mesin
    Dim arrcari, arrDivisiMesin As New ArrayList
    Dim Jenis = "Master_Mesin"

    Dim LvDivisiMesin, LvNmMesin, LvSeriMesin, LvKeterangan, LvNoUrut As String

    Dim itemDivisiMesin As Integer = 0
    Dim itemNmMesin As Integer = 2
    Dim itemSeriMesin As Integer = 2
    Dim itemKeterangan As Integer = 3
    Dim itemNoUrut As Integer = 1

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvDivisiMesin = Lv_Data_MasterMesin.Items(NoIndex).Text '0
        LvNoUrut = Lv_Data_MasterMesin.Items(NoIndex).SubItems(itemNoUrut).Text '4
        LvNmMesin = Lv_Data_MasterMesin.Items(NoIndex).SubItems(itemNmMesin).Text '1
        LvSeriMesin = Lv_Data_MasterMesin.Items(NoIndex).SubItems(itemNmMesin).Text '2
        LvKeterangan = Lv_Data_MasterMesin.Items(NoIndex).SubItems(itemKeterangan).Text '3

    End Sub

    Private Sub Master_Mesin_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_Mesin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh
            Btn_Simpan.Tag = "&Simpan"
            Btn_Hapus.Enabled = False

            Lbl_Judul.Text = Base_Language.Lang_Mesin_Judul
            Lbl_NmMesin.Text = Base_Language.Lang_Mesin_NmMesin
            Lbl_SeriMesin.Text = Base_Language.Lang_Mesin_SeriMesin
            Lbl_Keterangan.Text = Base_Language.lang_global_keterangan
            Label4.Text = Base_Language.Lang_Global_Kolom

            Lv_Data_MasterMesin.Columns.Clear()
            Lv_Data_MasterMesin.Columns.Add(Base_Language.Lang_Mesin_Divisi, 200, HorizontalAlignment.Left)
            Lv_Data_MasterMesin.Columns.Add(Base_Language.Lang_Mesin_NmMesin, 240, HorizontalAlignment.Left)
            Lv_Data_MasterMesin.Columns.Add(Base_Language.Lang_Mesin_SeriMesin, 200, HorizontalAlignment.Left)
            Lv_Data_MasterMesin.Columns.Add(Base_Language.lang_global_keterangan, 243, HorizontalAlignment.Left)
            Lv_Data_MasterMesin.Columns.Add("No Urut", 0, HorizontalAlignment.Left)
            Lv_Data_MasterMesin.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try

        kosong()

    End Sub

    Private Sub kosong()
        Cmb_Divisi.SelectedIndex = -1
        Txt_NmMesin.Text = ""
        Txt_SeriMesin.Text = ""
        Txt_Keterangan.Text = ""

        Try
            OpenConn()

            Lv_Data_MasterMesin.Items.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Master_Mesin "
            SQL = SQL & "Order By NoUrut"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Data_MasterMesin.Items.Add(Dr("Divisi_Mesin"))
                    lvw.SubItems.Add(Dr("Seri_Mesin"))
                    lvw.SubItems.Add(Dr("Nama_Mesin"))
                    lvw.SubItems.Add(Dr("Keterangan"))
                    lvw.SubItems.Add(Dr("NoUrut"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add(Base_Language.Lang_Mesin_Divisi) : arrcari.Add("Divisi_Mesin")
        ComboBox1.Items.Add(Base_Language.Lang_Mesin_NmMesin) : arrcari.Add("Nama_Mesin")
        ComboBox1.Items.Add(Base_Language.Lang_Mesin_SeriMesin) : arrcari.Add("Seri_Mesin")
        ComboBox1.Items.Add(Base_Language.lang_global_keterangan) : arrcari.Add("Keterangan")
        TextBox3.Text = ""

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Simpan.Enabled = True : Btn_Hapus.Enabled = False

        Get_Divisi_Mesin()
    End Sub

    Private Sub Get_Divisi_Mesin()
        Try
            OpenConn()

            Cmb_Divisi.Items.Clear() : arrDivisiMesin.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Divisi_Mesin "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Cmb_Divisi.Items.Add(Dr("Keterangan")) : arrDivisiMesin.Add(Dr("Id_Divisi"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Dim divisiMesin = Cmb_Divisi.SelectedItem
        Dim seriMesin = Txt_SeriMesin.Text
        Dim namaMesin = Txt_NmMesin.Text
        Dim keterangan = Txt_Keterangan.Text

        If Cmb_Divisi.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Mesin_Divisi & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Cmb_Divisi.Focus() : Exit Sub
        ElseIf seriMesin.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Mesin_SeriMesin & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_SeriMesin.Focus() : Exit Sub
        ElseIf namaMesin.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Mesin_NmMesin & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_NmMesin.Focus() : Exit Sub
        ElseIf keterangan.Trim.Length = 0 Then
            MessageBox.Show(Base_Language.lang_global_keterangan & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Keterangan.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            If Btn_Simpan.Text = Base_Language.Lang_Global_Simpan Then
                SQL = "Insert Into EMI_Master_Mesin "
                SQL = SQL & "(Kode_Perusahaan, Divisi_Mesin, Seri_Mesin, Nama_Mesin, Keterangan) "
                SQL = SQL & "Values ('" & KodePerusahaan & "', "
                SQL = SQL & "'" & divisiMesin.Trim.ToString & "', '" & seriMesin.Trim & "', "
                SQL = SQL & "'" & namaMesin.Trim & "', '" & keterangan.Trim & "') "
                ExecuteTrans(SQL)
            Else
                SQL = "Update EMI_Master_Mesin "
                SQL = SQL & "Set Divisi_Mesin = '" & divisiMesin.Trim.ToString & "', "
                SQL = SQL & "Seri_Mesin = '" & seriMesin.Trim & "', "
                SQL = SQL & "Nama_Mesin = '" & namaMesin.Trim & "', "
                SQL = SQL & "Keterangan = '" & keterangan.Trim & "' "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and NoUrut = '" & LvNoUrut & "'"
                ExecuteTrans(SQL)
            End If

            Cmd.Transaction.Commit()
            CloseConn()
            MessageBox.Show(Base_Language.Lang_Global_Sukses_Simpan, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Get_Isi_ListView(Lv_Data_MasterMesin.FocusedItem.Index)

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction

                SQL = "DELETE FROM EMI_Master_Mesin "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "NoUrut = '" & LvNoUrut & "' "
                ExecuteTrans(SQL)

                Cmd.Transaction.Commit()
                MessageBox.Show(Base_Language.Lang_Global_Sukses_Hapus, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                CloseConn()
            Catch ex As Exception
                CloseConn()
                MessageBox.Show(ex.Message)
                Exit Sub
            End Try
        Else
            MessageBox.Show(Base_Language.Lang_Global_Hapus_No, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        kosong()
        Cmb_Divisi.Focus()
    End Sub

    Private Sub Btn_Refresh_Click(sender As Object, e As EventArgs) Handles Btn_Refresh.Click
        kosong()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        'If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        'If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        If ComboBox1.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Global_Kolom & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ComboBox1.Focus() : Exit Sub
        ElseIf TextBox3.Text.Trim.Length = 0 Then
            MessageBox.Show("Value" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TextBox3.Focus() : Exit Sub
        End If

        Cari("T")
    End Sub

    Private Sub Cari(ByVal semua As String)

        Try

            OpenConn()

            Lv_Data_MasterMesin.Items.Clear()
            SQL = "Select * From EMI_Master_Mesin where kode_perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & "order by NoUrut"
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Data_MasterMesin.Items.Add(dr("Divisi_Mesin"))
                    lvw.SubItems.Add(dr("Seri_Mesin"))
                    lvw.SubItems.Add(dr("Nama_Mesin"))
                    lvw.SubItems.Add(dr("Keterangan"))
                    lvw.SubItems.Add(dr("NoUrut"))
                Loop
            End Using

            CloseConn()

        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari_Click(TextBox3, e)
    End Sub


    Private Sub Lv_Data_MasterMesin_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data_MasterMesin.DoubleClick
        If Lv_Data_MasterMesin.Items.Count = 0 Then Exit Sub

        Cmb_Divisi.SelectedItem = Lv_Data_MasterMesin.FocusedItem.Text
        Txt_NmMesin.Text = Lv_Data_MasterMesin.FocusedItem.SubItems(itemNmMesin).Text
        Txt_SeriMesin.Text = Lv_Data_MasterMesin.FocusedItem.SubItems(itemSeriMesin).Text
        Txt_Keterangan.Text = Lv_Data_MasterMesin.FocusedItem.SubItems(itemKeterangan).Text

        Cmb_Divisi_Leave(Lv_Data_MasterMesin, e)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox3.Enabled = True : TextBox3.Text = ""
    End Sub



    Private Sub Cmb_Divisi_Leave(sender As Object, e As EventArgs) Handles Cmb_Divisi.Leave
        Dim divisiMesin = Cmb_Divisi.SelectedItem
        Dim seriMesin = Txt_SeriMesin.Text
        Dim namaMesin = Txt_NmMesin.Text
        Dim keterangan = Txt_Keterangan.Text

        'If divisiMesin.Length = 0 Then Exit Sub
        If seriMesin.Trim.Length = 0 Then Exit Sub
        If namaMesin.Trim.Length = 0 Then Exit Sub
        If keterangan.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()
            Get_Isi_ListView(Lv_Data_MasterMesin.FocusedItem.Index)

            SQL = "Select * From "
            SQL = SQL & "EMI_Master_Mesin "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and NoUrut = '" & LvNoUrut & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    divisiMesin = Dr("Divisi_Mesin")
                    seriMesin = Dr("Seri_Mesin")
                    namaMesin = Dr("Nama_Mesin")
                    keterangan = Dr("Keterangan")
                    Btn_Simpan.Text = Base_Language.Lang_Global_Update
                    Btn_Hapus.Enabled = True
                Else
                    Cmb_Divisi.SelectedIndex = -1
                    seriMesin = ""
                    namaMesin = ""
                    keterangan = ""
                    Btn_Simpan.Text = Base_Language.Lang_Global_Simpan : Btn_Hapus.Enabled = True
                End If
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

    End Sub

    Private Sub Cmb_Divisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Divisi.KeyPress
        If e.KeyChar = Chr(13) Then Txt_NmMesin.Focus()
    End Sub

    Private Sub Txt_NmMesin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmMesin.KeyPress
        If e.KeyChar = Chr(13) Then Txt_SeriMesin.Focus()
    End Sub

    Private Sub Txt_SeriMesin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_SeriMesin.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

End Class