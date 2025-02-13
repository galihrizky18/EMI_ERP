Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView

Public Class Master_Ekspedisi
    Dim arrcari As New ArrayList
    Dim Jenis = "Master_Ekspedisi"
    Dim lvKdEkspedisi, lvNmEkspedisi, lvAlmtEkspedisi, lvTelpEkspedisi, lvPICEkspedisi, lvPembayaran, lvGolPPH, lvNilaiPPH, lvIdEkspedisi As String
    Dim itemKdEkspedisi As Integer = 0
    Dim itemNmEkspedisi As Integer = 1
    Dim itemAlmtEkspedisi As Integer = 2
    Dim itemTelpEkspedisi As Integer = 3
    Dim itemPICEkspedisi As Integer = 4
    Dim itemPembayaran As Integer = 5
    Dim itemGolPPH As Integer = 6
    Dim itemNilaiPPH As Integer = 7
    Dim itemIdEkspedisi As Integer = 8

    Private Sub Btn_Hapus_Click(sender As Object, e As EventArgs) Handles Btn_Hapus.Click
        Dim kodeEkspedisi = Txt_KodeEkspedisi.Text
        Dim namaEkspedisi = Txt_NmEkspedisi.Text
        Dim alamatEkspedisi = Txt_AlmtEkspedisi.Text
        Dim telpEkspedisi = Txt_TelpEkspedisi.Text
        Dim picEkspedisi = Txt_PenanggungJawab.Text
        Dim pembayaran = Cmb_Pembayaran.SelectedItem
        Dim golonganPPH = Cmb_GolPPH.SelectedItem
        Dim nilaiPPH = Txt_NilaiPPH.Text

        If kodeEkspedisi.Length = 0 Then
            Exit Sub
        ElseIf namaEkspedisi.Length = 0 Then
            Exit Sub
        ElseIf alamatEkspedisi.Length = 0 Then
            Exit Sub
        ElseIf telpEkspedisi.Length = 0 Then
            Exit Sub
        ElseIf picEkspedisi.Length = 0 Then
            Exit Sub
        ElseIf Cmb_Pembayaran.SelectedIndex = -1 Then
            Exit Sub
        ElseIf Cmb_GolPPH.SelectedIndex = -1 Then
            Exit Sub
        ElseIf nilaiPPH.Length = 0 Then
            Exit Sub
        End If

        Dim Hapus As String = MessageBox.Show(Base_Language.Lang_Global_Tanya_Hapus, Judul, MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If Hapus = vbYes Then
            Try
                OpenConn()
                Cmd.Transaction() = Cn.BeginTransaction

                SQL = "DELETE FROM EMI_Master_Ekspedisi WHERE "
                SQL = SQL & "Kode_Perusahaan = '" & KodePerusahaan & "' AND Id_Ekspedisi = '" & lvIdEkspedisi & "' "
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
        Txt_KodeEkspedisi.Focus()
    End Sub

    Private Sub Btn_Cari_Click(sender As Object, e As EventArgs) Handles Btn_Cari.Click
        If ComboBox1.Text.Trim.Length = 0 Then Exit Sub
        If TextBox3.Text.Trim.Length = 0 Then Exit Sub

        Cari("T")
    End Sub

    Private Sub ComboBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles ComboBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox3.Focus()
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If e.KeyChar = Chr(13) Then Btn_Cari_Click(TextBox3, e)
    End Sub

    Private Sub Cari(ByVal semua As String)
        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "SELECT Kode_Ekspedisi, Nama_Ekspedisi, Alamat_Ekspedisi, Telepon_Ekspedisi, "
            SQL = SQL & "PIC_Ekspedisi, Pembayaran, "
            SQL = SQL & "Golongan_PPH, Nilai_PPH, Id_Ekspedisi "
            SQL = SQL & "FROM EMI_Master_Ekspedisi "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
            If semua = "T" Then
                SQL = SQL & "and " & arrcari.Item(ComboBox1.SelectedIndex) & " like '%" & TextBox3.Text & "%' "
                SQL = SQL & "order by " & arrcari.Item(ComboBox1.SelectedIndex) & " "
            Else
                SQL = SQL & "order by Nama_Ekspedisi"
            End If
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("Kode_Ekspedisi"))
                    Lvw.SubItems.Add(dr("Nama_Ekspedisi"))
                    Lvw.SubItems.Add(dr("Alamat_Ekspedisi"))
                    Lvw.SubItems.Add(dr("Telepon_Ekspedisi"))
                    Lvw.SubItems.Add(dr("PIC_Ekspedisi"))
                    Lvw.SubItems.Add(dr("Pembayaran"))
                    Lvw.SubItems.Add(dr("Golongan_PPH"))
                    Lvw.SubItems.Add(dr("Nilai_PPH"))
                    Lvw.SubItems.Add(dr("Id_Ekspedisi"))
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
        Txt_KodeEkspedisi.Focus()
    End Sub

    Private Sub Master_Ekspedisi_welly_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")
    End Sub

    Private Sub Get_Isi_ListView(ByVal NoIndex As Integer)
        lvKdEkspedisi = ListView1.Items(NoIndex).Text
        lvNmEkspedisi = ListView1.Items(NoIndex).SubItems(itemNmEkspedisi).Text
        lvAlmtEkspedisi = ListView1.Items(NoIndex).SubItems(itemAlmtEkspedisi).Text
        lvTelpEkspedisi = ListView1.Items(NoIndex).SubItems(itemTelpEkspedisi).Text
        lvPICEkspedisi = ListView1.Items(NoIndex).SubItems(itemPICEkspedisi).Text
        lvPembayaran = ListView1.Items(NoIndex).SubItems(itemPembayaran).Text
        lvGolPPH = ListView1.Items(NoIndex).SubItems(itemGolPPH).Text
        lvNilaiPPH = ListView1.Items(NoIndex).SubItems(itemNilaiPPH).Text
        lvIdEkspedisi = ListView1.Items(NoIndex).SubItems(itemIdEkspedisi).Text
    End Sub

    Private Sub Master_Ekspedisi_welly_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Application.ChangeCulture("en-us")
        My.Application.ChangeUICulture("en-us")

        Txt_TelpEkspedisi.MaxLength = 15
        Txt_NilaiPPH.MaxLength = 3

        Try
            OpenConn()

            Base_Language.Get_Languages(Bahasa_Pilihan, "GLOBAL")
            Base_Language.Get_Languages(Bahasa_Pilihan, Jenis)

            Lbl_Judul.Text = Base_Language.Lang_Ekspedisi_Judul
            Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
            Btn_Hapus.Text = Base_Language.Lang_Global_Hapus
            Btn_Cari.Text = Base_Language.Lang_Global_Cari
            Btn_Refresh.Text = Base_Language.Lang_Global_Refresh

            Lbl_Kode.Text = Base_Language.Lang_Ekspedisi_KodeEkspedisi
            Lbl_Nama.Text = Base_Language.Lang_Ekspedisi_NamaEkspedisi
            Lbl_Alamat.Text = Base_Language.Lang_Ekspedisi_AlamatEkspedisi
            Lbl_Telp.Text = Base_Language.Lang_Ekspedisi_TeleponEkspedisi
            Lbl_PIC.Text = Base_Language.Lang_Ekspedisi_PenanggungJawab
            Lbl_Pembayaran.Text = Base_Language.Lang_Ekspedisi_Pembayaran
            Lbl_GolPPH.Text = Base_Language.Lang_Ekspedisi_GolonganPPH
            Lbl_NilaiPPH.Text = Base_Language.Lang_Ekspedisi_NilaiPPH
            Lbl_Kolom.Text = Base_Language.Lang_Global_Kolom

            ListView1.Columns.Clear()
            ListView1.Columns.Add(Base_Language.Lang_Ekspedisi_KodeEkspedisi, 110, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Ekspedisi_NamaEkspedisi, 150, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Ekspedisi_AlamatEkspedisi, 200, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Ekspedisi_TeleponEkspedisi, 130, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Ekspedisi_PenanggungJawab, 150, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Ekspedisi_Pembayaran, 90, HorizontalAlignment.Center)
            ListView1.Columns.Add(Base_Language.Lang_Ekspedisi_GolonganPPH, 120, HorizontalAlignment.Left)
            ListView1.Columns.Add(Base_Language.Lang_Ekspedisi_NilaiPPH, 120, HorizontalAlignment.Right)
            ListView1.Columns.Add("NoUrut", 0, HorizontalAlignment.Right)
            ListView1.View = View.Details

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        kosong()
    End Sub

    Private Sub kosong()
        Cmb_GolPPH.Items.Clear()
        Cmb_GolPPH.Items.Add("Non PPH")
        Cmb_GolPPH.Items.Add("PPH Golongan 1")
        Cmb_GolPPH.Items.Add("PPH Golongan 2")
        Cmb_GolPPH.Items.Add("PPH Golongan 3")

        Cmb_Pembayaran.Items.Clear()
        Cmb_Pembayaran.Items.Add("Kredit")
        Cmb_Pembayaran.Items.Add("Tunai")

        Try
            OpenConn()

            ListView1.Items.Clear()
            SQL = "SELECT Kode_Ekspedisi, Nama_Ekspedisi, Alamat_Ekspedisi, Telepon_Ekspedisi, "
            SQL = SQL & "PIC_Ekspedisi, Pembayaran, "
            SQL = SQL & "Golongan_PPH, Nilai_PPH, Id_Ekspedisi  "
            SQL = SQL & "FROM EMI_Master_Ekspedisi "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' "
            Using dr = OpenTrans(SQL)
                Do While dr.Read
                    Dim Lvw As ListViewItem
                    Lvw = ListView1.Items.Add(dr("Kode_Ekspedisi"))
                    Lvw.SubItems.Add(dr("Nama_Ekspedisi"))
                    Lvw.SubItems.Add(dr("Alamat_Ekspedisi"))
                    Lvw.SubItems.Add(dr("Telepon_Ekspedisi"))
                    Lvw.SubItems.Add(dr("PIC_Ekspedisi"))
                    Lvw.SubItems.Add(dr("Pembayaran"))
                    Lvw.SubItems.Add(dr("Golongan_PPH"))
                    Lvw.SubItems.Add(dr("Nilai_PPH"))
                    Lvw.SubItems.Add(dr("Id_Ekspedisi"))
                Loop
            End Using

            CloseConn()
        Catch ex As Exception
            CloseConn()
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try

        ComboBox1.Items.Clear() : arrcari.Clear()
        ComboBox1.Items.Add(Base_Language.Lang_Ekspedisi_KodeEkspedisi) : arrcari.Add("Kode_Ekspedisi")
        ComboBox1.Items.Add(Base_Language.Lang_Ekspedisi_NamaEkspedisi) : arrcari.Add("Nama_Ekspedisi")
        TextBox3.Text = ""

        Txt_KodeEkspedisi.Text = "" : Txt_NmEkspedisi.Text = "" : Txt_AlmtEkspedisi.Text = ""
        Txt_TelpEkspedisi.Text = "" : Txt_PenanggungJawab.Text = "" : Txt_NilaiPPH.Text = ""
        Btn_Simpan.Text = Base_Language.Lang_Global_Simpan
        Btn_Simpan.Enabled = True : Btn_Hapus.Enabled = False

    End Sub

    Private Sub Btn_Simpan_Click(sender As Object, e As EventArgs) Handles Btn_Simpan.Click
        Dim kodeEkspedisi = Txt_KodeEkspedisi.Text
        Dim namaEkspedisi = Txt_NmEkspedisi.Text
        Dim alamatEkspedisi = Txt_AlmtEkspedisi.Text
        Dim telpEkspedisi = Txt_TelpEkspedisi.Text
        Dim picEkspedisi = Txt_PenanggungJawab.Text
        Dim pembayaran = Cmb_Pembayaran.SelectedItem
        Dim golonganPPH = Cmb_GolPPH.SelectedItem
        Dim nilaiPPH = Txt_NilaiPPH.Text

        If kodeEkspedisi.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Ekspedisi_KodeEkspedisi & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_KodeEkspedisi.Focus() : Exit Sub
        ElseIf namaEkspedisi.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Ekspedisi_NamaEkspedisi & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NmEkspedisi.Focus() : Exit Sub
        ElseIf alamatEkspedisi.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Ekspedisi_AlamatEkspedisi & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_AlmtEkspedisi.Focus() : Exit Sub
        ElseIf telpEkspedisi.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Ekspedisi_TeleponEkspedisi & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_TelpEkspedisi.Focus() : Exit Sub
        ElseIf picEkspedisi.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Ekspedisi_PenanggungJawab & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_PenanggungJawab.Focus() : Exit Sub
        ElseIf Cmb_Pembayaran.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Ekspedisi_Pembayaran & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_Pembayaran.Focus() : Exit Sub
        ElseIf Cmb_GolPPH.SelectedIndex = -1 Then
            MessageBox.Show(Base_Language.Lang_Ekspedisi_GolonganPPH & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Cmb_GolPPH.Focus() : Exit Sub
        ElseIf nilaiPPH.Length = 0 Then
            MessageBox.Show(Base_Language.Lang_Ekspedisi_NilaiPPH & " " & Base_Language.Lang_Global_Belum_Diisi, Judul, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Txt_NilaiPPH.Focus() : Exit Sub
        End If

        Try
            OpenConn()
            Cmd.Transaction() = Cn.BeginTransaction

            If Btn_Simpan.Text = Base_Language.Lang_Global_Simpan Then
                SQL = "INSERT INTO EMI_Master_Ekspedisi(Kode_Perusahaan, Kode_Ekspedisi, Nama_Ekspedisi, Alamat_Ekspedisi, "
                SQL = SQL & "Telepon_Ekspedisi, PIC_Ekspedisi, Pembayaran, Golongan_PPH, Nilai_PPH) "
                SQL = SQL & "VALUES('" & KodePerusahaan & "', '" & kodeEkspedisi.Trim & "', "
                SQL = SQL & "'" & namaEkspedisi.Trim & "', '" & alamatEkspedisi.Trim & "', '" & telpEkspedisi.Trim & "', "
                SQL = SQL & "'" & picEkspedisi.Trim & "', '" & pembayaran.ToString & "', "
                SQL = SQL & "'" & golonganPPH.ToString & "', '" & nilaiPPH.Trim & "')"
                ExecuteTrans(SQL)
            Else
                SQL = "Update EMI_Master_Ekspedisi Set Kode_Ekspedisi = '" & kodeEkspedisi.Trim & "', "
                SQL = SQL & "Nama_Ekspedisi = '" & namaEkspedisi.Trim & "',"
                SQL = SQL & "Alamat_Ekspedisi = '" & alamatEkspedisi.Trim & "', "
                SQL = SQL & "Telepon_Ekspedisi =  '" & telpEkspedisi.Trim & "', "
                SQL = SQL & "PIC_Ekspedisi =  '" & picEkspedisi.Trim & "', "
                SQL = SQL & "Pembayaran =  '" & pembayaran.ToString & "', "
                SQL = SQL & "Golongan_PPH = '" & golonganPPH.ToString & "', "
                SQL = SQL & "Nilai_PPH = '" & nilaiPPH.Trim & "' "
                SQL = SQL & "Where Kode_Perusahaan = '" & KodePerusahaan & "' and "
                SQL = SQL & "Id_Ekspedisi = '" & lvIdEkspedisi & "'"
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
        Txt_KodeEkspedisi.Focus()
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count = 0 Then Exit Sub

        Txt_KodeEkspedisi.Text = ListView1.FocusedItem.Text
        Txt_NmEkspedisi.Text = ListView1.FocusedItem.SubItems(1).Text
        Txt_AlmtEkspedisi.Text = ListView1.FocusedItem.SubItems(2).Text
        Txt_TelpEkspedisi.Text = ListView1.FocusedItem.SubItems(3).Text
        Txt_PenanggungJawab.Text = ListView1.FocusedItem.SubItems(4).Text
        Cmb_Pembayaran.SelectedItem = ListView1.FocusedItem.SubItems(5).Text
        Cmb_GolPPH.SelectedItem = ListView1.FocusedItem.SubItems(6).Text
        Txt_NilaiPPH.Text = ListView1.FocusedItem.SubItems(7).Text

        Txt_KodeEkspedisi_Leave(ListView1, e)
    End Sub

    Private Sub Txt_KodeEkspedisi_Leave(sender As Object, e As EventArgs) Handles Txt_KodeEkspedisi.Leave
        Dim kodeEkspedisi = Txt_KodeEkspedisi.Text
        Dim namaEkspedisi = Txt_NmEkspedisi.Text
        Dim alamatEkspedisi = Txt_AlmtEkspedisi.Text
        Dim telpEkspedisi = Txt_TelpEkspedisi.Text
        Dim picEkspedisi = Txt_PenanggungJawab.Text
        Dim pembayaran = Cmb_Pembayaran.SelectedItem
        Dim golonganPPH = Cmb_GolPPH.SelectedItem
        Dim nilaiPPH = Txt_NilaiPPH.Text

        If kodeEkspedisi.Trim.Length = 0 Then Exit Sub
        If namaEkspedisi.Trim.Length = 0 Then Exit Sub
        If alamatEkspedisi.Trim.Length = 0 Then Exit Sub
        If telpEkspedisi.Trim.Length = 0 Then Exit Sub
        If picEkspedisi.Trim.Length = 0 Then Exit Sub
        If pembayaran.Length = 0 Then Exit Sub
        If golonganPPH.Length = 0 Then Exit Sub
        If nilaiPPH.Trim.Length = 0 Then Exit Sub

        Try
            OpenConn()

            Get_Isi_ListView(ListView1.FocusedItem.Index)
            SQL = "SELECT Kode_Perusahaan, Kode_Ekspedisi, Nama_Ekspedisi, Alamat_Ekspedisi, Telepon_Ekspedisi, "
            SQL = SQL & "PIC_Ekspedisi, Pembayaran, "
            SQL = SQL & "Golongan_PPH, Nilai_PPH "
            SQL = SQL & "FROM EMI_Master_Ekspedisi "
            SQL = SQL & "WHERE Kode_Perusahaan = '" & KodePerusahaan & "' and "
            SQL = SQL & "Id_Ekspedisi = '" & lvIdEkspedisi & "' "

            Using Dr = OpenTrans(SQL)
                If Dr.Read Then
                    kodeEkspedisi = Dr("Kode_Ekspedisi")
                    namaEkspedisi = Dr("Nama_Ekspedisi")
                    alamatEkspedisi = Dr("Alamat_Ekspedisi")
                    telpEkspedisi = Dr("Telepon_Ekspedisi")
                    picEkspedisi = Dr("PIC_Ekspedisi")
                    pembayaran = Dr("Pembayaran")
                    golonganPPH = Dr("Golongan_PPH")
                    nilaiPPH = Dr("Nilai_PPH")
                    Btn_Simpan.Text = Base_Language.Lang_Global_Update
                    'Btn_Simpan.Enabled = False
                    Btn_Hapus.Enabled = True
                Else
                    kodeEkspedisi = ""
                    namaEkspedisi = ""
                    alamatEkspedisi = ""
                    telpEkspedisi = ""
                    picEkspedisi = ""
                    Cmb_Pembayaran.SelectedIndex = -1
                    Cmb_GolPPH.SelectedIndex = -1
                    nilaiPPH = ""
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

    Private Sub Txt_NilaiPPH_TextChanged(sender As Object, e As EventArgs) Handles Txt_NilaiPPH.TextChanged

    End Sub

    Private Sub Txt_KodeEkspedisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_KodeEkspedisi.KeyPress
        If e.KeyChar = Chr(13) Then Txt_NmEkspedisi.Focus()
    End Sub

    Private Sub Txt_NmEkspedisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NmEkspedisi.KeyPress
        If e.KeyChar = Chr(13) Then Txt_AlmtEkspedisi.Focus()
    End Sub

    Private Sub Txt_AlmtEkspedisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_AlmtEkspedisi.KeyPress
        If e.KeyChar = Chr(13) Then Txt_TelpEkspedisi.Focus()
    End Sub

    Private Sub Txt_TelpEkspedisi_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_TelpEkspedisi.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
        If e.KeyChar = Chr(13) Then Txt_PenanggungJawab.Focus()
    End Sub

    Private Sub Txt_PenanggungJawab_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_PenanggungJawab.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_Pembayaran.Focus()
    End Sub

    Private Sub Cmb_Pembayaran_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_Pembayaran.KeyPress
        If e.KeyChar = Chr(13) Then Cmb_GolPPH.Focus()
    End Sub

    Private Sub Cmb_GolPPH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Cmb_GolPPH.KeyPress
        If e.KeyChar = Chr(13) Then Txt_NilaiPPH.Focus()
    End Sub

    Private Sub Txt_NilaiPPH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Txt_NilaiPPH.KeyPress
        If Not Char.IsDigit(e.KeyChar) And Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
        If e.KeyChar = Chr(13) Then Btn_Simpan.Focus()
    End Sub

End Class