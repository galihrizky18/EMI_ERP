Imports System.Diagnostics.Eventing.Reader
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button

Public Class Master_Kategori_Harga

    Dim arrcari, arrDivisiMesin As New ArrayList
    Dim Jenis = "Master_Jenis_Kategori_Harga"

    Dim LvID, LvKode, LvNama, LvKeterangan As String

    Dim itemID As Integer = 0
    Dim itemKode As Integer = 1
    Dim itemNama As Integer = 2
    Dim itemKeterangan As Integer = 3

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        LvID = Lv_Data_MasterJenisKategoriHrg.Items(NoIndex).Text
        LvKode = Lv_Data_MasterJenisKategoriHrg.Items(NoIndex).SubItems(itemKode).Text
        LvNama = Lv_Data_MasterJenisKategoriHrg.Items(NoIndex).SubItems(itemNama).Text
        LvKeterangan = Lv_Data_MasterJenisKategoriHrg.Items(NoIndex).SubItems(itemKeterangan).Text
    End Sub

    Private Sub Master_JenisKategoriHarga_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Master_JenisKategoriHarga_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

            Lbl_Judul.Text = Base_Language.Lang_JenisKategoriHarga_Judul
            Lbl_Kd.Text = Base_Language.Lang_JenisKategoriHarga_Kode
            Lbl_Keterangan.Text = Base_Language.lang_global_keterangan
            Label4.Text = Base_Language.Lang_Global_Kolom

            Lv_Data_MasterJenisKategoriHrg.Columns.Clear()
            Lv_Data_MasterJenisKategoriHrg.Columns.Add("ID", 0, HorizontalAlignment.Left)
            Lv_Data_MasterJenisKategoriHrg.Columns.Add(Base_Language.Lang_JenisKategoriHarga_Kode, 290, HorizontalAlignment.Left)
            Lv_Data_MasterJenisKategoriHrg.Columns.Add(Base_Language.Lang_JenisKategoriHarga_Nama, 0, HorizontalAlignment.Left)
            Lv_Data_MasterJenisKategoriHrg.Columns.Add(Base_Language.lang_global_keterangan, 300, HorizontalAlignment.Left)
            Lv_Data_MasterJenisKategoriHrg.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub

        End Try

        kosong()

    End Sub

    Private Sub kosong()
        Lbl_ID.Text = ""
        Txt_Kode.Text = ""

        Txt_Keterangan.Text = ""

        Try
            OpenConn()

            Lv_Data_MasterJenisKategoriHrg.Items.Clear()
            SQL = "Select * From "
            SQL = SQL & "EMI_Master_Jenis_Kategori_Harga "
            SQL = SQL & "Order By Id_Jenis_Kategori_Harga"
            Using Dr = OpenTrans(SQL)
                Do While Dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Data_MasterJenisKategoriHrg.Items.Add(Dr("Id_Jenis_Kategori_Harga"))
                    lvw.SubItems.Add(Dr("Kode_Jenis_Kategori_Harga"))
                    lvw.SubItems.Add(Dr("Nama_Jenis_Kategori_Harga"))
                    lvw.SubItems.Add(Dr("Keterangan"))
                Loop
            End Using
            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add(Base_Language.Lang_JenisKategoriHarga_Kode) : arrcari.Add("Kode_Jenis_Kategori_Harga")
        ComboBox1.Items.Add(Base_Language.Lang_JenisKategoriHarga_Nama) : arrcari.Add("Nama_Jenis_Kategori_Harga")
        ComboBox1.Items.Add(Base_Language.lang_global_keterangan) : arrcari.Add("Keterangan")
        TextBox3.Text = ""

        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Simpan.Enabled = True : Btn_Hapus.Enabled = False

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click

        Dim kode = Txt_Kode.Text
        Dim keterangan = Txt_Keterangan.Text

        If kode.Trim.Length = 0 Then
            MessageBox.Show("Kode" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Kode.Focus() : Exit Sub
        ElseIf keterangan.Trim.Length = 0 Then
            MessageBox.Show("Keterangan" & " " & Base_Language.Lang_Global_Belum_Diisi & " . . ! !", Judul, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Txt_Keterangan.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            If Btn_Simpan.Text = Base_Language.Lang_Global_Simpan Then
                SQL = "Insert Into EMI_Master_Jenis_Kategori_Harga "
                SQL = SQL & "(Kode_Perusahaan, Kode_Jenis_Kategori_Harga, Nama_Jenis_Kategori_Harga, Keterangan) "
                SQL = SQL & "Values ('" & KodePerusahaan & "', "
                SQL = SQL & "'" & kode.Trim & "', "
                SQL = SQL & "'" & keterangan.Trim & "', '" & keterangan.Trim & "') "
                ExecuteTrans(SQL)
            Else
                SQL = "Update EMI_Master_Jenis_Kategori_Harga "
                SQL = SQL & "Set Kode_Jenis_Kategori_Harga = '" & kode.Trim.ToString & "', "
                SQL = SQL & "Nama_Jenis_Kategori_Harga = '" & keterangan.Trim & "', "
                SQL = SQL & "Keterangan = '" & keterangan.Trim & "' "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
                SQL = SQL & "and Id_Jenis_Kategori_Harga = '" & LvID & "' "
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
        Get_Isi_ListView(Lv_Data_MasterJenisKategoriHrg.FocusedItem.Index)

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction

                SQL = "DELETE FROM EMI_Master_Jenis_Kategori_Harga "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Id_Jenis_Kategori_Harga = '" & LvID & "' "
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
        'Cmb_Divisi.Focus()
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
            Lv_Data_MasterJenisKategoriHrg.Items.Clear()
            SQL = "Select * From EMI_Master_Jenis_Kategori_Harga where kode_perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & "order by Id_Jenis_Kategori_Harga"
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim lvw As ListViewItem
                    lvw = Lv_Data_MasterJenisKategoriHrg.Items.Add(dr("Id_Jenis_Kategori_Harga"))
                    lvw.SubItems.Add(dr("Kode_Jenis_Kategori_Harga"))
                    lvw.SubItems.Add(dr("Nama_Jenis_Kategori_Harga"))
                    lvw.SubItems.Add(dr("Keterangan"))
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

    Private Sub Lv_Data_MasterMesin_DoubleClick(sender As Object, e As EventArgs) Handles Lv_Data_MasterJenisKategoriHrg.DoubleClick
        If Lv_Data_MasterJenisKategoriHrg.Items.Count = 0 Then Exit Sub

        Lbl_ID.Text = Lv_Data_MasterJenisKategoriHrg.FocusedItem.Text
        Txt_Kode.Text = Lv_Data_MasterJenisKategoriHrg.FocusedItem.SubItems(itemKode).Text
        Txt_Keterangan.Text = Lv_Data_MasterJenisKategoriHrg.FocusedItem.SubItems(itemKeterangan).Text

        Txt_Kode_Leave(Lv_Data_MasterJenisKategoriHrg, e)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        TextBox3.Enabled = True : TextBox3.Text = ""
    End Sub

    Private Sub Txt_Kode_Leave(sender As Object, e As EventArgs) Handles Txt_Kode.Leave
        Dim id = Lbl_ID.Text
        Dim kode = Txt_Kode.Text
        Dim keterangan = Txt_Keterangan.Text

        If id.Trim.Length = 0 Then Exit Sub
        If kode.Trim.Length = 0 Then Exit Sub
        If keterangan.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()
            Get_Isi_ListView(Lv_Data_MasterJenisKategoriHrg.FocusedItem.Index)

            SQL = "Select * From "
            SQL = SQL & "EMI_Master_Jenis_Kategori_Harga "
            SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' "
            SQL = SQL & "and Id_Jenis_Kategori_Harga = '" & LvID & "' "
            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    id = Dr("Id_Jenis_Kategori_Harga")
                    kode = Dr("Kode_Jenis_Kategori_Harga")
                    keterangan = Dr("Keterangan")
                    Btn_Simpan.Text = Base_Language.Lang_Global_Update
                    Btn_Hapus.Enabled = True
                Else
                    id = ""
                    kode = ""
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

    Private Sub Lv_Data_MasterJenisKategoriHrg_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Lv_Data_MasterJenisKategoriHrg.SelectedIndexChanged

    End Sub

    Private Sub Cmb_Divisi_KeyPress(sender As Object, e As KeyPressEventArgs)
        If e.KeyChar = Chr(13) Then Txt_Kode.Focus()
    End Sub

    Private Sub Txt_NmMesin_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Kode.KeyPress
        If e.KeyChar = Chr(13) Then Txt_Keterangan.Focus()
    End Sub

    Private Sub Txt_Keterangan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_Keterangan.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

    Private Sub Lv_Data_MasterJenisKategoriHrg_Disposed(sender As Object, e As EventArgs) Handles Lv_Data_MasterJenisKategoriHrg.Disposed

    End Sub
End Class